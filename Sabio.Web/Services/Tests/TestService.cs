using Sabio.Web.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

//Sabio: including this namespace is critical to use some of the utility funcitons we have built into our code
// these utility functions are Extension Methods built off of IDataReader
using Sabio.Data;
using Sabio.Web.Models.Requests.Tests;
using Sabio.Web.Domain.Tests;

namespace Sabio.Web.Services.Tests
{

    public class TestService : BaseService, Sabio.Web.Services.Tests.ITestService
    {
       

        public static Guid InsertTest(TestPersonAddRequest model)
        {
            Guid uid = Guid.Empty;//000-0000-0000-0000

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.TestTable_Insert"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@FirstName", model.Name);
                   paramCollection.AddWithValue("@Last", model.Last);
                   paramCollection.AddWithValue("@Age", model.Age);
                   paramCollection.AddWithValue("@Email", model.Email);


                   SqlParameter p = new SqlParameter("@Uid", System.Data.SqlDbType.UniqueIdentifier);
                   p.Direction = System.Data.ParameterDirection.Output;

                   paramCollection.Add(p);

               }, returnParameters: delegate(SqlParameterCollection param)
               {
                   Guid.TryParse(param["@Uid"].Value.ToString(), out uid);
               }
               );


            return uid;
        }


        public static Guid InsertTest(string name)
        {
            Guid uid = Guid.Empty;//000-0000-0000-0000

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.TestTable_Insert"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@FirstName", name);


                   SqlParameter p = new SqlParameter("@Uid", System.Data.SqlDbType.UniqueIdentifier);
                   p.Direction = System.Data.ParameterDirection.Output;

                   paramCollection.Add(p);

               }, returnParameters: delegate(SqlParameterCollection param)
               {
                   Guid.TryParse(param["@Uid"].Value.ToString(), out uid);
               }
               );


            return uid;
        }


        public static List<TestPerson> Get(bool demoError = true)
        {
            List<TestPerson> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.TestTable_Select"
               , inputParamMapper: null
               , map: delegate(IDataReader reader, short set)
               {
                   TestPerson p = new TestPerson();
                   int startingIndex = 0; //startingOrdinal

                   if (demoError)
                   {
                       p.Name = reader.GetString(startingIndex++);
                       p.Last = reader.GetString(startingIndex++);

                       p.Age = reader.GetInt32(startingIndex++);
                   }
                   else
                   {
                       p.Name = reader.GetSafeString(startingIndex++);
                       p.Last = reader.GetSafeString(startingIndex++);

                       p.Age = reader.GetSafeInt32(startingIndex++);
                   }

                   if (list == null)
                   {
                       list = new List<TestPerson>();
                   }

                   list.Add(p);
               }
               );


            return list;
        }


        public static List<TestPerson> StructuredDataTypes(Guid[] guids)
        {
            List<TestPerson> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.TestTable_Structured"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   SqlParameter p = new SqlParameter("@ParamName", System.Data.SqlDbType.Structured);

                   if (guids != null && guids.Any())
                   {
                       p.Value = new Sabio.Data.UniqueIdTable(guids);
                   }

                   paramCollection.Add(p);

               }, map: delegate(IDataReader reader, short set)
               {
                   TestPerson p = new TestPerson();
                   int startingIndex = 0; //startingOrdinal

                   p.Name = reader.GetSafeString(startingIndex++);
                   p.Last = reader.GetSafeString(startingIndex++);

                   p.Age = reader.GetSafeInt32(startingIndex++);


                   if (list == null)
                   {
                       list = new List<TestPerson>();
                   }

                   list.Add(p);
               }
               );


            return list;
        }


        public static List<TestEmployee> GetEmployees(List<int> ids)
        {
            List<TestEmployee> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Employees_SelectByIds"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   SqlParameter p = new SqlParameter("@EmployeeIds", System.Data.SqlDbType.Structured);

                   if (ids != null && ids.Any())
                   {
                       p.Value = new Sabio.Data.IntIdTable(ids);
                   }

                   paramCollection.Add(p);

               }, map: delegate(IDataReader reader, short set)
               {
                   TestEmployee p = new TestEmployee();
                   int startingIndex = 0; //startingOrdinal

                   p.Id = reader.GetSafeInt32(startingIndex++);
                   p.LastName = reader.GetSafeString(startingIndex++);

                   p.FirstName = reader.GetSafeString(startingIndex++);
                   p.Title = reader.GetSafeString(startingIndex++);
                   p.Dob = reader.GetSafeDateTime(startingIndex++);
                   p.Status = reader.GetSafeInt32(startingIndex++);

                   if (list == null)
                   {
                       list = new List<TestEmployee>();
                   }

                   list.Add(p);
               }
               );


            return list;
        }


        internal static List<Domain.Tests.TestEmployee> GetEmployees()
        {
            List<TestEmployee> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Employees_Select"
               , inputParamMapper: null
               , map: delegate(IDataReader reader, short set)
               {
                   TestEmployee p = new TestEmployee();
                   int startingIndex = 0; //startingOrdinal
                   
                   p.Id = reader.GetSafeInt32(startingIndex++);
                   p.LastName = reader.GetSafeString(startingIndex++);

                   p.FirstName = reader.GetSafeString(startingIndex++);
                   p.Title = reader.GetSafeString(startingIndex++);
                   p.Dob = reader.GetSafeDateTime(startingIndex++);
                   p.Status = reader.GetSafeInt32(startingIndex++);

                   if (list == null)
                   {
                       list = new List<TestEmployee>();
                   }

                   list.Add(p);
               }
               );


            return list;
        }
    }




}