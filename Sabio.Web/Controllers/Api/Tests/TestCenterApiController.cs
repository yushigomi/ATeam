using Microsoft.Practices.Unity;
//using Sabio.Web.Controllers.Attributes;
using Sabio.Web.Domain;
using Sabio.Web.Domain.Tests;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Requests.Tests;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services.Tests;

using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sabio.Web.Controllers.Api.Tests
{
    [RoutePrefix("api/testcenter")]
    public class TestCenterApiController : ApiController
    {
        private IEmployeeService _employeeService;
        private IEmployeeService _bossService;
        private ITestService _testService;

        //  see http://stackoverflow.com/a/27902226 for example on getting named instances to work      
        public TestCenterApiController(ITestService testService)
        {
            _testService = testService;
        }

        public TestCenterApiController()
        {
           
        }


        public TestCenterApiController(
            [Dependency("EmployeeService")] IEmployeeService employeeService, 
            [Dependency("BossService")] IEmployeeService bossService)
        {
            _employeeService = employeeService;
            _bossService = bossService;
        }

        #region - Dependency Injection Test Calls -
        [Route("injection/{employeeId:int}"), HttpGet]
        public HttpResponseMessage TestInjectedService(int employeeId)
        {
            TestEmployee employee = null;

            try
            {
                employee = _employeeService.Get(employeeId);
            }
            catch(ObjectNotFoundException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
            }

            ItemResponse<TestEmployee> response = new ItemResponse<TestEmployee>();

            response.Item = employee;

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

       // [SabioAuthorize]
        [Route("injection/{employeeId:int}/boss"), HttpGet]
        public HttpResponseMessage TestInjectedBossService(int employeeId)
        {
            TestBoss boss = null;

            try
            {
                //  we have to cast this to TestBoss explicitly because the interface specifies a TestEmployee. TestBoss inherits from TestEmployee.
                boss = (TestBoss) _bossService.Get(employeeId);
            }
            catch (ObjectNotFoundException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
            }

            ItemResponse<TestBoss> response = new ItemResponse<TestBoss>();

            response.Item = boss;

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        #endregion

        #region - TestService Calls -
        [Route("EchoPerson"), HttpPost]
        public HttpResponseMessage EchoPerson(TestPersonAddRequest model)
        {
            /*
             *There is no datavalidation in this method. this is simply here for you
             *to be able to echo data back to yourself with no validation
             
             */

            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        [Route("EchoPerson/validate"), HttpPost]
        public HttpResponseMessage EchoPersonValidation(TestPersonAddRequest model)
        {
            // if the Model does not pass validation, there will be an Error response returned with errors
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        [Route("people"), HttpPost]
        public HttpResponseMessage AddPerson(TestPersonAddRequest model)
        {
            // if the Model does not pass validation, there will be an Error response returned with errors
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }


            ItemResponse<Guid> response = new ItemResponse<Guid>();

            response.Item = TestService.InsertTest(model);

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }


        [Route("people"), HttpGet]
        public HttpResponseMessage GetPeople(bool demoError = true)
        {
            ItemsResponse<TestPerson> response = new ItemsResponse<TestPerson>();

            response.Items = TestService.Get(demoError);

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route("people/noerrors"), HttpGet]
        public HttpResponseMessage GetPeople()
        {
            ItemsResponse<TestPerson> response = new ItemsResponse<TestPerson>();

            response.Items = TestService.Get(false);

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        #endregion


        [Route("eta/qs")]
        public HttpResponseMessage Eta([FromUri]TestEtaRequests model)
        {
            ItemResponse<TestEtaRequests> response = new ItemResponse<TestEtaRequests>();
            response.Item = model;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }



        [Route("employees"), HttpGet]
        public HttpResponseMessage GetEmps()
        {
            ItemsResponse<TestEmployee> response = new ItemsResponse<TestEmployee>();

            response.Items = TestService.GetEmployees();

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }


        [Route("employees/search"), HttpGet]
        public HttpResponseMessage GetEmps([FromUri]ItemsRequest<int> model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<TestEmployee> response = new ItemsResponse<TestEmployee>();

            response.Items = TestService.GetEmployees(model.Items);

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }


    }


}
