using DataLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class StudentLogic
    {
        /*
        List<Student> students = new List<Student>(){
        new Student(){StudentId=1, Name="kamran", Password="abc", ConfirmPassword="abc", Email="kamran.nazir047@gmail.com", PhoneNo=0303},
        new Student(){StudentId=1, Name="kamran", Password="abc", ConfirmPassword="abc", Email="kamran.nazir047@gmail.com", PhoneNo=0303},
         new Student(){StudentId=1, Name="kamran", Password="abc", ConfirmPassword="abc", Email="kamran.nazir047@gmail.com", PhoneNo=0303},};
         */

        public IEnumerable<Student> GetStudents()
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
                context.Students.Add(student);
                context.SaveChanges();
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
    };
}
