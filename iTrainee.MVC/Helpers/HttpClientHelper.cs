using iTrainee.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace iTrainee.MVC.Helpers
{
    public static class HttpClientHelper
    {
       
        public static object ExecuteGetApiMethod<T>(string baseUrl, string method, string parameters)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + method).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<T>(data);
                }
            }
            return null;

        }
        public static object ExecuteGetAllApiMethod<T>(string baseUrl, string method, string parameters)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + method).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<T>>(data);
                }
            }
            return null;

        }

        public static bool ExecutePostApiMethod<T>(string baseUrl, string method, T parameters)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 5, 0);
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var jsonData = JsonConvert.SerializeObject(parameters);
                var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PostAsync(method, byteContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;

        }

    }
}
