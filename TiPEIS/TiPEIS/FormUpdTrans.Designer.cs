namespace TiPEIS
{
    partial class FormUpdTrans
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
            this.F_KindTrans = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.F_Date = new System.Windows.Forms.DateTimePicker();
            this.F_summa = new System.Windows.Forms.TextBox();
            this.F_Save = new System.Windows.Forms.Button();
            this.F_Cancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.F_Contr = new System.Windows.Forms.ComboBox();
            this.F_Agent = new System.Windows.Forms.ComboBox();
            this.F_Client = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.F_term = new System.Windows.Forms.TextBox();
            this.F_Calc = new System.Windows.Forms.Button();
            this.F_Wirs = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.F_Wirs)).BeginInit();
            this.SuspendLayout();
            // 
            // F_KindTrans
            // 
            this.F_KindTrans.FormattingEnabled = true;
            this.F_KindTrans.Location = new System.Drawing.Point(251, 55);
            this.F_KindTrans.Name = "F_KindTrans";
            this.F_KindTrans.Size = new System.Drawing.Size(343, 28);
            this.F_KindTrans.TabIndex = 0;
            this.F_KindTrans.SelectedIndexChanged += new System.EventHandler(this.F_KindTrans_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Вид операции";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Дата";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 271);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Сумма";
            // 
            // F_Date
            // 
            this.F_Date.Location = new System.Drawing.Point(251, 188);
            this.F_Date.Name = "F_Date";
            this.F_Date.Size = new System.Drawing.Size(186, 26);
            this.F_Date.TabIndex = 4;
            // 
            // F_summa
            // 
            this.F_summa.Location = new System.Drawing.Point(251, 265);
            this.F_summa.Name = "F_summa";
            this.F_summa.ReadOnly = true;
            this.F_summa.Size = new System.Drawing.Size(130, 26);
            this.F_summa.TabIndex = 5;
            // 
            // F_Save
            // 
            this.F_Save.Location = new System.Drawing.Point(757, 296);
            this.F_Save.Name = "F_Save";
            this.F_Save.Size = new System.Drawing.Size(92, 35);
            this.F_Save.TabIndex = 6;
            this.F_Save.Text = "Save";
            this.F_Save.UseVisualStyleBackColor = true;
            this.F_Save.Click += new System.EventHandler(this.F_Save_Click);
            // 
            // F_Cancel
            // 
            this.F_Cancel.Location = new System.Drawing.Point(872, 296);
            this.F_Cancel.Name = "F_Cancel";
            this.F_Cancel.Size = new System.Drawing.Size(92, 35);
            this.F_Cancel.TabIndex = 7;
            this.F_Cancel.Text = "Cancel";
            this.F_Cancel.UseVisualStyleBackColor = true;
            this.F_Cancel.Click += new System.EventHandler(this.F_Cancel_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(725, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Договор";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(725, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Агент";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(725, 201);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "Клиент";
            // 
            // F_Contr
            // 
            this.F_Contr.FormattingEnabled = true;
            this.F_Contr.Location = new System.Drawing.Point(829, 42);
            this.F_Contr.Name = "F_Contr";
            this.F_Contr.Size = new System.Drawing.Size(186, 28);
            this.F_Contr.TabIndex = 11;
            // 
            // F_Agent
            // 
            this.F_Agent.FormattingEnabled = true;
            this.F_Agent.Location = new System.Drawing.Point(829, 119);
            this.F_Agent.Name = "F_Agent";
            this.F_Agent.Size = new System.Drawing.Size(186, 28);
            this.F_Agent.TabIndex = 12;
            // 
            // F_Client
            // 
            this.F_Client.FormattingEnabled = true;
            this.F_Client.Location = new System.Drawing.Point(829, 193);
            this.F_Client.Name = "F_Client";
            this.F_Client.Size = new System.Drawing.Size(186, 28);
            this.F_Client.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(298, 122);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "Срок";
            this.label7.Visible = false;
            // 
            // F_term
            // 
            this.F_term.Location = new System.Drawing.Point(442, 116);
            this.F_term.Name = "F_term";
            this.F_term.Size = new System.Drawing.Size(130, 26);
            this.F_term.TabIndex = 15;
            this.F_term.Visible = false;
            // 
            // F_Calc
            // 
            this.F_Calc.Location = new System.Drawing.Point(460, 265);
            this.F_Calc.Name = "F_Calc";
            this.F_Calc.Size = new System.Drawing.Size(112, 39);
            this.F_Calc.TabIndex = 16;
            this.F_Calc.Text = "Рассчитать";
            this.F_Calc.UseVisualStyleBackColor = true;
            this.F_Calc.Click += new System.EventHandler(this.F_Calc_Click);
            // 
            // F_Wirs
            // 
            this.F_Wirs.AllowUserToAddRows = false;
            this.F_Wirs.AllowUserToDeleteRows = false;
            this.F_Wirs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.F_Wirs.Location = new System.Drawing.Point(55, 389);
            this.F_Wirs.Name = "F_Wirs";
            this.F_Wirs.ReadOnly = true;
            this.F_Wirs.RowTemplate.Height = 28;
            this.F_Wirs.Size = new System.Drawing.Size(959, 116);
            this.F_Wirs.TabIndex = 17;
            // 
            // FormUpdTrans
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 535);
            this.Controls.Add(this.F_Wirs);
            this.Controls.Add(this.F_Calc);
            this.Controls.Add(this.F_term);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.F_Client);
            this.Controls.Add(this.F_Agent);
            this.Controls.Add(this.F_Contr);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.F_Cancel);
            this.Controls.Add(this.F_Save);
            this.Controls.Add(this.F_summa);
            this.Controls.Add(this.F_Date);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.F_KindTrans);
            this.Name = "FormUpdTrans";
            this.Text = "FormUpdTrans";
            this.Load += new System.EventHandler(this.FormUpdTrans_Load);
            ((System.ComponentModel.ISupportInitialize)(this.F_Wirs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox F_KindTrans;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker F_Date;
        private System.Windows.Forms.TextBox F_summa;
        private System.Windows.Forms.Button F_Save;
        private System.Windows.Forms.Button F_Cancel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox F_Contr;
        private System.Windows.Forms.ComboBox F_Agent;
        private System.Windows.Forms.ComboBox F_Client;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox F_term;
        private System.Windows.Forms.Button F_Calc;
        private System.Windows.Forms.DataGridView F_Wirs;
    }
}