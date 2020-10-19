using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Context : DbContext
    {
        public Context() : base("name=studentdatabasestring")
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        public virtual decimal spInsertRecord(Student student)
        {
            var name_param = new SqlParameter("@Name",student.Name);
            var email_param= new SqlParameter("@Email",student.Email);
            var password_param = new SqlParameter("@Password",student.Password);
            var confirmPassword_param = new SqlParameter("@ConfirmPassword",student.ConfirmPassword);
            var phoneNo_param = new SqlParameter("@PhoneNo",student.PhoneNo);
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<decimal>(
                "spInsertRecord @Name,@Email,@Password,@ConfirmPassword,@PhoneNo", 
                name_param, email_param, password_param, confirmPassword_param, phoneNo_param
                ).First();
        }

        public virtual void spInsertCourses(StudentViewModel model, decimal id)
        {
            int studentId = Decimal.ToInt32(id);
            foreach (var course in model.Courses)
            {
                var studentId_param = new SqlParameter("@StudentId", studentId);
                var courseId_param = new SqlParameter("@CourseId", Convert.ToInt32(course));
                ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<Nullable<decimal>>
                ("spInsertCourses @StudentId,@CourseId", studentId_param, courseId_param).First();
            }
        }

        public virtual void spDeleteRecord(int id)
        {
                var studentId_param = new SqlParameter("@StudentId", id);
               ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<int>
                ("spDeleteRecord @StudentId", studentId_param).First();
        }
    }
}
