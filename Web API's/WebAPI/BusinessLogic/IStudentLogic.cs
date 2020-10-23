using System.Collections.Generic;
using DataLayer;
using Model;

namespace BusinessLogic
{
    public interface IStudentLogic : IBaseRepository<Student>
    {
        void AddCoursesBySP(StudentViewModel model, decimal id);
        decimal AddStudentBySP(StudentViewModel model);
        void AddStudentCourse(StudentViewModel viewModel);
        void DeleteRecordBySP(int id);
        IEnumerable<StudentViewModel> GetStudentCourse(IEnumerable<Student> students);
        void UpdatestudentCourse(StudentViewModel viewModel);
    }
}