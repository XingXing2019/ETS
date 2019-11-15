using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace ETS.Manager
{
    public class DataValidation : IDataErrorInfo
    {
        public int? EmpID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? DOB { get; set; }
        public string Phone { get; set; }
        public DateTime? WorkDate { get; set; }
        public double? Hours { get; set; }
        public string this[string columnName]
        {
            get
            {
                string msg = "";
                switch (columnName)
                {
                    case "EmpID":
                        if (EmpID == null)
                            msg = "EmpID Cannot be Empty!";
                        else if (EmpID.HasValue && EmpID <= 0)
                            msg = "Value Invaild, Try Again!";
                        break;
                    case "FirstName":
                        if (string.IsNullOrWhiteSpace(FirstName))
                            msg = "First Name Cannot be Empty!";
                        else if(!CheckName(FirstName))
                            msg = "Invaild Name Format, Try Again";
                        break;
                    case "LastName":
                        if (string.IsNullOrWhiteSpace(LastName))
                            msg = "Last Name Cannot be Empty!";
                        else if(!CheckName(LastName))
                            msg = "Invaild Name Format, Try Again";
                        break;
                    case "DOB":
                        if(DOB == null)
                            msg = "DOB Cannot be Empty!";
                        else if (DOB.HasValue && (DOB.Value.Year < 1919 || DOB.Value.Date > DateTime.Today))
                            msg = "Invaild DOB, Try Again!";
                        break;
                    case "Email":
                        if (string.IsNullOrWhiteSpace(Email))
                            msg = "Email Cannot be Empty!";
                        else if (!CheckEmail(Email))
                            msg = "Invaild Email Format, Try Again";
                        break;
                    case "Phone":
                        if (string.IsNullOrWhiteSpace(Phone))
                            msg = "Phone Cannot be Empty!";
                        else if (!CheckPhone(Phone))
                            msg = "Invaild Phone Number Format, Try Again";
                        break;
                    case "WorkDate":
                        if (WorkDate == null)
                            msg = "Work Date Cannot be Empty";
                        else if (WorkDate.HasValue && WorkDate.Value > DateTime.Today)
                            msg = "Work Date Cannot be in Future!";
                        break;
                    case "Hours":
                        if (Hours == null)
                            msg = "Work Hours Cannot be Empty";
                        else if (Hours <= 0.0 || Hours > 8.0)
                            msg = "Invalid Value, Try Again";
                        break;
                }
                return msg;
            }
        }
        public string Error
        {
            get
            {
                return null;
            }
        }
        public static bool CheckEmail(string Email)
        {
            bool Flag = false;
            string str = "([a-zA-Z0-9_\\.\\-])+\\@(([a-zA-Z0-9\\-])+\\.)+([a-zA-Z0-9]{2,5})+";
            string[] result = GetPathPoint(Email, str);
            if (result != null)
            {
                Flag = result.Contains(Email) ? true : Flag;
            }
            return Flag;
        }
        public static bool CheckPhone(string phone)
        {
            return Regex.IsMatch(phone, @"^(04)[0-9]{8}$");
        }
        public static bool CheckName(string name)
        {
            return Regex.IsMatch(name, @"[a-zA-Z]+");
        }
        public static string[] GetPathPoint(string value, string regx)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;
            bool isMatch = System.Text.RegularExpressions.Regex.IsMatch(value, regx);
            if (!isMatch)
                return null;
            System.Text.RegularExpressions.MatchCollection matchCol = System.Text.RegularExpressions.Regex.Matches(value, regx);
            string[] result = new string[matchCol.Count];
            if (matchCol.Count > 0)
            {
                for (int i = 0; i < matchCol.Count; i++)
                {
                    result[i] = matchCol[i].Value;
                }
            }
            return result;
        }
    }
}
