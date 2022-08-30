using DocumentFormat.OpenXml.ExtendedProperties;
using Registry.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Registry.DAO
{
    public class ServiceDAO
    {
        private static string serviceFilePath = Path.Combine(System.AppContext.BaseDirectory, "App_Data", "AllServices.txt");

        public List<ServiceInfo> GetAllService()
        {
            // Open the file to read from.
            string readText = File.ReadAllText(serviceFilePath);
            List<ServiceInfo> serviceInfos = ServiceInfo.FromJson(readText);
            return serviceInfos;
        }
        public void PublishService(ServiceInfo newService)
        {
            // GetAllService
            List<ServiceInfo> serviceInfos = GetAllService();
            if (serviceInfos != null && serviceInfos.Count > 0)
            {
                var serviceNames = serviceInfos.Select(p => p.Name).Distinct().ToList();
                if (serviceNames.Contains(newService.Name))
                {
                    // update a service and publish it
                    ServiceInfo old = serviceInfos.FindAll(p => p.Name == newService.Name).Distinct().ToList()[0];
                    serviceInfos.Remove(old);
               }
            }
            else 
            {
                serviceInfos = new List<ServiceInfo>();
            }
            // plublish a new servivce
            serviceInfos.Add(newService);
            // save
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(serviceInfos);
            File.WriteAllText(serviceFilePath, jsonString);
        }

        public void UnPublishService(string serviceName)
        {
            // GetAllService
            List<ServiceInfo> serviceInfos = GetAllService();
            if (serviceInfos != null && serviceInfos.Count > 0)
            {
                var serviceNames = serviceInfos.Select(p => p.Name).Distinct().ToList();
                if (serviceNames.Contains(serviceName))
                {
                    // remove a service
                    ServiceInfo old = serviceInfos.FindAll(p => p.Name == serviceName).Distinct().ToList()[0];
                    serviceInfos.Remove(old);
                }

                // save
                var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(serviceInfos);
                File.WriteAllText(serviceFilePath, jsonString);
            }
        }

        public List<ServiceInfo> SearchService(string key)
        {
            List<ServiceInfo> serviceInfos = GetAllService();
            List<ServiceInfo> result = null;
            if (serviceInfos != null && serviceInfos.Count > 0 && key != null && key != "")
            {
                result = serviceInfos.FindAll(p => p.Name.ToLower().Contains(key.ToLower()));
            }
            return result;
        }
    }
}