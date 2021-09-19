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

        public IActionResult Index()                        // http://localhost:5000/Home/Employees
        {
            return View(_Employees);
        }

        public IActionResult Details(int? id)            // http://localhost:5000/Home/Details/id
        {
            if (id == null) return RedirectToAction("Index");
            
            var employee = _Employees.SingleOrDefault(x => x.Id == id);
            /*if (employee is null)
                return NotFound();*/
            //ViewBag.Employee = employee;
            
            return View(employee);
        }

        public IActionResult Edit(int id)               // // http://localhost:5000/Home/Edit/id
        {
            var employee = _Employees.SingleOrDefault(x => x.Id == id);
            if (employee is null)
                return NotFound();

            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(Employee emp)
        {
            _Employees.ElementAt(emp.Id - 1).FirstName = emp.FirstName;
            _Employees.ElementAt(emp.Id - 1).LastName = emp.LastName;
            _Employees.ElementAt(emp.Id - 1).Age = emp.Age;
            _Employees.ElementAt(emp.Id - 1).Patronymic = emp.Patronymic;
            _Employees.ElementAt(emp.Id - 1).DateOfBorn = emp.DateOfBorn;
            _Employees.ElementAt(emp.Id - 1).DateOfEmployment = emp.DateOfEmployment;

            return RedirectToAction("Index");
        }
    }
}
