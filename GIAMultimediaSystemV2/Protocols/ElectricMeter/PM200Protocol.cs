using NModbus;
using Serilog;
using System;

namespace GIAMultimediaSystemV2.Protocols.ElectricMeter
{
    public class PM200Protocol:ElectricMeterData
    {
        public override void DataReader(IModbusMaster master)
        {
            try
            {
                int Index = 0;
                ushort[] kwhvalue = master.ReadHoldingRegisters(ID, 0, 2); //kwh
                ushort[] kwvalue = master.ReadHoldingRegisters(ID, 8, 2);//kw
                kWh = MathClass.work16to10(kwhvalue[Index + 1], kwhvalue[Index]) * 0.01;
                kW = MathClass.work16to10(kwvalue[Index + 1], kwvalue[Index]) * 0.001;
                ConnectFlag = true;
            }
            catch (Exception ex)
            {
                ConnectFlag = false;
                Log.Error(ex, $"PM200解析異常、通訊編號: {GatewayIndex}、設備編號: {DeviceIndex}");
            }
        }
        public override void DataAPIReader()
        {
        }
    }
}
