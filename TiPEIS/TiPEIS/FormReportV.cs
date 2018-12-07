using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections;
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
    public partial class FormReportV : Form
    {

        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        System.Data.DataTable DATA;
        private DataSet DS = new DataSet();
        private System.Data.DataTable DT = new System.Data.DataTable();
        private static string sPath = Path.Combine(System.Windows.Forms.Application.StartupPath, "mybd.db");
        string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";


        public FormReportV()
        {
            InitializeComponent();
        }

        private void F_Show_Click(object sender, EventArgs e)
        {
            string FromDate = F_From.Value.Date.ToString("yyyy.MM.dd");
            string ToDate = F_To.Value.Date.ToString("yyyy.MM.dd");

            if (Convert.ToDateTime(FromDate) > Convert.ToDateTime(ToDate))
            {
                MessageBox.Show("Проверьте дату");
                return;
            }


            Clear(dataGridView1);
            String selectCommand = "SELECT  C.FIO as SubcontoD2, A.FIO as SubcontoD1,  T.ContractId as SubcontoD3, T.Date, T.Summa FROM [LogTransaction] T left outer join Agent A ON T.AgentId = A.Id left outer join Client C ON T.ClientId= C.Id WHERE T.KindTransaction=2 and T.Date BETWEEN '" + FromDate + "' AND '" + ToDate + "'";
            selectTableDate2(ConnectionString, selectCommand);
            
        }

        private void selectTableDate(string connectionString, string selectCommand)
        {
            SQLiteConnection connect = new SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter sda = new SQLiteDataAdapter(selectCommand, connect);
            DATA = new System.Data.DataTable();
            sda.Fill(DATA);
            dataGridView1.DataSource = DATA;
            connect.Close();
        }

        private void FormReportV_Load(object sender, EventArgs e)
        {

        }

        public void Clear(DataGridView dataGridView)
        {
            while (dataGridView.Rows.Count > 1)
                for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
                    dataGridView.Rows.Remove(dataGridView.Rows[i]);
        }

        public void selectTableDate2(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter sda = new SQLiteDataAdapter(selectCommand, connect);
            System.Data.DataTable DATA = new System.Data.DataTable();
            sda.Fill(DATA);

            foreach(DataRow row in DATA.Rows)
            {
                var idDoc = row.ItemArray[2];
                object maxstate = selectValue(ConnectionString, "SELECT MAX(KindTransaction) FROM LogTransaction WHERE ContractId=" + idDoc + ";").ToString();
                if (Convert.ToInt32(maxstate) == 3)
                {
                    row.Delete();
                }
                else
                {
                    object summa = selectValue(ConnectionString, "SELECT summa FROM Contract WHERE Id=" + idDoc + ";");
                    double sumDoc = CalcProc(Convert.ToInt32(idDoc)) + Convert.ToDouble(summa)+ Convert.ToDouble(summa);
                    row.ItemArray[4] = sumDoc.ToString();
                }
            }
            dataGridView1.DataSource = DATA;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                var idDoc = row.Cells[2].Value;
                object summa = selectValue(ConnectionString, "SELECT summa FROM Contract WHERE Id=" + idDoc + ";");
                   double sumDoc = CalcProc(Convert.ToInt32(idDoc)) + Convert.ToDouble(summa) /*+ Convert.ToDouble(summa)*/;
                row.Cells[4].Value = sumDoc;
            }

            connect.Close();
        }

        private double CalcProc(int idDoc) //
        {
            double res = 0;
           double sumDoc = Convert.ToDouble(selectValue(ConnectionString, "SELECT summa FROM Contract WHERE Id=" + idDoc + ";").ToString());
           double percent2 = Convert.ToDouble((selectValue(ConnectionString, "SELECT percent2 FROM Contract WHERE Id=" + idDoc + ";")).ToString().Replace(".", ","));

            if (percent2 != 0)
            {
                res = Convert.ToDouble(sumDoc) * percent2;
            }
            return res;
        }

        public object selectValue(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new
            SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteCommand command = new SQLiteCommand(selectCommand,
            connect);
            SQLiteDataReader reader = command.ExecuteReader();
            object value = "";
            while (reader.Read())
            {
                value = reader[0];
            }
            connect.Close();
            return value;
        }

        private void F_Doc_Click(object sender, EventArgs e)
        {
            string FromDate = F_From.Value.Date.ToString("yyyy.MM.dd");
            string ToDate = F_To.Value.Date.ToString("yyyy.MM.dd");

            if (Convert.ToDateTime(FromDate) > Convert.ToDateTime(ToDate))
            {
                MessageBox.Show("Проверьте дату");
                return;
            }

            Clear(dataGridView1);

            String selectCommand = "SELECT  C.FIO as SubcontoD2, A.FIO as SubcontoD1,  T.ContractId as SubcontoD3, T.Date, T.Summa FROM [LogTransaction] T left outer join Agent A ON T.AgentId = A.Id left outer join Client C ON T.ClientId= C.Id WHERE T.KindTransaction=2 and T.Date BETWEEN '" + FromDate + "' AND '" + ToDate + "'";
            selectTableDate(ConnectionString, selectCommand);


            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "doc|*.doc|docx|*.docx"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    CreateDoc(sfd.FileName);
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void CreateDoc(string FileName)
        {
            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }

            var winword = new Microsoft.Office.Interop.Word.Application();
            try
            {
                object missing = System.Reflection.Missing.Value;
                //создаем документ
                Microsoft.Office.Interop.Word.Document document =
                    winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);
                //получаем ссылку на параграф
                var paragraph = document.Paragraphs.Add(missing);
                var range = paragraph.Range;
                //задаем текст
                range.Text = "Предстоящие поступления денежных средств";
                //задаем настройки шрифта
                var font = range.Font;
                font.Size = 16;
                font.Name = "Times New Roman";
                font.Bold = 1;
                //задаем настройки абзаца
                var paragraphFormat = range.ParagraphFormat;
                paragraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                paragraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
                paragraphFormat.SpaceAfter = 10;
                paragraphFormat.SpaceBefore = 0;
                //добавляем абзац в документ
                range.InsertParagraphAfter();


                string FromDate = F_From.Value.Date.ToString("yyyy.MM.dd");
                string ToDate = F_To.Value.Date.ToString("yyyy.MM.dd");
                // var products = context.Products.ToList();
                var paragraph2 = document.Paragraphs.Add(missing);
                var range2 = paragraph.Range;
                //задаем текстВ
                range2.Text = "С " + FromDate + "  По " + ToDate;
                //задаем настройки шрифта
                var font2 = range.Font;
                font2.Size = 12;
                font2.Name = "Times New Roman";
                font2.Bold = 1;
                //задаем настройки абзаца
                var paragraphFormat2 = range.ParagraphFormat;
                paragraphFormat2.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                paragraphFormat2.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
                paragraphFormat2.SpaceAfter = 10;
                paragraphFormat2.SpaceBefore = 0;
                //добавляем абзац в документ
                range2.InsertParagraphAfter();

                //создаем таблицу





                var paragraphTable = document.Paragraphs.Add(Type.Missing);
                var rangeTable = paragraphTable.Range;
                var table = document.Tables.Add(rangeTable, DATA.Rows.Count + 1, DATA.Columns.Count, ref missing, ref missing);

                font = table.Range.Font;
                font.Size = 14;
                font.Name = "Times New Roman";

                var paragraphTableFormat = table.Range.ParagraphFormat;
                paragraphTableFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
                paragraphTableFormat.SpaceAfter = 0;
                paragraphTableFormat.SpaceBefore = 0;


                table.Cell(1, 1).Range.Text = "Клиент";
                table.Cell(1, 2).Range.Text = "Агент";
                table.Cell(1, 3).Range.Text = "Договор";
                table.Cell(1, 4).Range.Text = "Дата";
                table.Cell(1, 5).Range.Text = "Сумма";

                for (int i = 0; i < DATA.Rows.Count; ++i)
                {
                    for (int j = 0; j < DATA.Columns.Count; ++j)
                    {
                        string s = DATA.Rows[i].ItemArray[j].ToString();
                        table.Cell(i + 2, j + 1).Range.Text = DATA.Rows[i].ItemArray[j].ToString();
                    }

                }
                //задаем границы таблицы
                table.Borders.InsideLineStyle = WdLineStyle.wdLineStyleInset;
                table.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;

                paragraph = document.Paragraphs.Add(missing);
                range = paragraph.Range;

                



                paragraphFormat = range.ParagraphFormat;
                paragraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                paragraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
                paragraphFormat.SpaceAfter = 10;
                paragraphFormat.SpaceBefore = 10;

                range.InsertParagraphAfter();
                //сохраняем
                object fileFormat = WdSaveFormat.wdFormatXMLDocument;
                document.SaveAs(FileName, ref fileFormat, ref missing,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing);
                document.Close(ref missing, ref missing, ref missing);
                MessageBox.Show("Успешно");
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка");
                throw;
            }
            finally
            {
                winword.Quit();
            }
        }

        private void F_Xls_Click(object sender, EventArgs e)
        {
            string FromDate = F_From.Value.Date.ToString("yyyy.MM.dd");
            string ToDate = F_To.Value.Date.ToString("yyyy.MM.dd");

            if (Convert.ToDateTime(FromDate) > Convert.ToDateTime(ToDate))
            {
                MessageBox.Show("Проверьте дату");
                return;
            }

            Clear(dataGridView1);

            String selectCommand = "SELECT  C.FIO as SubcontoD2, A.FIO as SubcontoD1,  T.ContractId as SubcontoD3, T.Date, T.Summa FROM [LogTransaction] T left outer join Agent A ON T.AgentId = A.Id left outer join Client C ON T.ClientId= C.Id WHERE T.KindTransaction=2 and T.Date BETWEEN '" + FromDate + "' AND '" + ToDate + "'";
            selectTableDate(ConnectionString, selectCommand);



            
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "xls|*.xls|xlsx|*.xlsx"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    CreateExcel(sfd.FileName);
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void CreateExcel(string FileName)
        {
            var excel = new Microsoft.Office.Interop.Excel.Application();
            try
            {
                //или создаем excel-файл, или открываем существующий
                if (File.Exists(FileName))
                {
                    excel.Workbooks.Open(FileName, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing);
                }
                else
                {
                    excel.SheetsInNewWorkbook = 1;
                    excel.Workbooks.Add(Type.Missing);
                    excel.Workbooks[1].SaveAs(FileName, XlFileFormat.xlExcel8, Type.Missing,
                        Type.Missing, false, false, XlSaveAsAccessMode.xlNoChange, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }

                Sheets excelsheets = excel.Workbooks[1].Worksheets;
                //Получаем ссылку на лист
                var excelworksheet = (Worksheet)excelsheets.get_Item(1);
                //очищаем ячейки
                excelworksheet.Cells.Clear();
                //настройки страницы
                excelworksheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                excelworksheet.PageSetup.CenterHorizontally = true;
                excelworksheet.PageSetup.CenterVertically = true;
                //получаем ссылку на первые 3 ячейки
                Microsoft.Office.Interop.Excel.Range excelcells = excelworksheet.get_Range("A1", "E1");
                //объединяем их
                excelcells.Merge(Type.Missing);
                //задаем текст, настройки шрифта и ячейки
                excelcells.Font.Bold = true;
                excelcells.Value2 = "Предстоящие поступления денежных средств";
                excelcells.RowHeight = 25;
                excelcells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                excelcells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                excelcells.Font.Name = "Times New Roman";
                excelcells.Font.Size = 11;


                string FromDate = F_From.Value.Date.ToString("yyyy.MM.dd");
                string ToDate = F_To.Value.Date.ToString("yyyy.MM.dd");


                excelcells = excelworksheet.get_Range("A2", "E2");
                excelcells.Merge(Type.Missing);
                excelcells.Value2 = "с " + FromDate+"    по   "+ ToDate;
                excelcells.RowHeight = 20;
                excelcells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                excelcells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                excelcells.Font.Name = "Times New Roman";
                excelcells.Font.Size = 12;



                if (DATA != null)
                {
                    excelcells = excelworksheet.get_Range("A4", "A4");
                    //excelcells = excelcells.get_Offset(2, -2);
                    excelcells.ColumnWidth = 15;
                    excelcells.Value2 = "Клиент";
                    excelcells = excelcells.get_Offset(0, 1);
                    excelcells.Value2 = "Агент";
                    excelcells = excelcells.get_Offset(0, 1);
                    excelcells.Value2 = "Договор";
                    excelcells = excelcells.get_Offset(0, 1);
                    excelcells.Value2 = "Дата";
                    excelcells = excelcells.get_Offset(0, 1);
                    excelcells.Value2 = "Сумма";
                    excelcells = excelcells.get_Offset(0, 1);
                    excelcells = excelcells.get_Offset(1, -5);
                    for (int i = 0; i < DATA.Rows.Count; ++i)
                    {
                        for (int j = 0; j < DATA.Columns.Count; ++j)
                        {

                            string s = DATA.Rows[i].ItemArray[j].ToString();
                            excelcells.Value2 = DATA.Rows[i].ItemArray[j].ToString();
                            excelcells = excelcells.get_Offset(0, 1);
                        }
                        excelcells = excelcells.get_Offset(0, -1);
                        excelcells = excelcells.get_Offset(1, -DATA.Columns.Count + 1);
                    }
                    //спускаемся на 2 ячейку вниз и 2 ячейкт влево

                   
                    excelcells.Font.Bold = true;
                }

                //сохраняем
                excel.Workbooks[1].Save();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                //закрываем
                excel.Quit();
            }
        }

        private void F_Pdf_Click(object sender, EventArgs e)
        {
            string FromDate = F_From.Value.Date.ToString("yyyy.MM.dd");
            string ToDate = F_To.Value.Date.ToString("yyyy.MM.dd");

            if (Convert.ToDateTime(FromDate) > Convert.ToDateTime(ToDate))
            {
                MessageBox.Show("Проверьте дату");
                return;
            }

            Clear(dataGridView1);

            String selectCommand = "SELECT  C.FIO as SubcontoD2, A.FIO as SubcontoD1,  T.ContractId as SubcontoD3, T.Date, T.Summa FROM [LogTransaction] T left outer join Agent A ON T.AgentId = A.Id left outer join Client C ON T.ClientId= C.Id WHERE T.KindTransaction=2 and T.Date BETWEEN '" + FromDate + "' AND '" + ToDate + "'";
            selectTableDate(ConnectionString, selectCommand);



            
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "pdf|*.pdf"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    CreatePdf(sfd.FileName);
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void CreatePdf(string FileName)
        {
            //из ресрусов получаем шрифт для кирилицы
            if (!File.Exists("TIMCYR.TTF"))
            {
                File.WriteAllBytes("TIMCYR.TTF", Properties.Resources.TIMCYR);
            }
            //открываем файл для работы
            FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write);
            //создаем документ, задаем границы, связываем документ и поток
            iTextSharp.text.Document doc = new iTextSharp.text.Document();
            doc.SetMargins(0.5f, 0.5f, 0.5f, 0.5f);
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);

            doc.Open();
            BaseFont baseFont = BaseFont.CreateFont("TIMCYR.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            //вставляем заголовок
            var phraseTitle = new Phrase("Предстоящие поступления денежных средств",
                new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD));
            iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph(phraseTitle)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 12
            };
            doc.Add(paragraph);
            string FromDate = F_From.Value.Date.ToString("yyyy.MM.dd");
            string ToDate = F_To.Value.Date.ToString("yyyy.MM.dd");
            var phrasePeriod = new Phrase("c " + FromDate +
                                    " по " + ToDate,
                                    new iTextSharp.text.Font(baseFont, 14, iTextSharp.text.Font.BOLD));
            paragraph = new iTextSharp.text.Paragraph(phrasePeriod)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 12
            };
            doc.Add(paragraph);

            //вставляем таблицу, задаем количество столбцов, и ширину колонок
            PdfPTable table = new PdfPTable(5)
            {
                TotalWidth = 800F
            };
            table.SetTotalWidth(new float[] { 160,160, 140, 160, 100 });
            //вставляем шапку
            PdfPCell cell = new PdfPCell();
            var fontForCellBold = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.BOLD);
            table.AddCell(new PdfPCell(new Phrase("Клиент", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Агент", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Договор", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Дата", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Сумма", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            //заполняем таблицу



            var fontForCells = new iTextSharp.text.Font(baseFont, 10);
            for (int i = 0; i < DATA.Rows.Count; ++i)
            {
                for (int j = 0; j < DATA.Columns.Count; ++j)
                {
                    string s = DATA.Rows[i].ItemArray[j].ToString();
                    cell = new PdfPCell(new Phrase(s, fontForCells));
                    table.AddCell(cell);
                }
            }

            
            //вставляем таблицу
            doc.Add(table);

            doc.Close();
        }
    }
}
