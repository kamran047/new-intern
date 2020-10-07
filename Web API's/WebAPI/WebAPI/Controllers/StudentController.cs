using BusinessLogic;
using DataLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var studentData=student.Get();
            //Getting student course record after getting students data and saving them in studentViewModels
            var studentViewModels = student.GetStudentCourse(studentData);
            return Ok(studentViewModels);
        }

        [HttpPost]
        public IHttpActionResult AddStudent(StudentViewModel viewModel)
        {
            student.Add(viewModel.Student);
            student.AddStudentCourse(viewModel);
            return Ok("Student Added");
        }

        [HttpPut]
        public IHttpActionResult UpdateStudent(StudentViewModel viewModel)
        {
            student.Update(viewModel.Student);
            student.UpdatestudentCourse(viewModel);
            return Ok("Student Updated");
        }

        [HttpDelete]
        public IHttpActionResult DeleteStudent(int id)
        {
            student.Delete(id);
            student.DeleteStudentCourse(id);
            return Ok("Student Deleted");
        }
    }
}