using Sabio.Web.Domain.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Web.Services.Tests
{
    public interface IEmployeeService
    {
        List<TestEmployee> GetAll();

        TestEmployee Get(int id);

        TestEmployee Add(TestEmployee item);

        bool Update(TestEmployee item);

        bool Delete(int id);   
    }
}
