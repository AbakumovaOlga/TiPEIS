namespace TiPEIS
{
    partial class FormUpdate
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
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.F_term = new System.Windows.Forms.TextBox();
            this.F_summa = new System.Windows.Forms.MaskedTextBox();
            this.F_termFact = new System.Windows.Forms.TextBox();
            this.F_Percent2 = new System.Windows.Forms.MaskedTextBox();
            this.F_Save = new System.Windows.Forms.Button();
            this.F_Percent1 = new System.Windows.Forms.MaskedTextBox();
            this.F_startDate = new System.Windows.Forms.DateTimePicker();
            this.F_finishDate = new System.Windows.Forms.DateTimePicker();
            this.F_done = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Дата начала договора *";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 180);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(202, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Срок займа по договору *";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 270);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Сумма договора *";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(277, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(199, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Фактический срок займа";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(277, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(250, 20);
            this.label5.TabIndex = 5;
            this.label5.Text = "Фактическая дата завершения";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(580, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(189, 20);
            this.label6.TabIndex = 6;
            this.label6.Text = "Начисленный процент1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(580, 179);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(189, 20);
            this.label7.TabIndex = 7;
            this.label7.Text = "Начисленный процент2";
            // 
            // F_term
            // 
            this.F_term.Location = new System.Drawing.Point(25, 203);
            this.F_term.Name = "F_term";
            this.F_term.Size = new System.Drawing.Size(200, 26);
            this.F_term.TabIndex = 8;
            // 
            // F_summa
            // 
            this.F_summa.Location = new System.Drawing.Point(25, 293);
            this.F_summa.Name = "F_summa";
            this.F_summa.Size = new System.Drawing.Size(200, 26);
            this.F_summa.TabIndex = 9;
            // 
            // F_termFact
            // 
            this.F_termFact.Enabled = false;
            this.F_termFact.Location = new System.Drawing.Point(281, 203);
            this.F_termFact.Name = "F_termFact";
            this.F_termFact.Size = new System.Drawing.Size(200, 26);
            this.F_termFact.TabIndex = 11;
            // 
            // F_Percent2
            // 
            this.F_Percent2.Location = new System.Drawing.Point(584, 202);
            this.F_Percent2.Name = "F_Percent2";
            this.F_Percent2.Size = new System.Drawing.Size(200, 26);
            this.F_Percent2.TabIndex = 13;
            // 
            // F_Save
            // 
            this.F_Save.Location = new System.Drawing.Point(388, 409);
            this.F_Save.Name = "F_Save";
            this.F_Save.Size = new System.Drawing.Size(75, 23);
            this.F_Save.TabIndex = 16;
            this.F_Save.Text = "Save";
            this.F_Save.UseVisualStyleBackColor = true;
            this.F_Save.Click += new System.EventHandler(this.F_Save_Click);
            // 
            // F_Percent1
            // 
            this.F_Percent1.Location = new System.Drawing.Point(584, 112);
            this.F_Percent1.Name = "F_Percent1";
            this.F_Percent1.Size = new System.Drawing.Size(200, 26);
            this.F_Percent1.TabIndex = 18;
            // 
            // F_startDate
            // 
            this.F_startDate.Location = new System.Drawing.Point(25, 112);
            this.F_startDate.Name = "F_startDate";
            this.F_startDate.Size = new System.Drawing.Size(200, 26);
            this.F_startDate.TabIndex = 20;
            // 
            // F_finishDate
            // 
            this.F_finishDate.Enabled = false;
            this.F_finishDate.Location = new System.Drawing.Point(281, 112);
            this.F_finishDate.Name = "F_finishDate";
            this.F_finishDate.Size = new System.Drawing.Size(200, 26);
            this.F_finishDate.TabIndex = 21;
            this.F_finishDate.ValueChanged += new System.EventHandler(this.F_finishDate_ValueChanged);
            // 
            // F_done
            // 
            this.F_done.AutoSize = true;
            this.F_done.Location = new System.Drawing.Point(315, 35);
            this.F_done.Name = "F_done";
            this.F_done.Size = new System.Drawing.Size(122, 24);
            this.F_done.TabIndex = 22;
            this.F_done.Text = "Завершено";
            this.F_done.UseVisualStyleBackColor = true;
            this.F_done.CheckedChanged += new System.EventHandler(this.F_done_CheckedChanged);
            // 
            // FormUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.F_done);
            this.Controls.Add(this.F_finishDate);
            this.Controls.Add(this.F_startDate);
            this.Controls.Add(this.F_Percent1);
            this.Controls.Add(this.F_Save);
            this.Controls.Add(this.F_Percent2);
            this.Controls.Add(this.F_termFact);
            this.Controls.Add(this.F_summa);
            this.Controls.Add(this.F_term);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormUpdate";
            this.Text = "FormUpdate";
            this.Load += new System.EventHandler(this.FormUpdate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox F_term;
        private System.Windows.Forms.MaskedTextBox F_summa;
        private System.Windows.Forms.TextBox F_termFact;
        private System.Windows.Forms.MaskedTextBox F_Percent2;
        private System.Windows.Forms.Button F_Save;
        private System.Windows.Forms.MaskedTextBox F_Percent1;
        private System.Windows.Forms.DateTimePicker F_startDate;
        private System.Windows.Forms.DateTimePicker F_finishDate;
        private System.Windows.Forms.CheckBox F_done;
    }
}