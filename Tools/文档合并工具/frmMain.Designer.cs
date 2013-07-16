namespace 文档合并工具
{
    partial class frmMain
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.labLocation = new System.Windows.Forms.Label();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.btnMerge = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtLocation
            // 
            this.txtLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLocation.Location = new System.Drawing.Point(70, 12);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(347, 21);
            this.txtLocation.TabIndex = 0;
            // 
            // labLocation
            // 
            this.labLocation.AutoSize = true;
            this.labLocation.Location = new System.Drawing.Point(10, 16);
            this.labLocation.Name = "labLocation";
            this.labLocation.Size = new System.Drawing.Size(59, 12);
            this.labLocation.TabIndex = 1;
            this.labLocation.Text = "文档位置:";
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSelectFolder.Location = new System.Drawing.Point(424, 11);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(46, 23);
            this.btnSelectFolder.TabIndex = 2;
            this.btnSelectFolder.Text = "...";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // btnMerge
            // 
            this.btnMerge.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMerge.Location = new System.Drawing.Point(424, 76);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(46, 23);
            this.btnMerge.TabIndex = 3;
            this.btnMerge.Text = "合并";
            this.btnMerge.UseVisualStyleBackColor = true;
            this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(287, 36);
            this.label1.TabIndex = 4;
            this.label1.Text = "点击合并按钮,将合并文档位置目录下的WebTOC.xml与\r\n文档目录\\UserManual目录下的WebTOC.xml文件\r\n注:可以进行多次合并,不会出错.\r" +
                "\n";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 109);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnMerge);
            this.Controls.Add(this.btnSelectFolder);
            this.Controls.Add(this.labLocation);
            this.Controls.Add(this.txtLocation);
            this.Name = "frmMain";
            this.Text = "文档合并工具";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label labLocation;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.Button btnMerge;
        private System.Windows.Forms.Label label1;
    }
}

