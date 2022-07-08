using ReminderNotificationProcess.Enums;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ReminderNotificationProcess.Common
{
    public class Utility
    {

        private static readonly HttpClient client = new HttpClient() { Timeout = new TimeSpan(0, 30, 0) };

        public string RestApiCall(string url, bool isExternalAPI = false, bool isLogin = false, string requestType = PCISEnum.APIMethodType.GetRequest, string cookie = null, string postParam = "")
        {
            string apiResult = "";
            try
            {
                ServicePoint sp = ServicePointManager.FindServicePoint(new Uri(url));
                sp.ConnectionLimit = 500;
                //checking whether the HttpContext current element is null or not.

                HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

                if (!isExternalAPI)
                {
                    var apiSecret = Environment.GetEnvironmentVariable(PCISEnum.EnvironmentVariables.ApiSecretFromKeyVault, EnvironmentVariableTarget.Process);
                    httpRequestMessage.Headers.Add("Authorization", string.Format("{0} {1}", PCISEnum.Constants.Bearer, apiSecret));
                }

                HttpResponseMessage response = null;
                if (!String.IsNullOrEmpty(requestType))
                {
                    if (requestType.Equals(PCISEnum.APIMethodType.GetRequest))
                    {

                        httpRequestMessage.Method = HttpMethod.Get;
                        httpRequestMessage.RequestUri = new Uri(url);

                        response = Task.Run(() => client.SendAsync(httpRequestMessage)).Result;
                    }
                    else if (requestType.Equals(PCISEnum.APIMethodType.PostRequest))
                    {
                        if (postParam != null)
                        {
                            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            StringContent content = new StringContent(postParam, Encoding.UTF8, "application/json");
                            httpRequestMessage.Content = content;
                        }
                        httpRequestMessage.Method = HttpMethod.Post;
                        httpRequestMessage.RequestUri = new Uri(url);
                        response = Task.Run(() => client.SendAsync(httpRequestMessage)).Result;
                    }
                    else if (requestType.Equals(PCISEnum.APIMethodType.PutRequest))
                    {
                        if (postParam != null)
                        {
                            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            StringContent content = new StringContent(postParam, Encoding.UTF8, "application/json");
                            httpRequestMessage.Content = content;
                        }
                        httpRequestMessage.Method = HttpMethod.Put;
                        httpRequestMessage.RequestUri = new Uri(url);
                        response = Task.Run(() => client.SendAsync(httpRequestMessage)).Result;
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        apiResult = response.Content.ReadAsStringAsync().Result;
                    }
                }

                return apiResult;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }

}
