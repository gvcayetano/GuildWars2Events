using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace GuildWars2Events.Model.Extensions
{
    public static class String
    {
        public static string SubmitQuery(this string value)
        {
            Console.WriteLine(
                "[{0}] Web Request: {1}"
                , DateTime.Now
                , value);
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(value);
            httpWebRequest.MaximumAutomaticRedirections = 10;
            httpWebRequest.MaximumResponseHeadersLength = 10;
            httpWebRequest.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            if (receiveStream == null)
            {
                return System.String.Empty;
            }
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            return readStream.ReadToEnd();
        }
    }
}
