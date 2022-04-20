﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NModbus;
using RestSharp;
using Serilog;
using System;
using System.Linq;

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
                var Taiwan_DistricsSetting = Taiwan_DistricsSettings.SingleOrDefault(g => g.CityName == GateWaySetting.GateWays[0].LocationName);
                if (Taiwan_DistricsSetting.dataid != "")
                {
                    var client = new RestClient($"{GateWaySetting.GateWays[0].WeatherAPILocation}/{Taiwan_DistricsSetting.dataid}?" + "Authorization=" + $"{GateWaySetting.GateWays[0].Authorization}" + "&locationName=" + $"{GateWaySetting.GateWays[0].DistrictName}");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.GET);
                    IRestResponse response = client.Execute(request);
                    if (response != null)
                    {
                        try
                        {
                            JObject jsondata = JsonConvert.DeserializeObject<JObject>(response.Content);
                            if (jsondata != null)
                            {
                                WeatherAnalysis weatherAnalysis = JsonConvert.DeserializeObject<WeatherAnalysis>(jsondata["records"].ToString());
                                #region 內容分析
                                foreach (var Locationsitem in weatherAnalysis.locations)
                                {
                                    LocationName = Locationsitem.locationsName;//區域名稱
                                    foreach (var locationitem in Locationsitem.location)
                                    {
                                        foreach (var WeatherElementitem in locationitem.weatherElement)
                                        {
                                            switch (WeatherElementitem.elementName)
                                            {
                                                case "Wx"://天氣氣象 
                                                    {
                                                        for (int i = 0; i < WeatherElementitem.time.Count; i++)
                                                        {
                                                            if (NowTime >= WeatherElementitem.time[i].startTime && NowTime < WeatherElementitem.time[i].endTime)
                                                            {
                                                                WxName = WeatherElementitem.time[i].elementValue[0].value;
                                                                WxIndex = Convert.ToInt32(WeatherElementitem.time[i].elementValue[1].value).ToString();
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                if (WeatherElementitem.time[0].elementValue != null)
                                                                {
                                                                    WxName = WeatherElementitem.time[0].elementValue[0].value;
                                                                    WxIndex = Convert.ToInt32(WeatherElementitem.time[0].elementValue[1].value).ToString();
                                                                }
                                                            }
                                                        }
                                                    }
                                                    break;
                                                case "PoP12h"://降雨機率
                                                    {
                                                        for (int i = 0; i < WeatherElementitem.time.Count; i++)
                                                        {
                                                            if (NowTime >= WeatherElementitem.time[i].startTime && NowTime < WeatherElementitem.time[i].endTime)
                                                            {
                                                                PoP = WeatherElementitem.time[i].elementValue[0].value;
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                PoP = WeatherElementitem.time[0].elementValue[0].value;
                                                            }
                                                        }
                                                    }
                                                    break;
                                                case "CI"://舒適度
                                                    {
                                                        foreach (var timeitem in WeatherElementitem.time)
                                                        {
                                                            TimeSpan timeSpan = DateTime.Now.Subtract(timeitem.dataTime);
                                                            if (timeSpan.TotalHours <= 3)
                                                            {
                                                                CIName = timeitem.elementValue[1].value;
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                CIName = timeitem.elementValue[1].value;
                                                            }
                                                        }
                                                    }
                                                    break;
                                                case "T"://室外溫度
                                                    {
                                                        foreach (var timeitem in WeatherElementitem.time)
                                                        {
                                                            TimeSpan timeSpan = DateTime.Now.Subtract(timeitem.dataTime);
                                                            if (timeSpan.TotalHours <= 3)
                                                            {
                                                                T = timeitem.elementValue[0].value;
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                T = timeitem.elementValue[0].value;
                                                            }
                                                        }
                                                    }
                                                    break;
                                                case "RH":
                                                    {
                                                        foreach (var timeitem in WeatherElementitem.time)
                                                        {
                                                            TimeSpan timeSpan = DateTime.Now.Subtract(timeitem.dataTime);
                                                            if (timeSpan.TotalHours <= 3)
                                                            {
                                                                RH = timeitem.elementValue[0].value;
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                RH = timeitem.elementValue[0].value;
                                                            }
                                                        }
                                                    }
                                                    break;
                                            }
                                        }
                                    }
                                }
                                #endregion
                                ConnectFlag = true;
                            }
                        }
                        catch (Exception)
                        {
                            Log.Error($"天氣資訊API : {response.Content}");
                            ConnectFlag = false;
                        }
                    }
                }
            }
            else
            {
                ConnectFlag = false;
            }
        }
    }
}
