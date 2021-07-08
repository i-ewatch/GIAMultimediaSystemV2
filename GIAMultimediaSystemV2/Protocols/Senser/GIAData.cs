using System;

namespace GIAMultimediaSystemV2.Protocols.Senser
{
    public abstract class GIAData : AbsProtocol
    {
        /// <summary>
        /// 空氣品質
        /// </summary>
        public double AirQuality { get; set; }
        /// <summary>
        /// PM2.5(µg/m3) 
        /// </summary>
        public double PM25 { get; set; }
        /// <summary>
        /// PM10(µg/m3) 
        /// </summary>
        public double PM10 { get; set; }
        /// <summary>
        /// CO2(PPM)
        /// </summary>
        public double CO2 { get; set; }
        /// <summary>
        /// TVOC(Ppb)
        /// </summary>
        public double TVOC { get; set; }
        /// <summary>
        /// TVOC的二氧化碳當量值傳感器(PPM)
        /// </summary>
        public double TVOC_CO2EQ { get; set; }
        /// <summary>
        /// TVOC傳感器的H2值
        /// </summary>
        public double TVOC_H2 { get; set; }
        /// <summary>
        /// TVOC傳感器的乙醇值
        /// </summary>
        public double TVOC_ETHANOL { get; set; }
        /// <summary>
        /// 相對濕度(帶補償)
        /// </summary>
        public double HumidityEstimation { get; set; }
        /// <summary>
        /// 相對濕度
        /// </summary>
        public double Humidity { get; set; }
        /// <summary>
        /// 溫度
        /// </summary>
        public double Temperature { get; set; }
        /// <summary>
        /// 溫度(帶補償)
        /// </summary>
        public double DeltaTemperature { get; set; }
        /// <summary>
        /// 甲醛濃度(Ppb)
        /// </summary>
        public double HCHO { get; set; }
        /// <summary>
        /// 甲醛濃度(µg/m3) 
        /// </summary>
        public double HCHO_UG { get; set; }
        /// <summary>
        /// 臭氧濃度(Ppb)
        /// </summary>
        public double O3 { get; set; }
        /// <summary>
        /// 一氧化碳濃度(PPM)
        /// </summary>
        public double CO { get; set; }
        /// <summary>
        /// 來自一氧化碳傳感器的溫度
        /// </summary>
        public double CO_Temperature { get; set; }
        /// <summary>
        /// 超細懸浮微粒
        /// </summary>
        public double PM1 { get; set; }
        /// <summary>
        /// 氫離子
        /// </summary>
        public double PH { get; set; }
        /// <summary>
        /// 氯氣
        /// </summary>
        public double Chlorine { get; set; }
        /// <summary>
        /// 感測器溫度
        /// </summary>
        public double TEMP1 { get; set; }
        /// <summary>
        /// 黴菌
        /// </summary>
        public double Mold
        {
            get;
            //{
                // 黴菌= 10.593*PM10+33.705*溫度(帶補償)-2.596*濕度(帶補償)+0.812*3+0.307*CO2-708.523
                //return (10.593f * PM10) + (33.705f * DeltaTemperature) - (2.596f * HumidityEstimation) + (0.812f * 3) + (0.307f * CO2) - 708.523f;
            //}
            set;
        }
        /// <summary>
        /// 室內指數
        /// </summary>
        public Double IAQ
        {
            get
            {
                double mIAQ = 0;
                #region IAQ-PM25分析
                double mIAQ_Pm25 = 0;
                if (PM25 <= 15)
                    mIAQ_Pm25 = PM25 / 15f * 50f;
                else if (PM25 > 15 && PM25 <= 35)
                    mIAQ_Pm25 = 50f + (PM25 - 15f) / (35f - 15f) * (100f - 50f);
                else if (PM25 > 35 && PM25 <= 55)
                    mIAQ_Pm25 = 100f + (PM25 - 35f) / (55f - 35f) * (150f - 100f);
                else if (PM25 > 55 && PM25 <= 150)
                    mIAQ_Pm25 = 150f + (PM25 - 55f) / (150f - 55f) * (200f - 150f);
                else if (PM25 > 150 && PM25 <= 250)
                    mIAQ_Pm25 = 200f + (PM25 - 150f) / (250f - 150f) * (300f - 200f);
                else if (PM25 > 250 && PM25 <= 350)
                    mIAQ_Pm25 = 300f + (PM25 - 250f) / (350f - 250f) * (400f - 300f);
                else if (PM25 > 350 && PM25 <= 500)
                    mIAQ_Pm25 = 400f + (PM25 - 350f) / (500f - 350f) * (500f - 400f);
                else if (PM25 >= 500)
                    mIAQ_Pm25 = 500;
                #endregion
                #region IAQ-PM10分析
                double mIAQ_Pm10 = 0;
                if (PM10 <= 50)
                    mIAQ_Pm10 = PM10 / 50f * 50f;
                else if (PM10 > 50 && PM10 <= 75)
                    mIAQ_Pm10 = 50f + (PM10 - 50f) / (75f - 50f) * (100f - 50f);
                else if (PM10 > 75 && PM10 <= 150)
                    mIAQ_Pm10 = 100f + (PM10 - 75f) / (150f - 75f) * (150f - 100f);
                else if (PM10 > 150 && PM10 <= 350)
                    mIAQ_Pm10 = 150f + (PM10 - 150f) / (350f - 150f) * (200f - 150f);
                else if (PM10 > 350 && PM10 <= 420)
                    mIAQ_Pm10 = 200f + (PM10 - 350f) / (420f - 350f) * (300f - 200f);
                else if (PM10 > 420 && PM10 <= 500)
                    mIAQ_Pm10 = 300f + (PM10 - 420f) / (500f - 420f) * (400f - 300f);
                else if (PM10 > 500 && PM10 <= 600)
                    mIAQ_Pm10 = 400f + (PM10 - 500f) / (600f - 500f) * (500f - 400f);
                else if (PM10 >= 600)
                    mIAQ_Pm10 = 500;
                #endregion
                #region IAQ-TVOC分析
                double mIAQ_Tvoc = 0;
                if (TVOC <= 0.261)
                    mIAQ_Tvoc = TVOC / 0.261f * 50f;
                else if (TVOC > 0.261f && TVOC <= 0.56f)
                    mIAQ_Tvoc = 50f + (TVOC - 0.261f) / (0.56f - 0.261f) * (100f - 50f);
                else if (TVOC > 0.56f && TVOC <= 1)
                    mIAQ_Tvoc = 100f + (TVOC - 0.56f) / (1f - 0.56f) * (150f - 100f);
                else if (TVOC > 1 && TVOC <= 2)
                    mIAQ_Tvoc = 150f + (TVOC - 1f) / (2f - 1f) * (200f - 150f);
                else if (TVOC > 2 && TVOC <= 3)
                    mIAQ_Tvoc = 200f + (TVOC - 2f) / (3f - 2f) * (300f - 200f);
                else if (TVOC > 3 && TVOC <= 25)
                    mIAQ_Tvoc = 300f + (TVOC - 3f) / (25f - 3f) * (500f - 300f);
                else if (TVOC >= 25)
                    mIAQ_Tvoc = 500;
                #endregion
                #region IAQ-CO2分析
                double mIAQ_Co2 = 0;
                if (CO2 <= 800)
                    mIAQ_Co2 = CO2 / 800f * 50;
                else if (CO2 > 800 && CO2 <= 1000)
                    mIAQ_Co2 = 50f + (CO2 - 800f) / (1000f - 800f) * (100f - 50f);
                else if (CO2 > 1000 && CO2 <= 3500)
                    mIAQ_Co2 = 100f + (CO2 - 1000f) / (3500f - 1000f) * (150f - 100f);
                else if (CO2 > 3500 && CO2 <= 5000)
                    mIAQ_Co2 = 150f + (CO2 - 3500f) / (5000f - 3500f) * (200f - 150f);
                else if (CO2 > 5000 && CO2 <= 10000)
                    mIAQ_Co2 = 200f + (CO2 - 5000f) / (10000f - 5000f) * (300 - 200);
                else if (CO2 > 10000 && CO2 <= 15000)
                    mIAQ_Co2 = 300 + (CO2 - 10000f) / (15000f - 10000f) * (500 - 300);
                else if (CO2 >= 15000)
                    mIAQ_Co2 = 500;
                #endregion
                if (mIAQ_Pm25 > mIAQ)
                    mIAQ = mIAQ_Pm25;
                if (mIAQ_Pm10 > mIAQ)
                    mIAQ = mIAQ_Pm10;
                if (mIAQ_Tvoc > mIAQ)
                    mIAQ = mIAQ_Tvoc;
                if (mIAQ_Co2 > mIAQ)
                    mIAQ = mIAQ_Co2;
                return mIAQ;
            }
        }
    }
}
