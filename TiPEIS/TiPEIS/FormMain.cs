﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TiPEIS
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void планСчетовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormChartAcc();
            form.Show();
        }

        private void агентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormAgent();
            form.Show();
        }

        private void основныеСредстваToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormContract();
            form.Show();
        }

        private void клиентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormClient();
            form.Show();
        }

        private void отчетыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormLogTransaction();
            form.Show();
        }

        private void журналПроводокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormLogWir();
            form.Show();
        }

        private void суммаВозвратаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormReportV();
            form.Show();
        }

        private void процентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormReportP();
            form.Show();
        }
    }
}
