using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ssh_vpn
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            string keyName = "ssh_vpn";
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(keyName))
            {
                key.SetValue("ip", txt_ip.Text);
                key.SetValue("port", txt_port.Value);
                key.SetValue("username", txt_username.Text);
                key.SetValue("password", txt_password.Text);
                key.SetValue("proxyport", txt_proxyport.Text);

                MessageBox.Show("Successfully saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        string WhiteListFilePath => System.IO.Path.Combine(Application.StartupPath, "whitelist.json");

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            string json = System.IO.File.ReadAllText(WhiteListFilePath); 
            Txt_List.Text = json;

            string keyName = "ssh_vpn";
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyName))
            {
                if (key == null) return;
                int port = 22;
                int proxyport = 9000;

                txt_ip.Text = key.GetValue("ip") as string;



                if (int.TryParse(key.GetValue("port") as string, out port))
                    txt_port.Value = port;
               

                txt_username.Text = key.GetValue("username") as string;
                txt_password.Text = key.GetValue("password") as string;

                if (int.TryParse(key.GetValue("proxyport") as string, out proxyport))
                    txt_proxyport.Value = proxyport;
            }
        }

        private void SaveWhiteList(System.Collections.Generic.List<string> domains)
        {
            var jsonObj = new
            {
                domains = domains
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            Txt_List.Text = json;
        }

        public bool AddDomain(string domain)
        {
            var list = LoadWhiteList();

            if (list.Contains(domain))
                return false;

            list.Add(domain);
            SaveWhiteList(list);
            return true;
        }
        public bool RemoveDomain(string domain)
        {
            var list = LoadWhiteList();

            if (!list.Contains(domain))
                return false;

            list.Remove(domain);
            SaveWhiteList(list);
            return true;
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
                string json = Txt_List.Text;

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




        private void Btn_saveList_Click(object sender, EventArgs e)
        {
            System.IO.File.WriteAllText(WhiteListFilePath, Txt_List.Text);
            MessageBox.Show("Whitelist saved.");
        }

        private void btn_addDomain_Click(object sender, EventArgs e)
        {
            AddDomain(txt_domain.Text);
        }

        private void btn_removeDomain_Click(object sender, EventArgs e)
        {
            RemoveDomain(txt_domain.Text);
        }

        private void btn_validate_Click(object sender, EventArgs e)
        {
        }
    }
}
