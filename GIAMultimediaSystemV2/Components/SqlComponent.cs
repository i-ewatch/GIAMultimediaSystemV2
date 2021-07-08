using GIAMultimediaSystemV2.Configuration;
using GIAMultimediaSystemV2.Protocols.ElectricMeter;
using GIAMultimediaSystemV2.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GIAMultimediaSystemV2.Protocols.Senser;
using GIAMultimediaSystemV2.Protocols;
using Serilog;

namespace GIAMultimediaSystemV2.Components
{
    public partial class SqlComponent : Field4Component
    {
        public SqlComponent(List<AbsProtocol> absProtocols)
        {
            InitializeComponent();
            AbsProtocols = absProtocols;
        }

        public SqlComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        protected override void AfterMyWorkStateChanged(object sender, EventArgs e)
        {
            if (myWorkState)
            {
                ReadThread = new Thread(SqlRecord);
                ReadThread.Priority = ThreadPriority.Lowest;
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
        public void SqlRecord()
        {
            while (myWorkState)
            {
                TimeSpan timeSpan = DateTime.Now.Subtract(ReadTime);
                if (timeSpan.TotalSeconds >= 30)
                {
                    try
                    {
                        if (AbsProtocols.Count > 0)
                        {
                            foreach (var item in AbsProtocols)
                            {
                                if (item.ConnectFlag)
                                {
                                    if (item.ElectricEnumType != -1)
                                    {
                                        ElectricEnumType = (ElectricEnumType)item.ElectricEnumType;
                                        switch (ElectricEnumType)
                                        {
                                            case ElectricEnumType.PA310:
                                                {
                                                    PA310Protocol protocol = (PA310Protocol)item;
                                                    PhaseEnumType PhaseEnumType = (PhaseEnumType)item.PhaseEnumType;
                                                    switch (PhaseEnumType)
                                                    {
                                                        case Enums.PhaseEnumType.ThreePhase:
                                                            SqlMethod.Insert_ThreePhaseElectricMeter(protocol);
                                                            break;
                                                        case Enums.PhaseEnumType.SinglePhase:
                                                            SqlMethod.Insert_SinglePhaseElectricMeter(protocol);
                                                            break;
                                                    }
                                                }
                                                break;
                                            case ElectricEnumType.HC660:
                                                {
                                                    HC6600Protocol protocol = (HC6600Protocol)item;
                                                    PhaseEnumType PhaseEnumType = (PhaseEnumType)item.PhaseEnumType;
                                                    switch (PhaseEnumType)
                                                    {
                                                        case Enums.PhaseEnumType.ThreePhase:
                                                            SqlMethod.Insert_ThreePhaseElectricMeter(protocol);
                                                            break;
                                                        case Enums.PhaseEnumType.SinglePhase:
                                                            SqlMethod.Insert_SinglePhaseElectricMeter(protocol);
                                                            break;
                                                    }
                                                }
                                                break;
                                            case ElectricEnumType.CPM6:
                                                {
                                                    CPM6Protocol protocol = (CPM6Protocol)item;
                                                    PhaseEnumType PhaseEnumType = (PhaseEnumType)item.PhaseEnumType;
                                                    switch (PhaseEnumType)
                                                    {
                                                        case Enums.PhaseEnumType.ThreePhase:
                                                            SqlMethod.Insert_ThreePhaseElectricMeter(protocol);
                                                            break;
                                                        case Enums.PhaseEnumType.SinglePhase:
                                                            SqlMethod.Insert_SinglePhaseElectricMeter(protocol);
                                                            break;
                                                    }
                                                }
                                                break;
                                            case ElectricEnumType.PA60:
                                                {
                                                    PA60Protocol protocol = (PA60Protocol)item;
                                                    PhaseEnumType PhaseEnumType = (PhaseEnumType)item.PhaseEnumType;
                                                    switch (PhaseEnumType)
                                                    {
                                                        case Enums.PhaseEnumType.ThreePhase:
                                                            SqlMethod.Insert_ThreePhaseElectricMeter(protocol);
                                                            break;
                                                        case Enums.PhaseEnumType.SinglePhase:
                                                            SqlMethod.Insert_SinglePhaseElectricMeter(protocol);
                                                            break;
                                                    }
                                                }
                                                break;
                                            case ElectricEnumType.ABBM2M:
                                                {
                                                    ABBM2MProtocol protocol = (ABBM2MProtocol)item;
                                                    PhaseEnumType PhaseEnumType = (PhaseEnumType)item.PhaseEnumType;
                                                    switch (PhaseEnumType)
                                                    {
                                                        case Enums.PhaseEnumType.ThreePhase:
                                                            SqlMethod.Insert_ThreePhaseElectricMeter(protocol);
                                                            break;
                                                        case Enums.PhaseEnumType.SinglePhase:
                                                            SqlMethod.Insert_SinglePhaseElectricMeter(protocol);
                                                            break;
                                                    }
                                                }
                                                break;
                                            case ElectricEnumType.PM200:
                                                {
                                                    PM200Protocol protocol = (PM200Protocol)item;
                                                    PhaseEnumType PhaseEnumType = (PhaseEnumType)item.PhaseEnumType;
                                                    switch (PhaseEnumType)
                                                    {
                                                        case Enums.PhaseEnumType.ThreePhase:
                                                            SqlMethod.Insert_ThreePhaseElectricMeter(protocol);
                                                            break;
                                                        case Enums.PhaseEnumType.SinglePhase:
                                                            SqlMethod.Insert_SinglePhaseElectricMeter(protocol);
                                                            break;
                                                    }
                                                }
                                                break;
                                            case ElectricEnumType.TWCPM4:
                                                {
                                                    TWCPM4Protocol protocol = (TWCPM4Protocol)item;
                                                    PhaseEnumType PhaseEnumType = (PhaseEnumType)item.PhaseEnumType;
                                                    switch (PhaseEnumType)
                                                    {
                                                        case Enums.PhaseEnumType.ThreePhase:
                                                            SqlMethod.Insert_ThreePhaseElectricMeter(protocol);
                                                            break;
                                                        case Enums.PhaseEnumType.SinglePhase:
                                                            SqlMethod.Insert_SinglePhaseElectricMeter(protocol);
                                                            break;
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                    else if (item.SenserEnumType != -1)
                                    {
                                        SenserEnumType = (SenserEnumType)item.SenserEnumType;
                                        switch (SenserEnumType)
                                        {
                                            case SenserEnumType.BlackSenser:
                                            case SenserEnumType.WhiteSenser:
                                                {
                                                    {
                                                        SenserData data = (SenserData)item;
                                                        SqlMethod.Insert_Senser(data);
                                                    }
                                                }
                                                break;
                                            case SenserEnumType.WeatherAPI:
                                                break;
                                        }
                                    }
                                }
                            }
                            Thread.Sleep(10);
                            ReadTime = DateTime.Now;
                        }
                    }
                    catch (ThreadAbortException) { }
                    catch (Exception ex) { Log.Error(ex, "資料庫紀錄失敗"); }
                }
                else
                {
                    Thread.Sleep(80);
                }
            }
        }
    }
}
