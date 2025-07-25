using OctAPITry.Models;
using OctAPITry.Models.ModelClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace OctAPITry.Controllers
{
    public class StudentMVCController : Controller
    {
        // GET: StudentMVC
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetStudents()
        {
            // call API
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44321/api/StudentAPI");
            var response = client.GetAsync("StudentAPI");
            response.Wait();
            var test = response.Result;
            List<Student> stdList = new List<Student>();
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<Student>>();
                display.Wait();
               stdList= display.Result;
            }


            return View(stdList);
        }
        [HttpGet]
        public ActionResult SearchStudent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SearchStudent(tblStudent std)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44321/api/StudentAPI");
            var response=client.GetAsync("StudentAPI?Roll="+std.roll);
            response.Wait();
            var test = response.Result;
            tblStudent stud = new tblStudent();
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<tblStudent>();
                display.Wait();
                stud = display.Result;
            }

            return RedirectToAction("showStudent",stud);
        }
        [HttpGet]
        public ActionResult showStudent(tblStudent stud)
        {
            return View(stud);
        }
    }
}