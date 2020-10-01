using BusinessLogic;
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
        StudentLogic student = new StudentLogic();
        [HttpGet]
        public IHttpActionResult GetAllStudents()
        {

            return Ok(student.GetStudents());
        }

        [HttpPost]
        public IHttpActionResult AddStudent(Student studentObject)
        {
            student.AddStudent(studentObject);
            return Ok("Student Added");
        }

        [HttpPut]
        public IHttpActionResult UpdateStudent(Student studentObject)
        {
            student.UpdateStudent(studentObject);
            return Ok("Student Updated");
        }

        [HttpDelete]
        public IHttpActionResult DeleteStudent(Student studentObject)
        {
            student.DeleteStudent(studentObject.StudentId);
            return Ok("Student Deleted");
        }
    }
}