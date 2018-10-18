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
    public partial class FormLogWir : Form
    {
        //LogWiring
        //int Id
        //double(5) Count
        //double(12,5) Summa
        //string Date
        //string content
        //int(100) subkontoDeb1/subkontoCred3


        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private string sPath = Path.Combine(Application.StartupPath, "mybd.db");


        public FormLogWir()
        {
            InitializeComponent();
        }

        private void ExecuteQuery(string txtQuery)
        {
            sql_con = new SQLiteConnection("Data Source=" + sPath + ";Version=3;New=False;Compress=True;");
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }

        public void selectTable(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(selectCommand, connect);
            //SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter("SELECT W.Id, W.Summa, W.Date, W.content, W.Deb, A.FIO, C.FIO, W.subkontoDeb3, W.Cred FROM [LogWiring] W JOIN Agent A ON W.subkontoDeb1 = A.Id JOIN Client C ON W.subkontoDeb2= C.Id ", connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = ds.Tables[0].ToString();
            connect.Close();
        }

        

        public object selectValue(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new
            SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteCommand command = new SQLiteCommand(selectCommand, connect);
            SQLiteDataReader reader = command.ExecuteReader();
            object value = "";
            while (reader.Read())
            {
                value = reader[0];
            }
            connect.Close();
            return value;
        }

        public void changeValue(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new
            SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteTransaction trans;
            SQLiteCommand cmd = new SQLiteCommand();
            trans = connect.BeginTransaction();
            cmd.Connection = connect;
            cmd.CommandText = selectCommand;
            cmd.ExecuteNonQuery();
            trans.Commit();
            connect.Close();
        }

        private void FormLogWir_Load(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
            String selectCommand = "Select * from LogWiring";
            selectTable(ConnectionString, selectCommand);
        }
        public void refreshForm(string ConnectionString, String selectCommand)
        {
            selectTable(ConnectionString, selectCommand);
            dataGridView1.Update();
            dataGridView1.Refresh();
            F_Com.Text = "";
        }
        private bool CheckValue()
        {
            if (F_Com.Text.Length >= 100)
            {
                MessageBox.Show("Слишком длинная входная строка. Введите не более 100 символов");
                return false;
            }
            return true;
        }
        private void F_Ok_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
            {
                return;
            }
            //выбрана строка CurrentRow
            int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
            //получить значение Name выбранной строки
            string valueId = dataGridView1[0, CurrentRow].Value.ToString();
            string changeName = F_Com.Text;
            //обновление Name
            if (CheckValue())
            {
                String selectCommand = "update LogWiring set content='" + changeName + "' where Id = " + valueId;
                string ConnectionString = @"Data Source=" + sPath +
                ";New=False;Version=3";
                changeValue(ConnectionString, selectCommand);
                //обновление dataGridView1
                selectCommand = "select * from LogWiring";
                refreshForm(ConnectionString, selectCommand);
                F_Com.Text = "";
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //выбрана строка CurrentRow
            int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
            //получить значение Name выбранной строки
            string com = dataGridView1[3, CurrentRow].Value.ToString();
            F_Com.Text = com;
        }
    }
}