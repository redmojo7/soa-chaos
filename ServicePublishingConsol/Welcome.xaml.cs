using Newtonsoft.Json;
using Registry.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Windows;


namespace ServicePublishingConsol
{
    /// <summary>
    /// Interaction logic for Welcome.xaml
    /// </summary>
    public partial class Welcome : Window
    {
        private RestClient client;

        List<ServiceInfo> services;

        public Welcome()
        {
            InitializeComponent();

            string URL = "https://localhost:44388/";
            client = new RestClient(URL);
            RestRequest restRequest = new RestRequest("api/AllServices", Method.Get);
            RestResponse restResponse = client.Execute(restRequest);

            // Console.WriteLine(restResponse.Content);
            services = JsonConvert.DeserializeObject<List<ServiceInfo>>(restResponse.Content);
            lvServices.ItemsSource = services;
        }

        private async void btnPublish_Click(object sender, RoutedEventArgs e)
        {
            RestRequest restRequest = new RestRequest("api/publish", Method.Post);
            ServiceInfo info = new ServiceInfo(textBoxName.Text, textBoxDescription.Text, new Uri(textBoxApiEndpoint.Text),
                                        int.Parse(textBoxNumberOfOperands.Text), textBoxOperandType.Text);
            restRequest.AddJsonBody(JsonConvert.SerializeObject(info));
            restRequest.AddHeader("Content-type", "application/json");
            RestResponse restResponse = await client.ExecuteAsync(restRequest);
            RefreshList();
        }

        private async void btnUnpublish_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(SelectedServie().Name);
            RestRequest restRequest = new RestRequest("api/unpublish", Method.Put);
            restRequest.AddParameter("serviceName", SelectedServie().Name);
            RestResponse restResponse = await client.ExecuteAsync(restRequest);
            RefreshList();
        }

        private ServiceInfo SelectedServie()
        {
            ServiceInfo current = null;
            foreach (object o in lvServices.Items)
            {
                if (o is ServiceInfo)
                {
                    current = (o as ServiceInfo);
                    if (null != lvServices.SelectedItem && lvServices.SelectedItem.Equals(o))
                    {
                        return current;
                    }
                }
            }
            return current;
        }

        private async void RefreshList()
        {
            RestRequest restRequest = new RestRequest("api/AllServices", Method.Get);
            RestResponse restResponse = await client.ExecuteAsync(restRequest);

            // Console.WriteLine(restResponse.Content);
            services = JsonConvert.DeserializeObject<List<ServiceInfo>>(restResponse.Content);
            lvServices.ItemsSource = services;
        }
    }
}
