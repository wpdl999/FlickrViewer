using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Q1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataTable cleandata = new DataTable();
        public MainWindow()
        {
            InitializeComponent();
            string filepath = "C:/Users/wpdl5/OneDrive/Desktop/Lab3/stockData.csv";

            cleandata = GetDataTabletFromCSVFile(filepath);
            cleandata.DefaultView.Sort = "date DESC";
            stockDataGrid.ItemsSource = cleandata.DefaultView;
        }
        private async void Stock_sbutton_Click(object sender, RoutedEventArgs e)
        {
            String searchstock = stock_searchbox.Text;
            Task<Stock_name> stockT = Task.Run(() => StockS_init(searchstock));
            stockDataGrid.ItemsSource = stockT.Result.sName_List;
        }

        Stock_name StockS_init(string stockname)
        {
            var ans = new Stock_name();
            ans.sName_List = StockSearch(stockname);
            return ans;
        }

        public List<Stock_name> StockSearch(string s)
        {
            // cleandata.DefaultView.RowFilter = "Symbol LIKE '%" + stockname + "%'";
            string filepath = "C:/Users/wpdl5/OneDrive/Desktop/Lab3/stockData.csv";
            var eachline = File.ReadAllLines(filepath);
            List<Stock_name> stocksym = new List<Stock_name>();

            for (int i = 1; i < eachline.Length; i++)
            {
                string[] l = eachline[i].Split(',');
                var eachcol = new Stock_name();
                {
                    eachcol.Symbol = l[0];
                    eachcol.date = l[1];
                    eachcol.Open = l[2];
                    eachcol.High = l[3];
                    eachcol.Low = l[4];
                    eachcol.close = l[5];
                };
                if (eachcol.Symbol.Contains(s))
                {
                    stocksym.Add(eachcol);
                }
            }
            return stocksym.OrderByDescending(s => s.date).ToList();
        }


        private static DataTable GetDataTabletFromCSVFile(string csv_file_path)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();
                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                       
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return csvData;
        }

        

        private async void Calculator_btn_Click(object sender, RoutedEventArgs e)
        {
            Factorial_ansTextBox.Text = "";
            string str_num = factorialTextBox.Text;
            int num = Convert.ToInt32(str_num);
        
            Task<Factorial> ftask1 = Task.Run(() => Factcal_init(num));
            Task<Factorial> ftask2 = Task.Run(() => Factcal_init(num));
            await Task.WhenAll(ftask1,ftask2);
            Factorial_ansTextBox.AppendText(ftask1.Result.factorial_ans.ToString());

        }

        Factorial Factcal_init(int number)
        {
            var ans = new Factorial();
            ans.factorial_ans = factorial(number);
            return ans;
        }

        private int factorial(int num) {
            int sum = 1;
            while (num != 1)
            {
                sum = sum * num;
                num--;
            }
            return sum;
        }

        private void stock_searchbox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
