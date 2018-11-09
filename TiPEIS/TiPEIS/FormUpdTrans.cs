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

            //перечисление 
            //закрытие
            //возврат

        private int Id;

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
        private static string sPath = Path.Combine(Application.StartupPath, "mybd.db");


        string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";

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

        private void LoadDate() // подгрузка данных по текущей операции
        {
            try
            {
                object doc = selectValue(ConnectionString, "SELECT ContractId FROM LogTransaction WHERE Id=" + Id + ";");
                F_Contr.SelectedValue = doc;
                idDoc = Convert.ToInt32(doc);
            }
            catch { }

            try
            {
                object kindObj = selectValue(ConnectionString, "SELECT KindTransaction FROM LogTransaction WHERE Id=" + Id + ";");
                F_KindTrans.SelectedValue = kindObj;
                kind = Convert.ToInt32(kindObj);
            }
            
            catch { }
            F_KindTrans.Enabled = false;
            try
            {
                object date = selectValue(ConnectionString, "SELECT Date FROM LogTransaction WHERE Id=" + Id + ";");
                F_Date.Value = Convert.ToDateTime(date.ToString());
                Date = date.ToString();
            }
            catch { }
            try
            {
                object summa = selectValue(ConnectionString, "SELECT Summa FROM LogTransaction WHERE Id=" + Id + ";");
                F_summa.Text = summa.ToString();
            }
            catch { }
            try
            {
                object agent = selectValue(ConnectionString, "SELECT AgentId FROM LogTransaction WHERE Id=" + Id + ";");
                F_Agent.SelectedValue = agent;
                idAgent = Convert.ToInt32(agent);
            }
            catch { }
            try
            {
                object client = selectValue(ConnectionString, "SELECT ClientId FROM LogTransaction WHERE Id=" + Id + ";");
                F_Client.SelectedValue = client;
                idClient = Convert.ToInt32(client);
            }
            catch { }

            F_Contr.Enabled = false;

            try
            {
                String selectCommand = "select W.Id, W.Summa, W.Date, W.content,  P1.NameAcc as Deb,  A1.FIO as Agent_FIO,  C1.FIO as Client_FIO, D1.Id as Doc_ID_Deb,  P2.NameAcc as Cred,  A2.FIO as Agent_FIO,  C2.FIO as Client_FIO, D2.Id as Doc_ID_Cred from LogWiring W left outer join ChartAccounts P1 on(W.Deb = P1.NumberAcc) left outer join ChartAccounts P2 on(W.Cred = P2.NumberAcc) left outer join Agent A1 on(W.subkontoDeb1 = A1.Id) left outer join Agent A2 on(W.subkontoCred1 = A2.Id) left outer join Client C1 on(W.subkontoDeb2 = C1.Id) left outer join Client C2 on(W.subkontoCred2 = C2.Id) left outer join Contract D1 on(W.subkontoDeb3 = D1.Id) left outer join Contract D2 on(W.subkontoCred3 = D2.Id) where W.LogTrId=" + Id;
                selectTable(ConnectionString, selectCommand);
            }
            catch { }

            SetupOld();
           // Setup();


        }

        private void SetupOld()
        {
            object doc = selectValue(ConnectionString, "SELECT ContractId FROM LogTransaction WHERE Id=" + Id + ";");
            idDoc = Convert.ToInt32(doc);
            object statusDocObj = selectValue(ConnectionString, "SELECT MAX(KindTransaction) FROM LogTransaction WHERE ContractId=" + idDoc + ";");
            if (statusDocObj.ToString() == "")
            {
                F_Client.Enabled = true;
                F_Agent.Enabled = true;
            }
            else
            {
                int statusDoc = Convert.ToInt32(statusDocObj.ToString());
                
                if (statusDoc == 1) //перечисление
                {
                    F_Client.Enabled = true;
                    F_Agent.Enabled = true;
                }
                if (statusDoc == 2) //закрытие
                {
                    F_Client.Enabled = false;
                    F_Agent.Enabled = false;
                }
                if (statusDoc == 3) //возврат
                {
                    F_Client.Enabled = false;
                    F_Agent.Enabled = false;
                }
            }
        }

        private void Setup()
        {
            if (Id != 0) return;
            if (F_Contr.SelectedIndex == -1)
            {
                F_KindTrans.Enabled = false;
            }
            else
            {
                F_KindTrans.Enabled = true;
            }
            /*зная idDoc пройтись по всему журналу проводок
            найти его статус 

            

             ПЕРЕЧИСЛЕНИЕ
             ЗАКРЫТИЕ
             ВОЗВРАТ
             */
            GetInfFromForm();
            int r = idDoc;
            object statusDocObj = selectValue(ConnectionString, "SELECT MAX(KindTransaction) FROM LogTransaction WHERE ContractId=" + idDoc + ";");
            if (statusDocObj.ToString() == "")
            {
                String selectKinds = "SELECT Id, Name FROM KindTransaction where Id<2";
                selectCombo(ConnectionString, selectKinds, F_KindTrans, "Name", "Id");
               // F_KindTrans.SelectedValue = kind;

                F_Client.Enabled = true;
                F_Agent.Enabled = true;
            }
            else
            {
                int statusDoc = Convert.ToInt32(statusDocObj.ToString());
               //перечисление
                if (statusDoc == 1)
                {
                    String selectKinds = "SELECT Id, Name FROM KindTransaction where Id=0 or Id=2";
                    selectCombo(ConnectionString, selectKinds, F_KindTrans, "Name", "Id");
                    //F_KindTrans.SelectedValue = kind;
                    object ki = selectValue(ConnectionString, "SELECT KindTransaction FROM LogTransaction WHERE ContractId=" + idDoc + ";");
                    selectCombo(ConnectionString, selectKinds, F_KindTrans, "Name", "Id");
                   // F_KindTrans.SelectedValue = ki;
                    try
                    {
                        object agent = selectValue(ConnectionString, "SELECT AgentId FROM LogTransaction WHERE ContractId=" + idDoc + ";");
                        F_Agent.SelectedValue = agent;
                        idAgent = Convert.ToInt32(agent);
                    }
                    catch { }
                    try
                    {
                        object client = selectValue(ConnectionString, "SELECT ClientId FROM LogTransaction WHERE ContractId=" + idDoc + ";");
                        F_Client.SelectedValue = client;
                        idClient = Convert.ToInt32(client);
                    }
                    catch { }
                    F_Client.Enabled = false;
                    F_Agent.Enabled = false;
                }
                if (statusDoc == 2)
                {
                    String selectKinds = "SELECT Id, Name FROM KindTransaction where Id=0 or Id=3";
                    selectCombo(ConnectionString, selectKinds, F_KindTrans, "Name", "Id");
                   // F_KindTrans.SelectedValue = kind;
                    object ki = selectValue(ConnectionString, "SELECT KindTransaction FROM LogTransaction WHERE ContractId=" + idDoc + ";");
                    selectCombo(ConnectionString, selectKinds, F_KindTrans, "Name", "Id");
                  //  F_KindTrans.SelectedValue = ki;
                    try
                    {
                        object agent = selectValue(ConnectionString, "SELECT AgentId FROM LogTransaction WHERE ContractId=" + idDoc + ";");
                        F_Agent.SelectedValue = agent;
                        idAgent = Convert.ToInt32(agent);
                    }
                    catch { }
                    try
                    {
                        object client = selectValue(ConnectionString, "SELECT ClientId FROM LogTransaction WHERE ContractId=" + idDoc + ";");
                        F_Client.SelectedValue = client;
                        idClient = Convert.ToInt32(client);
                    }
                    catch { }
                    F_Client.Enabled = false;
                    F_Agent.Enabled = false;
                }
                if (statusDoc == 3)
                {
                    String selectKinds = "SELECT Id, Name FROM KindTransaction where Id=0";
                    object ki = selectValue(ConnectionString, "SELECT KindTransaction FROM LogTransaction WHERE ContractId=" + idDoc + ";");
                    selectCombo(ConnectionString, selectKinds, F_KindTrans, "Name", "Id");
                    //F_KindTrans.SelectedValue = ki;
                    try
                    {
                        object agent = selectValue(ConnectionString, "SELECT AgentId FROM LogTransaction WHERE ContractId=" + idDoc + ";");
                        F_Agent.SelectedValue = agent;
                        idAgent = Convert.ToInt32(agent);
                    }
                    catch { }
                    try
                    {
                        object client = selectValue(ConnectionString, "SELECT ClientId FROM LogTransaction WHERE ContractId=" + idDoc + ";");
                        F_Client.SelectedValue = client;
                        idClient = Convert.ToInt32(client);
                    }
                    catch { }
                    F_Client.Enabled = false;
                    F_Agent.Enabled = false;
                }
            }

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

        public void selectTable(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            F_Wirs.DataSource = ds;
            F_Wirs.DataMember = ds.Tables[0].ToString();
            connect.Close();
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

            String selectKinds = "SELECT Id, Name FROM KindTransaction";
            selectCombo(ConnectionString, selectKinds, F_KindTrans, "Name", "Id");

            F_KindTrans.SelectedIndex = -1;
            F_Agent.SelectedIndex = -1;
            F_Client.SelectedIndex = -1;
            F_Contr.SelectedIndex = -1;

            if (F_Contr.SelectedIndex == -1)
            {
                F_KindTrans.Enabled = false;
            }
            else
            {
                F_KindTrans.Enabled = true;
            }

            if (Id != 0)
            {
                LoadDate();
            }
        }

        private void F_Save_Click(object sender, EventArgs e)
        {
            Date = F_Date.Value.Date.ToString("yyyy.MM.dd");

            kind = Convert.ToInt32(F_KindTrans.SelectedValue.ToString());

            switch (kind)
            {
                case 0:
                    CreateDoc(Date);
                    break;
                case 1://перечисление
                    if (!Check())
                    {
                        MessageBox.Show("Заполните все поля!");
                        return;
                    }
                    GetInfFromForm();

                    PayDoc(idDoc, idAgent, idClient, Date, kind);
                    break;
                case 2://закрытие
                    if (!Check())
                    {
                        MessageBox.Show("Заполните все поля!");
                        return;
                    }
                    GetInfFromForm();
                    CloseDoc(idDoc, idAgent, idClient, Date, kind);
                    break;
                case 3://возврат
                    if (!Check())
                    {
                        MessageBox.Show("Заполните все поля!");
                        return;
                    }
                    GetInfFromForm();
                    RetDoc(idDoc, idAgent, idClient, Date, kind);
                    break;
            }

            try
            {
                String selectCommand = "select W.Id, W.Summa, W.Date, W.content,  P1.NameAcc as Deb,  A1.FIO as Agent_FIO_Deb,  C1.FIO as Client_FIO_Deb, D1.Id as Doc_ID_Deb,  P2.NameAcc as Cred,  A2.FIO as Agent_FIO_Cred,  C2.FIO as Client_FIO_Cred, D2.Id as Doc_ID_Cred from LogWiring W left outer join ChartAccounts P1 on(W.Deb = P1.NumberAcc) left outer join ChartAccounts P2 on(W.Cred = P2.NumberAcc) left outer join Agent A1 on(W.subkontoDeb1 = A1.Id) left outer join Agent A2 on(W.subkontoCred1 = A2.Id) left outer join Client C1 on(W.subkontoDeb2 = C1.Id) left outer join Client C2 on(W.subkontoCred2 = C2.Id) left outer join Contract D1 on(W.subkontoDeb3 = D1.Id) left outer join Contract D2 on(W.subkontoCred3 = D2.Id) where W.LogTrId=" + Id;
                selectTable(ConnectionString, selectCommand);
            }
            catch { }
        }

        private void GetInfFromForm() //получить значения из комбо-боксов 
        {
            try
            {
                idDoc = Convert.ToInt32(F_Contr.SelectedValue.ToString());
            }
            catch { idDoc = 0; }
            try
            {
                idAgent = Convert.ToInt32(F_Agent.SelectedValue.ToString());
            }
            catch { idAgent = 0; }
            try
            {
                idClient = Convert.ToInt32(F_Client.SelectedValue.ToString());
            }
            catch { idClient = 0; }

            Date = F_Date.Value.Date.ToString("yyyy.MM.dd");
            try
            {
                kind = Convert.ToInt32(F_KindTrans.SelectedValue.ToString());
            }
            catch { }
        }

        private bool Check() //все комбо-боксы выбраны 
        {
            if (F_Contr.SelectedValue == null) return false;
            if (F_Agent.SelectedValue == null) return false;
            if (F_Client.SelectedValue == null) return false;
            if (F_KindTrans.SelectedValue == null) return false;

            return true;

        }

        private void RetDoc(int idDoc, int idAgent, int idClient, string finishDate, int kind)
        {
            /*операция возврата денежных средств по ранее
            закрытым договорам с формированием записей с кредита счета 58 в дебет
            счетов денежных средств на сумму займа, увеличенную на сумму начисленного
            процента по договору.
            51-58*/

            startDate = selectValue(ConnectionString, "SELECT startDate FROM Contract WHERE Id=" + idDoc + ";").ToString();
            if (Convert.ToDateTime(startDate) > Convert.ToDateTime(Date))
            {
                MessageBox.Show("Проверьте дату");
                return;
            }

            object datePere = selectValue(ConnectionString, "SELECT MAX(Date) FROM LogWiring WHERE subkontoDeb3=" + idDoc + ";").ToString();
            if (Convert.ToDateTime(datePere) > Convert.ToDateTime(Date))
            {
                MessageBox.Show("Дата возврата раньше даты закрытия. Проверьте дату");
                return;
            }

            //расчет суммы+проценты
            object summa = selectValue(ConnectionString, "SELECT summa FROM Contract WHERE Id=" + idDoc + ";");
            sumDoc = CalcProc() + Convert.ToDouble(summa);
            MessageBox.Show("Итоговая сумма " + sumDoc);



            if (Id == 0) //новая операция
            {
                String selectCommandTrans = "select MAX(Id) from LogTransaction";
                object maxValueT = selectValue(ConnectionString, selectCommandTrans);
                if (Convert.ToString(maxValueT) == "") maxValueT = 0;
                Id = Convert.ToInt32(maxValueT) + 1;

                //добавить запись в операции
                string txtSQLQueryTN = "insert into LogTransaction (Id, KindTransaction, Date, Summa, AgentId, ClientId, ContractId) values (" +
                    Id + ", " + kind + ", '" + finishDate + "', " + sumDoc.ToString().Replace(",", ".") + ", " + idAgent + ", " + idClient + ", " + idDoc + ")";
                ExecuteQuery(txtSQLQueryTN);
                MessageBox.Show("Добавлена новая операция");

                //добавить проводку 51-58
                String selectCommandWir = "select MAX(Id) from LogWiring";
                object maxValueW = selectValue(ConnectionString, selectCommandWir);
                if (Convert.ToString(maxValueW) == "") maxValueW = 0;
                string txtSQLQueryW = "insert into LogWiring (Id, Summa, Date, Deb, Cred,  subkontoCred1, subkontoCred2, subkontoCred3, LogTrId) values (" +
                    (Convert.ToInt32(maxValueW) + 1) + ", " + sumDoc.ToString().Replace(",", ".") + ", '" + finishDate + "', '" + 51 + "', '" + 58 + "', " + idAgent + ", " + idClient + ", " + idDoc + ", " + Id + ")";
                ExecuteQuery(txtSQLQueryW);
                MessageBox.Show("Добавлена новая проводка");
            }
            else //Изменение
            {
               
                String selectCommandTU = "update LogTransaction set " +
                  "KindTransaction=" + kind + "" +
                    ", Date='" + finishDate + "'" +
                    ", Summa=" + sumDoc.ToString().Replace(",", ".") +
                    ", AgentId=" + idAgent +
                    ", ClientId='" + idClient +
                    "', ContractId=" + idDoc
                    + " where Id = " + Id;
                changeValue(ConnectionString, selectCommandTU);
                MessageBox.Show("Операция изменена");

                String selectCommandWU = "update LogWiring set " +
                  "Summa=" + sumDoc.ToString().Replace(",", ".") +
                    ", Date='" + finishDate + "'" +
                    ", Deb='" + 51 + "'" +
                    ", Cred='" + 58 + "'" +
                    ", subkontoCred1=" + idAgent +
                    ", subkontoCred2=" + idClient +
                    ", subkontoCred3=" + idDoc +
                    ", subkontoDeb1=" + "NULL" +
                    ", subkontoDeb2=" + "NULL" +
                    ", subkontoDeb3=" + "NULL"
                    + " where LogTrId = " + Id;
                changeValue(ConnectionString, selectCommandWU);
                MessageBox.Show("Проводка изменена");
            }
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

            startDate = selectValue(ConnectionString, "SELECT startDate FROM Contract WHERE Id=" + idDoc + ";").ToString();
            if (Convert.ToDateTime(startDate) > Convert.ToDateTime(Date))
            {
                MessageBox.Show("Проверьте дату");
                return;
            }
            string datePere= selectValue(ConnectionString, "SELECT Date FROM LogTransaction WHERE ContractId=" + idDoc + " AND KindTransaction=1").ToString();
            if (Convert.ToDateTime(datePere) > Convert.ToDateTime(Date))
            {
                MessageBox.Show("Дата закрытия раньше даты перечисления. Проверьте дату");
                return;
            }

            //расчет суммы
            sumDoc = CalcProc();
            MessageBox.Show("Итоговая сумма " + sumDoc);

            if (Id == 0)
            {
                String selectCommandTrans = "select MAX(Id) from LogTransaction";
                object maxValueT = selectValue(ConnectionString, selectCommandTrans);
                if (Convert.ToString(maxValueT) == "") maxValueT = 0;
                Id = Convert.ToInt32(maxValueT) + 1;

                //добавить запись в операции
                string txtSQLQueryTN = "insert into LogTransaction (Id, KindTransaction, Date, Summa, AgentId, ClientId, ContractId) values (" +
                        Id + ", " + kind + ", '" + finishDate + "', " + sumDoc.ToString().Replace(",", ".") + ", " + idAgent + ", " + idClient + ", " + idDoc + ")";
                ExecuteQuery(txtSQLQueryTN);
                MessageBox.Show("Добавлена новая операция");

                //добавить проводку 58-84
                String selectCommandWN = "select MAX(Id) from LogWiring";
                object maxValueW = selectValue(ConnectionString, selectCommandWN);
                if (Convert.ToString(maxValueW) == "") maxValueW = 0;
                string txtSQLQueryW = "insert into LogWiring (Id, Summa, Date, Deb, subkontoDeb1, subkontoDeb2, subkontoDeb3, Cred, LogTrId) values (" +
                    (Convert.ToInt32(maxValueW) + 1) + ", " + sumDoc.ToString().Replace(",", ".") + ", '" + finishDate + "', '" + 58 + "', " + idAgent + ", " + idClient + ", " + idDoc + ", '" + 84 + "', " + Id + ")";
                ExecuteQuery(txtSQLQueryW);
                MessageBox.Show("Добавлена новая проводка");

            }
            else
            {//piyfudtysdfguhjgyufaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa если есть возврат, то меньше него, если нет, то любой
                //string re = selectValue(ConnectionString, "SELECT (Date) FROM LogWiring WHERE subkontoCred3=" + idDoc + " AND "+"" ).ToString();
                //string re= selectValue(ConnectionString, "SELECT Date FROM LogWiring WHERE subkontoCred3=" + idDoc + ";").ToString();//был ли возврат
                //if (re != "")
                //{
                //    if (Convert.ToDateTime(re) < Convert.ToDateTime(Date))
                //    {
                //        MessageBox.Show("Дата закрытия позже даты возврата. Проверьте дату");
                //        return;
                //    }
                //}

                //должна быть меньше возврата
                object idDocRed = selectValue(ConnectionString, "select ContractId from LogTransaction where Id=" + Id);

                object vozvr = selectValue(ConnectionString, "select Id from LogTransaction where KindTransaction=3 AND ContractId=" + idDocRed);
                {
                    if (vozvr.ToString() != "")
                    {
                        object dateClose = selectValue(ConnectionString, "select Date from LogTransaction where Id=" + vozvr);

                        if (Convert.ToDateTime(dateClose) < Convert.ToDateTime(Date))
                        {
                            MessageBox.Show("Дата перечисления позже даты закрытия и возврата. Проверьте дату");
                            return;
                        }
                    }
                }

               /* //должна быть больше перечисления
                object idDocRed = selectValue(ConnectionString, "select ContractId from LogTransaction where Id=" + Id);

                object zakr = selectValue(ConnectionString, "select Id from LogTransaction where KindTransaction=2 AND ContractId=" + idDocRed);
                {
                    if (zakr.ToString() != "")
                    {
                        object dateClose = selectValue(ConnectionString, "select Date from LogTransaction where Id=" + zakr);

                        if (Convert.ToDateTime(dateClose) < Convert.ToDateTime(Date))
                        {
                            MessageBox.Show("Дата перечисления позже даты закрытия и возврата. Проверьте дату");
                            return;
                        }
                    }
                }*/
                


                String selectCommandTU = "update LogTransaction set " +
                  "KindTransaction=" + kind + "" +
                    ", Date='" + finishDate + "'" +
                    ", Summa=" + sumDoc.ToString().Replace(",", ".") +
                    ", AgentId=" + idAgent +
                    ", ClientId='" + idClient +
                    "', ContractId=" + idDoc
                    + " where Id = " + Id;
                changeValue(ConnectionString, selectCommandTU);
                MessageBox.Show("Операция изменена");

                String selectCommandWU = "update LogWiring set " +
                  "Summa=" + sumDoc.ToString().Replace(",", ".") +
                    ", Date='" + finishDate + "'" +
                    ", Deb='" + 58 + "'" +
                    ", Cred='" + 84 + "'" +
                    ", subkontoDeb1=" + idAgent +
                    ", subkontoDeb2=" + idClient +
                    ", subkontoDeb3=" + idDoc +
                    ", subkontoCred1=" + "NULL" +
                    ", subkontoCred2=" + "NULL" +
                    ", subkontoCred3=" + "NULL"
                    + " where LogTrId = " + Id;
                changeValue(ConnectionString, selectCommandWU);
                MessageBox.Show("Проводка изменена");
            }

            //обновить договор
            int termfact = Convert.ToInt32((Convert.ToDateTime(finishDate) - Convert.ToDateTime(startDate)).TotalDays); //финиш-старт =фактический срок
            String selectCommandUpdDoc = "update Contract set finishDate='" + finishDate + "', termFact=" + termfact + " where Id = " + idDoc;
            changeValue(ConnectionString, selectCommandUpdDoc);
            MessageBox.Show("Договор обновлен");
        }

        private void PayDoc(int idDoc, int idAgent, int idClient, string finishDate, int kind)
        {
            /*Формируется запись в дебет счета 58 с 
              кредита счета денежных средств на сумму перечисления. Каждый вид займа 
              учитывается на отдельном субсчете. Проводка должна быть сформирована 
              автоматически после ввода даты операции и выбора договора займа.

              58-51(сумма договора)*/
            startDate = selectValue(ConnectionString, "SELECT startDate FROM Contract WHERE Id=" + idDoc + ";").ToString();
            if (Convert.ToDateTime(startDate) > Convert.ToDateTime(Date))
            {
                MessageBox.Show("Проверьте дату");
                return;
            }


            //расчет суммы
            double summa = Convert.ToDouble(selectValue(ConnectionString, "SELECT summa FROM Contract WHERE Id=" + idDoc + ";"));
            MessageBox.Show("Итоговая сумма " + summa);

            if (Id == 0)//создание
            {
                String selectCommandTrans = "select MAX(Id) from LogTransaction";
                object maxValueT = selectValue(ConnectionString, selectCommandTrans);
                if (Convert.ToString(maxValueT) == "") maxValueT = 0;
                Id = Convert.ToInt32(maxValueT) + 1;

                //добавить запись в операции
                string txtSQLQueryT = "insert into LogTransaction (Id, KindTransaction, Date, Summa, AgentId, ClientId, ContractId) values (" +
                        Id + ", '" + kind + "', '" + finishDate + "', " + summa.ToString().Replace(",", ".") + ", " + idAgent + ", " + idClient + ", " + idDoc + ")";
                ExecuteQuery(txtSQLQueryT);
                MessageBox.Show("Добавлена новая операция");

                //добавить проводку 58-51
                String selectCommandWir = "select MAX(Id) from LogWiring";
                object maxValueW = selectValue(ConnectionString, selectCommandWir);
                if (Convert.ToString(maxValueW) == "") maxValueW = 0;
                string txtSQLQueryW = "insert into LogWiring (Id, Summa, Date, Deb, subkontoDeb1, subkontoDeb2, subkontoDeb3, Cred, LogTrId) values (" +
                    (Convert.ToInt32(maxValueW) + 1) + ", " + summa.ToString().Replace(",", ".") + ", '" + finishDate + "', '" + 58 + "', " + idAgent + ", " + idClient + ", " + idDoc + ", '" + 51 + "', " + Id + ")";
                ExecuteQuery(txtSQLQueryW);
                MessageBox.Show("Добавлена новая проводка");
            }
            else
            {//должна быть меньше закрытия
                object idDocRed = selectValue(ConnectionString, "select ContractId from LogTransaction where Id=" + Id);

                object zakr = selectValue(ConnectionString, "select Id from LogTransaction where KindTransaction=2 AND ContractId="+ idDocRed);
                {
                    if (zakr.ToString() != "")
                    {
                        object dateClose = selectValue(ConnectionString, "select Date from LogTransaction where Id=" + zakr);

                        if (Convert.ToDateTime(dateClose) < Convert.ToDateTime(Date))
                        {
                            MessageBox.Show("Дата перечисления позже даты закрытия и возврата. Проверьте дату");
                            return;
                        }
                    }
                }


                //string dateRed = selectValue(ConnectionString, "SELECT MIN(Date) FROM LogWiring WHERE subkontoDeb3=" + idDoc + "AND ").ToString();
                //if (Convert.ToDateTime(dateRed) < Convert.ToDateTime(Date))
                //{
                //    MessageBox.Show("Дата перечисления позже даты закрытия и возврата. Проверьте дату");
                //    return;
                //}
                String selectCommandTU = "update LogTransaction set " +
                  "KindTransaction='" + kind + "'" +
                    ", Date='" + finishDate + "'" +
                    ", Summa=" + summa.ToString().Replace(",", ".") +
                    ", AgentId=" + idAgent +
                    ", ClientId='" + idClient +
                    "', ContractId=" + idDoc
                    + " where Id = " + Id;
                changeValue(ConnectionString, selectCommandTU);
                MessageBox.Show("Операция изменена");

                String selectCommandWU = "update LogWiring set " +
                  "Summa=" + summa.ToString().Replace(",", ".") +
                    ", Date='" + finishDate + "'" +
                    ", Deb='" + 58 + "'" +
                    ", Cred='" + 84 + "'" +
                    ", subkontoDeb1=" + idAgent +
                    ", subkontoDeb2=" + idClient +
                    ", subkontoDeb3=" + idDoc +
                    ", subkontoCred1=" + "NULL" +
                    ", subkontoCred2=" + "NULL" +
                    ", subkontoCred3=" + "NULL"
                    + " where LogTrId = " + Id;
                changeValue(ConnectionString, selectCommandWU);
                MessageBox.Show("Проводка изменена");
            }
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
                MessageBox.Show("Несоответсвие формату Срок");
                return;
            }

            if (Id == 0)
            {
                String selectCommand = "select MAX(Id) from Contract";
                object maxValue = selectValue(ConnectionString, selectCommand);
                if (Convert.ToString(maxValue) == "")
                    maxValue = 0;
                string txtSQLQuery = "insert into Contract (Id, startDate,term,summa,termFact,finishDate,percent1,percent2) values (" +
                    (Convert.ToInt32(maxValue) + 1) + ", '" + startDate + "', " + term + ", " + summa + ", " + 0 + ", '" + "" + "', " + 0 + ", " + 0 + ")";
                ExecuteQuery(txtSQLQuery);
                MessageBox.Show("Новый договор успешно создан");
            }
        }

        private void F_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void F_KindTrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*kind = F_KindTrans.SelectedIndex;
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

            if (kind == 1)
            {
                F_Agent.Enabled = true;
                F_Client.Enabled = true;

            }
            else
            {
                F_Agent.Enabled = false;
                F_Client.Enabled = false;
            }*/


            //запретить менять комб-бокс
        }

        private void F_Calc_Click(object sender, EventArgs e)
        {
            if (!Check())
            {
                return;
            }
            GetInfFromForm();


            startDate = selectValue(ConnectionString, "SELECT startDate FROM Contract WHERE Id=" + idDoc + ";").ToString();
            double summa = Convert.ToDouble(selectValue(ConnectionString, "SELECT summa FROM Contract WHERE Id=" + idDoc + ";"));
            kind = Convert.ToInt32(F_KindTrans.SelectedValue.ToString());


            if (Convert.ToDateTime(startDate) > Convert.ToDateTime(Date))
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
                    F_summa.Text = CalcProc().ToString();
                    break;
                case 3:
                    F_summa.Text = (CalcProc() + summa).ToString();
                    break;
            }
        }

        private double CalcProc() //
        {
            double res = 0;
            // string finishDate = F_Date.Value.Date.ToString("yyyy.MM.dd");
            // idDoc = Convert.ToInt32(F_Contr.SelectedValue.ToString());
            sumDoc = Convert.ToDouble(selectValue(ConnectionString, "SELECT summa FROM Contract WHERE Id=" + idDoc + ";").ToString());
            startDate = selectValue(ConnectionString, "SELECT startDate FROM Contract WHERE Id=" + idDoc + ";").ToString();
            term = Convert.ToInt32(selectValue(ConnectionString, "SELECT term FROM Contract WHERE Id=" + idDoc + ";").ToString());

            percent1 = Convert.ToDouble((selectValue(ConnectionString, "SELECT percent1 FROM Contract WHERE Id=" + idDoc + ";")).ToString().Replace(".", ","));
            percent2 = Convert.ToDouble((selectValue(ConnectionString, "SELECT percent2 FROM Contract WHERE Id=" + idDoc + ";")).ToString().Replace(".", ","));

            if ((Convert.ToDateTime(Date) - Convert.ToDateTime(startDate)).TotalDays > term) //финиш-старт>срока -> срок истек
            {
                if (percent1 != 0)
                    res = Convert.ToDouble(sumDoc) * percent1;
            }
            else
            {
                if (percent2 != 0)
                    res = Convert.ToDouble(sumDoc) * percent2;
            }
            return res;
        }

        private void F_Contr_SelectedValueChanged(object sender, EventArgs e)
        {
            if (F_Contr.DataSource == null) return;
            try
            {
                idDoc = Convert.ToInt32(F_Contr.SelectedValue.ToString());
                Setup();
            }
            catch { }

            if (F_Contr.SelectedIndex == -1)
            {
                F_KindTrans.Enabled = false;
            }
            else
            {
                F_KindTrans.Enabled = true;
            }
        }
    }
}
