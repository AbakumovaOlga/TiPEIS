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

        public void refreshForm(string ConnectionString)
        {
            selectTable(ConnectionString);
            dataGridView1.Update();
            dataGridView1.Refresh();
        }

        public void selectTable(string ConnectionString)
        {
            try
            {
                SQLiteConnection connect = new
                SQLiteConnection(ConnectionString);
                connect.Open();
                SQLiteDataAdapter dataAdapter = new
                SQLiteDataAdapter("Select Id, Date, DT, SubkontoDT1, SubkontoDT2, SubkontoDT3, KT, SubkontoKT1, SubkontoKT2, SubkontoKT3, Count, Summa, IDOperation, Comment from LogWiring", connect);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = ds.Tables[0].ToString();
                connect.Close();
                dataGridView1.Columns["IDJournalEntries"].DisplayIndex = 0;
                dataGridView1.Columns["Date"].DisplayIndex = 1;
                dataGridView1.Columns["DT"].DisplayIndex = 2;
                dataGridView1.Columns["SubkontoDT1"].DisplayIndex = 3;
                dataGridView1.Columns["SubkontoDT2"].DisplayIndex = 4;
                dataGridView1.Columns["SubkontoDT3"].DisplayIndex = 5;
                dataGridView1.Columns["KT"].DisplayIndex = 6;
                dataGridView1.Columns["SubkontoKT1"].DisplayIndex = 7;
                dataGridView1.Columns["SubkontoKT2"].DisplayIndex = 8;
                dataGridView1.Columns["SubkontoKT3"].DisplayIndex = 9;
                dataGridView1.Columns["Count"].DisplayIndex = 10;
                dataGridView1.Columns["Summa"].DisplayIndex = 11;
                dataGridView1.Columns["IDOperation"].DisplayIndex = 12;
                dataGridView1.Columns["Comment"].DisplayIndex = 13;
            }
            catch
            {
                MessageBox.Show("Произошла ошибка");
            }
        }

        public void selectCombo(string ConnectionString, String selectCommand, ComboBox comboBox, string displayMember, string valueMember)
        {
            SQLiteConnection connect = new
            SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new
            SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            comboBox.DataSource = ds.Tables[0];
            comboBox.DisplayMember = displayMember;
            comboBox.ValueMember = valueMember;
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
            selectTable(ConnectionString);
            // выбрать значения из справочников для отображения в comboBox
            /*String selectOS = "Select idOS, Name from Contract";
            selectCombo(ConnectionString, selectOS, F_Doc, "Name", "idOS");*/
            String selectSubd = "SELECT idSubdivision, Name FROM Subdivision";
            selectCombo(ConnectionString, selectSubd, F_Agent, "Name", "idSubdivision");
            String selectMOL = "SELECT idMOL, Name FROM MOL";
            selectCombo(ConnectionString, selectMOL, F_Client, "Name", "idMOL");
            F_Doc.SelectedIndex = -1;
            F_Agent.SelectedIndex = -1;
            F_Client.SelectedIndex = -1;
        }

        private void F_Add_Click(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
            // находим максимальное значение кода проводок для записи первичного ключа
            String mValue = "select MAX(IDJournalEntries) from JournalEntries";
            object maxValue = selectValue(ConnectionString, mValue);
            if (Convert.ToString(maxValue) == "")
                maxValue = 0;
            // Обнулить значения переменных
            string sum = "0";
            string count = "0";
            string coment = null;
            string Value1 = null;
            string Value2 = null;
            string Value3 = null;
            if (F_Doc.Text != "")
            {
                //ОС
                Value1 = F_Doc.SelectedValue.ToString();
            }
            if (F_Agent.Text != "")
            {
                //Подразделение
                Value2 = F_Agent.SelectedValue.ToString();
            }
            if (F_Client.Text != "")
            {
                //МОЛ
                Value3 = F_Client.SelectedValue.ToString();
            }

            //Поле количество
            if (F_Number.Text != "")
            {
                count = F_Number.Text;
            }
            //Поиск по базе данных значений
            String selectCost = "select Cost from OS where IdOS='''" + Value1 + "'''";
            double Summa = Convert.ToDouble(selectCost) *
            Convert.ToDouble(count);
            String selectDT = "select idChart from ChartOfAccounts where Account = '''01'''";
            object DT = selectValue(ConnectionString, selectDT);
            String selectKT = "select idChart from ChartOfAccounts where Account = '''00'''";
            object KT = selectValue(ConnectionString, selectKT);
            string add = "INSERT INTO JournalEntries (IDJournalEntries, Date, DT, " + "SubkontoDT1, SubkontoDT2, SubkontoDT3, KT, Count, Summa, IDOperation, Comment) VALUES(" + (Convert.ToInt32(maxValue) + 1) +/* dataTimePicker1.Text + */"'," + DT.ToString() + "," + Convert.ToInt32(Value1) + "," + Convert.ToInt32(Value2) + "," + Convert.ToInt32(Value3) + "," + KT.ToString() + "," + Convert.ToDouble(count) + "," + Summa + ", " + Convert.ToInt32(F_Number.Text) + ", " + ",'Поступление ОС','" + ")";
            ExecuteQuery(add);
            selectTable(ConnectionString);
        }


        private void F_Clear_Click(object sender, EventArgs e)
        {
            F_Doc.SelectedIndex = -1;
            F_Agent.SelectedIndex = -1;
            F_Client.SelectedIndex = -1;
            F_Number.Clear();
            F_Com.Clear();
        }
    }
}