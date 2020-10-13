using BusinessLogic;
using DataLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentProjectMVC.Controllers
{
    public class StudentController : Controller
    {
        StudentLogic studentRepository = new StudentLogic();
        CourseLogic courseRepository = new CourseLogic();

        public ActionResult StudentList()
        {
            //Getting student list using studentContext object and passing it to GetStudentCourse method to get the list of courses of students.
            var viewModel = studentRepository.GetStudentCourse(studentRepository.Get()).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AjaxGetJsonData()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var viewModel = studentRepository.GetStudentCourse(studentRepository.Get()).ToList();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            return Json(
                new
                {
                    draw = draw,
                    recordsFiltered = viewModel.Count,
                    recordsTotal = viewModel.Count,
                    data = viewModel.Skip(skip).Take(pageSize).ToList(),
                },
                    JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddStudentForm()
        {
            return View(new StudentCourseViewModel { CoursesList = courseRepository.Get().ToList() });
        }

        [HttpPost]
        public ActionResult SaveStudent(StudentViewModel model)
        {
            //This will run when new student is added to the list.
            if (model.Student.StudentId == 0)
            {
                studentRepository.Add(model.Student);
                studentRepository.AddStudentCourse(model);
            }
            //This code will run when we will update student record.
            else
            {
                studentRepository.Update(model.Student);
                studentRepository.UpdatestudentCourse(model);
            }
            return RedirectToAction("StudentList");
        }

        public ActionResult DeleteStudentRecord(int id)
        {
            studentRepository.Delete(id);
            return RedirectToAction("StudentList");
        }

        public ActionResult UpdateStudentRecord(int id)
        {
            //Getting student and passing it to GetStudentCourse method to get its registered courses.
            var studentViewModel = studentRepository.GetStudentCourse(studentRepository.Get()).ToList();
            var viewModel = studentViewModel.SingleOrDefault(svm => svm.Student.StudentId == id);
            return View("AddStudentForm", new StudentCourseViewModel
            {
                Student = viewModel.Student,
                CoursesList = courseRepository.Get().ToList(),
                Courses = viewModel.Courses
            });
        }
    }
}