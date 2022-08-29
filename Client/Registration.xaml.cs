using PublishingConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        //MainWindow login = new MainWindow();
        Welcome welcome = new Welcome();
        private PublishServiceInterface foob;

        public Registration()
        {
            InitializeComponent();

            // This is a factory that generates remote connections to our remote class. This is what hides the RPC stuff!
            ChannelFactory<PublishServiceInterface> channelFactory; NetTcpBinding tcp = new NetTcpBinding();
            // Set the URL and create the connection!
            string URL = "net.tcp://localhost:8100/PublishingConsole";
            //channelFactory = new ChannelFactory<PublishServiceInterface>(tcp, URL);
            //foob = channelFactory.CreateChannel();
        }

        private void ButtonLogin_ClickButton_Click(object sender, RoutedEventArgs e)
        {
            //login.Show();
            Close();
        }

        private void ButtonRegistration_Click(object sender, RoutedEventArgs e)
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
                foob.Login(in name, in password);

                if (name != "")
                {
                    welcome.textBoxWelcom.Text = name;//Sending value from one form to another form.  
                    welcome.Show();
                    Close();
                }
                else
                {
                    errormessage.Text = "Sorry! Please enter existing name/password.";
                }
            }
        }
    }
}
