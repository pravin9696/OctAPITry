using OctAPITry.Models;
using OctAPITry.Models.ModelClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace OctAPITry.Controllers
{

    public class StudentAPIController : ApiController
    {
        DB_authentication_authorisation_Oct_210725Entities dbo = new DB_authentication_authorisation_Oct_210725Entities();

        //API to read all student information

        [HttpGet]
        public IHttpActionResult getAllStudent()
        {
            List<tblStudent> studList = dbo.tblStudents.ToList();
            List<Student> sList = studList.Select(x => new Student() { id = x.id, Name = x.Name, roll = x.roll, TotalMarks = x.TotalMarks }).ToList();

            return Ok(sList);
        }
        [HttpGet]
            public IHttpActionResult getStudByRoll(int roll)
            {
                var std = dbo.tblStudents.FirstOrDefault(x => x.roll == roll);
                if (std == null)
                {
                    return NotFound();
                }
                else
            {
                return Ok(std);
            }
        }
        [HttpGet]
        public IHttpActionResult getStudentById(int id)
        {
            var stud = dbo.tblStudents.Find(id);
            if (stud!=null)
            {
                return Ok(stud);
            }
            return NotFound();
        }
        [HttpPost]
        public IHttpActionResult  insertStudent(tblStudent std)
        {
            dbo.tblStudents.Add(std);
            int n = dbo.SaveChanges();
            if (n>0)
            {
                return Ok();
            }
            else
            {
                return InternalServerError();
            }
        }
        [HttpPut]
        public IHttpActionResult updateStudent(tblStudent std)
        {
            var stud = dbo.tblStudents.FirstOrDefault(x => x.id == std.id);
            if (stud==null)
            {
                return NotFound();
            }
            else
            {
                stud.roll = std.roll;
                stud.Name = std.Name;
                stud.TotalMarks = std.TotalMarks;
                int n = dbo.SaveChanges();
                if (n>0)
                {
                    return Ok();
                }
                else
                {
                    return InternalServerError();
                }
            }
        }
        [HttpDelete]
        public IHttpActionResult DeleteStudById(int id)
        {
            var dbo = new DB_authentication_authorisation_Oct_210725Entities();
            var stud = dbo.tblStudents.FirstOrDefault(x => x.id == id);
            if (stud != null)
            {
                dbo.tblStudents.Remove(stud);
                try
                {
                    int n = dbo.SaveChanges();
                    if (n > 0)
                    {
                        return Ok();
                    }
                }
                 catch (Exception e)
                {

                    return BadRequest();
                }
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
