using Sabio.Web.Domain.Tests;
using Sabio.Web.Services.Tests;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Web;

namespace Sabio.Web.Services.Tests
{
    public class TestEmployeeService : IEmployeeService
    {
         private List<TestEmployee> _data = new List<TestEmployee>();

        private int _nextId = 1;

        public TestEmployeeService()
        {
            Add(new TestEmployee { FirstName = "Oscar", LastName = "De La Hoya", Dob = new DateTime(1972, 6, 15), Title = "Golden Boy", Status=1 });
            Add(new TestEmployee { FirstName = "Floyd", LastName = "Mayweather", Dob = new DateTime(1976, 12, 1), Title = "Pretty Boy Floyd", Status = 1 });
            Add(new TestEmployee { FirstName = "Manny", LastName = "Pacquiao", Dob = new DateTime(1979, 3, 31), Title = "Pac Man", Status=1 });   
        }

        public List<TestEmployee> GetAll()
        {
            return _data;
        }

        public TestEmployee Get(int id)
        {
            int index = id - 1;
            index = (index < 0) ? 0 : index;

            if (this._data.ElementAtOrDefault(index) != null)
                return this._data.ElementAt(index);

            throw new ObjectNotFoundException("could not locate employee with id " + id);
        }

        public TestEmployee Add(TestEmployee item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }            

            // TO DO : Code to save record into database  
            item.Id = _nextId++;
            this._data.Add(item);
            return item; 
        }

        public bool Update(TestEmployee item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            // TO DO : Code to update record into database  
            int index = this._data.FindIndex(p => p.Id == item.Id);
            if (index == -1)
            {
                return false;
            }
            this._data.RemoveAt(index);
            this._data.Add(item);
            return true;   
        }

        public bool Delete(int id)
        {
            // TO DO : Code to remove the records from database  
            this._data.RemoveAll(p => p.Id == id);
            return true;   
        }
    }
}