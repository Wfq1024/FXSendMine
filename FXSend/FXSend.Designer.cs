
namespace FXPro
{
    partial class FXSend
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FXSend));
            this.DataBaseNameLabel = new System.Windows.Forms.Label();
            this.IPLabel = new System.Windows.Forms.Label();
            this.MachineIDLabel = new System.Windows.Forms.Label();
            this.DataBaseName = new System.Windows.Forms.TextBox();
            this.IP = new System.Windows.Forms.TextBox();
            this.UserName = new System.Windows.Forms.TextBox();
            this.BtnSubmit = new System.Windows.Forms.Button();
            this.ClickSend = new System.Windows.Forms.Button();
            this.Password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.IsSuccessConnect = new System.Windows.Forms.Label();
            this.Modify = new System.Windows.Forms.Button();
            this.TestId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DataBaseNameLabel
            // 
            this.DataBaseNameLabel.AutoSize = true;
            this.DataBaseNameLabel.BackColor = System.Drawing.SystemColors.Control;
            this.DataBaseNameLabel.ForeColor = System.Drawing.Color.Black;
            this.DataBaseNameLabel.Location = new System.Drawing.Point(51, 49);
            this.DataBaseNameLabel.Name = "DataBaseNameLabel";
            this.DataBaseNameLabel.Size = new System.Drawing.Size(65, 12);
            this.DataBaseNameLabel.TabIndex = 0;
            this.DataBaseNameLabel.Text = "数据库名：";
            // 
            // IPLabel
            // 
            this.IPLabel.AutoSize = true;
            this.IPLabel.Location = new System.Drawing.Point(87, 78);
            this.IPLabel.Name = "IPLabel";
            this.IPLabel.Size = new System.Drawing.Size(29, 12);
            this.IPLabel.TabIndex = 1;
            this.IPLabel.Text = "IP：";
            // 
            // MachineIDLabel
            // 
            this.MachineIDLabel.AutoSize = true;
            this.MachineIDLabel.Location = new System.Drawing.Point(66, 136);
            this.MachineIDLabel.Name = "MachineIDLabel";
            this.MachineIDLabel.Size = new System.Drawing.Size(0, 12);
            this.MachineIDLabel.TabIndex = 2;
            // 
            // DataBaseName
            // 
            this.DataBaseName.Location = new System.Drawing.Point(122, 40);
            this.DataBaseName.Name = "DataBaseName";
            this.DataBaseName.Size = new System.Drawing.Size(100, 21);
            this.DataBaseName.TabIndex = 3;
            // 
            // IP
            // 
            this.IP.Location = new System.Drawing.Point(122, 75);
            this.IP.Name = "IP";
            this.IP.Size = new System.Drawing.Size(100, 21);
            this.IP.TabIndex = 4;
            // 
            // UserName
            // 
            this.UserName.Location = new System.Drawing.Point(122, 112);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(100, 21);
            this.UserName.TabIndex = 5;
            // 
            // BtnSubmit
            // 
            this.BtnSubmit.Location = new System.Drawing.Point(178, 202);
            this.BtnSubmit.Name = "BtnSubmit";
            this.BtnSubmit.Size = new System.Drawing.Size(79, 23);
            this.BtnSubmit.TabIndex = 6;
            this.BtnSubmit.Text = "修改";
            this.BtnSubmit.UseVisualStyleBackColor = true;
            this.BtnSubmit.Click += new System.EventHandler(this.BtnSubmit_Click);
            // 
            // ClickSend
            // 
            this.ClickSend.Location = new System.Drawing.Point(178, 245);
            this.ClickSend.Name = "ClickSend";
            this.ClickSend.Size = new System.Drawing.Size(122, 23);
            this.ClickSend.TabIndex = 7;
            this.ClickSend.Text = "模拟客户端下发指令";
            this.ClickSend.UseVisualStyleBackColor = true;
            this.ClickSend.Click += new System.EventHandler(this.ClickSend_Click);
            // 
            // Password
            // 
            this.Password.HideSelection = false;
            this.Password.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Password.Location = new System.Drawing.Point(122, 147);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '*';
            this.Password.Size = new System.Drawing.Size(100, 21);
            this.Password.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "密码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "用户名：";
            // 
            // IsSuccessConnect
            // 
            this.IsSuccessConnect.AutoSize = true;
            this.IsSuccessConnect.BackColor = System.Drawing.SystemColors.Control;
            this.IsSuccessConnect.ForeColor = System.Drawing.Color.Black;
            this.IsSuccessConnect.Location = new System.Drawing.Point(106, 181);
            this.IsSuccessConnect.Name = "IsSuccessConnect";
            this.IsSuccessConnect.Size = new System.Drawing.Size(0, 12);
            this.IsSuccessConnect.TabIndex = 11;
            // 
            // Modify
            // 
            this.Modify.Location = new System.Drawing.Point(94, 202);
            this.Modify.Name = "Modify";
            this.Modify.Size = new System.Drawing.Size(78, 23);
            this.Modify.TabIndex = 12;
            this.Modify.Text = "启用修改框";
            this.Modify.UseVisualStyleBackColor = true;
            this.Modify.Click += new System.EventHandler(this.Modify_Click);
            // 
            // TestId
            // 
            this.TestId.Location = new System.Drawing.Point(122, 245);
            this.TestId.Name = "TestId";
            this.TestId.Size = new System.Drawing.Size(50, 21);
            this.TestId.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 250);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "监控器Id：";
            // 
            // FXSend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 300);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TestId);
            this.Controls.Add(this.Modify);
            this.Controls.Add(this.IsSuccessConnect);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ClickSend);
            this.Controls.Add(this.BtnSubmit);
            this.Controls.Add(this.UserName);
            this.Controls.Add(this.IP);
            this.Controls.Add(this.DataBaseName);
            this.Controls.Add(this.MachineIDLabel);
            this.Controls.Add(this.IPLabel);
            this.Controls.Add(this.DataBaseNameLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FXSend";
            this.Text = "数据推送";
            this.Load += new System.EventHandler(this.FXSend_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label DataBaseNameLabel;
        private System.Windows.Forms.Label IPLabel;
        private System.Windows.Forms.Label MachineIDLabel;
        private System.Windows.Forms.TextBox DataBaseName;
        private System.Windows.Forms.TextBox IP;
        private System.Windows.Forms.TextBox UserName;
        private System.Windows.Forms.Button BtnSubmit;
        private System.Windows.Forms.Button ClickSend;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label IsSuccessConnect;
        private System.Windows.Forms.Button Modify;
        private System.Windows.Forms.TextBox TestId;
        private System.Windows.Forms.Label label3;
    }
}

