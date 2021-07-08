using NModbus;
using Serilog;
using System;

namespace GIAMultimediaSystemV2.Protocols.Senser
{
    public class GIAProtocol : GIAData
    {
        public override void DataReader(IModbusMaster master)
        {
            try
            {
                ushort[] value = master.ReadInputRegisters(ID, 0, 17);
                ushort[] value1 = master.ReadInputRegisters(ID, 28, 1);
                ushort[] value2 = master.ReadInputRegisters(ID, 30, 1);
                if (value.Length == 17)
                {
                    int Index = 0;
                    AirQuality = (value[Index]); Index++;
                    PM25 = value[Index]; Index++;
                    PM10 = (value[Index]); Index++;
                    CO2 = (value[Index]); Index++;
                    TVOC = (value[Index]) / 1000f; Index++;
                    TVOC_CO2EQ = (value[Index]); Index++;
                    TVOC_H2 = (value[Index]); Index++;
                    TVOC_ETHANOL = (value[Index]); Index++;
                    HumidityEstimation = (value[Index]) * 0.01f; Index++;
                    Humidity = (value[Index]) * 0.01f; Index++;
                    Temperature = (value[Index] - 4500) * 0.01f; Index++;
                    DeltaTemperature = (value[Index]) * 0.01f; Index++;
                    HCHO = (value[Index]) / 1000f; Index++;
                    HCHO_UG = (value[Index]); Index++;
                    O3 = (value[Index]) / 1000f; Index++;
                    CO = (value[Index]); Index++;
                    CO_Temperature = (value[Index]) * 0.01f;
                    Mold = value1[0];
                    PM1 = value2[0];
                }
            }
            catch (Exception ex)
            {
                ConnectFlag = false;
                Log.Error(ex, $"GIA感測器解析異常、通訊編號: {GatewayIndex}、設備編號: {DeviceIndex}");
            }
        }
        public override void DataAPIReader() { }
    }
}
