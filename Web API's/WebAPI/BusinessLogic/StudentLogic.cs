using DataLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class StudentLogic : BaseRepository<Student>
    {
        public StudentLogic(Context context) : base(context)
        {
        }

        public void AddStudentCourse(StudentViewModel viewModel)
        {
            using (var context = new Context())
            {
                foreach (var course in viewModel.Courses)
                {
                    //Getting student id and coursse id from the Student and course table using view model and saving them in Student Courses table
                    context.StudentCourses
                        .Add(new StudentCourse { StudentId = viewModel.Student.StudentId, CourseId = int.Parse(course) });

                }
                context.SaveChanges();
            }

        }

        public IEnumerable<StudentViewModel> GetStudentCourse(IEnumerable<Student> students)
        {
            var studentViewModels = new List<StudentViewModel>();
            foreach (var student in students)
            {
                using (var context = new Context())
                {
                    //Getting student object and courses with respect to particular student id's and storing them in View Model
                    var studentCourse = context.StudentCourses
                        .Where(sc => sc.StudentId == student.StudentId)
                        .Select(sc => sc.CourseId.ToString())
                        .ToList();

                    var viewModel = new StudentViewModel { Student = student, Courses = studentCourse };

                    studentViewModels.Add(viewModel);
                }
            }
            return studentViewModels;
        }

        public void DeleteStudentCourse(int id)
        {
            using (var context = new Context())
            {
                var deleteStudentCourses = context.StudentCourses.Where(sc => sc.StudentId == id).ToList();
                foreach (var studentCourse in deleteStudentCourses)
                {
                    context.StudentCourses.Remove(studentCourse);
                }
                context.SaveChanges();
            }
        }

        public void UpdatestudentCourse(StudentViewModel viewModel)
        {
            using (var context = new Context())
            {
                var updateCourses = context.StudentCourses.Where(sc => sc.StudentId == viewModel.Student.StudentId).ToList();
                foreach (var studentCourse in updateCourses)
                {
                    context.StudentCourses.Remove(studentCourse);
                }
                foreach (var course in viewModel.Courses)
                {
                    context.StudentCourses.Add(new StudentCourse { StudentId = viewModel.Student.StudentId, CourseId = int.Parse(course) });
                }
                context.SaveChanges();
            }
        }
    };
}
