using BusinessLogic;
using DataLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class StudentController: ApiController
    {
        StudentLogic student = new StudentLogic(new Context());
        [HttpGet]
        public IHttpActionResult GetAllStudents()
        {
            return Ok(student.Get());
        }

        [HttpPost]
        public IHttpActionResult AddStudent(Student studentObject)
        {
            student.Add(studentObject);
            return Ok("Student Added");
        }

        [HttpPut]
        public IHttpActionResult UpdateStudent(Student studentObject)
        {
            student.Update(studentObject);
            return Ok("Student Updated");
        }

        [HttpDelete]
        public IHttpActionResult DeleteStudent(int id)
        {
            student.Delete(id);
            return Ok("Student Deleted");
        }
    }
}