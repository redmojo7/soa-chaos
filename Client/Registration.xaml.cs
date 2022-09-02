﻿using Authenticator;
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
        private AuthServiceInterface foob;
        public static MainWindow login;
        public Registration()
        {
            InitializeComponent();
            ChannelFactory<AuthServiceInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            //Set the URL and create the connection!
            string URL = "net.tcp://localhost:8100/AuthService";
            foobFactory = new ChannelFactory<AuthServiceInterface>(tcp, URL);
            foob = foobFactory.CreateChannel();
        }

        private void ButtonLogin_ClickButton_Click(object sender, RoutedEventArgs e)
        {
            this.Opacity = 0;
            this.Hide();
            login.Opacity = 1;
            login.textBoxName.Text = "";
            login.passwordBox.Password = "";
            login.errormessage.Text = "";
            login.Show();
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
                // register
                foob.Register(name, password, out string result);
                if (result != "")
                {
                    this.Opacity = 0;
                    this.Hide();
                    login.Opacity = 1;
                    login.textBoxName.Text = "";
                    login.passwordBox.Password = "";
                    login.errormessage.Text = "";
                    login.Show();
                }
                else
                {
                    errormessage.Text = "Sorry! Please enter existing name/password.";
                }
            }
        }
    }
}
