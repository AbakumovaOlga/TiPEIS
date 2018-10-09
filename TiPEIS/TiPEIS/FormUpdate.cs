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
        //int Id !
        //data startDate !
        //int(10) term !
        //double(10,2) summa
        //int(10) termFact
        //data finishDate
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
                F_startDate.Value = Convert.ToDateTime(startDate.ToString());

                object term = selectValue(ConnectionString, "SELECT term FROM Contract WHERE Id=" + Id + ";");
                F_term.Text = term.ToString();

                object summa = selectValue(ConnectionString, "SELECT summa FROM Contract WHERE Id=" + Id + ";");
                F_summa.Text = summa.ToString();

                object termFact = selectValue(ConnectionString, "SELECT termFact FROM Contract WHERE Id=" + Id + ";");
                F_termFact.Text = termFact.ToString();

                object finishDate = selectValue(ConnectionString, "SELECT finishDate FROM Contract WHERE Id=" + Id + ";");
                if(finishDate.ToString()!="")
                F_finishDate.Value = Convert.ToDateTime(finishDate.ToString());

                object percent1 = selectValue(ConnectionString, "SELECT percent1 FROM Contract WHERE Id=" + Id + ";");
                F_Percent1.Text = percent1.ToString();

                object percent2 = selectValue(ConnectionString, "SELECT percent2 FROM Contract WHERE Id=" + Id + ";");
                F_Percent2.Text = percent2.ToString();

                if(Convert.ToInt32(termFact) > 0)
                {
                    F_done.Checked=true;
                }
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
            string startDate = F_startDate.Value.Date.ToString("yyyy.MM.dd");

            int term;
            Regex regexTerm = new Regex(@"^\d{1,7}$");
            if (F_term.Text == "")
            {
                MessageBox.Show("Заполните обязательные поля");
                return;
            }else if (F_term.Text.Length>7)
            {
                MessageBox.Show("Слишком большое число Срок по договору. Не более 7 символов");
                return;
            }
            else if (regexTerm.IsMatch(F_term.Text))
            {
                term = Convert.ToInt32(F_term.Text);
            }
            else
            {
                MessageBox.Show("Несоответсвие формату Срок по договору");
                return;
            }

            string summa;
            string s = F_summa.Text;
            s = s.Replace(",", ".");
            int k = s.IndexOf(".");
            Regex regexSumma = new Regex(@"^[0-9]{0,10}(?:[.,][0-9]{0,2})?\z");
            if (F_summa.Text == "")
            {
                MessageBox.Show("Заполните обязательные поля");
                return;
            }
            else if (s.IndexOf(".") != -1)
            {
                if (s.Substring(0, s.LastIndexOf('.')).Length > 11)
                {
                    MessageBox.Show("Слишком длинное число. Не более 11 символов");
                    return;
                }
                else
                {
                    if (regexSumma.IsMatch(F_summa.Text))
                    {
                        summa = F_summa.Text.Replace(",", ".");
                    }
                    else
                    {
                        MessageBox.Show("Несоответсвие формату Сумма");
                        return;
                    }
                }
            }
            else
            {
                if (s.Length >= 11)
                {
                    MessageBox.Show("Слишком длинное число сумма. Не более 11 символов");
                    return;
                }
                else
                {
                    if (regexSumma.IsMatch(F_summa.Text))
                    {
                        summa = F_summa.Text.Replace(",", ".");
                    }
                    else
                    {
                        MessageBox.Show("Несоответсвие формату Сумма");
                        return;
                    }
                }
            }

            int termFact;
            string finishDate;
            if (F_done.Checked) {
                finishDate = F_finishDate.Value.Date.ToString("yyyy.MM.dd");

                if(F_startDate.Value.Date > F_finishDate.Value.Date)
                {
                    MessageBox.Show("Проверьте дату");
                    return;
                }

                if (F_termFact.Text == "")
                {
                    termFact = 0;
                }
                else if (F_termFact.Text.Length > 7)
                {
                    MessageBox.Show("Слишком большое число Фактический срок. Не более 7 символов");
                    return;
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

            }
            else
            {
                termFact = 0;
                finishDate = "";
            }


            Regex regexPer = new Regex(@"^[0-1]{1}(?:[.,][0-9]{0,2})?\z");

            string percent1;
            if (F_Percent1.Text == "")
            {
                percent1 = "0";
            }
            else if (F_Percent1.Text.Length > 4)
            {
                MessageBox.Show("Слишком большое число  Процент1. Не более 4 символов");
                return;
            }
            else if (regexPer.IsMatch(F_Percent1.Text))
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
            else if (F_Percent2.Text.Length > 4)
            {
                MessageBox.Show("Слишком большое число  Процент2. Не более 4 символов");
                return;
            }
            else if (regexPer.IsMatch(F_Percent2.Text))
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
                  "startDate='" + startDate + "'" +
                    ", term=" + term +
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

        private void F_done_CheckedChanged(object sender, EventArgs e)
        {
            if (F_done.Checked)
            {
                F_finishDate.Enabled = true;
                F_termFact.Enabled = true;
            }
        }

        private void F_finishDate_ValueChanged(object sender, EventArgs e)
        {
            F_termFact.Text = (F_finishDate.Value.Date - F_startDate.Value.Date).TotalDays.ToString();
        }
    }
}
