using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NModbus;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIAMultimediaSystemV2.Protocols.Senser
{
    public class WeatherProtocol : SenserData
    {
        public override void DataReader(IModbusMaster master) { }
        public override void DataAPIReader()
        {
            DateTime NowTime = DateTime.Now;
            if (GateWaySetting.GateWays[0].Authorization != "氣象開放資料平台會員授權碼" && GateWaySetting.GateWays[0].Authorization != "")
            {
                var client = new RestClient($"{GateWaySetting.GateWays[0].WeatherAPILocation}" + "Authorization=" + $"{GateWaySetting.GateWays[0].Authorization}" + "&locationName=" + $"{GateWaySetting.GateWays[0].LocationName}");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);
                if (response != null)
                {
                    JObject jsondata = JsonConvert.DeserializeObject<JObject>(response.Content);
                    if (jsondata != null)
                    {
                        WeatherAnalysis weatherAnalysis = JsonConvert.DeserializeObject<WeatherAnalysis>(jsondata["records"].ToString());
                        #region 內容分析
                        foreach (var Locationitem in weatherAnalysis.location)
                        {
                            LocationName = Locationitem.locationName;//區域名稱
                            foreach (var WeatherElementitem in Locationitem.weatherElement)
                            {
                                switch (WeatherElementitem.elementName)
                                {
                                    case "Wx"://天氣氣象 
                                        {
                                            foreach (var timeitem in WeatherElementitem.time)
                                            {
                                                if (NowTime >= timeitem.startTime && NowTime < timeitem.endTime)
                                                {
                                                    WxName = timeitem.parameter.parameterName;
                                                    WxIndex = timeitem.parameter.parameterValue;
                                                    break;
                                                }
                                                else
                                                {
                                                    WxName = WeatherElementitem.time[0].parameter.parameterName;
                                                    WxIndex = WeatherElementitem.time[0].parameter.parameterValue;
                                                }
                                            }
                                        }
                                        break;
                                    case "PoP"://降雨機率
                                        {
                                            foreach (var timeitem in WeatherElementitem.time)
                                            {
                                                if (NowTime >= timeitem.startTime && NowTime < timeitem.endTime)
                                                {
                                                    PoP = timeitem.parameter.parameterName;
                                                    break;
                                                }
                                                else
                                                {
                                                    PoP = WeatherElementitem.time[0].parameter.parameterName;
                                                }
                                            }
                                        }
                                        break;
                                    case "MinT"://最低溫
                                        {
                                            foreach (var timeitem in WeatherElementitem.time)
                                            {
                                                if (NowTime >= timeitem.startTime && NowTime < timeitem.endTime)
                                                {
                                                    MinT = timeitem.parameter.parameterName;
                                                    break;
                                                }
                                                else
                                                {
                                                    MinT = WeatherElementitem.time[0].parameter.parameterName;
                                                }
                                            }
                                        }
                                        break;
                                    case "CI"://舒適度
                                        {
                                            foreach (var timeitem in WeatherElementitem.time)
                                            {
                                                if (NowTime >= timeitem.startTime && NowTime < timeitem.endTime)
                                                {
                                                    CIName = timeitem.parameter.parameterName;
                                                    break;
                                                }
                                                else
                                                {
                                                    CIName = WeatherElementitem.time[0].parameter.parameterName;
                                                }
                                            }
                                        }
                                        break;
                                    case "MaxT"://最高溫
                                        {
                                            foreach (var timeitem in WeatherElementitem.time)
                                            {
                                                if (NowTime >= timeitem.startTime && NowTime < timeitem.endTime)
                                                {
                                                    MaxT = timeitem.parameter.parameterName;
                                                    break;
                                                }
                                                else
                                                {
                                                    MaxT = WeatherElementitem.time[0].parameter.parameterName;
                                                }
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                        #endregion
                    }
                }
                ConnectFlag = true;
            }
            else
            {
                ConnectFlag = false;
            }
        }
    }
}
