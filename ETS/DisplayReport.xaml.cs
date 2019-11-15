using ETS.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace ETS
{
    /// <summary>
    /// Interaction logic for DisplayReport.xaml
    /// </summary>
    public partial class DisplayReport : Page
    {
        public DisplayReport()
        {
            InitializeComponent();
            DataContext = new DataValidation() { WorkDate =null };
        }

        private void ShowReportOnSelectedDate(object sender, RoutedEventArgs e)
        {
            var isWorkDateValid = !TxtWorkDate.GetBindingExpression(DatePicker.SelectedDateProperty).HasError;
            if (isWorkDateValid)
            {
                DateTime workDate = DateTime.Parse(TxtWorkDate.Text);
                EmpManager empManager = new EmpManager();
                var empWorkHours = empManager.SelectEmpByWorkDate(workDate);
                if (empWorkHours.Count != 0)
                {
                    ListDisplayWin.ItemsSource = empWorkHours;
                    double totalHours = 0;
                    foreach (var empWorkHour in empWorkHours)
                        totalHours += empWorkHour.Hours;
                    TxtTotalHours.Text = totalHours + " hours";
                }
                else
                {
                    MessageBox.Show("No Record on Selected Date, Please Select Another Date!");
                    ListDisplayWin.ItemsSource = null;
                    TxtTotalHours.Text = "";
                }
                   
            }
            else
            {
                ListDisplayWin.ItemsSource = null;
                TxtTotalHours.Text = "";
                MessageBox.Show("Invalid Work Date, Please Double Check!");
            }
                
        }
    }
}
