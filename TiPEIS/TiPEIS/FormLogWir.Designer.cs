namespace TiPEIS
{
    partial class FormLogWir
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.F_DateOper = new System.Windows.Forms.DateTimePicker();
            this.F_Number = new System.Windows.Forms.TextBox();
            this.F_Doc = new System.Windows.Forms.ComboBox();
            this.F_Agent = new System.Windows.Forms.ComboBox();
            this.F_Client = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.F_Com = new System.Windows.Forms.TextBox();
            this.F_Add = new System.Windows.Forms.Button();
            this.F_Clear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Дата операции";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Номер операции";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(425, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Договор";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(444, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Агент";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(433, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Клиент";
            // 
            // F_DateOper
            // 
            this.F_DateOper.Location = new System.Drawing.Point(204, 51);
            this.F_DateOper.Name = "F_DateOper";
            this.F_DateOper.Size = new System.Drawing.Size(182, 26);
            this.F_DateOper.TabIndex = 5;
            // 
            // F_Number
            // 
            this.F_Number.Location = new System.Drawing.Point(213, 132);
            this.F_Number.Name = "F_Number";
            this.F_Number.Size = new System.Drawing.Size(173, 26);
            this.F_Number.TabIndex = 6;
            // 
            // F_Doc
            // 
            this.F_Doc.FormattingEnabled = true;
            this.F_Doc.Location = new System.Drawing.Point(540, 42);
            this.F_Doc.Name = "F_Doc";
            this.F_Doc.Size = new System.Drawing.Size(204, 28);
            this.F_Doc.TabIndex = 7;
            // 
            // F_Agent
            // 
            this.F_Agent.FormattingEnabled = true;
            this.F_Agent.Location = new System.Drawing.Point(540, 98);
            this.F_Agent.Name = "F_Agent";
            this.F_Agent.Size = new System.Drawing.Size(204, 28);
            this.F_Agent.TabIndex = 8;
            // 
            // F_Client
            // 
            this.F_Client.FormattingEnabled = true;
            this.F_Client.Location = new System.Drawing.Point(540, 155);
            this.F_Client.Name = "F_Client";
            this.F_Client.Size = new System.Drawing.Size(204, 28);
            this.F_Client.TabIndex = 9;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(28, 282);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(716, 244);
            this.dataGridView1.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 553);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "Комментарий";
            // 
            // F_Com
            // 
            this.F_Com.Location = new System.Drawing.Point(176, 553);
            this.F_Com.Name = "F_Com";
            this.F_Com.Size = new System.Drawing.Size(569, 26);
            this.F_Com.TabIndex = 12;
            // 
            // F_Add
            // 
            this.F_Add.Location = new System.Drawing.Point(338, 230);
            this.F_Add.Name = "F_Add";
            this.F_Add.Size = new System.Drawing.Size(228, 33);
            this.F_Add.TabIndex = 13;
            this.F_Add.Text = "Ввод новой записи";
            this.F_Add.UseVisualStyleBackColor = true;
            this.F_Add.Click += new System.EventHandler(this.F_Add_Click);
            // 
            // F_Clear
            // 
            this.F_Clear.Location = new System.Drawing.Point(627, 230);
            this.F_Clear.Name = "F_Clear";
            this.F_Clear.Size = new System.Drawing.Size(118, 33);
            this.F_Clear.TabIndex = 14;
            this.F_Clear.Text = "Очистить";
            this.F_Clear.UseVisualStyleBackColor = true;
            this.F_Clear.Click += new System.EventHandler(this.F_Clear_Click);
            // 
            // FormLogWir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 598);
            this.Controls.Add(this.F_Clear);
            this.Controls.Add(this.F_Add);
            this.Controls.Add(this.F_Com);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.F_Client);
            this.Controls.Add(this.F_Agent);
            this.Controls.Add(this.F_Doc);
            this.Controls.Add(this.F_Number);
            this.Controls.Add(this.F_DateOper);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormLogWir";
            this.Text = "FormLogWir";
            this.Load += new System.EventHandler(this.FormLogWir_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker F_DateOper;
        private System.Windows.Forms.TextBox F_Number;
        private System.Windows.Forms.ComboBox F_Doc;
        private System.Windows.Forms.ComboBox F_Agent;
        private System.Windows.Forms.ComboBox F_Client;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox F_Com;
        private System.Windows.Forms.Button F_Add;
        private System.Windows.Forms.Button F_Clear;
    }
}