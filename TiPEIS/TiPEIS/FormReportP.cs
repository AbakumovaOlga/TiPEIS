using System;
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
    public partial class FormReportP : Form
    {

        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private static string sPath = Path.Combine(Application.StartupPath, "mybd.db");
        string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";


        //closedoc 2

        public FormReportP()
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

            String selectCommand = "SELECT  C.FIO as SubcontoD2,  T.ContractId as SubcontoD3, T.Date, T.Summa FROM [LogTransaction] T left outer join Agent A ON T.AgentId = A.Id left outer join Client C ON T.ClientId= C.Id WHERE T.KindTransaction=2 and T.Date BETWEEN '" + FromDate + "' AND '" + ToDate + "'";
            selectTableDate(ConnectionString, selectCommand);
            


            string selectSum = "select SUM (Summa) from LogTransaction WHERE KindTransaction=2 and Date BETWEEN '" + FromDate + "' AND '" + ToDate + "'";
            object sum = selectValue(ConnectionString, selectSum);
            F_sum.Text = "Итого:" + sum.ToString();
        }

        

        private void FormReportP_Load(object sender, EventArgs e)
        {
        }

        public void Clear(DataGridView dataGridView)
        {
            while (dataGridView.Rows.Count > 1)
                for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
                    dataGridView.Rows.Remove(dataGridView.Rows[i]);
        }

        public void comboBoxColumn(string ConnectionString, String selectCommand, string valueMember, string displayMember)
        {
            SQLiteConnection connect = new
            SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new
            SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            BindingSource licorgSource = new BindingSource();
            licorgSource.DataSource = ds;
            licorgSource.DataMember = ds.Tables[0].ToString();
            connect.Close();
            DataGridViewComboBoxColumn comboColumn = new
            DataGridViewComboBoxColumn();
            comboColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            comboColumn.FlatStyle = FlatStyle.Popup;
            comboColumn.DataSource = licorgSource;
            comboColumn.DisplayMember = displayMember;
            comboColumn.ValueMember = valueMember;
            dataGridView1.Columns.Add(comboColumn);
        }
        //выбираем источник данных для заполнения таблицы
        public void selectTable(string ConnectionString, String selectdate)
        {
            SQLiteConnection connect = new
            SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new
            SQLiteDataAdapter(selectdate, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = ds.Tables[0].ToString();
            connect.Close();
            dataGridView1.ColumnHeadersVisible = true;
            dataGridView1.Columns["SubkontoDT1"].DisplayIndex = 0;
            dataGridView1.Columns["SubkontoDT2"].DisplayIndex = 1;
            dataGridView1.Columns["SubkontoDT3"].DisplayIndex = 2;
            dataGridView1.Columns["Count"].DisplayIndex = 3;
            dataGridView1.Columns["Summa"].DisplayIndex = 4;
        }

        //выполняем команду для запроса
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
        public void selectTableDate(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter sda = new SQLiteDataAdapter(selectCommand, connect);
            DataTable DATA = new DataTable();
            sda.Fill(DATA);
            dataGridView1.DataSource = DATA;
            connect.Close();
        }
    }
}
