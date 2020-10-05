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
    };
}


//This code can be used later.
/* public IEnumerable<Student> GetStudents()
 {
     using (var context= new Context())
     {
         var students = context.Students.ToList();
         return students;
     }   
 }

 public void AddStudent(Student student)
 {
     using(var context=new Context())
     {
         try
         {
             context.Students.Add(student);
             context.SaveChanges();
         }

         catch (Exception e)
         {
             Console.Write(e);
         }

     }

 }


 public void UpdateStudent(Student student)
 {
     using(var context=new Context())
     {
         try
         {
             var updateRecord = context.Students.Single(s => s.StudentId == student.StudentId);
             updateRecord.StudentId = student.StudentId;
             updateRecord.Name = student.Name;
             updateRecord.Email = student.Email;
             updateRecord.Password = student.Password;
             updateRecord.ConfirmPassword = student.ConfirmPassword;
             updateRecord.PhoneNo = student.PhoneNo;
             context.SaveChanges();
         }
         catch(Exception e)
         {
             Console.Write(e);
         }

     }
 }

 public void DeleteStudent(int id)
 {
     using (var context=new Context())
     {
         try
         {
             var deleteRecord = context.Students.Single(s => s.StudentId == id);
             context.Students.Remove(deleteRecord);
             context.SaveChanges();
         }
         catch(Exception e)
         {
             Console.Write(e);
         }

     }
 }
 */
