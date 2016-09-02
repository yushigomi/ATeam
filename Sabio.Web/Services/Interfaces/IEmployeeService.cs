using Sabio.Starter.Template.Web.Domain.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Starter.Template.Web.Services.Interfaces
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
