namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            txtURL = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            btnDownload = new System.Windows.Forms.Button();
            btnExportPDF = new System.Windows.Forms.Button();
            progressBar1 = new System.Windows.Forms.ProgressBar();
            txtName = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            lblLoading = new System.Windows.Forms.Label();
            txtPage = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            flowLayoutPanel1.Location = new System.Drawing.Point(0, 334);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new System.Drawing.Size(800, 156);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // txtURL
            // 
            txtURL.Location = new System.Drawing.Point(73, 12);
            txtURL.Name = "txtURL";
            txtURL.Size = new System.Drawing.Size(511, 23);
            txtURL.TabIndex = 0;
            txtURL.Text = "https://drive.google.com/drive/folders/15TDlbPq5XJhjVt3VYmAQuOZHTb5pCLcM";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(14, 15);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(53, 15);
            label1.TabIndex = 2;
            label1.Text = "Link PDF";
            // 
            // btnDownload
            // 
            btnDownload.Location = new System.Drawing.Point(590, 12);
            btnDownload.Name = "btnDownload";
            btnDownload.Size = new System.Drawing.Size(95, 55);
            btnDownload.TabIndex = 2;
            btnDownload.Text = "Download";
            btnDownload.UseVisualStyleBackColor = true;
            btnDownload.Click += btnDownload_ClickAsync;
            // 
            // btnExportPDF
            // 
            btnExportPDF.Enabled = false;
            btnExportPDF.Location = new System.Drawing.Point(693, 11);
            btnExportPDF.Name = "btnExportPDF";
            btnExportPDF.Size = new System.Drawing.Size(95, 56);
            btnExportPDF.TabIndex = 3;
            btnExportPDF.Text = "Export PDF";
            btnExportPDF.UseVisualStyleBackColor = true;
            btnExportPDF.Click += btnExportPDF_ClickAsync;
            // 
            // progressBar1
            // 
            progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            progressBar1.Location = new System.Drawing.Point(0, 491);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new System.Drawing.Size(800, 10);
            progressBar1.TabIndex = 4;
            // 
            // txtName
            // 
            txtName.Location = new System.Drawing.Point(217, 44);
            txtName.Name = "txtName";
            txtName.Size = new System.Drawing.Size(367, 23);
            txtName.TabIndex = 1;
            txtName.Text = "giáo trình hán ngữ quyển 1 tiếng việt.pdf";
            txtName.KeyPress += txtPage_KeyPress;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(148, 47);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(63, 15);
            label2.TabIndex = 2;
            label2.Text = "Name PDF";
            // 
            // lblLoading
            // 
            lblLoading.AutoSize = true;
            lblLoading.Location = new System.Drawing.Point(12, 72);
            lblLoading.Name = "lblLoading";
            lblLoading.Size = new System.Drawing.Size(0, 15);
            lblLoading.TabIndex = 5;
            // 
            // txtPage
            // 
            txtPage.Location = new System.Drawing.Point(73, 44);
            txtPage.Name = "txtPage";
            txtPage.Size = new System.Drawing.Size(57, 23);
            txtPage.TabIndex = 1;
            txtPage.Text = "175";
            txtPage.KeyPress += txtPage_KeyPress;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(14, 47);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(38, 15);
            label3.TabIndex = 2;
            label3.Text = "Pages";
            // 
            // backgroundWorker1
            // 
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new System.Drawing.Point(0, 90);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new System.Drawing.Size(800, 238);
            dataGridView1.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 501);
            Controls.Add(dataGridView1);
            Controls.Add(lblLoading);
            Controls.Add(progressBar1);
            Controls.Add(btnExportPDF);
            Controls.Add(btnDownload);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtPage);
            Controls.Add(txtName);
            Controls.Add(txtURL);
            Controls.Add(flowLayoutPanel1);
            Name = "Form1";
            Text = "Download GG";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnExportPDF;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblLoading;
        private System.Windows.Forms.TextBox txtPage;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}
