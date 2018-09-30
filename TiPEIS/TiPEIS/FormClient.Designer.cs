namespace TiPEIS
{
    partial class FormClient
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
            this.F_Delete = new System.Windows.Forms.Button();
            this.F_Update = new System.Windows.Forms.Button();
            this.F_Add = new System.Windows.Forms.Button();
            this.F_value = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 59);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(776, 380);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            // 
            // F_Delete
            // 
            this.F_Delete.Location = new System.Drawing.Point(713, 20);
            this.F_Delete.Name = "F_Delete";
            this.F_Delete.Size = new System.Drawing.Size(75, 33);
            this.F_Delete.TabIndex = 9;
            this.F_Delete.Text = "Delete";
            this.F_Delete.UseVisualStyleBackColor = true;
            this.F_Delete.Click += new System.EventHandler(this.F_Delete_Click);
            // 
            // F_Update
            // 
            this.F_Update.Location = new System.Drawing.Point(632, 19);
            this.F_Update.Name = "F_Update";
            this.F_Update.Size = new System.Drawing.Size(75, 32);
            this.F_Update.TabIndex = 8;
            this.F_Update.Text = "Update";
            this.F_Update.UseVisualStyleBackColor = true;
            this.F_Update.Click += new System.EventHandler(this.F_Update_Click);
            // 
            // F_Add
            // 
            this.F_Add.Location = new System.Drawing.Point(551, 19);
            this.F_Add.Name = "F_Add";
            this.F_Add.Size = new System.Drawing.Size(75, 32);
            this.F_Add.TabIndex = 7;
            this.F_Add.Text = "Add";
            this.F_Add.UseVisualStyleBackColor = true;
            this.F_Add.Click += new System.EventHandler(this.F_Add_Click);
            // 
            // F_value
            // 
            this.F_value.Location = new System.Drawing.Point(12, 19);
            this.F_value.Name = "F_value";
            this.F_value.Size = new System.Drawing.Size(533, 26);
            this.F_value.TabIndex = 6;
            // 
            // FormClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.F_Delete);
            this.Controls.Add(this.F_Update);
            this.Controls.Add(this.F_Add);
            this.Controls.Add(this.F_value);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FormClient";
            this.Text = "Client";
            this.Load += new System.EventHandler(this.FormClient_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button F_Delete;
        private System.Windows.Forms.Button F_Update;
        private System.Windows.Forms.Button F_Add;
        private System.Windows.Forms.TextBox F_value;
    }
}