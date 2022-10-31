
namespace ChatClient
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_connect = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ClearButton = new System.Windows.Forms.Button();
            this.LogOutButton = new System.Windows.Forms.Button();
            this.ReportText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ReportBtn = new System.Windows.Forms.Button();
            this.ConsentButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("교보 손글씨 2019", 10F);
            this.textBox1.Location = new System.Drawing.Point(632, 26);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(288, 28);
            this.textBox1.TabIndex = 0;
            // 
            // btn_connect
            // 
            this.btn_connect.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btn_connect.Font = new System.Drawing.Font("교보 손글씨 2019", 11F);
            this.btn_connect.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btn_connect.Location = new System.Drawing.Point(737, 71);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(183, 42);
            this.btn_connect.TabIndex = 2;
            this.btn_connect.Text = "Connect to Server";
            this.btn_connect.UseVisualStyleBackColor = false;
            this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.Info;
            this.textBox2.Location = new System.Drawing.Point(29, 140);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(891, 292);
            this.textBox2.TabIndex = 3;
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("교보 손글씨 2019", 10F);
            this.textBox3.Location = new System.Drawing.Point(29, 447);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(891, 28);
            this.textBox3.TabIndex = 4;
            // 
            // btn_send
            // 
            this.btn_send.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btn_send.Font = new System.Drawing.Font("교보 손글씨 2019", 11F);
            this.btn_send.Location = new System.Drawing.Point(744, 494);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(176, 34);
            this.btn_send.TabIndex = 5;
            this.btn_send.Text = "Send Message";
            this.btn_send.UseVisualStyleBackColor = false;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("교보 손글씨 2019", 19.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(26, 489);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(270, 42);
            this.label2.TabIndex = 6;
            this.label2.Text = "Chat Server 0.1v";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.BackColor = System.Drawing.Color.Maroon;
            this.ClearButton.Font = new System.Drawing.Font("교보 손글씨 2019", 12F);
            this.ClearButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClearButton.Location = new System.Drawing.Point(550, 494);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(171, 33);
            this.ClearButton.TabIndex = 7;
            this.ClearButton.Text = "Clear";
            this.ClearButton.UseVisualStyleBackColor = false;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // LogOutButton
            // 
            this.LogOutButton.BackColor = System.Drawing.Color.Red;
            this.LogOutButton.Font = new System.Drawing.Font("교보 손글씨 2019", 12F);
            this.LogOutButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.LogOutButton.Location = new System.Drawing.Point(632, 71);
            this.LogOutButton.Name = "LogOutButton";
            this.LogOutButton.Size = new System.Drawing.Size(99, 42);
            this.LogOutButton.TabIndex = 8;
            this.LogOutButton.Text = "Log Out";
            this.LogOutButton.UseVisualStyleBackColor = false;
            this.LogOutButton.Click += new System.EventHandler(this.LogOutButton_Click);
            // 
            // ReportText
            // 
            this.ReportText.Font = new System.Drawing.Font("교보 손글씨 2019", 10F);
            this.ReportText.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.ReportText.Location = new System.Drawing.Point(33, 106);
            this.ReportText.Name = "ReportText";
            this.ReportText.Size = new System.Drawing.Size(288, 28);
            this.ReportText.TabIndex = 9;
            this.ReportText.Text = "Plz enter user\'s name...";
            this.ReportText.TextChanged += new System.EventHandler(this.ReportText_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("교보 손글씨 2019", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(30, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 18);
            this.label3.TabIndex = 10;
            this.label3.Text = "Report User";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // ReportBtn
            // 
            this.ReportBtn.BackColor = System.Drawing.Color.Red;
            this.ReportBtn.Font = new System.Drawing.Font("교보 손글씨 2019", 12F);
            this.ReportBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ReportBtn.Location = new System.Drawing.Point(222, 58);
            this.ReportBtn.Name = "ReportBtn";
            this.ReportBtn.Size = new System.Drawing.Size(99, 42);
            this.ReportBtn.TabIndex = 11;
            this.ReportBtn.Text = "Report";
            this.ReportBtn.UseVisualStyleBackColor = false;
            this.ReportBtn.Click += new System.EventHandler(this.ReportBtn_Click);
            // 
            // ConsentButton
            // 
            this.ConsentButton.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ConsentButton.Font = new System.Drawing.Font("교보 손글씨 2019", 12F);
            this.ConsentButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ConsentButton.Location = new System.Drawing.Point(128, 58);
            this.ConsentButton.Name = "ConsentButton";
            this.ConsentButton.Size = new System.Drawing.Size(99, 42);
            this.ConsentButton.TabIndex = 12;
            this.ConsentButton.Text = "Consent";
            this.ConsentButton.UseVisualStyleBackColor = false;
            this.ConsentButton.Click += new System.EventHandler(this.ConsentButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ChatClient.Properties.Resources.Pixcel_Ad;
            this.pictureBox1.Location = new System.Drawing.Point(338, 58);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(226, 79);
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("교보 손글씨 2019", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(635, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 18);
            this.label1.TabIndex = 15;
            this.label1.Text = "User Name";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 540);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ConsentButton);
            this.Controls.Add(this.ReportBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ReportText);
            this.Controls.Add(this.LogOutButton);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.btn_connect);
            this.Controls.Add(this.textBox1);
            this.Font = new System.Drawing.Font("교보 손글씨 2019", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Chat Client";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_connect;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button LogOutButton;
        private System.Windows.Forms.TextBox ReportText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ReportBtn;
        private System.Windows.Forms.Button ConsentButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
    }
}

