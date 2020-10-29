using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Model;

namespace DataLayer
{
    public interface IContext
    {
        DbSet<Course> Courses { get; set; }
        DbSet<StudentCourse> StudentCourses { get; set; }
        DbSet<Student> Students { get; set; }
        DbSet<User> Users { get; set; }

        int SaveChanges();
        void spDeleteRecord(int id);
        void spInsertCourses(StudentViewModel model, decimal id);
        DbSet<T> Set<T>() where T:class;
        decimal spInsertRecord(Student student);
        DbEntityEntry Entry(object entity);
        void Dispose();
    }
}