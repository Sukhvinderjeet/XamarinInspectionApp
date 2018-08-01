namespace CompanyX.Common.Integration.Provider
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    public class CompanyXHttpClient
    {
        public T Get<T>(string url, Dictionary<string, string> headers)
        {
            url = string.Format("{0}/{1}", Constants.ServerUri.BaseUrl, url);
            T data = default(T);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            foreach (var header in headers)
            {
                request.Headers[header.Key] = header.Value;
            }

            request.SetRawHeader(Constants.HeadersConstants.Accept, Constants.HeadersConstants.JsonContentType);
            using (var response = request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                var rawData = reader.ReadToEnd();
                data = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(rawData);
            }
            return data;
        }

        public T Post<T>(string url, object body = null, Dictionary<string, string> headers = null)
        {
            T data = default(T);

            byte[] dataBytes = null;

            if (body != null)
            {
                dataBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(body));
            }

            url = string.Format("{0}/{1}", Constants.ServerUri.BaseUrl, url);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.ContentLength = dataBytes.Length;
            request.ContentType = "application/json";
            request.Method = "POST";

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers[header.Key] = header.Value;
                }

            }
            using (Stream requestBody = request.GetRequestStream())
            {
                if (dataBytes != null)
                {
                    requestBody.Write(dataBytes, 0, dataBytes.Length);
                }
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                var rawData = reader.ReadToEnd();
                data = JsonConvert.DeserializeObject<T>(rawData);
            }
            return data;
        }
    }
}
