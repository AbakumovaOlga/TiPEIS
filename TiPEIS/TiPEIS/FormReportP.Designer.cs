﻿namespace TiPEIS
{
    partial class FormReportP
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
            this.F_sum = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.F_Doc = new System.Windows.Forms.Button();
            this.F_Xls = new System.Windows.Forms.Button();
            this.F_Pdf = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // F_Show
            // 
            this.F_Show.Location = new System.Drawing.Point(669, 21);
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
            this.label2.Location = new System.Drawing.Point(398, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 20);
            this.label2.TabIndex = 17;
            this.label2.Text = "To";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(110, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 20);
            this.label1.TabIndex = 16;
            this.label1.Text = "From";
            // 
            // F_To
            // 
            this.F_To.Location = new System.Drawing.Point(434, 29);
            this.F_To.Name = "F_To";
            this.F_To.Size = new System.Drawing.Size(200, 26);
            this.F_To.TabIndex = 15;
            // 
            // F_From
            // 
            this.F_From.Location = new System.Drawing.Point(162, 29);
            this.F_From.Name = "F_From";
            this.F_From.Size = new System.Drawing.Size(200, 26);
            this.F_From.TabIndex = 14;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(7, 64);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(1134, 394);
            this.dataGridView1.TabIndex = 13;
            // 
            // F_sum
            // 
            this.F_sum.Location = new System.Drawing.Point(973, 21);
            this.F_sum.Name = "F_sum";
            this.F_sum.Size = new System.Drawing.Size(168, 26);
            this.F_sum.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(875, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 20);
            this.label3.TabIndex = 21;
            this.label3.Text = "Итого";
            // 
            // F_Doc
            // 
            this.F_Doc.Location = new System.Drawing.Point(12, 464);
            this.F_Doc.Name = "F_Doc";
            this.F_Doc.Size = new System.Drawing.Size(98, 35);
            this.F_Doc.TabIndex = 23;
            this.F_Doc.Text = "Doc";
            this.F_Doc.UseVisualStyleBackColor = true;
            this.F_Doc.Click += new System.EventHandler(this.F_Doc_Click);
            // 
            // F_Xls
            // 
            this.F_Xls.Location = new System.Drawing.Point(206, 464);
            this.F_Xls.Name = "F_Xls";
            this.F_Xls.Size = new System.Drawing.Size(98, 35);
            this.F_Xls.TabIndex = 24;
            this.F_Xls.Text = "Xls";
            this.F_Xls.UseVisualStyleBackColor = true;
            this.F_Xls.Click += new System.EventHandler(this.F_Xls_Click);
            // 
            // F_Pdf
            // 
            this.F_Pdf.Location = new System.Drawing.Point(421, 464);
            this.F_Pdf.Name = "F_Pdf";
            this.F_Pdf.Size = new System.Drawing.Size(98, 35);
            this.F_Pdf.TabIndex = 25;
            this.F_Pdf.Text = "Pdf";
            this.F_Pdf.UseVisualStyleBackColor = true;
            this.F_Pdf.Click += new System.EventHandler(this.F_Pdf_Click);
            // 
            // FormReportP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1153, 506);
            this.Controls.Add(this.F_Pdf);
            this.Controls.Add(this.F_Xls);
            this.Controls.Add(this.F_Doc);
            this.Controls.Add(this.F_sum);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.F_Show);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.F_To);
            this.Controls.Add(this.F_From);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FormReportP";
            this.Text = "Ведомость доходов от предоставления займов";
            this.Load += new System.EventHandler(this.FormReportP_Load);
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
        private System.Windows.Forms.TextBox F_sum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button F_Doc;
        private System.Windows.Forms.Button F_Xls;
        private System.Windows.Forms.Button F_Pdf;
    }
}