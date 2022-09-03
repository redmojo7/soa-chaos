using Newtonsoft.Json;
using PublishingConsole;
using Registry.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using Authenticator;

namespace Client
{
    /// <summary>
    /// Interaction logic for Welcome.xaml
    /// </summary>
    public partial class Welcome : Window
    {

        private List<ServiceInfo> services;

        private ServiceInfo current;

        public static MainWindow login;

        private AuthServiceInterface foob;

        public string token;

        public Welcome()
        {
            InitializeComponent();

            operand1.Visibility = Visibility.Hidden;
            operand2.Visibility = Visibility.Hidden;
            operand3.Visibility = Visibility.Hidden;

            textBoxoperand1.Visibility = Visibility.Hidden;
            textBoxoperand2.Visibility = Visibility.Hidden;
            textBoxoperand3.Visibility = Visibility.Hidden;
            btnCalculate.Visibility = Visibility.Hidden;
            serviceName.Visibility = Visibility.Hidden;
            textBoxRresult.Visibility = Visibility.Hidden;
            result.Visibility = Visibility.Hidden;

            ChannelFactory<AuthServiceInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            //Set the URL and create the connection!
            string Auth_URL = "net.tcp://localhost:8100/AuthService";
            foobFactory = new ChannelFactory<AuthServiceInterface>(tcp, Auth_URL);
            foob = foobFactory.CreateChannel();
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            foreach (object o in lvServices.Items)
            {
                
                if (o is ServiceInfo)
                {
                    current = (o as ServiceInfo);
                    if (null != lvServices.SelectedItem && lvServices.SelectedItem.Equals(o))
                    {
                        Console.WriteLine(current.Name);
                        //
                        serviceName.Text = current.Name;
                        operand1.Visibility = Visibility.Visible;
                        operand2.Visibility = Visibility.Visible;

                        textBoxoperand1.Visibility = Visibility.Visible;
                        textBoxoperand2.Visibility = Visibility.Visible;
                        btnCalculate.Visibility = Visibility.Visible;
                        btnCalculate.Content = current.Name; 
                        serviceName.Visibility = Visibility.Visible;

                        if (current.NumberOfOperands == 3)
                        {
                            operand3.Visibility = Visibility.Visible;
                            textBoxoperand3.Visibility = Visibility.Visible;
                        }
                        else 
                        {
                            operand3.Visibility = Visibility.Hidden;
                            textBoxoperand3.Visibility = Visibility.Hidden;
                        }

                        break;
                    }
                }
            }
        }

        private async void btnCalculate_ClickAsync(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(current.ApiEndpoint);
            //
            RestClient client = new RestClient(current.ApiEndpoint);
            RestRequest request = new RestRequest();
            request.AddParameter("operand1", textBoxoperand1.Text);
            request.AddParameter("operand2", textBoxoperand2.Text);
            request.AddHeader("token", token);
            if (current.NumberOfOperands == 3)
            {
                request.AddParameter("operand3", textBoxoperand3.Text);
            }

            RestResponse response = await client.ExecuteGetAsync(request);
            if (response.IsSuccessful)
            {
                textBoxRresult.Visibility = Visibility.Visible;
                textBoxRresult.Text = response.Content;
                result.Visibility = Visibility.Visible;
            }
            else
            {
                BadInfoDTO badInfoDTO = JsonConvert.DeserializeObject<BadInfoDTO>(response.Content);
                MessageBox.Show(badInfoDTO.Reason, badInfoDTO.Status);
            }
        }

        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string URL = "https://localhost:44388/";
            RestClient client = new RestClient(URL);
            RestRequest request = new RestRequest("api/search", Method.Get);
            request.AddParameter("key", textBoxSearch.Text);
            request.AddHeader("token", token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            Console.WriteLine(restResponse.StatusCode);
            Console.WriteLine(restResponse.Content);
            if (restResponse.IsSuccessful)
            {
                services = JsonConvert.DeserializeObject<List<ServiceInfo>>(restResponse.Content);
                lvServices.ItemsSource = services;
            }
            else
            {
                BadInfoDTO badInfoDTO = JsonConvert.DeserializeObject<BadInfoDTO>(restResponse.Content);
                MessageBox.Show(badInfoDTO.Reason, badInfoDTO.Status);
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Logout();
        }

        private void Logout()
        {
            this.Opacity = 0;
            this.Hide();
            login.Opacity = 1;
            login.ClearValue();
            login.Show();
        }
    }
}
