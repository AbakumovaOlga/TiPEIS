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
    public partial class FormUpdTrans : Form
    {
        //LogTransaction
        //int Id
        //string(15) KindTransaction
        //string Date
        //double(10,2) Summa

        public int Id;

        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private string sPath = Path.Combine(Application.StartupPath, "mybd.db");

        public FormUpdTrans()
        {
            InitializeComponent();
        }

        public FormUpdTrans(int id)
        {
            InitializeComponent();
            Id = id;
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

        private void FormUpdTrans_Load(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
            String selectAgents = "SELECT Id, FIO FROM Agent";
            selectCombo(ConnectionString, selectAgents, F_Agent, "FIO", "Id");

            String selectClients = "SELECT Id, FIO FROM Client";
            selectCombo(ConnectionString, selectClients, F_Client, "FIO", "Id");

            String selectContracts = "SELECT Id FROM Contract";
            selectCombo(ConnectionString, selectContracts, F_Contr, "Id", "Id");

            F_KindTrans.DataSource = new String[] { "Заключение договора - 0", "Перечисление суммы займа - 1", "Закрытие договора - 2", "Возврат займа - 3" };

        }

        private void F_Save_Click(object sender, EventArgs e)
        {
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

            int idDoc = Convert.ToInt32(F_Contr.SelectedValue.ToString());

            int idAgent = Convert.ToInt32(F_Agent.SelectedValue.ToString());

            int idClient = Convert.ToInt32(F_Client.SelectedValue.ToString());

            string Date = F_Date.Value.Date.ToString("yyyy.MM.dd");

            int kind = F_KindTrans.SelectedIndex;
            switch (kind)
            {
                case 0:
                    CreateDoc(Date, summa);
                    break;
                case 1:
                    PayDoc();
                    break;
                case 2:
                    CloseDoc(idDoc, Date, summa);
                    break;
                case 3:
                    RetDoc();
                    break;
            }
        }

        private void RetDoc()
        {
            /*операция возврата денежных средств по ранее
            закрытым договорам с формированием записей с кредита счета 58 в дебет
            счетов денежных средств на сумму займа, увеличенную на сумму начисленного
            процента по договору.*/
            throw new NotImplementedException();
        }

        private void CloseDoc(int idDoc, string finishDate, string summa)
        {
            /*По истечении (до истечения) срока договора
            формируется проводка на сумму начисленного процента1 (процент2) в кредит
            счета 84 «Нераспределенная прибыль (непокрытый убыток)» по статье:
            «Проценты по договорам займа» и дебет счета 58.
            Сумма проводки зависит от даты проводки, если закрытие осуществляется
            после истечения срока займа, то
            СуммаПроводки=Договор.Процент1*Договор.СуммаДоговора, иначе
            СуммаПроводки=Договор.Процент2*Договор.СуммаДоговора.*/

            string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
            string startDate = selectValue(ConnectionString, "SELECT startDate FROM Contract WHERE Id=" + idDoc + ";").ToString();
            int term = Convert.ToInt32(selectValue(ConnectionString, "SELECT term FROM Contract WHERE Id=" + idDoc + ";").ToString());

            double totalSum= Convert.ToDouble(summa);
            /*object t = selectValue(ConnectionString, "SELECT term FROM Contract WHERE Id=" + idDoc + ";");
            int term = Convert.ToInt32(t);*/
           /* string p= selectValue(ConnectionString, "SELECT percent1 FROM Contract WHERE Id=" + idDoc + ";").ToString();
            string pr = p.Replace(".", ",");
            double percent1 = Convert.ToDouble(pr);*/
            double percent1 = Convert.ToDouble((selectValue(ConnectionString, "SELECT percent1 FROM Contract WHERE Id=" + idDoc + ";")).ToString().Replace(".", ","));
            int percent2 = Convert.ToInt32(selectValue(ConnectionString, "SELECT percent2 FROM Contract WHERE Id=" + idDoc + ";").ToString());

            if (Convert.ToDateTime(startDate) > Convert.ToDateTime(finishDate))
            {
                MessageBox.Show("Проверьте дату");
                return;
            }

            if ((Convert.ToDateTime(finishDate) - Convert.ToDateTime(startDate)).TotalDays > term) //финиш-старт>срока -> срок истек
            {
                if(percent1!=0)
                totalSum = Convert.ToDouble(summa) * percent1;
            }

            MessageBox.Show("Общая сумма проводки "+ totalSum);

            throw new NotImplementedException();
        }

        private void PayDoc()
        {
            /*Формируется запись в дебет счета 58 с 
              кредита счета денежных средств на сумму перечисления. Каждый вид займа 
              учитывается на отдельном субсчете. Проводка должна быть сформирована 
              автоматически после ввода даты операции и выбора договора займа.*/


            throw new NotImplementedException();
        }

        private void CreateDoc(string startDate, string summa)
        {
            int term;
            Regex regexTerm = new Regex(@"^\d{1,7}$");
            if (F_term.Text == "")
            {
                MessageBox.Show("Заполните поле срок");
                return;
            }
            else if (F_term.Text.Length > 7)
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
            string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
            String selectCommand = "select MAX(Id) from Contract";
            object maxValue = selectValue(ConnectionString, selectCommand);
            if (Convert.ToString(maxValue) == "")
                maxValue = 0;
            string txtSQLQuery = "insert into Contract (Id, startDate,term,summa,termFact,finishDate,percent1,percent2) values (" +
                (Convert.ToInt32(maxValue) + 1) + ", '" + startDate + "', " + term + ", " + summa + ", " + 0 + ", '" + "" + "', " + 0 + ", " + 0 + ")";
            ExecuteQuery(txtSQLQuery);
            MessageBox.Show("Успешно");
        }

        private void F_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void F_KindTrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (F_KindTrans.SelectedIndex == 0)
            {
                F_term.Visible = true;
                label7.Visible = true;
            }
            else
            {
                F_term.Visible = false;
                label7.Visible = false;
            }
        }
    }
}
