﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIAMultimediaSystemV2.Protocols.Senser
{
    public abstract class SenserData : AbsProtocol
    {
        /// <summary>
        /// 溫度
        /// </summary>
        public float Temperature { get; set; }
        /// <summary>
        /// 濕度
        /// </summary>
        public float Humidity { get; set; }
        /// <summary>
        /// 超細懸浮微粒
        /// </summary>
        public float PM1 { get; set; }
        /// <summary>
        /// 懸浮微粒
        /// </summary>
        public float PM25 { get; set; }
        /// <summary>
        /// 細懸浮微粒
        /// </summary>
        public float PM10 { get; set; }
        /// <summary>
        /// 二氧化碳
        /// </summary>
        public float CO2 { get; set; }
        /// <summary>
        /// 揮發性有機物
        /// </summary>
        public float TVOC { get; set; }
        /// <summary>
        /// 甲醛
        /// </summary>
        public float HCHO { get; set; }
        /// <summary>
        /// 臭氧
        /// </summary>
        public float O3 { get; set; }
        /// <summary>
        /// 一氧化碳
        /// </summary>
        public float CO { get; set; }
        /// <summary>
        /// 黴菌
        /// </summary>
        public float Mold { get; set; }
        /// <summary>
        /// 室內指數
        /// </summary>
        public float IAQ { get; set; }
        /// <summary>
        /// 地區名稱
        /// </summary>
        public string LocationName { get; set; }
        /// <summary>
        /// 天氣現象
        /// </summary>
        public string WxName { get; set; }
        /// <summary>
        /// 天氣現象編號
        /// </summary>
        public string WxIndex { get; set; }
        /// <summary>
        /// 降雨機率
        /// </summary>
        public string PoP { get; set; }
        /// <summary>
        /// 舒適度
        /// </summary>
        public string CIName { get; set; }
        /// <summary>
        /// 溫度
        /// </summary>
        public string T { get; set; }
        /// <summary>
        /// 相對溼度
        /// </summary>
        public string RH { get; set; }

        public EwatchWeather EwatchWeather { get; set; } = new EwatchWeather();
        public GIAWeatherData GIAWeatherData { get; set; } = new GIAWeatherData();
    }
    #region 中央氣象天氣資訊回傳值
    /// <summary>
    /// 天氣資訊回傳值
    /// </summary>
    public class WeatherAnalysis
    {
        /// <summary>
        /// 查詢內容名稱
        /// </summary>
        public string datasetDescription;

        public List<Locations> locations = new List<Locations>();
    }
    /// <summary>
    /// 地區名稱
    /// </summary>
    public class Locations
    {
        /// <summary>
        /// 地區名稱
        /// </summary>
        public string locationsName;
        /// <summary>
        /// 資訊
        /// </summary>
        public List<location> location = new List<location>();

    }
    public class location
    {
        public string locationName;
        public List<WeatherElement> weatherElement = new List<WeatherElement>();
    }
    public class WeatherElement
    {
        /// <summary>
        /// 狀態名稱
        /// </summary>
        public string elementName;
        /// <summary>
        /// 時間
        /// </summary>
        public List<Time> time = new List<Time>();
    }
    /// <summary>
    /// 時間
    /// </summary>
    public class Time
    {
        /// <summary>
        /// 起始時間
        /// </summary>
        public DateTime startTime;
        /// <summary>
        /// 結束時間
        /// </summary>
        public DateTime endTime;
        public DateTime dataTime;
        /// <summary>
        /// 內容
        /// </summary>
        public List<elementValue> elementValue;
    }
    /// <summary>
    /// 內容(新版 20220311)
    /// </summary>
    public class elementValue
    {
        public string value;
        public string measures;
    }
    /// <summary>
    /// 內容(舊版)
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// 狀態名稱
        /// </summary>
        public string parameterName;
        /// <summary>
        /// 數值
        /// </summary>
        public string parameterValue;
        /// <summary>
        /// 單位
        /// </summary>
        public string parameterUnit;
    }
    #endregion
    #region 新茂天氣資訊
    public class EwatchWeather
    {
        /// <summary>
        /// 時間格式
        /// </summary>
        public DateTime ttimen { get; set; }
        /// <summary>
        /// 時間字串
        /// </summary>
        public string ttime { get; set; }
        /// <summary>
        /// 縣市代碼
        /// </summary>
        public string resource_id { get; set; }
        /// <summary>
        /// 鄉區代碼
        /// </summary>
        public string geocode { get; set; }
        public double pop12h { get; set; }
        /// <summary>
        /// 天氣資訊
        /// </summary>
        public string wx { get; set; }
        /// <summary>
        /// 天氣資訊代碼
        /// </summary>
        public int wx_Code { get; set; }
        public double at { get; set; }
        /// <summary>
        /// 溫度
        /// </summary>
        public double t { get; set; }
        /// <summary>
        /// 相對溼度
        /// </summary>
        public double rh { get; set; }
        public double ci { get; set; }
        /// <summary>
        /// 天氣描述
        /// </summary>
        public string weatherDescription { get; set; }
        public double pop6h { get; set; }
        public double ws { get; set; }
        public string wd { get; set; }
        public double td { get; set; }
    }
    #endregion
}

public class GIAWeatherData
{
    public WeatherData data { get; set; }
    public string ret { get; set; }
    public string retDescription { get; set; }
}

public class WeatherData
{
    public int tick { get; set; }
    public string alias { get; set; }
    public string uuid { get; set; }
    public string engName { get; set; }
    public string County { get; set; }
    public string MajorPollutant { get; set; }
    public string Status { get; set; }
    public string pm25 { get; set; }
    public string pm10 { get; set; }
    public string temperature { get; set; }
    public string humidity { get; set; }
    public string co { get; set; }
    public string iaq { get; set; }
    public string o3 { get; set; }
    public string so2 { get; set; }
    public string no2 { get; set; }
    public string windSpeed { get; set; }
    public string windDirec { get; set; }
    public string pressure { get; set; }
    public string attribution { get; set; }
}
