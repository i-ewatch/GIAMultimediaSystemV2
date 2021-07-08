using GIAMultimediaSystemV2.Configuration;
using GIAMultimediaSystemV2.Enums;
using GIAMultimediaSystemV2.Protocols;
using GIAMultimediaSystemV2.Protocols.ElectricMeter;
using GIAMultimediaSystemV2.Protocols.Senser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UpToIewatchLibrary;

namespace GIAMultimediaSystemV2.Methods
{
    public class UploadMethod
    {
        /// <summary>
        /// 上傳設定
        /// </summary>
        public UploadSetting UploadSetting { get; set; }
        /// <summary>
        /// 上傳公式
        /// </summary>
        public UpToIewatchClass upToIewatchClass = new UpToIewatchClass();
        /// <summary>
        /// 迴路類型
        /// </summary>
        private LoopEnumType LoopEnumType { get; set; }
        /// <summary>
        /// 相位類型
        /// </summary>
        private PhaseEnumType PhaseEnumType { get; set; }
        /// <summary>
        /// 相位角類型
        /// </summary>
        private PhaseAngleEnumType PhaseAngleEnumType { get; set; }
        /// <summary>
        /// 上傳類型
        /// </summary>
        private UploadEnumType UploadEnumType { get; set; }
        /// <summary>
        /// 感測器類型
        /// </summary>
        private SenserEnumType SenserEnumType { get; set; }
        /// <summary>
        /// 電表類型
        /// </summary>
        private ElectricEnumType ElectricEnumType { get; set; }
        /// <summary>
        /// 上傳方法
        /// </summary>
        public void Upload_Value(List<AbsProtocol> protocols)
        {
            UploadEnumType = (UploadEnumType)UploadSetting.UploadEnumType;
            switch (UploadEnumType)
            {
                case UploadEnumType.X200:
                    {
                        Send_X200(protocols);
                    }
                    break;
                case UploadEnumType.API:
                    {

                    }
                    break;
            }
        }
        private void Send_X200(List<AbsProtocol> protocols)
        {
            foreach (var item in UploadSetting.SendX200s)
            {
                string Card = item.CardNo;
                string Board = item.BoardNo;
                float RSV = 0, STV = 0, TRV = 0;
                float RA = 0, SA = 0, TA = 0;
                float KW = 0, KWH = 0;
                float PFE = 0;
                byte[] DI = new byte[8];
                byte[] DO = new byte[8];
                int[] AI = new int[8];
                bool deviceCount = false;           //電力設備標籤
                int diCount = 0;
                int aiCount = 0;
                foreach (var senseritem in item.SenserSendItems)
                {
                    SenserEnumType = (SenserEnumType)senseritem.SenserEnumType;
                    switch (SenserEnumType)
                    {
                        case SenserEnumType.BlackSenser:
                            {
                                if (aiCount < 8)
                                {
                                    var data = protocols.Where(g => g.GatewayIndex == senseritem.GatewayIndex & g.DeviceIndex == senseritem.DeviceIndex).ToList();
                                    if (data.Count > 0)
                                    {
                                        BlackSenserProtocol protocol = (BlackSenserProtocol)data[0];
                                        AI[aiCount] = Convert.ToInt32(protocol.Temperature * 10); aiCount++;
                                        AI[aiCount] = Convert.ToInt32(protocol.Humidity * 10); aiCount++;
                                    }
                                }
                            }
                            break;
                        case SenserEnumType.WhiteSenser:
                            {
                                if (aiCount < 8)
                                {
                                    var data = protocols.Where(g => g.GatewayIndex == senseritem.GatewayIndex & g.DeviceIndex == senseritem.DeviceIndex).ToList();
                                    if (data.Count > 0)
                                    {
                                        WhiteSenserProtocol protocol = (WhiteSenserProtocol)data[0];
                                        AI[aiCount] = Convert.ToInt32(protocol.Temperature * 10); aiCount++;
                                        AI[aiCount] = Convert.ToInt32(protocol.Humidity * 10); aiCount++;
                                    }
                                }
                            }
                            break;

                        case SenserEnumType.WeatherAPI:
                            break;
                    }
                }
                foreach (var electricitem in item.ElectricSendItems)
                {
                    ElectricEnumType = (ElectricEnumType)electricitem.ElectricEnumType;
                    switch (ElectricEnumType)
                    {
                        case ElectricEnumType.PA310:
                            {
                                if (!deviceCount)
                                {
                                    var data = protocols.Where(g => g.GatewayIndex == electricitem.GatewayIndex & g.DeviceIndex == electricitem.DeviceIndex).ToList();
                                    PhaseEnumType = (PhaseEnumType)electricitem.PhaseEnumType;
                                    switch (PhaseEnumType)
                                    {
                                        case PhaseEnumType.ThreePhase:
                                            {
                                                PA310Protocol electricProtocol = (PA310Protocol)data[0];
                                                RSV = Convert.ToSingle(electricProtocol.RSv);
                                                STV = Convert.ToSingle(electricProtocol.STv);
                                                TRV = Convert.ToSingle(electricProtocol.TRv);
                                                RA = Convert.ToSingle(electricProtocol.RA);
                                                SA = Convert.ToSingle(electricProtocol.SA);
                                                TA = Convert.ToSingle(electricProtocol.TA);
                                                KW = Convert.ToSingle(electricProtocol.kW);
                                                KWH = Convert.ToSingle(electricProtocol.kWh);
                                                PFE = Convert.ToSingle(electricProtocol.PF);
                                                deviceCount = true;         //已經有設備了
                                            }
                                            break;
                                        case PhaseEnumType.SinglePhase:
                                            {
                                                PA310Protocol electricProtocol = (PA310Protocol)data[0];
                                                PhaseAngleEnumType = (PhaseAngleEnumType)electricitem.PhaseAngleEnumType;
                                                switch (PhaseAngleEnumType)
                                                {
                                                    case PhaseAngleEnumType.R:
                                                        {
                                                            RSV = Convert.ToSingle(electricProtocol.Rv);
                                                            RA = Convert.ToSingle(electricProtocol.RA);
                                                            KW = Convert.ToSingle(electricProtocol.kW_A);
                                                            KWH = Convert.ToSingle(electricProtocol.kWh_A);
                                                            PFE = Convert.ToSingle(electricProtocol.PF_A);
                                                            deviceCount = true;         //已經有設備了
                                                        }
                                                        break;
                                                    case PhaseAngleEnumType.S:
                                                        {
                                                            RSV = Convert.ToSingle(electricProtocol.Sv);
                                                            RA = Convert.ToSingle(electricProtocol.SA);
                                                            KW = Convert.ToSingle(electricProtocol.kW_B);
                                                            KWH = Convert.ToSingle(electricProtocol.kWh_B);
                                                            PFE = Convert.ToSingle(electricProtocol.PF_B);
                                                            deviceCount = true;         //已經有設備了
                                                        }
                                                        break;
                                                    case PhaseAngleEnumType.T:
                                                        {
                                                            RSV = Convert.ToSingle(electricProtocol.Tv);
                                                            RA = Convert.ToSingle(electricProtocol.TA);
                                                            KW = Convert.ToSingle(electricProtocol.kW_C);
                                                            KWH = Convert.ToSingle(electricProtocol.kWh_C);
                                                            PFE = Convert.ToSingle(electricProtocol.PF_C);
                                                            deviceCount = true;         //已經有設備了
                                                        }
                                                        break;
                                                }
                                            }
                                            break;
                                    }
                                }
                            }
                            break;
                        case ElectricEnumType.HC660:
                            {
                                if (!deviceCount)
                                {
                                    var data = protocols.Where(g => g.GatewayIndex == electricitem.GatewayIndex & g.DeviceIndex == electricitem.DeviceIndex).ToList();
                                    PhaseEnumType = (PhaseEnumType)electricitem.PhaseEnumType;
                                    switch (PhaseEnumType)
                                    {
                                        case PhaseEnumType.ThreePhase:
                                            {
                                                HC6600Protocol electricProtocol = (HC6600Protocol)data[0];
                                                RSV = Convert.ToSingle(electricProtocol.RSv);
                                                STV = Convert.ToSingle(electricProtocol.STv);
                                                TRV = Convert.ToSingle(electricProtocol.TRv);
                                                RA = Convert.ToSingle(electricProtocol.RA);
                                                SA = Convert.ToSingle(electricProtocol.SA);
                                                TA = Convert.ToSingle(electricProtocol.TA);
                                                KW = Convert.ToSingle(electricProtocol.kW);
                                                KWH = Convert.ToSingle(electricProtocol.kWh);
                                                PFE = Convert.ToSingle(electricProtocol.PF);
                                                deviceCount = true;         //已經有設備了
                                            }
                                            break;
                                        case PhaseEnumType.SinglePhase:
                                            {
                                                HC6600Protocol electricProtocol = (HC6600Protocol)data[0];
                                                PhaseAngleEnumType = (PhaseAngleEnumType)electricitem.PhaseAngleEnumType;
                                                switch (PhaseAngleEnumType)
                                                {
                                                    case PhaseAngleEnumType.R:
                                                        {
                                                            RSV = Convert.ToSingle(electricProtocol.Rv);
                                                            RA = Convert.ToSingle(electricProtocol.RA);
                                                            KW = Convert.ToSingle(electricProtocol.kW_A);
                                                            KWH = Convert.ToSingle(electricProtocol.kWh_A);
                                                            PFE = Convert.ToSingle(electricProtocol.PF_A);
                                                            deviceCount = true;         //已經有設備了
                                                        }
                                                        break;
                                                    case PhaseAngleEnumType.S:
                                                        {
                                                            RSV = Convert.ToSingle(electricProtocol.Sv);
                                                            RA = Convert.ToSingle(electricProtocol.SA);
                                                            KW = Convert.ToSingle(electricProtocol.kW_B);
                                                            KWH = Convert.ToSingle(electricProtocol.kWh_B);
                                                            PFE = Convert.ToSingle(electricProtocol.PF_B);
                                                            deviceCount = true;         //已經有設備了
                                                        }
                                                        break;
                                                    case PhaseAngleEnumType.T:
                                                        {
                                                            RSV = Convert.ToSingle(electricProtocol.Tv);
                                                            RA = Convert.ToSingle(electricProtocol.TA);
                                                            KW = Convert.ToSingle(electricProtocol.kW_C);
                                                            KWH = Convert.ToSingle(electricProtocol.kWh_C);
                                                            PFE = Convert.ToSingle(electricProtocol.PF_C);
                                                            deviceCount = true;         //已經有設備了
                                                        }
                                                        break;
                                                }
                                            }
                                            break;
                                    }
                                }
                            }
                            break;
                        case ElectricEnumType.CPM6:
                            {
                                if (!deviceCount)
                                {
                                    var data = protocols.Where(g => g.GatewayIndex == electricitem.GatewayIndex & g.DeviceIndex == electricitem.DeviceIndex).ToList();
                                    PhaseEnumType = (PhaseEnumType)electricitem.PhaseEnumType;
                                    switch (PhaseEnumType)
                                    {
                                        case PhaseEnumType.ThreePhase:
                                            {
                                                CPM6Protocol electricProtocol = (CPM6Protocol)data[0];
                                                RSV = Convert.ToSingle(electricProtocol.RSv);
                                                STV = Convert.ToSingle(electricProtocol.STv);
                                                TRV = Convert.ToSingle(electricProtocol.TRv);
                                                RA = Convert.ToSingle(electricProtocol.RA);
                                                SA = Convert.ToSingle(electricProtocol.SA);
                                                TA = Convert.ToSingle(electricProtocol.TA);
                                                KW = Convert.ToSingle(electricProtocol.kW);
                                                KWH = Convert.ToSingle(electricProtocol.kWh);
                                                PFE = Convert.ToSingle(electricProtocol.PF);
                                                deviceCount = true;         //已經有設備了
                                            }
                                            break;
                                        case PhaseEnumType.SinglePhase:
                                            {
                                                CPM6Protocol electricProtocol = (CPM6Protocol)data[0];
                                                PhaseAngleEnumType = (PhaseAngleEnumType)electricitem.PhaseAngleEnumType;
                                                switch (PhaseAngleEnumType)
                                                {
                                                    case PhaseAngleEnumType.R:
                                                        {
                                                            RSV = Convert.ToSingle(electricProtocol.Rv);
                                                            RA = Convert.ToSingle(electricProtocol.RA);
                                                            KW = Convert.ToSingle(electricProtocol.kW_A);
                                                            KWH = Convert.ToSingle(electricProtocol.kWh_A);
                                                            PFE = Convert.ToSingle(electricProtocol.PF_A);
                                                            deviceCount = true;         //已經有設備了
                                                        }
                                                        break;
                                                    case PhaseAngleEnumType.S:
                                                        {
                                                            RSV = Convert.ToSingle(electricProtocol.Sv);
                                                            RA = Convert.ToSingle(electricProtocol.SA);
                                                            KW = Convert.ToSingle(electricProtocol.kW_B);
                                                            KWH = Convert.ToSingle(electricProtocol.kWh_B);
                                                            PFE = Convert.ToSingle(electricProtocol.PF_B);
                                                            deviceCount = true;         //已經有設備了
                                                        }
                                                        break;
                                                    case PhaseAngleEnumType.T:
                                                        {
                                                            RSV = Convert.ToSingle(electricProtocol.Tv);
                                                            RA = Convert.ToSingle(electricProtocol.TA);
                                                            KW = Convert.ToSingle(electricProtocol.kW_C);
                                                            KWH = Convert.ToSingle(electricProtocol.kWh_C);
                                                            PFE = Convert.ToSingle(electricProtocol.PF_C);
                                                            deviceCount = true;         //已經有設備了
                                                        }
                                                        break;
                                                }
                                            }
                                            break;
                                    }
                                }
                            }
                            break;
                        case ElectricEnumType.PA60:
                            {
                                if (!deviceCount)
                                {
                                    var data = protocols.Where(g => g.GatewayIndex == electricitem.GatewayIndex & g.DeviceIndex == electricitem.DeviceIndex).ToList();
                                    PhaseEnumType = (PhaseEnumType)electricitem.PhaseEnumType;
                                    switch (PhaseEnumType)
                                    {
                                        case PhaseEnumType.ThreePhase:
                                            {
                                                PA60Protocol electricProtocol = (PA60Protocol)data[0];
                                                RSV = Convert.ToSingle(electricProtocol.RSv[electricProtocol.LoopEnumType]);
                                                STV = Convert.ToSingle(electricProtocol.STv[electricProtocol.LoopEnumType]);
                                                TRV = Convert.ToSingle(electricProtocol.TRv[electricProtocol.LoopEnumType]);
                                                RA = Convert.ToSingle(electricProtocol.RA[electricProtocol.LoopEnumType]);
                                                SA = Convert.ToSingle(electricProtocol.SA[electricProtocol.LoopEnumType]);
                                                TA = Convert.ToSingle(electricProtocol.TA[electricProtocol.LoopEnumType]);
                                                KW = Convert.ToSingle(electricProtocol.kW[electricProtocol.LoopEnumType]);
                                                KWH = Convert.ToSingle(electricProtocol.kWh[electricProtocol.LoopEnumType]);
                                                PFE = Convert.ToSingle(electricProtocol.PF[electricProtocol.LoopEnumType]);
                                                deviceCount = true;         //已經有設備了
                                            }
                                            break;
                                        case PhaseEnumType.SinglePhase:
                                            {
                                                PA60Protocol electricProtocol = (PA60Protocol)data[0];
                                                PhaseAngleEnumType = (PhaseAngleEnumType)electricitem.PhaseAngleEnumType;
                                                switch (PhaseAngleEnumType)
                                                {
                                                    case PhaseAngleEnumType.R:
                                                        {
                                                            RSV = Convert.ToSingle(electricProtocol.Rv[electricProtocol.LoopEnumType]);
                                                            RA = Convert.ToSingle(electricProtocol.RA[electricProtocol.LoopEnumType]);
                                                            KW = Convert.ToSingle(electricProtocol.R_kW[electricProtocol.LoopEnumType]);
                                                            KWH = Convert.ToSingle(electricProtocol.R_kWh[electricProtocol.LoopEnumType]);
                                                            PFE = Convert.ToSingle(electricProtocol.R_PF[electricProtocol.LoopEnumType]);
                                                            deviceCount = true;         //已經有設備了
                                                        }
                                                        break;
                                                    case PhaseAngleEnumType.S:
                                                        {
                                                            RSV = Convert.ToSingle(electricProtocol.Sv[electricProtocol.LoopEnumType]);
                                                            RA = Convert.ToSingle(electricProtocol.SA[electricProtocol.LoopEnumType]);
                                                            KW = Convert.ToSingle(electricProtocol.S_kW[electricProtocol.LoopEnumType]);
                                                            KWH = Convert.ToSingle(electricProtocol.S_kWh[electricProtocol.LoopEnumType]);
                                                            PFE = Convert.ToSingle(electricProtocol.S_PF[electricProtocol.LoopEnumType]);
                                                            deviceCount = true;         //已經有設備了
                                                        }
                                                        break;
                                                    case PhaseAngleEnumType.T:
                                                        {
                                                            RSV = Convert.ToSingle(electricProtocol.Tv[electricProtocol.LoopEnumType]);
                                                            RA = Convert.ToSingle(electricProtocol.TA[electricProtocol.LoopEnumType]);
                                                            KW = Convert.ToSingle(electricProtocol.T_kW[electricProtocol.LoopEnumType]);
                                                            KWH = Convert.ToSingle(electricProtocol.T_kWh[electricProtocol.LoopEnumType]);
                                                            PFE = Convert.ToSingle(electricProtocol.T_PF[electricProtocol.LoopEnumType]);
                                                            deviceCount = true;         //已經有設備了
                                                        }
                                                        break;
                                                }
                                            }
                                            break;
                                    }
                                }
                            }
                            break;
                        case ElectricEnumType.ABBM2M:
                            {
                                if (!deviceCount)
                                {
                                    var data = protocols.Where(g => g.GatewayIndex == electricitem.GatewayIndex & g.DeviceIndex == electricitem.DeviceIndex).ToList();
                                    PhaseEnumType = (PhaseEnumType)electricitem.PhaseEnumType;
                                    switch (PhaseEnumType)
                                    {
                                        case PhaseEnumType.ThreePhase:
                                            {
                                                ABBM2MProtocol electricProtocol = (ABBM2MProtocol)data[0];
                                                RSV = Convert.ToSingle(electricProtocol.RSv);
                                                STV = Convert.ToSingle(electricProtocol.STv);
                                                TRV = Convert.ToSingle(electricProtocol.TRv);
                                                RA = Convert.ToSingle(electricProtocol.RA);
                                                SA = Convert.ToSingle(electricProtocol.SA);
                                                TA = Convert.ToSingle(electricProtocol.TA);
                                                KW = Convert.ToSingle(electricProtocol.kW);
                                                KWH = Convert.ToSingle(electricProtocol.kWh);
                                                PFE = Convert.ToSingle(electricProtocol.PF);
                                                deviceCount = true;         //已經有設備了
                                            }
                                            break;
                                        case PhaseEnumType.SinglePhase:
                                            {
                                                ABBM2MProtocol electricProtocol = (ABBM2MProtocol)data[0];
                                                PhaseAngleEnumType = (PhaseAngleEnumType)electricitem.PhaseAngleEnumType;
                                                switch (PhaseAngleEnumType)
                                                {
                                                    case PhaseAngleEnumType.R:
                                                        {
                                                            RSV = Convert.ToSingle(electricProtocol.Rv);
                                                            RA = Convert.ToSingle(electricProtocol.RA);
                                                            KW = Convert.ToSingle(electricProtocol.kW_A);
                                                            KWH = Convert.ToSingle(electricProtocol.kWh_A);
                                                            PFE = Convert.ToSingle(electricProtocol.PF_A);
                                                            deviceCount = true;         //已經有設備了
                                                        }
                                                        break;
                                                    case PhaseAngleEnumType.S:
                                                        {
                                                            RSV = Convert.ToSingle(electricProtocol.Sv);
                                                            RA = Convert.ToSingle(electricProtocol.SA);
                                                            KW = Convert.ToSingle(electricProtocol.kW_B);
                                                            KWH = Convert.ToSingle(electricProtocol.kWh_B);
                                                            PFE = Convert.ToSingle(electricProtocol.PF_B);
                                                            deviceCount = true;         //已經有設備了
                                                        }
                                                        break;
                                                    case PhaseAngleEnumType.T:
                                                        {
                                                            RSV = Convert.ToSingle(electricProtocol.Tv);
                                                            RA = Convert.ToSingle(electricProtocol.TA);
                                                            KW = Convert.ToSingle(electricProtocol.kW_C);
                                                            KWH = Convert.ToSingle(electricProtocol.kWh_C);
                                                            PFE = Convert.ToSingle(electricProtocol.PF_C);
                                                            deviceCount = true;         //已經有設備了
                                                        }
                                                        break;
                                                }
                                            }
                                            break;
                                    }
                                }
                            }
                            break;
                    }
                }
                upToIewatchClass.scan_adioco(Card, Board, AI, DI, DO, RSV, STV, TRV, RA, SA, TA, KW, KWH, PFE);
                Thread.Sleep(200);
            }
        }
    }
}
