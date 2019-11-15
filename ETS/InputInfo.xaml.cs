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
    /// Interaction logic for InputInfo.xaml
    /// </summary>
    public partial class InputInfo : Page
    {
        public InputInfo()
        {
            InitializeComponent();
            DataContext = new DataValidation();
        }

        private void SaveEmpInfo(object sender, RoutedEventArgs e)
        {
            var isEmpIDValid = !TxtID.GetBindingExpression(TextBox.TextProperty).HasError;
            var isFirstNameValid = !TxtFN.GetBindingExpression(TextBox.TextProperty).HasError;
            var isLastNameValid = !TxtLN.GetBindingExpression(TextBox.TextProperty).HasError;
            var isEmailValid = !TxtEmail.GetBindingExpression(TextBox.TextProperty).HasError;
            var isDOBValid = !TxtDOB.GetBindingExpression(DatePicker.SelectedDateProperty).HasError;
            var isPhoneValid = !TxtPh.GetBindingExpression(TextBox.TextProperty).HasError;

            if (!isEmpIDValid || !isFirstNameValid || !isLastNameValid || !isEmailValid || !isDOBValid || !isPhoneValid)
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
                empManager.InsertEmp(empID, firstName, lastName, email, dob, phone);
                MessageBox.Show("Employee Information Saved");
                var employees = empManager.SelectAllEmp();
                ListDisplayWin.ItemsSource = employees;
            }
            
        }

        private void ClearEmpInfo(object sender, RoutedEventArgs e)
        {
            TxtID.Text = "";
            TxtFN.Text = "";
            TxtLN.Text = "";
            TxtEmail.Text = "";
            TxtDOB.Text = "";
            TxtPh.Text = "";
        }

        private void RefreshDisplayWindow(object sender, RoutedEventArgs e)
        {
            EmpManager empManager = new EmpManager();
            var employees = empManager.SelectAllEmp();
            ListDisplayWin.ItemsSource = employees;
        }
    }
}
