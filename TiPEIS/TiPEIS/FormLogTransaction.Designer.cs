namespace TiPEIS
{
    partial class FormLogTransaction
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
            this.F_Delete = new System.Windows.Forms.Button();
            this.F_Reload = new System.Windows.Forms.Button();
            this.F_Create = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.F_From = new System.Windows.Forms.DateTimePicker();
            this.F_To = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.F_Show = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // F_Delete
            // 
            this.F_Delete.Location = new System.Drawing.Point(872, 555);
            this.F_Delete.Name = "F_Delete";
            this.F_Delete.Size = new System.Drawing.Size(143, 38);
            this.F_Delete.TabIndex = 7;
            this.F_Delete.Text = "Delete";
            this.F_Delete.UseVisualStyleBackColor = true;
            this.F_Delete.Click += new System.EventHandler(this.F_Delete_Click);
            // 
            // F_Reload
            // 
            this.F_Reload.Location = new System.Drawing.Point(12, 555);
            this.F_Reload.Name = "F_Reload";
            this.F_Reload.Size = new System.Drawing.Size(147, 38);
            this.F_Reload.TabIndex = 6;
            this.F_Reload.Text = "обновить";
            this.F_Reload.UseVisualStyleBackColor = true;
            this.F_Reload.Click += new System.EventHandler(this.F_Reload_Click);
            // 
            // F_Create
            // 
            this.F_Create.Location = new System.Drawing.Point(396, 555);
            this.F_Create.Name = "F_Create";
            this.F_Create.Size = new System.Drawing.Size(259, 38);
            this.F_Create.TabIndex = 5;
            this.F_Create.Text = "Создать новый";
            this.F_Create.UseVisualStyleBackColor = true;
            this.F_Create.Click += new System.EventHandler(this.F_Create_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 156);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(1274, 393);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseDoubleClick);
            // 
            // F_From
            // 
            this.F_From.Location = new System.Drawing.Point(128, 38);
            this.F_From.Name = "F_From";
            this.F_From.Size = new System.Drawing.Size(200, 26);
            this.F_From.TabIndex = 8;
            // 
            // F_To
            // 
            this.F_To.Location = new System.Drawing.Point(400, 38);
            this.F_To.Name = "F_To";
            this.F_To.Size = new System.Drawing.Size(200, 26);
            this.F_To.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(102, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "С";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(364, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "По";
            // 
            // F_Show
            // 
            this.F_Show.Location = new System.Drawing.Point(312, 81);
            this.F_Show.Name = "F_Show";
            this.F_Show.Size = new System.Drawing.Size(96, 37);
            this.F_Show.TabIndex = 12;
            this.F_Show.Text = "Показать";
            this.F_Show.UseVisualStyleBackColor = true;
            this.F_Show.Click += new System.EventHandler(this.F_Show_Click);
            // 
            // FormLogTransaction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 606);
            this.Controls.Add(this.F_Show);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.F_To);
            this.Controls.Add(this.F_From);
            this.Controls.Add(this.F_Delete);
            this.Controls.Add(this.F_Reload);
            this.Controls.Add(this.F_Create);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FormLogTransaction";
            this.Text = "FormLogTransaction";
            this.Load += new System.EventHandler(this.FormLogTransaction_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button F_Delete;
        private System.Windows.Forms.Button F_Reload;
        private System.Windows.Forms.Button F_Create;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker F_From;
        private System.Windows.Forms.DateTimePicker F_To;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button F_Show;
    }
}