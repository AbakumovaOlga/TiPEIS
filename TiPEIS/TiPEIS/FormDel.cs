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
    public partial class FormDel : Form
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private static string sPath = Path.Combine(Application.StartupPath, "mybd.db");

        int id;
        string table;

        string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";

        public FormDel(int Id, string Table)
        {
            InitializeComponent();
            id = Id;
            table = Table;
        }

        private void FormDel_Load(object sender, EventArgs e)
        {
            try
            {
                String selectCommand = "select * from LogTransaction " + "where " + table + "Id=" + id;
                selectTable(ConnectionString, selectCommand);
            }
            catch { }
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
        public void refreshForm(string ConnectionString, String selectCommand)
        {
            selectTable(ConnectionString, selectCommand);
            dataGridView1.Update();
            dataGridView1.Refresh();
        }
        public void selectTable(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = ds.Tables[0].ToString();
            connect.Close();
        }

        private void button1_Click(object sender, EventArgs e)
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


            try
            {
                String selectCommand2 = "select * from LogTransaction " + "where " + table + "Id=" + id;
                selectTable(ConnectionString, selectCommand2);
            }
            catch { }
        }
    }
}
