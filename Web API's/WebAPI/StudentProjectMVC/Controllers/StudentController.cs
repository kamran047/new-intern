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
        StudentLogic studentContext = new StudentLogic(new Context());
        CourseLogic courseContext = new CourseLogic(new Context());

        public ActionResult StudentList()
        {
            //Getting student list using studentContext object and passing it to GetStudentCourse method to get the list of courses of students.
            var viewModel = studentContext.GetStudentCourse(studentContext.Get()).ToList();
            //Making an Object of StudentCourseViewModel and passing it directly to the view.
            return View(new StudentCourseViewModel
            {
                StudentViewModels = viewModel,
                CoursesList = courseContext.Get().ToList()
            });
        }

        [HttpGet]
        public ActionResult AddStudentForm()
        {
            return View(new StudentCourseViewModel { CoursesList = courseContext.Get().ToList() });
        }

        [HttpPost]
        public ActionResult SaveStudent(StudentViewModel model)
        {
            //This will run when new student is added to the list.
            if (model.Student.StudentId == 0)
            {
                studentContext.Add(model.Student);
                studentContext.AddStudentCourse(model);
            }
            //This code will run when we will update student record.
            else
            {
                studentContext.Update(model.Student);
                studentContext.UpdatestudentCourse(model);
            }
            return RedirectToAction("StudentList");
        }

        public ActionResult DeleteStudentRecord(int id)
        {
            studentContext.Delete(id);
            return RedirectToAction("StudentList");
        }

        public ActionResult UpdateStudentRecord(int id)
        {
            //Getting student and passing it to GetStudentCourse method to get its registered courses.
            var studentViewModel = studentContext.GetStudentCourse(studentContext.Get()).ToList();
            var viewModel = studentViewModel.SingleOrDefault(svm => svm.Student.StudentId == id);
            return View("AddStudentForm", new StudentCourseViewModel
            {
                Student = viewModel.Student,
                CoursesList = courseContext.Get().ToList(),
                Courses = viewModel.Courses
            });
        }
    }
}