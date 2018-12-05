namespace TiPEIS
{
    partial class FormReportV
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.F_Show = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.F_To = new System.Windows.Forms.DateTimePicker();
            this.F_From = new System.Windows.Forms.DateTimePicker();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // F_Show
            // 
            this.F_Show.Location = new System.Drawing.Point(665, 2);
            this.F_Show.Name = "F_Show";
            this.F_Show.Size = new System.Drawing.Size(96, 37);
            this.F_Show.TabIndex = 18;
            this.F_Show.Text = "Show";
            this.F_Show.UseVisualStyleBackColor = true;
            this.F_Show.Click += new System.EventHandler(this.F_Show_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(394, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 20);
            this.label2.TabIndex = 17;
            this.label2.Text = "To";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(106, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 20);
            this.label1.TabIndex = 16;
            this.label1.Text = "From";
            // 
            // F_To
            // 
            this.F_To.Location = new System.Drawing.Point(430, 10);
            this.F_To.Name = "F_To";
            this.F_To.Size = new System.Drawing.Size(200, 26);
            this.F_To.TabIndex = 15;
            // 
            // F_From
            // 
            this.F_From.Location = new System.Drawing.Point(158, 10);
            this.F_From.Name = "F_From";
            this.F_From.Size = new System.Drawing.Size(200, 26);
            this.F_From.TabIndex = 14;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 45);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(1134, 502);
            this.dataGridView1.TabIndex = 13;
            // 
            // FormReportV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1156, 578);
            this.Controls.Add(this.F_Show);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.F_To);
            this.Controls.Add(this.F_From);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FormReportV";
            this.Text = "Предстоящие поступления денежных средств";
            this.Load += new System.EventHandler(this.FormReportV_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button F_Show;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker F_To;
        private System.Windows.Forms.DateTimePicker F_From;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}