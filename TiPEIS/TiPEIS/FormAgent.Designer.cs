﻿namespace TiPEIS
{
    partial class FormAgent
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.F_value = new System.Windows.Forms.TextBox();
            this.F_Add = new System.Windows.Forms.Button();
            this.F_Update = new System.Windows.Forms.Button();
            this.F_Delete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 50);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(776, 388);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick_1);
            // 
            // F_value
            // 
            this.F_value.Location = new System.Drawing.Point(12, 12);
            this.F_value.Name = "F_value";
            this.F_value.Size = new System.Drawing.Size(533, 26);
            this.F_value.TabIndex = 2;
            // 
            // F_Add
            // 
            this.F_Add.Location = new System.Drawing.Point(551, 12);
            this.F_Add.Name = "F_Add";
            this.F_Add.Size = new System.Drawing.Size(75, 32);
            this.F_Add.TabIndex = 3;
            this.F_Add.Text = "Add";
            this.F_Add.UseVisualStyleBackColor = true;
            this.F_Add.Click += new System.EventHandler(this.F_Add_Click);
            // 
            // F_Update
            // 
            this.F_Update.Location = new System.Drawing.Point(632, 12);
            this.F_Update.Name = "F_Update";
            this.F_Update.Size = new System.Drawing.Size(75, 32);
            this.F_Update.TabIndex = 4;
            this.F_Update.Text = "Update";
            this.F_Update.UseVisualStyleBackColor = true;
            this.F_Update.Click += new System.EventHandler(this.F_Update_Click);
            // 
            // F_Delete
            // 
            this.F_Delete.Location = new System.Drawing.Point(713, 12);
            this.F_Delete.Name = "F_Delete";
            this.F_Delete.Size = new System.Drawing.Size(75, 32);
            this.F_Delete.TabIndex = 5;
            this.F_Delete.Text = "Delete";
            this.F_Delete.UseVisualStyleBackColor = true;
            this.F_Delete.Click += new System.EventHandler(this.F_Delete_Click);
            // 
            // FormAgent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.F_Delete);
            this.Controls.Add(this.F_Update);
            this.Controls.Add(this.F_Add);
            this.Controls.Add(this.F_value);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FormAgent";
            this.Text = "Agent";
            this.Load += new System.EventHandler(this.FormAgent_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox F_value;
        private System.Windows.Forms.Button F_Add;
        private System.Windows.Forms.Button F_Update;
        private System.Windows.Forms.Button F_Delete;
    }
}