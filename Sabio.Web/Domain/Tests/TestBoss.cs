using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain.Tests
{
    public class TestBoss : TestEmployee
    {
        public List<TestEmployee> Employees { get; set; }
    }
}