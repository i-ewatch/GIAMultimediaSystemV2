namespace GIAMultimediaSystemV2.Protocols.Senser
{
    public abstract class GIAAPIData :AbsProtocol
    {
        public GIAAPIValue GIAAPIValue { get; set; }
    }
    public class GIAAPIValue
    {
        public double tick { get; set; }
        public double pm25 { get; set; }
        public double pm10 { get; set; }
        public double temperature { get; set; }
        public double tvoc { get; set; }
        public double humidity { get; set; }
        public double iaq { get; set; }
        public double co2 { get; set; }
        public double hcho { get; set; }
        public double mold { get; set; }
        public double co { get; set; }
        public double o3 { get; set; }
        public double pm1 { get; set; }
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
    }
}
