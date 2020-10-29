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
    public class StudentController : ApiController
    {
        private IStudentLogic _student;

        public StudentController(IStudentLogic student)
        {
            _student = student;
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetAllStudents()
        {
            var studentData = _student.Get();
            //Getting student course record after getting students data and saving them in studentViewModels
            var studentViewModels = _student.GetStudentCourse(studentData);
            return Ok(studentViewModels);
        }

        [HttpPost]
        public IHttpActionResult AddStudent(StudentViewModel viewModel)
        {
            _student.Add(viewModel.Student);
            _student.AddStudentCourse(viewModel);
            return Ok("Student Added");
        }

        [HttpPut]
        public IHttpActionResult UpdateStudent(StudentViewModel viewModel)
        {
            _student.Update(viewModel.Student);
            _student.UpdatestudentCourse(viewModel);
            return Ok("Student Updated");
        }

        [HttpDelete]
        public IHttpActionResult DeleteStudent(int id)
        {
            _student.Delete(id);
            return Ok("Student Deleted");
        }
    }
}