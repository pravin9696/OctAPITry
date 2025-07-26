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
        public ActionResult SearchStudent(tblStudent std,String btnSubmit)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44321/api/StudentAPI");
            var response = client.GetAsync("StudentAPI?Roll=" + std.roll);
            response.Wait();
            var test = response.Result;
            tblStudent stud = new tblStudent();
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<tblStudent>();
                display.Wait();
                stud = display.Result;
            }             
            
            if (btnSubmit == "ShowDetails")
            {

                return RedirectToAction("showStudent", stud);


            }
            else if (btnSubmit=="Edit")
            {               
                    return RedirectToAction("updateStudent", stud);               
                
            }
            ModelState.AddModelError("", "Record not Found!!!");
            return View();
        }
        
        [HttpGet]
        public ActionResult showStudent(tblStudent stud)
        {
            return View(stud);
        }
        [HttpGet]
        public ActionResult InsertStudent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InsertStudent(Student st)
        {
            if (ModelState.IsValid)
            {
                tblStudent std = new tblStudent();
                std.id = st.id;
                std.roll = st.roll;
                std.Name = st.Name;
                std.TotalMarks = st.TotalMarks;

                // Call APi to insert student data into DB
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:44321/api/StudentAPI");
                var response = client.PostAsJsonAsync<tblStudent>("StudentAPI", std);
                response.Wait();

                var test = response.Result;
                if (test.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                
            }
            ModelState.AddModelError("", "Record not inserted!!!!");
            return View();
        }
        [HttpGet]
        public ActionResult SearchStudentToEdit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SearchStudentToEdit(tblStudent std)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44321/api/StudentAPI");
            var response = client.GetAsync("StudentAPI?Roll=" + std.roll);
            response.Wait();
            var test = response.Result;
            tblStudent stud = new tblStudent();
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<tblStudent>();
                display.Wait();
                stud = display.Result;
                return RedirectToAction("updateStudent", stud);
            }
            ModelState.AddModelError("", "Record not Found!!!");
            return View();
          
        }
        [HttpGet]
        public ActionResult updateStudent(tblStudent std)
        {
            return View(std);
        }
        [HttpPost]
        public ActionResult updateStudentSave(tblStudent std)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:44321/api/StudentAPI");
                var response = client.PutAsJsonAsync<tblStudent>("StudentAPI", std);
                response.Wait();

                var test = response.Result;
                if (test.IsSuccessStatusCode)
                {
                    return RedirectToAction("index");
                }
               
            }
            return RedirectToAction("updateStudent", std);

        }
    }
}