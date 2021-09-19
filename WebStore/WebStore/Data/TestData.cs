using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Data
{
    public static class TestData
    {
        public static List<Employee> Employees { get; set; } = new()
        {
            new Employee { Id = 1, LastName = "Иванов", FirstName = "Иван", Patronymic = "Иванович", Age = 27, DateOfBorn = Convert.ToDateTime("01.01.2001"), DateOfEmployment = Convert.ToDateTime("02.02.2019") },
            new Employee { Id = 2, LastName = "Петров", FirstName = "Петр", Patronymic = "Петрович", Age = 31, DateOfBorn = Convert.ToDateTime("03.03.2002"), DateOfEmployment = Convert.ToDateTime("04.04.2020") },
            new Employee { Id = 3, LastName = "Сидоров", FirstName = "Сидор", Patronymic = "Сидорович", Age = 18, DateOfBorn = Convert.ToDateTime("05.05.2003"), DateOfEmployment = Convert.ToDateTime("06.06.2021") }
        };
    }
}
