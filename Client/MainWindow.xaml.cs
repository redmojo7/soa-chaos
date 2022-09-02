using Authenticator;
using Newtonsoft.Json;
using PublishingConsole;
using Registry.DTO;
using Registry.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///         
    /// 

    public partial class MainWindow : Window
    {

        private Registration registration = new Registration();
        private Welcome welcome = new Welcome();
        public AuthServiceInterface foob;
        private RestClient client;
        public MainWindow()
        {
            InitializeComponent();
            Welcome.login = this;
            Registration.login = this;

            ChannelFactory<AuthServiceInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            //Set the URL and create the connection!
            string URL = "net.tcp://localhost:8100/AuthService";
            foobFactory = new ChannelFactory<AuthServiceInterface>(tcp, URL);
            foob = foobFactory.CreateChannel();
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxName.Text.Length == 0)
            {
                errormessage.Text = "Enter an Name.";
                textBoxName.Focus();
            }
            else if (!Regex.Match(textBoxName.Text, "[a-z0-9_]+$").Success)
            {
                errormessage.Text = "Enter a valid Name.";
                textBoxName.Select(0, textBoxName.Text.Length);
                textBoxName.Focus();
            }
            else
            {
                string name = textBoxName.Text;
                string password = passwordBox.Password;
                //
                string result = null;
                //foob.Login(in name, in password);
                foob.Login(name, password, out result);
                Console.WriteLine("result :  " + result);
                if (result != null && result.Length > 0)
                {
                    welcome.textBoxWelcom.Text = name;//Sending value from one form to another form.  
                    //welcome.Show();
                    //Close();
                    this.Opacity = 0;
                    this.Hide();
                    welcome.Opacity = 1;
                    welcome.token = result;
                    welcome.Show();
                    LoadAllServices(result);
                }
                else
                {
                    errormessage.Text = "Sorry! Please enter existing name/password.";
                }
            }
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            //registration.Show();
            //Close();
            this.Opacity = 0;
            this.Hide();   
            registration.Opacity = 1;
            registration.Show();
        }

        private void LoadAllServices(string token)
        {
            string URL = "https://localhost:44388/";
            client = new RestClient(URL);
            RestRequest restRequest = new RestRequest("api/AllServices", Method.Get);
            restRequest.AddHeader("token", token);
            RestResponse restResponse = client.Execute(restRequest);
            if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                // Console.WriteLine(restResponse.Content);
                List<ServiceInfo> services = JsonConvert.DeserializeObject<List<ServiceInfo>>(restResponse.Content);
                welcome.lvServices.ItemsSource = services;
            }
            else 
            {
                welcome.Hide();
                this.Opacity = 1;
                this.Show();
                BadInfoDTO badInfoDTO = JsonConvert.DeserializeObject<BadInfoDTO>(restResponse.Content);
                MessageBox.Show(badInfoDTO.Reason, badInfoDTO.Status);
            }
        }
    }
}
