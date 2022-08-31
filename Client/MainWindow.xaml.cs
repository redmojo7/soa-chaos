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
        public MainWindow()
        {
            InitializeComponent();
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
                //foob.Login(in name, in password);

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

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            registration.Show();
            Close();
        }
    }
}
