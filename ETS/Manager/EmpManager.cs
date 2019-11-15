using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ETS.Entity;

namespace ETS.Manager
{
    public class EmpManager
    {
        //Methods using LINQ
        public void InsertEmp(int empID, string firstName, string lastName, string email, DateTime dob, string phone)
        {
            using (var dbContext = new EmployeeWorkingHoursEntities())
            {
                var employee = new Employee() { EmpID = empID, FirstName = firstName, LastName = lastName, Email = email, DOB = dob, Phone = phone };
                dbContext.Employees.Add(employee);
                dbContext.SaveChanges();
            }
        }
        public List<Employee> SelectAllEmp()
        {
            using (var dbContext = new EmployeeWorkingHoursEntities())
            {
                var employees = dbContext.Employees.ToList();
                return employees;
            }
        }
        public List<EmpWorkHour> SelectEmpByWorkDate(DateTime workDate)
        {
            using (var dbContext = new EmployeeWorkingHoursEntities())
            {
                var empWorkHours = dbContext.EmpWorkHours.Where(e => e.WorkDate == workDate).ToList();
                foreach (var empWorkHour in empWorkHours)
                    empWorkHour.Employee = dbContext.Employees.First(e => e.EmpID == empWorkHour.EmpID);
                return empWorkHours;
            }
        }
        public double ShowWorkHoursByDateAndEmpID(DateTime workDate, int empID)
        {
            using (var dbContext = new EmployeeWorkingHoursEntities())
            {
                var empWorkHours = dbContext.EmpWorkHours.Where(e => e.WorkDate == workDate).ToList();
                double hours = 0;
                foreach (var empHour in empWorkHours)
                    if (empHour.EmpID == empID)
                        hours += empHour.Hours;
                return hours;
            }
        }
        public void UpdateEmp(int empID, string firstName, string lastName, string email, DateTime dob, string phone)
        {
            using (var dbContext = new EmployeeWorkingHoursEntities())
            {
                var employee = dbContext.Employees.Where(p => p.EmpID == empID).First();
                employee.FirstName = firstName;
                employee.LastName = lastName;
                employee.Email = email;
                employee.DOB = dob;
                employee.Phone = phone;
                dbContext.SaveChanges();
            }
        }
        public void DeleteEmpByID(int empID)
        {
            using (var dbContext = new EmployeeWorkingHoursEntities())
            {
                var employee = dbContext.Employees.First(p => p.EmpID == empID);
                var empWorkHours = dbContext.EmpWorkHours.Where(e => e.EmpID == empID).ToList();
                dbContext.Employees.Remove(employee);
                dbContext.EmpWorkHours.RemoveRange(empWorkHours);
                dbContext.SaveChanges();
            }
        }
        public void InsertEmpHours(int empID, DateTime workDate, double hours)
        {
            using (var dbContext = new EmployeeWorkingHoursEntities())
            {
                var empHours = new EmpWorkHour() { EmpID = empID, WorkDate = workDate, Hours = hours };
                dbContext.EmpWorkHours.Add(empHours);
                dbContext.SaveChanges();
            }
        }
        public Employee SelectEmpByID(int empID)
        {
            using (var dbContext = new EmployeeWorkingHoursEntities())
            {
                if (dbContext.Employees.Any(p => p.EmpID == empID))
                {
                    var employee = dbContext.Employees.Where(p => p.EmpID == empID).First();
                    return employee;
                }
                else
                    return null;
            }
        }
        public double CalcTotalHoursbyEmpID(int empID)
        {
            using (var dbContext = new EmployeeWorkingHoursEntities())
            {
                double totalHours = 0;
                var empWorkHours = dbContext.EmpWorkHours.Where(h => h.EmpID == empID).ToList();
                foreach (var h in empWorkHours)
                    totalHours += h.Hours;
                return totalHours;
            }
        }
    }
}
