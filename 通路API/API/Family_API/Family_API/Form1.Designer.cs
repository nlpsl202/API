namespace Family_API
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.GetTickets_API_Start_btn = new System.Windows.Forms.Button();
            this.GetTickets_API_Stop_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.VerifyTickets_API_Stop_btn = new System.Windows.Forms.Button();
            this.VerifyTickets_API_Start_btn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.GetTickets_timer = new System.Windows.Forms.Timer(this.components);
            this.VerifyTickets_timer = new System.Windows.Forms.Timer(this.components);
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // GetTickets_API_Start_btn
            // 
            this.GetTickets_API_Start_btn.Location = new System.Drawing.Point(385, 57);
            this.GetTickets_API_Start_btn.Name = "GetTickets_API_Start_btn";
            this.GetTickets_API_Start_btn.Size = new System.Drawing.Size(81, 45);
            this.GetTickets_API_Start_btn.TabIndex = 0;
            this.GetTickets_API_Start_btn.Text = "START";
            this.GetTickets_API_Start_btn.UseVisualStyleBackColor = true;
            this.GetTickets_API_Start_btn.Click += new System.EventHandler(this.GetTickets_API_Start_btn_Click);
            // 
            // GetTickets_API_Stop_btn
            // 
            this.GetTickets_API_Stop_btn.Location = new System.Drawing.Point(472, 57);
            this.GetTickets_API_Stop_btn.Name = "GetTickets_API_Stop_btn";
            this.GetTickets_API_Stop_btn.Size = new System.Drawing.Size(81, 45);
            this.GetTickets_API_Stop_btn.TabIndex = 1;
            this.GetTickets_API_Stop_btn.Text = "STOP";
            this.GetTickets_API_Stop_btn.UseVisualStyleBackColor = true;
            this.GetTickets_API_Stop_btn.Click += new System.EventHandler(this.GetTickets_API_Stop_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "GetTickets_API";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 70);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 47;
            this.label2.Text = "商品代碼";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(115, 69);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(90, 22);
            this.checkBox1.TabIndex = 48;
            this.checkBox1.Text = "6314295";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(202, 69);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(90, 22);
            this.checkBox2.TabIndex = 49;
            this.checkBox2.Text = "6314318";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 104);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 18);
            this.label3.TabIndex = 56;
            this.label3.Text = "批次執行中......";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 236);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 18);
            this.label4.TabIndex = 57;
            this.label4.Text = "VerifyTickets_API";
            // 
            // VerifyTickets_API_Stop_btn
            // 
            this.VerifyTickets_API_Stop_btn.Location = new System.Drawing.Point(153, 257);
            this.VerifyTickets_API_Stop_btn.Name = "VerifyTickets_API_Stop_btn";
            this.VerifyTickets_API_Stop_btn.Size = new System.Drawing.Size(104, 45);
            this.VerifyTickets_API_Stop_btn.TabIndex = 59;
            this.VerifyTickets_API_Stop_btn.Text = "STOP";
            this.VerifyTickets_API_Stop_btn.UseVisualStyleBackColor = true;
            this.VerifyTickets_API_Stop_btn.Click += new System.EventHandler(this.VerifyTickets_API_Stop_btn_Click);
            // 
            // VerifyTickets_API_Start_btn
            // 
            this.VerifyTickets_API_Start_btn.Location = new System.Drawing.Point(30, 257);
            this.VerifyTickets_API_Start_btn.Name = "VerifyTickets_API_Start_btn";
            this.VerifyTickets_API_Start_btn.Size = new System.Drawing.Size(104, 45);
            this.VerifyTickets_API_Start_btn.TabIndex = 58;
            this.VerifyTickets_API_Start_btn.Text = "START";
            this.VerifyTickets_API_Start_btn.UseVisualStyleBackColor = true;
            this.VerifyTickets_API_Start_btn.Click += new System.EventHandler(this.VerifyTickets_API_Start_btn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 314);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 18);
            this.label5.TabIndex = 60;
            this.label5.Text = "批次執行中......";
            // 
            // GetTickets_timer
            // 
            this.GetTickets_timer.Tick += new System.EventHandler(this.GetTickets_timer_Tick);
            // 
            // VerifyTickets_timer
            // 
            this.VerifyTickets_timer.Tick += new System.EventHandler(this.VerifyTickets_timer_Tick);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(288, 69);
            this.checkBox3.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(90, 22);
            this.checkBox3.TabIndex = 63;
            this.checkBox3.Text = "6314307";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(406, 169);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 18);
            this.label10.TabIndex = 86;
            this.label10.Text = "完成!";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 202);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(174, 18);
            this.label9.TabIndex = 85;
            this.label9.Text = "格式：YYYY/MM/DD";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 169);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 18);
            this.label8.TabIndex = 84;
            this.label8.Text = "日期：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(197, 169);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 18);
            this.label7.TabIndex = 83;
            this.label7.Text = "~";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 143);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 18);
            this.label6.TabIndex = 82;
            this.label6.Text = "手動抓取";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(332, 156);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 45);
            this.button1.TabIndex = 81;
            this.button1.Text = "GO";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(217, 164);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 29);
            this.textBox2.TabIndex = 80;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(92, 164);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 29);
            this.textBox1.TabIndex = 79;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 540);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.VerifyTickets_API_Stop_btn);
            this.Controls.Add(this.VerifyTickets_API_Start_btn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GetTickets_API_Stop_btn);
            this.Controls.Add(this.GetTickets_API_Start_btn);
            this.Name = "Form1";
            this.Text = "Family_API";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button GetTickets_API_Start_btn;
        private System.Windows.Forms.Button GetTickets_API_Stop_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button VerifyTickets_API_Stop_btn;
        private System.Windows.Forms.Button VerifyTickets_API_Start_btn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer GetTickets_timer;
        private System.Windows.Forms.Timer VerifyTickets_timer;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
    }
}

