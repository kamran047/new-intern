using DataLayer;
using Model;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{

    public class StudentLogic : BaseRepository<Student>, IStudentLogic
    {
        private IContext _context;
        public StudentLogic(IContext context) : base(context)
        {
            _context = context;
        }

        public void AddStudentCourse(StudentViewModel viewModel)
        {
            foreach (var course in viewModel.Courses)
            {
                //Getting student id and coursse id from the Student and course table using view model and saving them in Student Courses table
                _context.StudentCourses
                        .Add(new StudentCourse { StudentId = viewModel.Student.StudentId, CourseId = int.Parse(course) });
            }
            _context.SaveChanges();
        }

        public IEnumerable<StudentViewModel> GetStudentCourse(IEnumerable<Student> students)
        {
            var studentViewModels = new List<StudentViewModel>();
            foreach (var student in students)
            {
                //Getting student object and courses with respect to particular student id's and storing them in View Model
                var studentCourse = _context.StudentCourses
                    .Where(sc => sc.StudentId == student.StudentId)
                    .Select(sc => sc.Course.CourseName.ToString())
                    .ToList();

                var viewModel = new StudentViewModel { Student = student, Courses = studentCourse };
                studentViewModels.Add(viewModel);
            }
            return studentViewModels;
        }

        public void UpdatestudentCourse(StudentViewModel viewModel)
        {
            var updateCourses = _context.StudentCourses.Where(sc => sc.StudentId == viewModel.Student.StudentId).ToList();
            foreach (var studentCourse in updateCourses)
            {
                _context.StudentCourses.Remove(studentCourse);
            }
            foreach (var course in viewModel.Courses)
            {
                _context.StudentCourses.Add(new StudentCourse { StudentId = viewModel.Student.StudentId, CourseId = int.Parse(course) });
            }
            _context.SaveChanges();
        }

        public decimal AddStudentBySP(StudentViewModel model)
        {
            return _context.spInsertRecord(model.Student);
        }

        public void AddCoursesBySP(StudentViewModel model, decimal id)
        {
            _context.spInsertCourses(model, id);
        }

        public void DeleteRecordBySP(int id)
        {
            _context.spDeleteRecord(id);
        }
    };
}
