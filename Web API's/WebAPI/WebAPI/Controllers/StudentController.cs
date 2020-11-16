using BusinessLogic;
using DataLayer;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
            char[] seperator = { ':', '/', ';' };
            String[] strlist = viewModel.ImagePath[0].Split(seperator);
            byte[] bytes = Convert.FromBase64String(viewModel.ImagePath[1]);
            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }
            var subpath = "~/Images/";
            bool exist = Directory.Exists(HttpContext.Current.Server.MapPath(subpath));
            if (!exist) Directory.CreateDirectory(HttpContext.Current.Server.MapPath(subpath));
            var fileName = viewModel.Student.Name + "." + strlist[2];
            var filepath = HttpContext.Current.Server.MapPath("~/Images/");
            var path = Path.Combine(filepath, fileName);
            if (strlist[2] == "png") image.Save(path, ImageFormat.Png);
            viewModel.Student.ImagePath = fileName;
            _student.Add(viewModel.Student);
            _student.AddStudentCourse(viewModel);
            return Ok("Student Added");
        }

        [HttpPut]
        public IHttpActionResult UpdateStudent(StudentViewModel viewModel)
        {
            if (viewModel.Student == null) return BadRequest();
            char[] seperator = { ':', '/', ';' };
            String[] strlist = viewModel.ImagePath[0].Split(seperator);
            byte[] bytes = Convert.FromBase64String(viewModel.ImagePath[1]);
            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }
            var subpath = "~/Images/";
            bool exist = Directory.Exists(HttpContext.Current.Server.MapPath(subpath));
            if (!exist) Directory.CreateDirectory(HttpContext.Current.Server.MapPath(subpath));
            var fileName = viewModel.Student.Name + "." + strlist[2];
            var filepath = HttpContext.Current.Server.MapPath("~/Images/");
            var path = Path.Combine(filepath, fileName);
            if (strlist[2] == "png") image.Save(path, ImageFormat.Png);

            var updatedStudent = _student.GetOne(viewModel.Student.StudentId);
            updatedStudent.Name = viewModel.Student.Name;
            updatedStudent.Email = viewModel.Student.Email;
            updatedStudent.Password = viewModel.Student.Password;
            updatedStudent.ConfirmPassword = viewModel.Student.ConfirmPassword;
            updatedStudent.PhoneNo = viewModel.Student.PhoneNo;
            updatedStudent.ImagePath = fileName;

            _student.Update(updatedStudent);
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