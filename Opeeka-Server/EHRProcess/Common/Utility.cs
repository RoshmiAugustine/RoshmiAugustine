using EHRProcess.DTO;
using EHRProcess.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EHRProcess.Common
{
    public class Utility
    {

        private static readonly HttpClient client = new HttpClient() { Timeout = new TimeSpan(0, 30, 0) };

        public string RestApiCall(ILogger log, string url, bool isExternalAI = false, bool isLogin = false, string requestType = PCISEnum.APIMethodType.GetRequest, string cookie = null, string postParam = "")
        {
            string apiResult = "";
            try
            {
                ServicePoint sp = ServicePointManager.FindServicePoint(new Uri(url));
                sp.ConnectionLimit = 500;
                //checking whether the HttpContext current element is null or not.

                HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

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

        internal bool EHRPostAPICall(string cookie, EHRAssessmentDTO ehrAssessmentDTO, InstrumentConfiguration instrument, ILogger log, string timezone = "")
        {

            try
            {
                if (!string.IsNullOrEmpty(ehrAssessmentDTO.PersonUniversalID) && !string.IsNullOrEmpty(ehrAssessmentDTO.HelperExternalID))
                {
                    string urlToPost = string.Format(instrument.URLToPost, ehrAssessmentDTO.PersonUniversalID, instrument.TypeID);
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, urlToPost);
                    request.Headers.Add("Accept-Language", "en-US");
                    request.Headers.Add("Cookie", "LtpaToken=" + cookie);
                    var content_body = string.Format(instrument.URLBodyParam, ehrAssessmentDTO.ApprovedDate.ToDateStringWithTimeZone(timezone), ehrAssessmentDTO.HelperExternalID);
                    StringContent content = new StringContent(content_body, Encoding.UTF8, "application/x-www-form-urlencoded");
                    request.Content = content;
                    var handler = new HttpClientHandler()
                    {
                        AllowAutoRedirect = false
                    };
                    HttpClient client = new HttpClient(handler);
                    HttpResponseMessage response = Task.Run(() => client.SendAsync(request)).Result;
                    if (response.StatusCode == HttpStatusCode.Redirect)//302 redirect
                    {
                        var redirectURLToComplete = response.Headers.Location.ToString();
                        if (!string.IsNullOrEmpty(redirectURLToComplete))
                        {
                            if (redirectURLToComplete.EndsWith(PCISEnum.Constants.RedirectURLAction + PCISEnum.Constants.RedirectURLParam2))
                            {
                                redirectURLToComplete = redirectURLToComplete.Replace(PCISEnum.Constants.RedirectURLAction, PCISEnum.Constants.RedirectURLReplaceAction);
                            }
                            else
                            {
                                redirectURLToComplete = redirectURLToComplete + PCISEnum.Constants.RedirectURLReplaceAction + PCISEnum.Constants.RedirectURLParam2;
                            }
                            var requestToComplete = new HttpRequestMessage(HttpMethod.Post, redirectURLToComplete);
                            requestToComplete.Headers.Add("Accept-Language", "en-US");
                            requestToComplete.Headers.Add("Cookie", "LtpaToken=" + cookie);
                            response = Task.Run(() => client.SendAsync(requestToComplete)).Result;
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                log.LogInformation($"EHRUpdateProcess : {DateTime.Now} : AgencyID-{ehrAssessmentDTO.AgencyID} : Failed at EHRPostAPICall : {ex.Message}");
                return false;
            }
        }
    }
}
