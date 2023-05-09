namespace Simple_VoskAsr
{
    partial class frmVoskASRDemo
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
            this.btnRecord = new System.Windows.Forms.Button();
            this.txtfilePath = new System.Windows.Forms.TextBox();
            this.txtResult = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnRecord
            // 
            this.btnRecord.Enabled = false;
            this.btnRecord.Location = new System.Drawing.Point(99, 164);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(158, 62);
            this.btnRecord.TabIndex = 0;
            this.btnRecord.Text = "录音";
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button1_MouseDown);
            this.btnRecord.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button1_MouseUp);
            // 
            // txtfilePath
            // 
            this.txtfilePath.Location = new System.Drawing.Point(99, 23);
            this.txtfilePath.Name = "txtfilePath";
            this.txtfilePath.Size = new System.Drawing.Size(335, 21);
            this.txtfilePath.TabIndex = 1;
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(99, 50);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(335, 96);
            this.txtResult.TabIndex = 2;
            this.txtResult.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "音频路径";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "识别结果";
            // 
            // frmVoskASRDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 258);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.txtfilePath);
            this.Controls.Add(this.btnRecord);
            this.Name = "frmVoskASRDemo";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmVoskASRDemo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.TextBox txtfilePath;
        private System.Windows.Forms.RichTextBox txtResult;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

