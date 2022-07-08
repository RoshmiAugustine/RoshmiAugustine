using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ImportProcess.Enums;
using Newtonsoft.Json.Linq;

namespace ImportProcess.Common
{
    public class Utility
    {
        private static readonly HttpClient client = new HttpClient() { Timeout = new TimeSpan(0, 30, 0) };

        public string RestApiCall(ILogger log, string url, bool isExternalAI = false, bool isLogin = false, string requestType = PCISEnum.APIMethodType.GetRequest, string cookie = null, string postParam = "", long agencyID=0)
        {
            string apiResult = "";
            try
            {
                ServicePoint sp = ServicePointManager.FindServicePoint(new Uri(url));
                sp.ConnectionLimit = 500;
                //checking whether the HttpContext current element is null or not.
               
                
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

                if (agencyID > 0)
                {
                    httpRequestMessage.Headers.Add("tenantId", agencyID.ToString());
                }

                if (!isExternalAI)
                {
                    var apiSecret = Environment.GetEnvironmentVariable(PCISEnum.EnvironmentVariables.ApiSecretFromKeyVault, EnvironmentVariableTarget.Process);
                    httpRequestMessage.Headers.Add("Authorization", string.Format("{0} {1}", PCISEnum.Constants.Bearer, apiSecret));
                }
                else
                {
                    httpRequestMessage.Headers.Add("Accept", "*/*");
                    httpRequestMessage.Headers.Add("Cookie", "LtpaToken=" + cookie);
                }

                HttpResponseMessage response = null;
                if (!String.IsNullOrEmpty(requestType))
                {
                    if (requestType.Equals(PCISEnum.APIMethodType.GetRequest))
                    {

                        if (isLogin)
                        {
                            CookieContainer cookies = new CookieContainer();
                            HttpClientHandler handler = new HttpClientHandler();
                            handler.CookieContainer = cookies;

                            HttpClient client = new HttpClient(handler);
                            response = client.GetAsync(url).Result;

                            Uri uri = new Uri(url);
                            IEnumerable<Cookie> responseCookies = cookies.GetCookies(uri).Cast<Cookie>();
                            apiResult = responseCookies?.Where(x => x.Name == PCISEnum.Constants.TokenName).Select(y => y.Value).FirstOrDefault();
                            return apiResult;
                        }
                        else
                        {
                            httpRequestMessage.Method = HttpMethod.Get;
                            httpRequestMessage.RequestUri = new Uri(url);

                            response = Task.Run(() => client.SendAsync(httpRequestMessage)).Result;

                        }
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

                        if (isLogin)
                        {
                            CookieContainer cookies = new CookieContainer();
                            HttpClientHandler handler = new HttpClientHandler();
                            handler.CookieContainer = cookies;

                            HttpClient client = new HttpClient(handler);
                            response = client.GetAsync(url).Result;

                            Uri uri = new Uri(url);
                            IEnumerable<Cookie> responseCookies = cookies.GetCookies(uri).Cast<Cookie>();
                            apiResult = responseCookies?.Where(x => x.Name == PCISEnum.Constants.TokenName).Select(y => y.Value).FirstOrDefault();
                            return apiResult;
                        }
                        else
                        {
                            httpRequestMessage.Method = HttpMethod.Put;
                            httpRequestMessage.RequestUri = new Uri(url);

                            response = Task.Run(() => client.SendAsync(httpRequestMessage)).Result;

                        }
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        apiResult = response.Content.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        log.LogInformation("EHR : {0} : API failed : StatusCode-{1} : Reason-{2} : url-{3}", DateTime.Now, (int)response?.StatusCode, response?.ReasonPhrase, url);
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
