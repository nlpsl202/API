namespace Gomaji_API
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
            this.GetTickets_timer = new System.Windows.Forms.Timer(this.components);
            this.VerifyTickets_timer = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // GetTickets_API_Start_btn
            // 
            this.GetTickets_API_Start_btn.Location = new System.Drawing.Point(30, 98);
            this.GetTickets_API_Start_btn.Name = "GetTickets_API_Start_btn";
            this.GetTickets_API_Start_btn.Size = new System.Drawing.Size(104, 45);
            this.GetTickets_API_Start_btn.TabIndex = 0;
            this.GetTickets_API_Start_btn.Text = "START";
            this.GetTickets_API_Start_btn.UseVisualStyleBackColor = true;
            this.GetTickets_API_Start_btn.Click += new System.EventHandler(this.GetTickets_API_Start_btn_Click);
            // 
            // GetTickets_API_Stop_btn
            // 
            this.GetTickets_API_Stop_btn.Location = new System.Drawing.Point(153, 98);
            this.GetTickets_API_Stop_btn.Name = "GetTickets_API_Stop_btn";
            this.GetTickets_API_Stop_btn.Size = new System.Drawing.Size(104, 45);
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
            this.label2.Text = "查詢類別";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(115, 69);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(142, 22);
            this.checkBox1.TabIndex = 48;
            this.checkBox1.Text = "門票訂購時間";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(265, 70);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(142, 22);
            this.checkBox2.TabIndex = 49;
            this.checkBox2.Text = "門票異動時間";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 155);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 18);
            this.label3.TabIndex = 56;
            this.label3.Text = "批次執行中......";
            // 
            // GetTickets_timer
            // 
            this.GetTickets_timer.Tick += new System.EventHandler(this.GetTickets_timer_Tick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 18);
            this.label4.TabIndex = 61;
            this.label4.Text = "label4";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 232);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GetTickets_API_Stop_btn);
            this.Controls.Add(this.GetTickets_API_Start_btn);
            this.Name = "Form1";
            this.Text = "Gomaji_API";
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
        private System.Windows.Forms.Timer GetTickets_timer;
        private System.Windows.Forms.Timer VerifyTickets_timer;
        private System.Windows.Forms.Label label4;
    }
}

