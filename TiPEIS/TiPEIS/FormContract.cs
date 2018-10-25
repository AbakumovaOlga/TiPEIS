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
    public partial class FormContract : Form
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private static string sPath = Path.Combine(Application.StartupPath, "mybd.db");
        string ConnectionString = @"Data Source=" + sPath +
            ";New=False;Version=3";

        public FormContract()
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
            connect.Open();
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(selectCommand, connect);
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

        private void FormContract_Load(object sender, EventArgs e)
        {
           string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
            String selectCommand = "Select * from Contract";
            selectTable(ConnectionString, selectCommand);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count!=1)
            {
                return;
            }
            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            var form = new FormUpdate(id);
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new FormUpdate();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
            String selectCommand = "Select * from Contract";
            selectTable(ConnectionString, selectCommand);
        }

        private void F_Delete_Click(object sender, EventArgs e)
        {
            Dop();
            // НЕ УДАЛЯТЬ
            //если удален доп
            /*
            if (dataGridView1.SelectedRows.Count != 1)
            {
                return;
            }
            // выбрана строка CurrentRow
            int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
            //получить значение idAgent выбранной строки
            string valueId = dataGridView1[0, CurrentRow].Value.ToString();
            String selectCommand = "delete from Contract where Id=" + valueId;
            string ConnectionString = @"Data Source=" + sPath +
            ";New=False;Version=3";
            changeValue(ConnectionString, selectCommand);
            //обновление dataGridView1
            selectCommand = "select * from Contract";
            refreshForm(ConnectionString, selectCommand);*/
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
        private void Dop()
        {
            int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
            //получить значение idAgent выбранной строки
            string valueId = dataGridView1[0, CurrentRow].Value.ToString();
            object res = selectValue(ConnectionString, "SELECT Id FROM LogTransaction WHERE ContractId=" + valueId + ";");
            if (res.ToString() != "")
            {
                var form = new FormDel(Convert.ToInt32(valueId), "Contract");
                form.Show();
            }
            else
            {
                if (dataGridView1.SelectedRows.Count != 1)
                {
                    return;
                }

                String selectCommand = "delete from Contract where Id=" + valueId;
                string ConnectionString = @"Data Source=" + sPath +
                ";New=False;Version=3";
                changeValue(ConnectionString, selectCommand);
                //обновление dataGridView1
                selectCommand = "select * from Contract";
                refreshForm(ConnectionString, selectCommand);
            }
        }
        }
}
