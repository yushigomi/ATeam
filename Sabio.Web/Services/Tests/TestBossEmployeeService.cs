using Microsoft.Practices.Unity;
using Sabio.Web.Domain.Tests;
using Sabio.Web.Services.Tests;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Web;

namespace Sabio.Web.Services.Tests
{
    public class TestBossEmployeeService : IEmployeeService
    {
         private List<TestBoss> _data = new List<TestBoss>();

        private int _nextId = 1;

        private IEmployeeService _employeeService;

        public TestBossEmployeeService([Dependency("EmployeeService")] IEmployeeService employeeService)
        {
            _employeeService = employeeService;

            Add(new TestBoss { FirstName = "George", LastName = "Washington", Dob = new DateTime(1720, 6, 15), Title = "Father of America", Status = 1 });
            Add(new TestBoss { FirstName = "Abraham", LastName = "Lincoln", Dob = new DateTime(1810, 12, 1), Title = "Emancipator", Status = 1 });
            Add(new TestBoss { FirstName = "Theodore", LastName = "Roosevelt", Dob = new DateTime(1875, 3, 31), Title = "Bull Moose", Status = 1 });  
        }

        public TestEmployee Get(int id)
        {
            int index = id - 1;
            index = (index < 0) ? 0 : index;

            if (this._data.ElementAtOrDefault(index) != null)
            {
                TestBoss row = this._data.ElementAt(index);

                row.Employees = _employeeService.GetAll();

                return row;
            }
                
            throw new ObjectNotFoundException("could not locate boss with id " + id);
        }

        public List<TestEmployee> GetAll()
        {
            throw new NotImplementedException();
        }

        public TestEmployee Add(TestEmployee item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }            

            // TO DO : Code to save record into database  
            item.Id = _nextId++;
            this._data.Add((TestBoss) item);
            return item; 
        }

        public bool Update(TestEmployee item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}