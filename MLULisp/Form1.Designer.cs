namespace MLULisp
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.OutputLabel = new System.Windows.Forms.Label();
            this.SrcFiletextBox = new System.Windows.Forms.TextBox();
            this.CMDtextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DebugDataGridView = new System.Windows.Forms.DataGridView();
            this.VarlistDataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DebugDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VarlistDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(685, 519);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Run";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // OutputLabel
            // 
            this.OutputLabel.AutoSize = true;
            this.OutputLabel.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.OutputLabel.Location = new System.Drawing.Point(691, 170);
            this.OutputLabel.Name = "OutputLabel";
            this.OutputLabel.Size = new System.Drawing.Size(49, 20);
            this.OutputLabel.TabIndex = 1;
            this.OutputLabel.Text = "Null";
            // 
            // SrcFiletextBox
            // 
            this.SrcFiletextBox.Location = new System.Drawing.Point(33, 38);
            this.SrcFiletextBox.Multiline = true;
            this.SrcFiletextBox.Name = "SrcFiletextBox";
            this.SrcFiletextBox.Size = new System.Drawing.Size(609, 459);
            this.SrcFiletextBox.TabIndex = 2;
            // 
            // CMDtextBox
            // 
            this.CMDtextBox.Location = new System.Drawing.Point(33, 519);
            this.CMDtextBox.Multiline = true;
            this.CMDtextBox.Name = "CMDtextBox";
            this.CMDtextBox.Size = new System.Drawing.Size(609, 25);
            this.CMDtextBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(661, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Output:";
            // 
            // DebugDataGridView
            // 
            this.DebugDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DebugDataGridView.Location = new System.Drawing.Point(746, 38);
            this.DebugDataGridView.Name = "DebugDataGridView";
            this.DebugDataGridView.RowTemplate.Height = 23;
            this.DebugDataGridView.Size = new System.Drawing.Size(141, 478);
            this.DebugDataGridView.TabIndex = 6;
            // 
            // VarlistDataGridView
            // 
            this.VarlistDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.VarlistDataGridView.Location = new System.Drawing.Point(893, 38);
            this.VarlistDataGridView.Name = "VarlistDataGridView";
            this.VarlistDataGridView.RowTemplate.Height = 23;
            this.VarlistDataGridView.Size = new System.Drawing.Size(141, 478);
            this.VarlistDataGridView.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1044, 552);
            this.Controls.Add(this.VarlistDataGridView);
            this.Controls.Add(this.DebugDataGridView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CMDtextBox);
            this.Controls.Add(this.SrcFiletextBox);
            this.Controls.Add(this.OutputLabel);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DebugDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VarlistDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label OutputLabel;
        private System.Windows.Forms.TextBox SrcFiletextBox;
        private System.Windows.Forms.TextBox CMDtextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView DebugDataGridView;
        private System.Windows.Forms.DataGridView VarlistDataGridView;
    }
}

