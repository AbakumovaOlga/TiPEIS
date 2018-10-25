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
    public partial class FormLogTransaction : Form
    {

        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private static string sPath = Path.Combine(Application.StartupPath, "mybd.db");
        string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";

        public FormLogTransaction()
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

        public void refreshForm(string ConnectionString, String selectCommand)
        {
            selectTable(ConnectionString, selectCommand);
            dataGridView1.Update();
            dataGridView1.Refresh();
        }

        public void selectTable(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new SQLiteConnection(ConnectionString);
            /*connect.Open();
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = ds.Tables[0].ToString();
            connect.Close();*/
            connect.Open();
            SQLiteDataAdapter sda = new SQLiteDataAdapter("SELECT T.Id, T.KindTransaction, T.Date, T.Summa, A.FIO, C.FIO, T.ContractId FROM [LogTransaction] T left outer join Agent A ON T.AgentId = A.Id left outer join Client C ON T.ClientId= C.Id ", connect);
            DataTable DATA = new DataTable();
            sda.Fill(DATA);
            dataGridView1.DataSource = DATA;
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


        private void FormLogTransaction_Load(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
            String selectCommand = "Select * from LogTransaction";
            selectTable(ConnectionString, selectCommand);
            dataGridView1.Columns[4].HeaderText = "Agent FIO";
            dataGridView1.Columns[5].HeaderText = "Client FIO";
            dataGridView1.Columns[6].HeaderText = "Contract №";
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
            {
                return;
            }
            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            var form = new FormUpdTrans(id);
            form.Show();
        }

        private void F_Reload_Click(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
            String selectCommand = "Select * from LogTransaction";
            selectTable(ConnectionString, selectCommand);
        }

        private void F_Create_Click(object sender, EventArgs e)
        {
            var form = new FormUpdTrans();
            form.Show();
        }

        private void F_Delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
            {
                return;
            }
            // выбрана строка CurrentRow
            int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
            //получить значение idAgent выбранной строки
            string valueId = dataGridView1[0, CurrentRow].Value.ToString();

            //убираем завершение договора
            object kind = selectValue(ConnectionString, "select KindTransaction from LogTransaction where Id=" + valueId);
            if (kind.ToString() == "Закрытие договора - 2")
            {
                object doc = selectValue(ConnectionString, "select ContractId from LogTransaction where Id=" + valueId);
                String selectCommandUpdDoc = "update Contract set finishDate='', termFact=" + 0 + " where Id = " + Convert.ToInt32(doc);
                changeValue(ConnectionString, selectCommandUpdDoc);

                MessageBox.Show("Договор изменен");
            }

            //удаление операции
            String selectCommand = "delete from LogTransaction where Id=" + valueId;
            changeValue(ConnectionString, selectCommand);
            MessageBox.Show("Удалена операция");

            //удаление проводки
            String selectCommandDelWir = "delete from LogWiring where LogTrId=" + valueId;
            changeValue(ConnectionString, selectCommandDelWir);
            MessageBox.Show("Удалена проводка");



            //обновление dataGridView1
            selectCommand = "select * from LogTransaction";
            refreshForm(ConnectionString, selectCommand);
        }

        private void F_Show_Click(object sender, EventArgs e)
        {
            string FromDate = F_From.Value.Date.ToString("yyyy.MM.dd");
            string ToDate = F_To.Value.Date.ToString("yyyy.MM.dd");

            string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
            String selectCommand = "Select * from LogTransaction WHERE Date BETWEEN " + FromDate + " AND " + ToDate;
            selectTable(ConnectionString, selectCommand);
        }
    }
}
