using DevExpress.XtraEditors;
using GIAMultimediaSystemV2.Configuration;
using GIAMultimediaSystemV2.Enums;
using GIAMultimediaSystemV2.Methods;
using GIAMultimediaSystemV2.Protocols.Senser;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NModbus;
using NModbus.Serial;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GIAMultimediaSystemV2.Views.Setting
{
    public partial class ProtocolSettingUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// 設定按鈕視窗
        /// </summary>
        private SettingButtonUserControl SettingButtonUserControl { get; set; }
        private SerialPort SerialPort = new SerialPort();
        private GateWaySetting GateWaySetting;
        private IModbusMaster master;
        private ModbusFactory Factory = new ModbusFactory();
        private GateWaySenserID SenserData;
        private List<Taiwan_DistricsSetting> Taiwan_DistricsSetting = new List<Taiwan_DistricsSetting>();
        public ProtocolSettingUserControl(SettingButtonUserControl settingButtonUserControl, GateWaySetting gateWaySetting, List<Taiwan_DistricsSetting> taiwan_DistricsSetting)
        {
            InitializeComponent();
            SettingButtonUserControl = settingButtonUserControl;
            GateWaySetting = gateWaySetting;
            Taiwan_DistricsSetting = taiwan_DistricsSetting;
            SenserData = GateWaySetting.GateWays[0].GateWaySenserIDs.SingleOrDefault(g => g.SenserEnumType == 4 || g.SenserEnumType == 3);
            if (SenserData != null)
            {
                switch ((SenserEnumType)SenserData.SenserEnumType)
                {
                    case SenserEnumType.GIAAPI:
                        {
                            Protocol_TypecomboBoxEdit.SelectedIndex = 2;
                            ProtocolTabControl.SelectedTabPageIndex = 2;
                        }
                        break;
                    case SenserEnumType.GIA:
                        {
                            Protocol_TypecomboBoxEdit.SelectedIndex = GateWaySetting.GateWays[0].GatewayEnumType;
                            ProtocolTabControl.SelectedTabPageIndex = GateWaySetting.GateWays[0].GatewayEnumType;
                        }
                        break;
                }
            }
            RS485_COMcomboBoxEdit.Text = GateWaySetting.GateWays[0].ModbusRTULocation;
            RS485_IDtextEdit.Text = SenserData.DeviceID.ToString();
            TCP_IPtextEdit.Text = GateWaySetting.GateWays[0].ModbusTCPLocation;
            TCP_IDtextEdit.Text = SenserData.DeviceID.ToString();
            URLtextEdit.Text = GateWaySetting.GateWays[0].GIAAPILocation;
            WeatherItem(WeathercomboBoxEdit);
            WeathercomboBoxEdit.Text = GateWaySetting.GateWays[0].LocationName;
            var DistrictsLoad = Taiwan_DistricsSetting.Where(g => g.CityName == WeathercomboBoxEdit.Text).Select(v => v.AreaList).Single();
            foreach (var item in DistrictsLoad)
            {
                DistrictscomboBoxEdit.Properties.Items.Add(item.AreaName);
            }
            DistrictscomboBoxEdit.Text = null;
            DistrictscomboBoxEdit.Text = GateWaySetting.GateWays[0].DistrictName;

            RS485_COMcomboBoxEdit.ButtonClick += (s, e) =>
            {
                if (RS485_COMcomboBoxEdit.Properties.Items.Count > 0)
                {
                    RS485_COMcomboBoxEdit.Properties.Items.Clear();
                }
                string[] myPorts = SerialPort.GetPortNames(); //取得所有port的名字的方法
                foreach (var Portitem in myPorts)
                {
                    RS485_COMcomboBoxEdit.Properties.Items.Add(Portitem);
                }
                RS485_COMcomboBoxEdit.ShowPopup();
            };
            WeathercomboBoxEdit.SelectedIndexChanged += (s, e) =>
             {
                 if (DistrictscomboBoxEdit.Properties.Items.Count > 0)
                 {
                     DistrictscomboBoxEdit.Properties.Items.Clear();
                 }
                 var Districts = Taiwan_DistricsSetting.Where(g => g.CityName == WeathercomboBoxEdit.Text).Select(v => v.AreaList).Single();
                 foreach (var item in Districts)
                 {
                     DistrictscomboBoxEdit.Properties.Items.Add(item.AreaName);
                 }
                 DistrictscomboBoxEdit.Text = null;
             };
            Protocol_TypecomboBoxEdit.SelectedIndexChanged += (s, e) =>
             {
                 ProtocolTabControl.SelectedTabPageIndex = Protocol_TypecomboBoxEdit.SelectedIndex;
             };

            OKsimpleButton.Click += (s, e) =>
            {
                switch (Protocol_TypecomboBoxEdit.SelectedIndex)
                {
                    case 0:
                        {
                            GateWaySetting.GateWays[0].GatewayEnumType = 0;
                            GateWaySetting.GateWays[0].ModbusRTULocation = RS485_COMcomboBoxEdit.Text;
                            GateWaySetting.GateWays[0].ModbusRTURate =9600;
                            SenserData.DeviceID = Convert.ToByte(RS485_IDtextEdit.Text);
                            SenserData.SenserEnumType = 4;
                        }
                        break;
                    case 1:
                        {
                            GateWaySetting.GateWays[0].GatewayEnumType = 1;
                            GateWaySetting.GateWays[0].ModbusTCPLocation = TCP_IPtextEdit.Text;
                            GateWaySetting.GateWays[0].ModbusTCPRate = 502;
                            SenserData.DeviceID=Convert.ToByte(TCP_IDtextEdit.Text);
                            SenserData.SenserEnumType = 4;
                        }
                        break;
                    case 2:
                        {
                            GateWaySetting.GateWays[0].GatewayEnumType = 1;
                            GateWaySetting.GateWays[0].GIAAPILocation = URLtextEdit.Text;
                            SenserData.SenserEnumType = 3;
                        }
                        break;
                }
                GateWaySetting.GateWays[0].LocationName = WeathercomboBoxEdit.Text;
                GateWaySetting.GateWays[0].WeatherTypeEnum = WeathercomboBoxEdit.SelectedIndex;
                GateWaySetting.GateWays[0].DistrictName = DistrictscomboBoxEdit.Text;
                InitialMethod.Save_GateWay(GateWaySetting);
                SettingButtonUserControl.FlyoutFlag = false;
                SettingButtonUserControl.flyout.Close();
                SettingButtonUserControl.Restart();
            };
            CancelsimpleButton.Click += (s, e) =>
              {
                  SettingButtonUserControl.FlyoutFlag = false;
                  SettingButtonUserControl.flyout.Close();
              };
        }
        /// <summary>
        /// 天氣項目
        /// </summary>
        /// <param name="comboBox"></param>
        private void WeatherItem(ComboBoxEdit comboBox)
        {
            comboBox.Properties.Items.Add("基隆市");
            comboBox.Properties.Items.Add("臺北市");
            comboBox.Properties.Items.Add("新北市");
            comboBox.Properties.Items.Add("桃園市");
            comboBox.Properties.Items.Add("新竹市");
            comboBox.Properties.Items.Add("新竹縣");
            comboBox.Properties.Items.Add("苗栗縣");
            comboBox.Properties.Items.Add("臺中市");
            comboBox.Properties.Items.Add("彰化縣");
            comboBox.Properties.Items.Add("南投縣");
            comboBox.Properties.Items.Add("雲林縣");
            comboBox.Properties.Items.Add("嘉義市");
            comboBox.Properties.Items.Add("嘉義縣");
            comboBox.Properties.Items.Add("臺南市");
            comboBox.Properties.Items.Add("高雄市");
            comboBox.Properties.Items.Add("屏東縣");
            comboBox.Properties.Items.Add("臺東縣");
            comboBox.Properties.Items.Add("花蓮縣");
            comboBox.Properties.Items.Add("宜蘭縣");
            comboBox.Properties.Items.Add("澎湖縣");
            comboBox.Properties.Items.Add("金門縣");
            comboBox.Properties.Items.Add("連江縣");
        }

        private void ProtocolsimpleButton_Click(object sender, EventArgs e)
        {
            stateIndicatorComponent1.StateIndex = 0;
            ProtocollabelControl.Text = "-";
            switch (Protocol_TypecomboBoxEdit.SelectedIndex)
            {
                case 0:
                    {
                        try
                        {
                            #region Rs485通訊功能初始化
                            try
                            {
                                if (SerialPort == null)
                                {
                                    SerialPort = new SerialPort();
                                }
                                if (!SerialPort.IsOpen)
                                {
                                    SerialPort.PortName = RS485_COMcomboBoxEdit.Text;
                                    SerialPort.BaudRate = 9600;
                                    SerialPort.DataBits = 8;
                                    SerialPort.StopBits = StopBits.One;
                                    SerialPort.Parity = Parity.None;
                                    SerialPort.Open();
                                }
                            }
                            catch (ArgumentException)
                            {
                                Log.Error("通訊埠設定有誤");
                                stateIndicatorComponent1.StateIndex = 1;
                                ProtocollabelControl.Text = "通訊失敗";
                            }
                            catch (InvalidOperationException)
                            {
                                Log.Error("通訊埠被占用");
                                stateIndicatorComponent1.StateIndex = 1;
                                ProtocollabelControl.Text = "通訊失敗";
                            }
                            catch (IOException)
                            {
                                Log.Error("通訊埠無效");
                                stateIndicatorComponent1.StateIndex = 1;
                                ProtocollabelControl.Text = "通訊失敗";
                            }
                            catch (Exception ex)
                            {
                                Log.Error(ex, "通訊埠發生不可預期的錯誤。");
                                stateIndicatorComponent1.StateIndex = 1;
                                ProtocollabelControl.Text = "通訊失敗";
                            }
                            master = ModbusFactoryExtensions.CreateRtuMaster(Factory, SerialPort);//建立RTU通訊
                            master.Transport.Retries = 3;
                            master.Transport.ReadTimeout = 500;
                            master.Transport.WriteTimeout = 500;
                            ushort[] value = master.ReadInputRegisters(Convert.ToByte(RS485_IDtextEdit.Text), 0, 17);
                            stateIndicatorComponent1.StateIndex = 3;
                            ProtocollabelControl.Text = "通訊成功";
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            stateIndicatorComponent1.StateIndex = 1;
                            ProtocollabelControl.Text = "通訊失敗";
                        }
                        SerialPort.Close();
                    }
                    break;
                case 1:
                    {
                        try
                        {
                            using (TcpClient client = new TcpClient(TCP_IPtextEdit.Text, 502))
                            {
                                master = Factory.CreateMaster(client);//建立TCP通訊
                                master.Transport.Retries = 3;
                                master.Transport.ReadTimeout = 500;
                                master.Transport.WriteTimeout = 500;
                                ushort[] value = master.ReadInputRegisters(Convert.ToByte(TCP_IDtextEdit.Text), 0, 17);
                                stateIndicatorComponent1.StateIndex = 3;
                                ProtocollabelControl.Text = "通訊成功";
                            }
                        }
                        catch (Exception ex)
                        {
                            stateIndicatorComponent1.StateIndex = 1;
                            ProtocollabelControl.Text = "通訊失敗";
                        }
                    }
                    break;
                case 2:
                    {
                        var client = new RestClient($"{URLtextEdit.Text.Trim()}");
                        client.Timeout = -1;
                        var request = new RestRequest(Method.GET);
                        IRestResponse response = client.Execute(request);
                        if (response != null)
                        {
                            JObject jsondata = JsonConvert.DeserializeObject<JObject>(response.Content);
                            if (jsondata != null)
                            {
                                JArray jsonArraydata = JsonConvert.DeserializeObject<JArray>(jsondata["data"].ToString());
                                GIAAPIValue Value = JsonConvert.DeserializeObject<GIAAPIValue>(jsonArraydata[0]["sensors"].ToString());
                                stateIndicatorComponent1.StateIndex = 3;
                                ProtocollabelControl.Text = "通訊成功";
                            }
                            else
                            {
                                stateIndicatorComponent1.StateIndex = 1;
                                ProtocollabelControl.Text = "通訊失敗";
                            }
                        }
                    }
                    break;
            }
        }
    }
}
