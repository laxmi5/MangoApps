using System;
using System.Windows;
using System.Text;
using System.Net;
using System.Configuration;
using System.IO;
using MangoAppsLoginApi.Model;
using Newtonsoft.Json;

namespace MangoAppsLoginApi.RestApi
{
    public class ApiHelper
    {
        FormData formDataobj = null;

        MsRequest msobj = null;

        public ApiHelper()
        {
            formDataobj = new FormData();
            msobj = new MsRequest();
            formDataobj.ms_request = msobj;
        }

        public void ConnectToServer(User userobj)
        {
            msobj.user = userobj;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["DomainUrl"].ToString());
            request.Method = "Post";  

            string postData = JsonConvert.SerializeObject(formDataobj);

            byte[] data = Encoding.UTF8.GetBytes(postData);

            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "application/json";
            request.ContentLength = data.Length;
            request.CookieContainer = new CookieContainer();

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(data, 0, data.Length);
            }

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();

                    /* Checking Authentication errors also and displaying a generic error message */
                    if (response.StatusCode != HttpStatusCode.OK || responseFromServer.Contains("ms_errors"))
                    {
                        MessageBox.Show("We were unable to reach the server or it did not properly respond. \n\n Kindly verify the following and try again: \n1.Your Domain URL is correct(e.g. https://demo.mangoapps.com) \n 2.You are connected to the Internet \n 3.The proxy settings are configured correctly in Preference(if you use a proxy to connect to the internet) \n 4. Server may be temporarily down. Please re-try in a few minutes.", "Unable to Connect", MessageBoxButton.OK, MessageBoxImage.Stop);
                    }
                    else
                    {
                        MessageBox.Show("Successfull Login", "", MessageBoxButton.OK, MessageBoxImage.Information);

                        /* As per the given scenario, expiring _felix_session_id 
                           so that next login is treated as a fresh login as we are not calling any logout API for now */

                        if (response.Cookies["_felix_session_id"] != null)
                        {
                            response.Cookies["_felix_session_id"].Expired = true;
                            response.Cookies["_felix_session_id"].Expires = DateTime.Now.AddYears(-30);
                            response.Cookies.Add(response.Cookies["_felix_session_id"]);
                        }
                    }
                    }
                }
            catch (WebException ex)
            {
                MessageBox.Show("We were unable to reach the server or it did not properly respond. \n\n Kindly verify the following and try again: \n1.Your Domain URL is correct(e.g. https://demo.mangoapps.com) \n 2.You are connected to the Internet \n 3.The proxy settings are configured correctly in Preference(if you use a proxy to connect to the internet) \n 4. Server may be temporarily down. Please re-try in a few minutes.", "Unable to Connect", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

    }
}