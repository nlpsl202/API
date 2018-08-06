namespace iBon_API
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
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.TotalCount_lbl = new System.Windows.Forms.Label();
            this.BlockCount_lbl = new System.Windows.Forms.Label();
            this.CheckConnection_btn = new System.Windows.Forms.Button();
            this.ConnectionStatus_lbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.GetTicketData_API_Start_btn = new System.Windows.Forms.Button();
            this.GetTicketData_API_Stop_btn = new System.Windows.Forms.Button();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.CheckTicketData_API_Stop_btn = new System.Windows.Forms.Button();
            this.CheckTicketData_API_Start_btn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.GetTicketCount_API_Start_btn = new System.Windows.Forms.Button();
            this.GetTicketData_timer = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.CheckTicketData_timer = new System.Windows.Forms.Timer(this.components);
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.block_lbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(219, 367);
            this.checkBox4.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(111, 22);
            this.checkBox4.TabIndex = 24;
            this.checkBox4.Text = "B011M9SZ";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(112, 367);
            this.checkBox3.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(111, 22);
            this.checkBox3.TabIndex = 23;
            this.checkBox3.Text = "B01LITNY";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 369);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 18);
            this.label7.TabIndex = 22;
            this.label7.Text = "商品代碼";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(146, 400);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(133, 29);
            this.textBox1.TabIndex = 25;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(403, 399);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(133, 29);
            this.textBox2.TabIndex = 26;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 406);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 18);
            this.label8.TabIndex = 27;
            this.label8.Text = "訂單時間(起)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(286, 406);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(110, 18);
            this.label9.TabIndex = 28;
            this.label9.Text = "訂單時間(訖)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 447);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(103, 18);
            this.label10.TabIndex = 32;
            this.label10.Text = "票券總筆數:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(238, 447);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 18);
            this.label11.TabIndex = 33;
            this.label11.Text = "區塊數:";
            // 
            // TotalCount_lbl
            // 
            this.TotalCount_lbl.AutoSize = true;
            this.TotalCount_lbl.Location = new System.Drawing.Point(130, 447);
            this.TotalCount_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TotalCount_lbl.Name = "TotalCount_lbl";
            this.TotalCount_lbl.Size = new System.Drawing.Size(110, 18);
            this.TotalCount_lbl.TabIndex = 34;
            this.TotalCount_lbl.Text = "TotalCount_lbl";
            // 
            // BlockCount_lbl
            // 
            this.BlockCount_lbl.AutoSize = true;
            this.BlockCount_lbl.Location = new System.Drawing.Point(313, 447);
            this.BlockCount_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.BlockCount_lbl.Name = "BlockCount_lbl";
            this.BlockCount_lbl.Size = new System.Drawing.Size(114, 18);
            this.BlockCount_lbl.TabIndex = 35;
            this.BlockCount_lbl.Text = "BlockCount_lbl";
            // 
            // CheckConnection_btn
            // 
            this.CheckConnection_btn.Location = new System.Drawing.Point(27, 50);
            this.CheckConnection_btn.Margin = new System.Windows.Forms.Padding(4);
            this.CheckConnection_btn.Name = "CheckConnection_btn";
            this.CheckConnection_btn.Size = new System.Drawing.Size(124, 34);
            this.CheckConnection_btn.TabIndex = 36;
            this.CheckConnection_btn.Text = "測試連接";
            this.CheckConnection_btn.UseVisualStyleBackColor = true;
            this.CheckConnection_btn.Click += new System.EventHandler(this.CheckConnection_btn_Click);
            // 
            // ConnectionStatus_lbl
            // 
            this.ConnectionStatus_lbl.AutoSize = true;
            this.ConnectionStatus_lbl.Location = new System.Drawing.Point(107, 28);
            this.ConnectionStatus_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ConnectionStatus_lbl.Name = "ConnectionStatus_lbl";
            this.ConnectionStatus_lbl.Size = new System.Drawing.Size(153, 18);
            this.ConnectionStatus_lbl.TabIndex = 37;
            this.ConnectionStatus_lbl.Text = "ConnectionStatus_lbl";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 18);
            this.label1.TabIndex = 40;
            this.label1.Text = "連接狀態:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 18);
            this.label2.TabIndex = 41;
            this.label2.Text = "GetTicketData_API";
            // 
            // GetTicketData_API_Start_btn
            // 
            this.GetTicketData_API_Start_btn.Location = new System.Drawing.Point(347, 138);
            this.GetTicketData_API_Start_btn.Name = "GetTicketData_API_Start_btn";
            this.GetTicketData_API_Start_btn.Size = new System.Drawing.Size(75, 36);
            this.GetTicketData_API_Start_btn.TabIndex = 42;
            this.GetTicketData_API_Start_btn.Text = "Start";
            this.GetTicketData_API_Start_btn.UseVisualStyleBackColor = true;
            this.GetTicketData_API_Start_btn.Click += new System.EventHandler(this.GetTicketData_API_Start_btn_Click);
            // 
            // GetTicketData_API_Stop_btn
            // 
            this.GetTicketData_API_Stop_btn.Location = new System.Drawing.Point(428, 138);
            this.GetTicketData_API_Stop_btn.Name = "GetTicketData_API_Stop_btn";
            this.GetTicketData_API_Stop_btn.Size = new System.Drawing.Size(75, 36);
            this.GetTicketData_API_Stop_btn.TabIndex = 43;
            this.GetTicketData_API_Stop_btn.Text = "Stop";
            this.GetTicketData_API_Stop_btn.UseVisualStyleBackColor = true;
            this.GetTicketData_API_Stop_btn.Click += new System.EventHandler(this.GetTicketData_API_Stop_btn_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(219, 146);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(111, 22);
            this.checkBox2.TabIndex = 45;
            this.checkBox2.Text = "B011M9SZ";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(110, 146);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(111, 22);
            this.checkBox1.TabIndex = 44;
            this.checkBox1.Text = "B01LITNY";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 148);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 18);
            this.label3.TabIndex = 46;
            this.label3.Text = "商品代碼";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 256);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 18);
            this.label5.TabIndex = 52;
            this.label5.Text = "商品代碼";
            // 
            // CheckTicketData_API_Stop_btn
            // 
            this.CheckTicketData_API_Stop_btn.Location = new System.Drawing.Point(193, 247);
            this.CheckTicketData_API_Stop_btn.Name = "CheckTicketData_API_Stop_btn";
            this.CheckTicketData_API_Stop_btn.Size = new System.Drawing.Size(75, 36);
            this.CheckTicketData_API_Stop_btn.TabIndex = 49;
            this.CheckTicketData_API_Stop_btn.Text = "Stop";
            this.CheckTicketData_API_Stop_btn.UseVisualStyleBackColor = true;
            this.CheckTicketData_API_Stop_btn.Click += new System.EventHandler(this.CheckTicketData_API_Stop_btn_Click);
            // 
            // CheckTicketData_API_Start_btn
            // 
            this.CheckTicketData_API_Start_btn.Location = new System.Drawing.Point(112, 247);
            this.CheckTicketData_API_Start_btn.Name = "CheckTicketData_API_Start_btn";
            this.CheckTicketData_API_Start_btn.Size = new System.Drawing.Size(75, 36);
            this.CheckTicketData_API_Start_btn.TabIndex = 48;
            this.CheckTicketData_API_Start_btn.Text = "Start";
            this.CheckTicketData_API_Start_btn.UseVisualStyleBackColor = true;
            this.CheckTicketData_API_Start_btn.Click += new System.EventHandler(this.CheckTicketData_API_Start_btn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 225);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(163, 18);
            this.label4.TabIndex = 47;
            this.label4.Text = "CheckTicketData_API";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 340);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(152, 18);
            this.label6.TabIndex = 53;
            this.label6.Text = "GetTicketCount_API";
            // 
            // GetTicketCount_API_Start_btn
            // 
            this.GetTicketCount_API_Start_btn.Location = new System.Drawing.Point(551, 394);
            this.GetTicketCount_API_Start_btn.Name = "GetTicketCount_API_Start_btn";
            this.GetTicketCount_API_Start_btn.Size = new System.Drawing.Size(75, 36);
            this.GetTicketCount_API_Start_btn.TabIndex = 54;
            this.GetTicketCount_API_Start_btn.Text = "Start";
            this.GetTicketCount_API_Start_btn.UseVisualStyleBackColor = true;
            this.GetTicketCount_API_Start_btn.Click += new System.EventHandler(this.GetTicketCount_API_Start_btn_Click);
            // 
            // GetTicketData_timer
            // 
            this.GetTicketData_timer.Tick += new System.EventHandler(this.GetTicketData_timer_Tick);
            // 
            // CheckTicketData_timer
            // 
            this.CheckTicketData_timer.Tick += new System.EventHandler(this.CheckTicketData_timer_Tick);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(24, 176);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(122, 18);
            this.label12.TabIndex = 55;
            this.label12.Text = "批次執行中......";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(24, 283);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(122, 18);
            this.label13.TabIndex = 56;
            this.label13.Text = "批次執行中......";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(165, 176);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(85, 18);
            this.label14.TabIndex = 57;
            this.label14.Text = "目前區塊:";
            // 
            // block_lbl
            // 
            this.block_lbl.AutoSize = true;
            this.block_lbl.Location = new System.Drawing.Point(245, 176);
            this.block_lbl.Name = "block_lbl";
            this.block_lbl.Size = new System.Drawing.Size(45, 18);
            this.block_lbl.TabIndex = 58;
            this.block_lbl.Text = "block";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 488);
            this.Controls.Add(this.block_lbl);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.GetTicketCount_API_Start_btn);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CheckTicketData_API_Stop_btn);
            this.Controls.Add(this.CheckTicketData_API_Start_btn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.GetTicketData_API_Stop_btn);
            this.Controls.Add(this.GetTicketData_API_Start_btn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ConnectionStatus_lbl);
            this.Controls.Add(this.CheckConnection_btn);
            this.Controls.Add(this.BlockCount_lbl);
            this.Controls.Add(this.TotalCount_lbl);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.checkBox4);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.label7);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label TotalCount_lbl;
        private System.Windows.Forms.Label BlockCount_lbl;
        private System.Windows.Forms.Button CheckConnection_btn;
        private System.Windows.Forms.Label ConnectionStatus_lbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button GetTicketData_API_Start_btn;
        private System.Windows.Forms.Button GetTicketData_API_Stop_btn;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button CheckTicketData_API_Stop_btn;
        private System.Windows.Forms.Button CheckTicketData_API_Start_btn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button GetTicketCount_API_Start_btn;
        private System.Windows.Forms.Timer GetTicketData_timer;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer CheckTicketData_timer;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label block_lbl;
    }
}

