namespace ssh_vpn
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.txt_ip = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_username = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.txt_port = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_proxyport = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Txt_List = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Btn_saveList = new System.Windows.Forms.Button();
            this.txt_domain = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_addDomain = new System.Windows.Forms.Button();
            this.btn_removeDomain = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txt_port)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_proxyport)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_ip
            // 
            this.txt_ip.Location = new System.Drawing.Point(99, 26);
            this.txt_ip.Margin = new System.Windows.Forms.Padding(4);
            this.txt_ip.Name = "txt_ip";
            this.txt_ip.Size = new System.Drawing.Size(248, 22);
            this.txt_ip.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server IP : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 95);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Username : ";
            // 
            // txt_username
            // 
            this.txt_username.Location = new System.Drawing.Point(99, 91);
            this.txt_username.Margin = new System.Windows.Forms.Padding(4);
            this.txt_username.Name = "txt_username";
            this.txt_username.Size = new System.Drawing.Size(248, 22);
            this.txt_username.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 127);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Password : ";
            // 
            // txt_password
            // 
            this.txt_password.Location = new System.Drawing.Point(98, 124);
            this.txt_password.Margin = new System.Windows.Forms.Padding(4);
            this.txt_password.Name = "txt_password";
            this.txt_password.Size = new System.Drawing.Size(248, 22);
            this.txt_password.TabIndex = 4;
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(42, 314);
            this.btn_save.Margin = new System.Windows.Forms.Padding(4);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(347, 48);
            this.btn_save.TabIndex = 6;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // txt_port
            // 
            this.txt_port.Location = new System.Drawing.Point(99, 58);
            this.txt_port.Margin = new System.Windows.Forms.Padding(4);
            this.txt_port.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txt_port.Name = "txt_port";
            this.txt_port.Size = new System.Drawing.Size(249, 22);
            this.txt_port.TabIndex = 9;
            this.txt_port.Value = new decimal(new int[] {
            22,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 63);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Server port :";
            // 
            // txt_proxyport
            // 
            this.txt_proxyport.Location = new System.Drawing.Point(98, 41);
            this.txt_proxyport.Margin = new System.Windows.Forms.Padding(4);
            this.txt_proxyport.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txt_proxyport.Name = "txt_proxyport";
            this.txt_proxyport.Size = new System.Drawing.Size(249, 22);
            this.txt_proxyport.TabIndex = 11;
            this.txt_proxyport.Value = new decimal(new int[] {
            9000,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 43);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Proxy port :";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Turquoise;
            this.groupBox1.Controls.Add(this.txt_proxyport);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(19, 187);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(401, 105);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Proxy Settings";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.LightGray;
            this.groupBox2.Controls.Add(this.txt_port);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txt_password);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txt_username);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txt_ip);
            this.groupBox2.Location = new System.Drawing.Point(19, 21);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(401, 160);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Connection Settings";
            // 
            // Txt_List
            // 
            this.Txt_List.Location = new System.Drawing.Point(6, 103);
            this.Txt_List.Name = "Txt_List";
            this.Txt_List.Size = new System.Drawing.Size(333, 184);
            this.Txt_List.TabIndex = 15;
            this.Txt_List.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_removeDomain);
            this.groupBox3.Controls.Add(this.btn_addDomain);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txt_domain);
            this.groupBox3.Controls.Add(this.Btn_saveList);
            this.groupBox3.Controls.Add(this.Txt_List);
            this.groupBox3.Location = new System.Drawing.Point(457, 13);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(405, 348);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "WhiteList";
            // 
            // Btn_saveList
            // 
            this.Btn_saveList.Location = new System.Drawing.Point(192, 297);
            this.Btn_saveList.Name = "Btn_saveList";
            this.Btn_saveList.Size = new System.Drawing.Size(111, 41);
            this.Btn_saveList.TabIndex = 16;
            this.Btn_saveList.Text = "Save List";
            this.Btn_saveList.UseVisualStyleBackColor = true;
            this.Btn_saveList.Click += new System.EventHandler(this.Btn_saveList_Click);
            // 
            // txt_domain
            // 
            this.txt_domain.Location = new System.Drawing.Point(102, 39);
            this.txt_domain.Name = "txt_domain";
            this.txt_domain.Size = new System.Drawing.Size(237, 22);
            this.txt_domain.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 42);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 21);
            this.label6.TabIndex = 18;
            this.label6.Text = "Domain";
            // 
            // btn_addDomain
            // 
            this.btn_addDomain.Location = new System.Drawing.Point(30, 71);
            this.btn_addDomain.Name = "btn_addDomain";
            this.btn_addDomain.Size = new System.Drawing.Size(133, 23);
            this.btn_addDomain.TabIndex = 19;
            this.btn_addDomain.Text = "Add Domain";
            this.btn_addDomain.UseVisualStyleBackColor = true;
            this.btn_addDomain.Click += new System.EventHandler(this.btn_addDomain_Click);
            // 
            // btn_removeDomain
            // 
            this.btn_removeDomain.Location = new System.Drawing.Point(192, 71);
            this.btn_removeDomain.Name = "btn_removeDomain";
            this.btn_removeDomain.Size = new System.Drawing.Size(132, 23);
            this.btn_removeDomain.TabIndex = 20;
            this.btn_removeDomain.Text = "Remove Domain";
            this.btn_removeDomain.UseVisualStyleBackColor = true;
            this.btn_removeDomain.Click += new System.EventHandler(this.btn_removeDomain_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(874, 397);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_save);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txt_port)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_proxyport)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txt_ip;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_username;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.NumericUpDown txt_port;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown txt_proxyport;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox Txt_List;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button Btn_saveList;
        private System.Windows.Forms.Button btn_removeDomain;
        private System.Windows.Forms.Button btn_addDomain;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_domain;
    }
}