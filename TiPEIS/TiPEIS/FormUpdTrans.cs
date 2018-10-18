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
        //агент, клиент. договор

        private int Id;
        private string[] kinds = new string[] { "Заключение договора - 0", "Перечисление суммы займа - 1", "Закрытие договора - 2", "Возврат займа - 3" };

        int idDoc;
        int idAgent;
        int idClient;
        string Date;
        int kind;

        string startDate;
        int term;
        double sumDoc;
        double percent1;
        double percent2;


        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private string sPath = Path.Combine(Application.StartupPath, "mybd.db");


        string ConnectionString;

        public FormUpdTrans()
        {
            InitializeComponent();
            ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
        }

        public FormUpdTrans(int id)
        {
            InitializeComponent();
            ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
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

        private void FormUpdTrans_Load(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";

            String selectAgents = "SELECT Id, FIO FROM Agent";
            selectCombo(ConnectionString, selectAgents, F_Agent, "FIO", "Id");

            String selectClients = "SELECT Id, FIO FROM Client";
            selectCombo(ConnectionString, selectClients, F_Client, "FIO", "Id");

            String selectContracts = "SELECT Id FROM Contract";
            selectCombo(ConnectionString, selectContracts, F_Contr, "Id", "Id");

            F_KindTrans.DataSource = kinds;
        }

        private void F_Save_Click(object sender, EventArgs e)
        {
            idDoc = Convert.ToInt32(F_Contr.SelectedValue.ToString());

            idAgent = Convert.ToInt32(F_Agent.SelectedValue.ToString());

            idClient = Convert.ToInt32(F_Client.SelectedValue.ToString());

            Date = F_Date.Value.Date.ToString("yyyy.MM.dd");

            kind = F_KindTrans.SelectedIndex;

            switch (kind)
            {
                case 0:
                    CreateDoc(Date);
                    break;
                case 1:
                    PayDoc(idDoc, idAgent, idClient, Date, kind);
                    break;
                case 2:
                    CloseDoc(idDoc, idAgent, idClient, Date, kind);
                    break;
                case 3:
                    RetDoc(idDoc, idAgent, idClient, Date, kind);
                    break;
            }
        }

        private void RetDoc(int idDoc, int idAgent, int idClient, string finishDate, int kind)
        {
            /*операция возврата денежных средств по ранее
            закрытым договорам с формированием записей с кредита счета 58 в дебет
            счетов денежных средств на сумму займа, увеличенную на сумму начисленного
            процента по договору.
            51-58*/
            //  throw new NotImplementedException();

           // idDoc = Convert.ToInt32(F_Contr.SelectedValue.ToString());


            //расчет суммы+
            object summa = selectValue(ConnectionString, "SELECT summa FROM Contract WHERE Id=" + idDoc + ";");
            sumDoc = CalcSum()+Convert.ToDouble(summa);
            MessageBox.Show("Итоговая сумма " + sumDoc);

            //добавить запись в операции
            String selectCommandTrans = "select MAX(Id) from LogTransaction";
            object maxValueT = selectValue(ConnectionString, selectCommandTrans);
            if (Convert.ToString(maxValueT) == "") maxValueT = 0;
            Id = Convert.ToInt32(maxValueT) + 1;
            string txtSQLQueryT = "insert into LogTransaction (Id, KindTransaction, Date, Summa, AgentId, ClientId, ContractId) values (" +
                    Id + ", '" + kinds[kind] + "', '" + finishDate + "', " + sumDoc.ToString().Replace(",", ".") + ", " + idAgent + ", " + idClient + ", " + idDoc + ")";
            ExecuteQuery(txtSQLQueryT);
            MessageBox.Show("Добавлена новая операция");

            //добавить проводку 51-58
            String selectCommandWir = "select MAX(Id) from LogWiring";
            object maxValueW = selectValue(ConnectionString, selectCommandWir);
            if (Convert.ToString(maxValueW) == "") maxValueW = 0;
            string txtSQLQueryW = "insert into LogWiring (Id, Summa, Date, Deb, Cred,  subkontoCred1, subkontoCred2, subkontoCred3, LogTrId) values (" +
        (Convert.ToInt32(maxValueW) + 1) + ", " + sumDoc.ToString().Replace(",", ".") + ", '" + finishDate + "', '" + 51+ "', '"+58+"', " + idAgent + ", " + idClient + ", " + idDoc + ", " + Id + ")";
            ExecuteQuery(txtSQLQueryW);
            MessageBox.Show("Добавлена новая проводка");
        }

        private void CloseDoc(int idDoc, int idAgent, int idClient, string finishDate, int kind)
        {
            /*По истечении (до истечения) срока договора
            формируется проводка на сумму начисленного процента1 (процент2) в кредит
            счета 84 «Нераспределенная прибыль (непокрытый убыток)» по статье:
            «Проценты по договорам займа» и дебет счета 58.
            Сумма проводки зависит от даты проводки, если закрытие осуществляется
            после истечения срока займа, то
            СуммаПроводки=Договор.Процент1*Договор.СуммаДоговора, иначе
            СуммаПроводки=Договор.Процент2*Договор.СуммаДоговора.
            58-84 (начисленные проценты)*/

            /*sumDoc = Convert.ToDouble(selectValue(ConnectionString, "SELECT summa FROM Contract WHERE Id=" + idDoc + ";").ToString());

            startDate = selectValue(ConnectionString, "SELECT startDate FROM Contract WHERE Id=" + idDoc + ";").ToString();

            term = Convert.ToInt32(selectValue(ConnectionString, "SELECT term FROM Contract WHERE Id=" + idDoc + ";").ToString());

            percent1 = Convert.ToDouble((selectValue(ConnectionString, "SELECT percent1 FROM Contract WHERE Id=" + idDoc + ";")).ToString().Replace(".", ","));
            percent2 = Convert.ToDouble((selectValue(ConnectionString, "SELECT percent2 FROM Contract WHERE Id=" + idDoc + ";")).ToString().Replace(".", ","));

            if (Convert.ToDateTime(startDate) > Convert.ToDateTime(finishDate))
            {
                MessageBox.Show("Проверьте дату");
                return;
            }

            int termfact = Convert.ToInt32((Convert.ToDateTime(finishDate) - Convert.ToDateTime(startDate)).TotalDays); //финиш-старт =фактический срок
            if (termfact > term) //факт>срока -> срок истек
            {
                if (percent1 != 0)
                    sumDoc = Convert.ToDouble(sumDoc) * percent1;
            }
            else
            {
                if (percent2 != 0)
                    sumDoc = Convert.ToDouble(sumDoc) * percent2;
            }
            */


            //расчет суммы
            sumDoc = CalcSum();
            MessageBox.Show("Итоговая сумма " + sumDoc);

            //добавить запись в операции
            String selectCommandTrans = "select MAX(Id) from LogTransaction";
            object maxValueT = selectValue(ConnectionString, selectCommandTrans);
            if (Convert.ToString(maxValueT) == "") maxValueT = 0;
            Id = Convert.ToInt32(maxValueT) + 1;
            string txtSQLQueryT = "insert into LogTransaction (Id, KindTransaction, Date, Summa, AgentId, ClientId, ContractId) values (" +
                    Id + ", '" + kinds[kind] + "', '" + finishDate + "', " + sumDoc.ToString().Replace(",", ".") + ", " + idAgent + ", " + idClient + ", " + idDoc + ")";
            ExecuteQuery(txtSQLQueryT);
            MessageBox.Show("Добавлена новая операция");

            //добавить проводку 58-84
            String selectCommandWir = "select MAX(Id) from LogWiring";
            object maxValueW = selectValue(ConnectionString, selectCommandWir);
            if (Convert.ToString(maxValueW) == "") maxValueW = 0;
            string txtSQLQueryW = "insert into LogWiring (Id, Summa, Date, Deb, subkontoDeb1, subkontoDeb2, subkontoDeb3, Cred, LogTrId) values (" +
        (Convert.ToInt32(maxValueW) + 1) + ", " + sumDoc.ToString().Replace(",", ".") + ", '" + finishDate + "', '" + 58 + "', " + idAgent + ", " + idClient + ", " + idDoc + ", '" + 84 + "', "+ Id + ")";
            ExecuteQuery(txtSQLQueryW);
            MessageBox.Show("Добавлена новая проводка");

            //обновить договор
            int termfact = Convert.ToInt32((Convert.ToDateTime(finishDate) - Convert.ToDateTime(startDate)).TotalDays); //финиш-старт =фактический срок
            String selectCommandUpdDoc = "update Contract set finishDate='" + finishDate + "', termFact=" + termfact + " where Id = " + idDoc;
            changeValue(ConnectionString, selectCommandUpdDoc);
            MessageBox.Show("Договор обновлен");




            //throw new NotImplementedException();
        }

        private void PayDoc(int idDoc, int idAgent, int idClient, string finishDate, int kind)
        {
            /*Формируется запись в дебет счета 58 с 
              кредита счета денежных средств на сумму перечисления. Каждый вид займа 
              учитывается на отдельном субсчете. Проводка должна быть сформирована 
              автоматически после ввода даты операции и выбора договора займа.
              58-51(сумма договора)*/
            if (Id == 0)//создание
            {
                //расчет суммы
                double summa = Convert.ToDouble(selectValue(ConnectionString, "SELECT summa FROM Contract WHERE Id=" + idDoc + ";"));
                MessageBox.Show("Итоговая сумма " + summa);

                //добавить запись в операции
                String selectCommandTrans = "select MAX(Id) from LogTransaction";
                object maxValueT = selectValue(ConnectionString, selectCommandTrans);
                if (Convert.ToString(maxValueT) == "") maxValueT = 0;
                Id = Convert.ToInt32(maxValueT) + 1;
                string txtSQLQueryT = "insert into LogTransaction (Id, KindTransaction, Date, Summa, AgentId, ClientId, ContractId) values (" +
                        Id + ", '" + kinds[kind] + "', '" + finishDate + "', " + sumDoc.ToString().Replace(",", ".") + ", " + idAgent + ", " + idClient + ", " + idDoc + ")";
                ExecuteQuery(txtSQLQueryT);
                MessageBox.Show("Добавлена новая операция");

                //добавить проводку 58-51
                String selectCommandWir = "select MAX(Id) from LogWiring";
                object maxValueW = selectValue(ConnectionString, selectCommandWir);
                if (Convert.ToString(maxValueW) == "") maxValueW = 0;
                string txtSQLQueryW = "insert into LogWiring (Id, Summa, Date, Deb, subkontoDeb1, subkontoDeb2, subkontoDeb3, Cred, LogTrId) values (" +
            (Convert.ToInt32(maxValueW) + 1) + ", " + sumDoc.ToString().Replace(",", ".") + ", '" + finishDate + "', '" + 58 + "', " + idAgent + ", " + idClient + ", " + idDoc + ", '" + 51 + "', " + Id + ")";
                ExecuteQuery(txtSQLQueryW);
                MessageBox.Show("Добавлена новая проводка");
            }
            else
            {
                //расчет суммы
                double summa = Convert.ToDouble(selectValue(ConnectionString, "SELECT summa FROM Contract WHERE Id=" + idDoc + ";"));
                MessageBox.Show("Итоговая сумма " + summa);

                //изменить запись в операции
                string txtSQLQueryT = "insert into LogTransaction (Id, KindTransaction, Date, Summa, AgentId, ClientId, ContractId) values (" +
                        Id + ", '" + kinds[kind] + "', '" + finishDate + "', " + sumDoc.ToString().Replace(",", ".") + ", " + idAgent + ", " + idClient + ", " + idDoc + ")";
                ExecuteQuery(txtSQLQueryT);
                MessageBox.Show("Добавлена новая операция");

                //изменить проводку 58-51
                String idWir = "select Id from LogWiring where LogTrId=" + Id + ";";
                string txtSQLQueryW = "insert into LogWiring (Id, Summa, Date, Deb, subkontoDeb1, subkontoDeb2, subkontoDeb3, Cred, LogTrId) values (" +
            idWir + ", " + sumDoc.ToString().Replace(",", ".") + ", '" + finishDate + "', '" + 58 + "', " + idAgent + ", " + idClient + ", " + idDoc + ", '" + 51 + "', " + Id + ")";
                ExecuteQuery(txtSQLQueryW);
                MessageBox.Show("Добавлена новая проводка");
            }
            // throw new NotImplementedException();
        }

        private void CreateDoc(string startDate)
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
            kind = F_KindTrans.SelectedIndex;
            if (kind == 0)
            {
                F_term.Visible = true;
                label7.Visible = true;
                F_summa.ReadOnly = false;
            }
            else
            {
                F_term.Visible = false;
                label7.Visible = false;
                F_summa.ReadOnly = true;
            }
        }

        private void F_Calc_Click(object sender, EventArgs e)
        {
            idDoc = Convert.ToInt32(F_Contr.SelectedValue.ToString());
            string finishDate = F_Date.Value.Date.ToString("yyyy.MM.dd");
            startDate = selectValue(ConnectionString, "SELECT startDate FROM Contract WHERE Id=" + idDoc + ";").ToString();
            double summa = Convert.ToDouble(selectValue(ConnectionString, "SELECT summa FROM Contract WHERE Id=" + idDoc + ";"));
            kind = F_KindTrans.SelectedIndex;
            // idAgent = Convert.ToInt32(F_Agent.SelectedValue.ToString());
            //idClient = Convert.ToInt32(F_Client.SelectedValue.ToString());




            /*   term = Convert.ToInt32(selectValue(ConnectionString, "SELECT term FROM Contract WHERE Id=" + idDoc + ";").ToString());
               idDoc = Convert.ToInt32(F_Contr.SelectedValue.ToString());
               sumDoc = Convert.ToDouble(selectValue(ConnectionString, "SELECT summa FROM Contract WHERE Id=" + idDoc + ";").ToString());

               percent1 = Convert.ToDouble((selectValue(ConnectionString, "SELECT percent1 FROM Contract WHERE Id=" + idDoc + ";")).ToString().Replace(".", ","));
               percent2 = Convert.ToDouble((selectValue(ConnectionString, "SELECT percent2 FROM Contract WHERE Id=" + idDoc + ";")).ToString().Replace(".", ","));

               if (Convert.ToDateTime(startDate) > Convert.ToDateTime(finishDate))
               {
                   MessageBox.Show("Проверьте дату");
                   return;
               }

               if ((Convert.ToDateTime(finishDate) - Convert.ToDateTime(startDate)).TotalDays > term) //финиш-старт>срока -> срок истек
               {
                   if (percent1 != 0)
                       sumDoc = Convert.ToDouble(sumDoc) * percent1;
               }
               else
               {
                   if (percent2 != 0)
                       sumDoc = Convert.ToDouble(sumDoc) * percent2;
               }*/

            if (Convert.ToDateTime(startDate) > Convert.ToDateTime(finishDate))
            {
                MessageBox.Show("Проверьте дату");
                return;
            }
            
            
            switch (kind)
            {
                case 0:
                    break;
                case 1:
                    F_summa.Text = summa.ToString();
                    break;
                case 2:
                    F_summa.Text = CalcSum().ToString();
                    break;
                case 3:
                    F_summa.Text = (CalcSum()+summa).ToString();
                    break;
            }
        }

        private double CalcSum()
        {
            string finishDate = F_Date.Value.Date.ToString("yyyy.MM.dd");
            idDoc = Convert.ToInt32(F_Contr.SelectedValue.ToString());
            sumDoc = Convert.ToDouble(selectValue(ConnectionString, "SELECT summa FROM Contract WHERE Id=" + idDoc + ";").ToString());


            startDate = selectValue(ConnectionString, "SELECT startDate FROM Contract WHERE Id=" + idDoc + ";").ToString();

            term = Convert.ToInt32(selectValue(ConnectionString, "SELECT term FROM Contract WHERE Id=" + idDoc + ";").ToString());

            percent1 = Convert.ToDouble((selectValue(ConnectionString, "SELECT percent1 FROM Contract WHERE Id=" + idDoc + ";")).ToString().Replace(".", ","));
            percent2 = Convert.ToDouble((selectValue(ConnectionString, "SELECT percent2 FROM Contract WHERE Id=" + idDoc + ";")).ToString().Replace(".", ","));



            if ((Convert.ToDateTime(finishDate) - Convert.ToDateTime(startDate)).TotalDays > term) //финиш-старт>срока -> срок истек
            {
                if (percent1 != 0)
                    sumDoc = Convert.ToDouble(sumDoc) * percent1;
            }
            else
            {
                if (percent2 != 0)
                    sumDoc = Convert.ToDouble(sumDoc) * percent2;
            }
            return sumDoc;
        }
    }
}
