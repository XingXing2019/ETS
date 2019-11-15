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
    /// Interaction logic for UpdateInfo.xaml
    /// </summary>
    public partial class UpdateInfo : Page
    {
        public UpdateInfo()
        {
            InitializeComponent();
            DataContext = new DataValidation() { WorkDate = null };
        }

        private void RefreshDisplayWin(object sender, RoutedEventArgs e)
        {
            EmpManager empManager = new EmpManager();
            var employees = empManager.SelectAllEmp();
            ListDisplayWin.ItemsSource = employees;
        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            var isEmpIDValid = TxtID.Text != "";
            var isDOBValid = TxtDOB.SelectedDate != null;
            if (!isEmpIDValid || !isDOBValid)
                MessageBox.Show("Some Inputs are Invalid! Please Double Check!");
            else
            {
                int empID = int.Parse(TxtID.Text);
                string firstName = TxtFN.Text;
                string lastName = TxtLN.Text;
                string email = TxtEmail.Text;
                DateTime dob = DateTime.Parse(TxtDOB.Text);
                string phone = TxtPh.Text;
                EmpManager empManager = new EmpManager();
                empManager.UpdateEmp(empID, firstName, lastName, email, dob, phone);
                MessageBox.Show("Employee Information Chenge Saved!");
            }
        }

        private void ClearChangeInfo(object sender, RoutedEventArgs e)
        {
            TxtID.Text = "";
            TxtFN.Text = "";
            TxtLN.Text = "";
            TxtEmail.Text = "";
            TxtDOB.Text = "";
            TxtPh.Text = "";
        }

        private void RecordHours(object sender, RoutedEventArgs e)
        {
            var isWorkDateValid = !TxtWorkDate.GetBindingExpression(DatePicker.SelectedDateProperty).HasError;
            var isHoursValid = !TxtHours.GetBindingExpression(TextBox.TextProperty).HasError;
            var isEmpIDValid = TxtID.Text != "";

            if (!isEmpIDValid || !isWorkDateValid || !isHoursValid)
                MessageBox.Show("Some Inputs are Invalid! Please Double Check!");
            else
            {
                int empID = int.Parse(TxtID.Text);
                DateTime workDate = DateTime.Parse(TxtWorkDate.Text);
                double hour = double.Parse(TxtHours.Text);
                EmpManager empManager = new EmpManager();
                empManager.InsertEmpHours(empID, workDate, hour);
                MessageBox.Show("Employee Working Hours Saved!");
                TxtWorkDate.Text = "";
                TxtHours.Text = "";
            }
        }

        private void DeleteEmpByID(object sender, RoutedEventArgs e)
        {
            var isEmpIDValid = TxtID.Text != "";
            if (isEmpIDValid)
            {
                int empID = int.Parse(TxtID.Text);
                EmpManager empManager = new EmpManager();
                empManager.DeleteEmpByID(empID);
                MessageBox.Show("Information Delete!");
                var employees = empManager.SelectAllEmp();
                ListDisplayWin.ItemsSource = employees;
            }
            else
                MessageBox.Show("Please Select an Employee First!");
        }
        private void DateSelected(object sender, RoutedEventArgs e)
        {
            var isWorkDateValid = !TxtWorkDate.GetBindingExpression(DatePicker.SelectedDateProperty).HasError;
            var isEmpIDValid = TxtID.Text != "";
            if (!isWorkDateValid || !isEmpIDValid)
                return;
            else
            {
                EmpManager empManager = new EmpManager();
                int empID = int.Parse(TxtID.Text);
                DateTime workDate = DateTime.Parse(TxtWorkDate.Text);
                double hours = empManager.ShowWorkHoursByDateAndEmpID(workDate, empID);
                var employee = empManager.SelectEmpByID(empID);
                if (hours == 0)
                    TxtWorkHours.Text = $"{employee.FirstName} had no working hours recorded on {workDate.ToString("dd-MM-yyyy")}.";
                else
                    TxtWorkHours.Text = $"{employee.FirstName} worded {hours} hours on {workDate.ToString("dd-MM-yyyy")}.";
            }
        }

        private void TxtID_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TxtWorkDate.SelectedDate = null;
            TxtWorkHours.Text = "";
        }
    }
}
