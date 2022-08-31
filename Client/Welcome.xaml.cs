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


namespace Client
{
    /// <summary>
    /// Interaction logic for Welcome.xaml
    /// </summary>
    public partial class Welcome : Window
    {
        private RestClient client;

        List<ServiceInfo> services;

        ServiceInfo current;

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

            string URL = "https://localhost:44388/";
            client = new RestClient(URL);
            RestRequest restRequest = new RestRequest("api/AllServices", Method.Get);
            RestResponse restResponse = client.Execute(restRequest);

            // Console.WriteLine(restResponse.Content);
            services = JsonConvert.DeserializeObject<List<ServiceInfo>>(restResponse.Content);
            lvServices.ItemsSource = services;
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
            client = new RestClient(current.ApiEndpoint);
            RestRequest request = new RestRequest();
            request.AddParameter("operand1", textBoxoperand1.Text);
            request.AddParameter("operand2", textBoxoperand2.Text);
            if (current.NumberOfOperands == 3)
            {
                request.AddParameter("operand3", textBoxoperand3.Text);
            }
            RestResponse result = await client.GetAsync(request);
            textBoxRresult.Visibility = Visibility.Visible;
            textBoxRresult.Text = result.Content;
        }

        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string URL = "https://localhost:44388/";
            client = new RestClient(URL);
            RestRequest request = new RestRequest("api/search", Method.Get);
            request.AddParameter("key", textBoxSearch.Text);
            RestResponse restResponse = await client.ExecuteAsync(request);

            // Console.WriteLine(restResponse.Content);
            services = JsonConvert.DeserializeObject<List<ServiceInfo>>(restResponse.Content);
            lvServices.ItemsSource = services;
        }
    }
}
