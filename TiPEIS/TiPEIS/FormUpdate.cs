using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TiPEIS
{
    public partial class FormUpdate : Form
    {
        //int Id
        //string startDate
        //int(10) term
        //double(10,2) summa в бд 15
        //int(10) termFact
        //string finishDate
        //double(10,2) percent1
        //double(10,2) percent2

        public int Id;

        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private string sPath = Path.Combine(Application.StartupPath, "mybd.db");

        public FormUpdate(int id)
        {
            InitializeComponent();
            Id = id;
        }

        public FormUpdate()
        {
            InitializeComponent();
        }

        private void FormUpdate_Load(object sender, EventArgs e)
        {
            if (Id != 0)
            {
                string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";

                object startDate = selectValue(ConnectionString, "SELECT startDate FROM Contract WHERE Id=" + Id + ";");
                F_dateStart.Text = startDate.ToString();

                object term = selectValue(ConnectionString, "SELECT term FROM Contract WHERE Id=" + Id + ";");
                F_term.Text = term.ToString();

                object summa = selectValue(ConnectionString, "SELECT summa FROM Contract WHERE Id=" + Id + ";");
                F_summa.Text = summa.ToString();

                object termFact = selectValue(ConnectionString, "SELECT termFact FROM Contract WHERE Id=" + Id + ";");
                F_termFact.Text = termFact.ToString();

                object finishDate = selectValue(ConnectionString, "SELECT finishDate FROM Contract WHERE Id=" + Id + ";");
                F_dateFinish.Text = finishDate.ToString();

                object percent1 = selectValue(ConnectionString, "SELECT percent1 FROM Contract WHERE Id=" + Id + ";");
                F_Percent1.Text = percent1.ToString();

                object percent2 = selectValue(ConnectionString, "SELECT percent2 FROM Contract WHERE Id=" + Id + ";");
                F_Percent2.Text = percent2.ToString();
            }
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

        private void ExecuteQuery(string txtQuery)
        {
            sql_con = new SQLiteConnection("Data Source=" + sPath + ";Version=3;New=False;Compress=True;");
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }

        private void F_Save_Click(object sender, EventArgs e)
        {
            string startDate = F_dateStart.Text;

            int term;
            Regex regexTerm = new Regex(@"^\d{1,10}$");
            if (F_term.Text == "")
            {
                term = 0;
            }
            else if (regexTerm.IsMatch(F_term.Text))
            {
                term = Convert.ToInt32(F_term.Text);
            }
            else
            {
                MessageBox.Show("Несоответсвие формату Срок по догвору");
                return;
            }
            
            string summa;
            Regex regexSumma = new Regex(@"^[0-9]{0,10}(?:[.,][0-9]{0,2})?\z");
            if (F_summa.Text == "")
            {
                summa = "0";
            }
            else if(regexSumma.IsMatch(F_summa.Text))
            {
                summa = F_summa.Text.Replace(",", ".");

                //summa = Convert.ToDouble(s);
            }
            else
            {
                MessageBox.Show("Несоответсвие формату Сумма");
                return;
            }


            int termFact;
            
            if (F_termFact.Text == "")
            {
                termFact = 0;
            }
            else if (regexTerm.IsMatch(F_termFact.Text))
            {
                termFact = Convert.ToInt32(F_termFact.Text);
            }
            else
            {
                MessageBox.Show("Несоответсвие формату Фактический срок");
                return;
            }

            string finishDate = F_dateFinish.Text;

            string percent1;
            if (F_Percent1.Text == "")
            {
                percent1 = "0";
            }
            else if(regexSumma.IsMatch(F_Percent1.Text))
            {
                percent1 = F_Percent1.Text.Replace(",", ".");
            }
            else
            {
                MessageBox.Show("Несоответсвие формату Процент1");
                return;
            }

            string percent2;
            if (F_Percent2.Text == "")
            {
                percent2 = "0";
            }
            else if(regexSumma.IsMatch(F_Percent2.Text))
            {
                percent2 = F_Percent2.Text.Replace(",", ".");
            }
            else
            {
                MessageBox.Show("Несоответсвие формату Процент2");
                return;
            }

            //обновление 
            if (Id != 0)
            {
                String selectCommand = "update Contract set " +
                    "startDate='" + startDate +
                    "', term=" + term +
                    ", summa=" + summa +
                    ", termFact=" + termFact +
                    ", finishDate='" + finishDate +
                    "', percent1=" + percent1 +
                    ", percent2=" + percent2
                    + " where Id = " + Id;
                string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
                changeValue(ConnectionString, selectCommand);
            }
            //создание
            else
            {
                string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
                String selectCommand = "select MAX(Id) from Contract";
                object maxValue = selectValue(ConnectionString, selectCommand);
                if (Convert.ToString(maxValue) == "")
                    maxValue = 0;
                string txtSQLQuery = "insert into Contract (Id, startDate,term,summa,termFact,finishDate,percent1,percent2) values (" +
            (Convert.ToInt32(maxValue) + 1) + ", '" + startDate + "', " + term + ", " + summa + ", " + termFact + ", '" + finishDate + "', " + percent1 + ", " + percent2 + ")";
                ExecuteQuery(txtSQLQuery);
            }
            MessageBox.Show("Успешно");
        }
    }
}
