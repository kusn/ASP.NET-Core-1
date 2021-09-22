using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;
using WebStore.Data;
using WebStore.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace WebStore.Controllers
{
    //[Route("Staff/[action]/{id?}")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _EmployeesData;
        private readonly ILogger<EmployeesController> _Logger;


        public EmployeesController(IEmployeesData EmployeesData, ILogger<EmployeesController> Logger)
        {            
            _EmployeesData = EmployeesData;
            _Logger = Logger;
        }

        //[Route("~employees/all")]
        public IActionResult Index()                        // http://localhost:5000/Home/Employees
        {
            return View(_EmployeesData.GetAll());
        }

        //[Route("~employees/info-{id}")]
        public IActionResult Details(int id)            // http://localhost:5000/Home/Details/id
        {
            var employee = _EmployeesData.GetById(id);

            if (employee is null) return NotFound();


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
