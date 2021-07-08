using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NModbus;
using RestSharp;

namespace GIAMultimediaSystemV2.Protocols.Senser
{
    public class GIAAPIProtocol : GIAAPIData
    {
        public override void DataReader(IModbusMaster master){ }
        public override void DataAPIReader()
        {
            var client = new RestClient($"{GIALocation}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            if (response != null)
            {
                JObject jsondata = JsonConvert.DeserializeObject<JObject>(response.Content);
                if (jsondata != null)
                {
                    JArray jsonArraydata = JsonConvert.DeserializeObject<JArray>(jsondata["data"].ToString());
                    GIAAPIValue Value = JsonConvert.DeserializeObject<GIAAPIValue>(jsonArraydata[0]["sensors"].ToString());
                    GIAAPIValue = Value;
                }
            }
        }
    }
}
