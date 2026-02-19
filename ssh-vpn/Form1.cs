using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using Newtonsoft.Json;
using Renci.SshNet;
using Renci.SshNet.Common;

namespace ssh_vpn
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SystemEvents.SessionEnding += new SessionEndingEventHandler(SystemEvents_SessionEnding);
        }

        SshClient sshClient = new SshClient("0.0.0.0", 22, "0000", "0000");
        string ProxyPort = "9000";
        ForwardedPortDynamic portForwarded;
        readonly string PacFileName = "proxy.pac";
        readonly string WhiteListFileName = "whitelist.json";

        string PacFilePath => System.IO.Path.Combine(Application.StartupPath, PacFileName);
        string WhiteListFilePath => System.IO.Path.Combine(Application.StartupPath, WhiteListFileName);


        void Connect()
        {
            btnToggle.Text = "Connecting...";
            btn_reconnect.Visible = true;
            string password = registery_get_data("password");
            string username = registery_get_data("username");
            string ip = registery_get_data("ip");
            int port;
            if (!int.TryParse(registery_get_data("port"), out port)) port = 22;
            int proxyport;
            if (!int.TryParse(registery_get_data("proxyport"), out proxyport)) proxyport = 9000;
            ProxyPort = proxyport.ToString();
            //BuildPacFile(ProxyPort);
            portForwarded = new ForwardedPortDynamic(Convert.ToUInt16(ProxyPort));


            if (password == "" || password == "" || username == "" || ip == "")
            {
                MessageBox.Show("Error : You should set SSH server settings...", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Invoke((MethodInvoker)delegate
                {
                    btnOpenSettings_Click(null, null);
                    btnToggle.Text = "Connect";
                });
                return;
            }


            ThreadPool.QueueUserWorkItem(new WaitCallback((state) =>
            {
                sshClient = new SshClient(ip, port, username, password);

                try
                {
                    sshClient.Connect();
                    sshClient.AddForwardedPort(portForwarded);
                    portForwarded.Exception += delegate (object sender, ExceptionEventArgs e)
                    {
                        Console.WriteLine(e.Exception.ToString());
                    };
                    portForwarded.RequestReceived += PortForwarded_RequestReceived;
                    portForwarded.Start();

                    set_windows_proxy();

                    Invoke((MethodInvoker)delegate
                    {
                        lblStatus.BackColor = Color.Green;
                        lblStatus.Text = "Connected      00:00:00";
                        btnToggle.Text = "Disconnect";

                        timer_check_status.Enabled = true;
                        timer_check_status.Start();
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error : " + ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Invoke((MethodInvoker)delegate { btnToggle.Text = "Connect"; });
                }
                finally
                {
                    Invoke((MethodInvoker)delegate { btnToggle.Enabled = true; });
                }
            }));
        }

        private void PortForwarded_RequestReceived(object sender, PortForwardEventArgs e)
        {
            Console.WriteLine(e.OriginatorHost.ToString() + ":" +e.OriginatorPort.ToString());

        }

        void Disconnect()
        {
            btnToggle.Text = "Disconnecting...";
            btn_reconnect.Visible = false;
            portForwarded.Stop();
            sshClient.Disconnect();
            unset_windows_proxy();

            btnToggle.Text = "Connect";
            lblStatus.BackColor = Color.Red;
            lblStatus.Text = "Not Connected";

            timer_check_status.Enabled = false;
            timer_check_status.Stop();
            seconds = 0;

            btnToggle.Enabled = true;
        }


        private void btnToggle_Click(object sender, EventArgs e)
        {
            btnToggle.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;

            if (sshClient.IsConnected)
                Disconnect();

            else Connect();

            Cursor.Current = Cursors.Default;
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!sshClient.IsConnected) return;

            DialogResult result = MessageBox.Show("Do you really wish to exit? the connection will be stopped.", "Exit Program?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
                e.Cancel = true;
            else
                Disconnect();

        }
        private void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
        {
            if (sshClient.IsConnected)
                btnToggle_Click(null, null);

            Application.Exit();
        }

        private void btnOpenSettings_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
        }

        bool back_status = false;
        int seconds = 0;
        private void timer_check_status_Tick(object sender, EventArgs e)
        {
            if (!sshClient.IsConnected && sshClient.IsConnected != back_status)
            {
                Disconnect();

                notifyIcon1.Icon = SystemIcons.Warning;
                notifyIcon1.ShowBalloonTip(10);
            }
            else if (sshClient.IsConnected && sshClient.IsConnected != back_status)
            {
                seconds = 0;
                lblStatus.Text = "Connected      00:00:00";
            }
            else if (sshClient.IsConnected)
            {
                seconds++;

                int hours = seconds / 3600;
                int minutes = (seconds % 3600) / 60;
                int remainingSeconds = seconds % 60;
                lblStatus.Text = "Connected      " + hours.ToString("D2") + ":" + minutes.ToString("D2") + ":" + remainingSeconds.ToString("D2");
            }

            back_status = sshClient.IsConnected;
        }
        private void set_windows_proxy()
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey(
                "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);

            registry.SetValue("ProxyEnable", 1);
            registry.SetValue("ProxyServer", "socks5://127.0.0.1:" + ProxyPort);

            // وایت‌لیست را از JSON بخوان و به Exceptions اضافه کن
            AddToProxyExceptions(LoadWhiteList());

            WinINetInterop.InternetSetOption(IntPtr.Zero, WinINetInterop.INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
            WinINetInterop.InternetSetOption(IntPtr.Zero, WinINetInterop.INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
        }


        private void set_windows_proxy_pac()
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey(
                "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);

            // فعال‌سازی PAC
            string pacUrl = "file:///" + PacFilePath.Replace("\\", "/");

            registry.SetValue("AutoConfigURL", pacUrl);

            // فعال‌سازی پروکسی برای fallback
            registry.SetValue("ProxyEnable", 1);
            registry.SetValue("ProxyServer", "socks=127.0.0.1:" + ProxyPort);

            WinINetInterop.InternetSetOption(IntPtr.Zero, WinINetInterop.INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
            WinINetInterop.InternetSetOption(IntPtr.Zero, WinINetInterop.INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
        }


        private void set_windows_proxy_old()
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
            registry.SetValue("ProxyEnable", 1);
            registry.SetValue("ProxyServer", "socks5://127.0.0.1:" + ProxyPort);

            WinINetInterop.InternetSetOption(IntPtr.Zero, WinINetInterop.INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
            WinINetInterop.InternetSetOption(IntPtr.Zero, WinINetInterop.INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
        }


        private void unset_windows_proxy_pac()
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey(
                "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);

            registry.SetValue("AutoConfigURL", "");
            registry.SetValue("ProxyEnable", 0);
            registry.SetValue("ProxyServer", "");

            WinINetInterop.InternetSetOption(IntPtr.Zero, WinINetInterop.INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
            WinINetInterop.InternetSetOption(IntPtr.Zero, WinINetInterop.INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
        }




        private void unset_windows_proxy_old()
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
            registry.SetValue("ProxyEnable", 0);
            registry.SetValue("ProxyServer", "");
        }

        private string registery_get_data(string name)
        {
            string keyName = "ssh_vpn";
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyName))
            {
                if (key == null)
                    return "";
                else
                    return key.GetValue(name) as string;
            }
        }


        private void unset_windows_proxy()
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey(
                "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);

            registry.SetValue("ProxyEnable", 0);
            registry.SetValue("ProxyServer", "");

            ClearProxyExceptions();

            WinINetInterop.InternetSetOption(IntPtr.Zero, WinINetInterop.INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
            WinINetInterop.InternetSetOption(IntPtr.Zero, WinINetInterop.INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
        }



        private void btnGh_Click(object sender, EventArgs e)
        {
            //Process.Start("https://github.com/omidmousavi/csharp-ssh-vpn");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(WhiteListFilePath))
            {
                var defaultJson = "{\n  \"domains\": [\n    \"google.com\",\n    \"*.google.com\"\n  ]\n}";
                System.IO.File.WriteAllText(WhiteListFilePath, defaultJson);
            }

        }
        private void BuildPacFile(string socksPort)
        {
            try
            {
                string json = System.IO.File.ReadAllText(WhiteListFilePath);
                dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                var domains = data.domains;

                string conditions = "";

                foreach (var d in domains)
                {
                    string domain = d.ToString().Trim();

                    // IP Range مثل 192.168.1.*
                    if (domain.Contains("*") && System.Net.IPAddress.TryParse(domain.Replace("*", "0"), out _))
                    {
                        string baseIp = domain.Replace("*", "0");
                        string mask = "255.255.255.0";

                        conditions += $"    if (isInNet(cleanHost, '{baseIp}', '{mask}')) return 'DIRECT';\n";
                    }
                    else
                    {
                        conditions += $"    if (dnsDomainIs(cleanHost, '{domain}') || shExpMatch(cleanHost, '{domain}')) return 'DIRECT';\n";
                    }
                }

                string pac = $@"
function FindProxyForURL(url, host) {{

    // پاک‌سازی host
    var cleanHost = host.toLowerCase();

    // حذف پورت
    var portIndex = cleanHost.indexOf(':');
    if (portIndex > -1) cleanHost = cleanHost.substring(0, portIndex);

    // حذف نقطه آخر
    if (cleanHost.endsWith('.')) cleanHost = cleanHost.slice(0, -1);

{conditions}

    return 'PROXY 127.0.0.1:{socksPort}';
}}
";

                System.IO.File.WriteAllText(PacFilePath, pac);
            }
            catch (Exception ex)
            {
                MessageBox.Show("PAC Build Error: " + ex.Message);
            }
        }


        private void AddToProxyExceptions(List<string> domains)
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey(
                "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);

            string current = registry.GetValue("ProxyOverride", "")?.ToString() ?? "";

            List<string> list = current.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                                       .Select(x => x.Trim())
                                       .ToList();

            foreach (var d in domains)
            {
                if (!list.Contains(d))
                    list.Add(d);
            }

            string result = string.Join(";", list);

            registry.SetValue("ProxyOverride", result);

            WinINetInterop.InternetSetOption(IntPtr.Zero, WinINetInterop.INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
            WinINetInterop.InternetSetOption(IntPtr.Zero, WinINetInterop.INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
        }
        private bool ValidateWhitelistJson(string json)
        {
            try
            {
                dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                if (data.domains == null)
                    return false;

                foreach (var d in data.domains)
                {
                    if (string.IsNullOrWhiteSpace(d.ToString()))
                        return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }


        private List<string> LoadWhiteList()
        {
            try
            {
                string json = System.IO.File.ReadAllText(WhiteListFilePath);

                if (!ValidateWhitelistJson(json))
                    return new List<string>();

                dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                List<string> list = new List<string>();

                foreach (var d in data.domains)
                    list.Add(d.ToString().Trim());

                return list;
            }
            catch
            {
                return new List<string>();
            }
        }


        private void ClearProxyExceptions()
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey(
                "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);

            registry.SetValue("ProxyOverride", "");

            WinINetInterop.InternetSetOption(IntPtr.Zero, WinINetInterop.INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
            WinINetInterop.InternetSetOption(IntPtr.Zero, WinINetInterop.INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
        }

        private void btn_reconnect_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (sshClient.IsConnected)
            {
                Disconnect();
                Thread.Sleep(3000);
                Connect();
            }
            Cursor.Current = Cursors.Default;
        }
    }

    public static class WinINetInterop
    {
        public const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
        public const int INTERNET_OPTION_REFRESH = 37;

        [System.Runtime.InteropServices.DllImport("wininet.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int lpdwBufferLength);
    }
}
