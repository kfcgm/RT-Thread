namespace competition
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.LineType = new System.Windows.Forms.TextBox();
            this.CloseWork = new System.Windows.Forms.Button();
            this.tbLog = new System.Windows.Forms.RichTextBox();
            this.StartWork = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.JpgImage = new System.Windows.Forms.PictureBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.JpgImage)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(399, 624);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "工作状态：";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Window;
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(482, 622);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.MenuBar;
            this.button2.Enabled = false;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(28, 620);
            this.button2.Name = "button2";
            this.button2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button2.Size = new System.Drawing.Size(134, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "售卖";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.MenuBar;
            this.button4.Enabled = false;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.Location = new System.Drawing.Point(213, 620);
            this.button4.Name = "button4";
            this.button4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button4.Size = new System.Drawing.Size(134, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "入库";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(224, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "连接状态：";
            // 
            // LineType
            // 
            this.LineType.Enabled = false;
            this.LineType.Location = new System.Drawing.Point(295, 14);
            this.LineType.Name = "LineType";
            this.LineType.Size = new System.Drawing.Size(100, 21);
            this.LineType.TabIndex = 7;
            // 
            // CloseWork
            // 
            this.CloseWork.Location = new System.Drawing.Point(122, 12);
            this.CloseWork.Name = "CloseWork";
            this.CloseWork.Size = new System.Drawing.Size(75, 23);
            this.CloseWork.TabIndex = 8;
            this.CloseWork.Text = "停止工作";
            this.CloseWork.UseVisualStyleBackColor = true;
            this.CloseWork.Click += new System.EventHandler(this.CloseWork_Click);
            // 
            // tbLog
            // 
            this.tbLog.Location = new System.Drawing.Point(28, 50);
            this.tbLog.Name = "tbLog";
            this.tbLog.Size = new System.Drawing.Size(567, 144);
            this.tbLog.TabIndex = 9;
            this.tbLog.Text = "";
            this.tbLog.TextChanged += new System.EventHandler(this.tbLog_TextChanged);
            // 
            // StartWork
            // 
            this.StartWork.Location = new System.Drawing.Point(28, 12);
            this.StartWork.Name = "StartWork";
            this.StartWork.Size = new System.Drawing.Size(75, 23);
            this.StartWork.TabIndex = 10;
            this.StartWork.Text = "开始工作";
            this.StartWork.UseVisualStyleBackColor = true;
            this.StartWork.Click += new System.EventHandler(this.StartWork_Click);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(795, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "结账";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // JpgImage
            // 
            this.JpgImage.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.JpgImage.Location = new System.Drawing.Point(156, 251);
            this.JpgImage.Name = "JpgImage";
            this.JpgImage.Size = new System.Drawing.Size(320, 240);
            this.JpgImage.TabIndex = 13;
            this.JpgImage.TabStop = false;
            this.JpgImage.Click += new System.EventHandler(this.JpgImage_Click);
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(28, 672);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(567, 21);
            this.textBox2.TabIndex = 12;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1518, 707);
            this.Controls.Add(this.JpgImage);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.StartWork);
            this.Controls.Add(this.CloseWork);
            this.Controls.Add(this.LineType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbLog);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.JpgImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox LineType;
        private System.Windows.Forms.Button CloseWork;
        private System.Windows.Forms.RichTextBox tbLog;
        private System.Windows.Forms.Button StartWork;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox JpgImage;
        private System.Windows.Forms.TextBox textBox2;
    }
}

