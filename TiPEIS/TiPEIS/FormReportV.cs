using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TiPEIS
{
    public partial class FormReportV : Form
    {

        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private static string sPath = Path.Combine(Application.StartupPath, "mybd.db");
        string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";


        public FormReportV()
        {
            InitializeComponent();
        }

        private void F_Show_Click(object sender, EventArgs e)
        {
            string FromDate = F_From.Value.Date.ToString("yyyy.MM.dd");
            string ToDate = F_To.Value.Date.ToString("yyyy.MM.dd");

            if (Convert.ToDateTime(FromDate) > Convert.ToDateTime(ToDate))
            {
                MessageBox.Show("Проверьте дату");
                return;
            }


            Clear(dataGridView1);
            String selectCommand = "SELECT  C.FIO as SubcontoD2, A.FIO as SubcontoD1,  T.ContractId as SubcontoD3, T.Date, T.Summa FROM [LogTransaction] T left outer join Agent A ON T.AgentId = A.Id left outer join Client C ON T.ClientId= C.Id WHERE T.KindTransaction=2 and T.Date BETWEEN '" + FromDate + "' AND '" + ToDate + "'";
            selectTableDate2(ConnectionString, selectCommand);
            
        }

        private void selectTableDate(string connectionString, string selectCommand)
        {
            SQLiteConnection connect = new SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter sda = new SQLiteDataAdapter(selectCommand, connect);
            DataTable DATA = new DataTable();
            sda.Fill(DATA);
            dataGridView1.DataSource = DATA;
            connect.Close();
        }

        private void FormReportV_Load(object sender, EventArgs e)
        {

        }

        public void Clear(DataGridView dataGridView)
        {
            while (dataGridView.Rows.Count > 1)
                for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
                    dataGridView.Rows.Remove(dataGridView.Rows[i]);
        }

        public void selectTableDate2(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter sda = new SQLiteDataAdapter(selectCommand, connect);
            DataTable DATA = new DataTable();
            sda.Fill(DATA);

            foreach(DataRow row in DATA.Rows)
            {
                var idDoc = row.ItemArray[2];
                object maxstate = selectValue(ConnectionString, "SELECT MAX(KindTransaction) FROM LogTransaction WHERE ContractId=" + idDoc + ";").ToString();
                if (Convert.ToInt32(maxstate) == 3)
                {
                    row.Delete();
                }
                else
                {
                    object summa = selectValue(ConnectionString, "SELECT summa FROM Contract WHERE Id=" + idDoc + ";");
                    double sumDoc = CalcProc(Convert.ToInt32(idDoc)) + Convert.ToDouble(summa)+ Convert.ToDouble(summa);
                    row.ItemArray[4] = sumDoc.ToString();
                }
            }
            dataGridView1.DataSource = DATA;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                var idDoc = row.Cells[2].Value;
                object summa = selectValue(ConnectionString, "SELECT summa FROM Contract WHERE Id=" + idDoc + ";");
                   double sumDoc = CalcProc(Convert.ToInt32(idDoc)) + Convert.ToDouble(summa) /*+ Convert.ToDouble(summa)*/;
                row.Cells[4].Value = sumDoc;
            }

            connect.Close();
        }

        private double CalcProc(int idDoc) //
        {
            double res = 0;
           double sumDoc = Convert.ToDouble(selectValue(ConnectionString, "SELECT summa FROM Contract WHERE Id=" + idDoc + ";").ToString());
           double percent2 = Convert.ToDouble((selectValue(ConnectionString, "SELECT percent2 FROM Contract WHERE Id=" + idDoc + ";")).ToString().Replace(".", ","));

            if (percent2 != 0)
            {
                res = Convert.ToDouble(sumDoc) * percent2;
            }
            return res;
        }

        public object selectValue(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new
            SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteCommand command = new SQLiteCommand(selectCommand,
            connect);
            SQLiteDataReader reader = command.ExecuteReader();
            object value = "";
            while (reader.Read())
            {
                value = reader[0];
            }
            connect.Close();
            return value;
        }
    }
}
