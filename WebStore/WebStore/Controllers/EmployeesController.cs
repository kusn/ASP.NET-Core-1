using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;
using WebStore.Data;

namespace WebStore.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEnumerable<Employee> _Employees;

        public EmployeesController()
        {
            _Employees = TestData.Employees;
        }

        public IActionResult Index()
        {
            return View(_Employees);
        }

        /*public IActionResult Employees()                // http://localhost:5000/Home/Employees
        {
            return View(__Employees);
        }

        public IActionResult Details(int? id)            // http://localhost:5000/Home/Details/id
        {
            if (id == null) return RedirectToAction("Index");
            ViewBag.__EmployeeId = id;

            return View(__Employees.Find(x => x.Id == id));
        }*/
    }
}
