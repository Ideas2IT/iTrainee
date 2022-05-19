using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace iTrainee.MVC.Helpers
{
    public static class HttpClientHelper
    {

        public static object ExecuteGetApiMethod<T>(string baseUrl, string method, string parameters)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + method + parameters).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<T>(data);
                }
            }
            return null;
        }

        public static string[] ExecuteGetIdsApiMethod<T>(string baseUrl, string method)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + method).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<string[]>(data);
                }
            }
            return null;
        }

        public static object ExecuteGetAllApiMethod<T>(string baseUrl, string method, string parameters)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + method + parameters).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<T>>(data);
                }
            }

            return null;
        }

        public static bool ExecutePostApiMethod<T>(string baseUrl, string method, T model, string token)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.Timeout = new TimeSpan(0, 5, 0);
                client.BaseAddress = new Uri(baseUrl);
                                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var jsonData = JsonConvert.SerializeObject(model);
                var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + method, byteContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }

        public static int ExecuteInsertPostApiMethod<T>(string baseUrl, string method, T model, string token)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.Timeout = new TimeSpan(0, 5, 0);
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var jsonData = JsonConvert.SerializeObject(model);
                var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + method, byteContent).Result;

                return Convert.ToInt32(response.Content.ReadAsStringAsync().Result);
            }
        }

        public static bool ExecuteDeleteApiMethod<T>(string baseUrl, string method, string parameter, string token)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.Timeout = new TimeSpan(0, 5, 0);
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + method + parameter).Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
