using Newtonsoft.Json;
using RestSharp;
using TelefonRehberiNuevo.CORE.Constants;

namespace TelefonRehberiNuevo.WEB.Managers
{
    public class ServiceManager
    {
        private const string ServerIp = ApiUrlConstants.BaseUrl;

        protected static RestClient Client;
        protected static object LockSync = new object();



        public static void Init()
        {
            lock (LockSync)
            {
                if (Client != null) return;
                Client = new RestClient(ServerIp);
                //var req = new RestRequest("values/Init", Method.GET);
                //Client.Execute(req);
                //var statu = Client.Execute<Dictionary<string, object>>(req);
                //var value = statu.Data.FirstOrDefault(p => p.Key == "Result").Value.ToString();
            }
        }

        /// <summary>
        /// <para>Girilen url ' e gönderilen data ile get istegi atar </para>
        /// <para>Belirtilen dönüs tipinde veriyi geriye döndürür</para>
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public static TResponse RestSharpGet<TResponse>(string url)
        {
            var req = new RestRequest(url, Method.GET);
            //req.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            var response = Client.Execute(req);
            return JsonConvert.DeserializeObject<TResponse>(response.Content);
        }


        /// <summary>
        /// <para>Girilen url ' e gönderilen data ile post istegi atar </para>
        /// <para>Belirtilen dönüş tipinde veriyi geriye döndürür</para>
        /// </summary>
        /// <typeparam name="TResponse">Geriye dönüs tipi</typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static TResponse RestSharpPost<TResponse>(string url, object data)
        {
            var jsonToSend = JsonConvert.SerializeObject(data);
            var req = new RestRequest(url, Method.POST);
            req.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);
            req.RequestFormat = DataFormat.Json;
            var response = Client.Execute(req);
            return JsonConvert.DeserializeObject<TResponse>(response.Content);
        }
    }
}