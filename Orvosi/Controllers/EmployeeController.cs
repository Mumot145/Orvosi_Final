using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Orvosi.Models;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class EmployeeController : Controller
    {

        // GET: Employee
        public ActionResult Details()
        {
            var info = this.Request.QueryString["info"];
            EmployeeContext employeeContext = new EmployeeContext();
            Employee employee = new Employee();

            if (info == "random")
            {
                Random rnd = new Random();
                string random = rnd.Next(1, 1001).ToString();
                employee = employeeContext.Employees.Single(emp => emp.id == random);
                return View(employee);
            }
            else if (info == "all")
            {
                List<Employee> employees = new List<Employee>();
                for (int i = 1; i < 1001; i++)
                {
                    employees.Add(employeeContext.Employees.Single(emp => emp.id == i.ToString()));

                }
                return View("~/Views/Employee/AllDetails.cshtml", employees);
            }
            return View("~/Views/Home/Index.cshtml");
        }
        [HttpPost]
        public ActionResult SearchDetails(string search)
        {
            int test = 0;
            EmployeeContext employeeContext = new EmployeeContext();

            if (int.TryParse(search, out test) == true)
            {
                Employee employee = new Employee();
                employee = employeeContext.Employees.Single(emp => emp.id == search.ToString());
                return View("~/Views/Employee/Details.cshtml", employee);
            }
            else if (search != String.Empty)
            {
                List<Employee> employees = new List<Employee>();
                string query = "SELECT * FROM Users"
                + " WHERE first_name LIKE '" + search + "%'"
                + " OR first_name LIKE '%" + search + "'"
                + " OR first_name LIKE '%" + search + "%'"
                + " OR first_name = '" + search + "'";
                IEnumerable<Employee> data = employeeContext.Database.SqlQuery<Employee>(query);

                string query2 = "SELECT * FROM Users"
                + " WHERE last_name LIKE '" + search + "%'"
                + " OR last_name LIKE '%" + search + "'"
                + " OR last_name LIKE '%" + search + "%'"
                + " OR last_name = '" + search + "'";
                IEnumerable<Employee> data2 = employeeContext.Database.SqlQuery<Employee>(query2);

                var model = new FirstAndLastNameModel { FirstN = data, LastN = data2 };
                return View("~/Views/Employee/SearchedDetails.cshtml", model);
            }
            return View("~/Views/Home/Index.cshtml");

        }

    }
    public class FirstAndLastNameModel
    {
        public IEnumerable<Employee> FirstN { get; set; }
        public IEnumerable<Employee> LastN { get; set; }
    }
}

