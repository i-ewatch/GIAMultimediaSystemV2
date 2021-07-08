using GIAMultimediaSystemV2.Configuration;
using GIAMultimediaSystemV2.Enums;
using GIAMultimediaSystemV2.Methods;
using GIAMultimediaSystemV2.Protocols.ElectricMeter;
using GIAMultimediaSystemV2.Protocols.Senser;
using NModbus;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GIAMultimediaSystemV2.Components
{
    public partial class TCPComponent : Field4Component
    {
        public TCPComponent(GateWaySetting gateWaySetting, GateWay gateWay, SqlMethod sqlMethod)
        {
            InitializeComponent();
            GateWaySetting = gateWaySetting;
            GateWay = gateWay;
            SqlMethod = sqlMethod;
        }

        public TCPComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        protected override void AfterMyWorkStateChanged(object sender, EventArgs e)
        {
            if (myWorkState)
            {
                Factory = new ModbusFactory();
                foreach (var item in GateWay.GateWayElectricIDs)
                {
                    ElectricEnumType = (ElectricEnumType)item.ElectricEnumType;
                    switch (ElectricEnumType)
                    {
                        //case ElectricEnumType.PA310:
                        //    {
                        //        PA310Protocol protocol = new PA310Protocol() { GateWaySetting = GateWaySetting, GatewayIndex = GateWay.GatewayIndex, DeviceIndex = item.DeviceIndex, GroupIndex = item.GroupIndex, ID = item.DeviceID, LoopEnumType = item.LoopEnumType, PhaseAngleEnumType = item.PhaseAngleEnumType, PhaseEnumType = item.PhaseEnumType, ElectricEnumType = item.ElectricEnumType };
                        //        AbsProtocols.Add(protocol);
                        //    }
                        //    break;
                        //case ElectricEnumType.HC660:
                        //    {
                        //        HC6600Protocol protocol = new HC6600Protocol() { GateWaySetting = GateWaySetting, GatewayIndex = GateWay.GatewayIndex, DeviceIndex = item.DeviceIndex, GroupIndex = item.GroupIndex, ID = item.DeviceID, LoopEnumType = item.LoopEnumType, PhaseAngleEnumType = item.PhaseAngleEnumType, PhaseEnumType = item.PhaseEnumType, ElectricEnumType = item.ElectricEnumType };
                        //        AbsProtocols.Add(protocol);
                        //    }
                        //    break;
                        //case ElectricEnumType.CPM6:
                        //    {
                        //        CPM6Protocol protocol = new CPM6Protocol() { GateWaySetting = GateWaySetting, GatewayIndex = GateWay.GatewayIndex, DeviceIndex = item.DeviceIndex, GroupIndex = item.GroupIndex, ID = item.DeviceID, LoopEnumType = item.LoopEnumType, PhaseAngleEnumType = item.PhaseAngleEnumType, PhaseEnumType = item.PhaseEnumType, ElectricEnumType = item.ElectricEnumType };
                        //        AbsProtocols.Add(protocol);
                        //    }
                        //    break;
                        //case ElectricEnumType.PA60:
                        //    {
                        //        PA60Protocol protocol = new PA60Protocol() { GateWaySetting = GateWaySetting, GatewayIndex = GateWay.GatewayIndex, DeviceIndex = item.DeviceIndex, GroupIndex = item.GroupIndex, ID = item.DeviceID, LoopEnumType = item.LoopEnumType, PhaseAngleEnumType = item.PhaseAngleEnumType, PhaseEnumType = item.PhaseEnumType, ElectricEnumType = item.ElectricEnumType };
                        //        AbsProtocols.Add(protocol);
                        //    }
                        //    break;
                        //case ElectricEnumType.ABBM2M:
                        //    {
                        //        ABBM2MProtocol protocol = new ABBM2MProtocol() { GateWaySetting = GateWaySetting, GatewayIndex = GateWay.GatewayIndex, DeviceIndex = item.DeviceIndex, GroupIndex = item.GroupIndex, ID = item.DeviceID, LoopEnumType = item.LoopEnumType, PhaseAngleEnumType = item.PhaseAngleEnumType, PhaseEnumType = item.PhaseEnumType, ElectricEnumType = item.ElectricEnumType };
                        //        AbsProtocols.Add(protocol);
                        //    }
                        //    break;
                        //case ElectricEnumType.PM200:
                        //    {
                        //        PM200Protocol protocol = new PM200Protocol() { GateWaySetting = GateWaySetting, GatewayIndex = GateWay.GatewayIndex, DeviceIndex = item.DeviceIndex, GroupIndex = item.GroupIndex, ID = item.DeviceID, LoopEnumType = item.LoopEnumType, PhaseAngleEnumType = item.PhaseAngleEnumType, PhaseEnumType = item.PhaseEnumType, ElectricEnumType = item.ElectricEnumType };
                        //        AbsProtocols.Add(protocol);
                        //    }
                        //    break;
                        //case ElectricEnumType.TWCPM4:
                        //    {
                        //        TWCPM4Protocol protocol = new TWCPM4Protocol() { GateWaySetting = GateWaySetting, GatewayIndex = GateWay.GatewayIndex, DeviceIndex = item.DeviceIndex, GroupIndex = item.GroupIndex, ID = item.DeviceID, LoopEnumType = item.LoopEnumType, PhaseAngleEnumType = item.PhaseAngleEnumType, PhaseEnumType = item.PhaseEnumType, ElectricEnumType = item.ElectricEnumType };
                        //        AbsProtocols.Add(protocol);
                        //    }
                        //    break;
                    }
                }
                foreach (var item in GateWay.GateWaySenserIDs)
                {
                    SenserEnumType = (SenserEnumType)item.SenserEnumType;
                    switch (SenserEnumType)
                    {
                        //case SenserEnumType.BlackSenser:
                        //    {
                        //        BlackSenserProtocol protocol = new BlackSenserProtocol() { GateWaySetting = GateWaySetting, GatewayIndex = GateWay.GatewayIndex, DeviceIndex = item.DeviceIndex, ID = item.DeviceID, SenserEnumType = item.SenserEnumType };
                        //        AbsProtocols.Add(protocol);
                        //    }
                        //    break;
                        //case SenserEnumType.WhiteSenser:
                        //    {
                        //        WhiteSenserProtocol protocol = new WhiteSenserProtocol() { GateWaySetting = GateWaySetting, GatewayIndex = GateWay.GatewayIndex, DeviceIndex = item.DeviceIndex, ID = item.DeviceID, SenserEnumType = item.SenserEnumType };
                        //        AbsProtocols.Add(protocol);
                        //    }
                        //    break;
                        case SenserEnumType.WeatherAPI:
                            {
                                WeatherProtocol protocol = new WeatherProtocol() { GateWaySetting = GateWaySetting, GatewayIndex = GateWay.GatewayIndex, DeviceIndex = item.DeviceIndex, ID = item.DeviceID, Tag = "API" };
                                AbsProtocols.Add(protocol);
                            }
                            break;
                        case SenserEnumType.GIAAPI:
                            {
                                GIAAPIProtocol protocol = new GIAAPIProtocol() { GateWaySetting = GateWaySetting, GatewayIndex = GateWay.GatewayIndex, DeviceIndex = item.DeviceIndex, ID = item.DeviceID, GIALocation = GateWay.GIAAPILocation, Tag = "API" };
                                AbsProtocols.Add(protocol);
                            }
                            break;
                            //case SenserEnumType.GIA:
                            //    {
                            //        GIAProtocol protocol = new GIAProtocol() { GateWaySetting = GateWaySetting, GatewayIndex = GateWay.GatewayIndex, DeviceIndex = item.DeviceIndex, ID = item.DeviceID };
                            //        AbsProtocols.Add(protocol);
                            //    }
                            //    break;
                    }
                }
                ReadThread = new Thread(Analysis);
                ReadThread.Priority = ThreadPriority.Highest;
                ReadThread.Start();
            }
            else
            {
                if (ReadThread != null)
                {
                    ReadThread.Abort();
                }
            }
        }
        private void Analysis()
        {
            while (myWorkState)
            {
                TimeSpan timeSpan = DateTime.Now.Subtract(ReadTime);
                if (timeSpan.TotalSeconds >= 5)
                {
                    try
                    {
                        foreach (var item in AbsProtocols)
                        {
                            string Tag = item.Tag.ToString();
                            if (Tag == "API")
                            {
                                item.DataAPIReader();
                                item.DataReader(master);
                                Thread.Sleep(10);
                                ReadTime = DateTime.Now;
                            }
                            else
                            {
                                using (TcpClient client = new TcpClient(GateWay.ModbusTCPLocation, GateWay.ModbusTCPRate))
                                {
                                    master = Factory.CreateMaster(client);//建立TCP通訊
                                    master.Transport.Retries = 3;
                                    master.Transport.ReadTimeout = 500;
                                    master.Transport.WriteTimeout = 500;
                                    item.DataAPIReader();
                                    item.DataReader(master);
                                    Thread.Sleep(10);
                                    ReadTime = DateTime.Now;
                                }
                            }
                        }
                    }
                    catch (ThreadAbortException) { }
                    catch (Exception ex)
                    {
                        ReadTime = DateTime.Now;
                        Log.Error(ex, $"通訊失敗 IP:{GateWay.ModbusTCPLocation} Port:{GateWay.ModbusTCPRate} ");
                    }
                }
                else
                {
                    Thread.Sleep(80);
                }
            }
        }

    }
}
