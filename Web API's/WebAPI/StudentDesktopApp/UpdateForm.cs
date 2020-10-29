using BusinessLogic;
using DataLayer;
using DependencyInjection;
using Model;
using Ninject;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace StudentDesktopApp
{
    public partial class UpdateForm : Form
    {
        private int studentId;
        private IStudentLogic _student;
        private ICourseLogic _course;
        public UpdateForm(int studentId)
        {
            InitializeComponent();
            this.studentId = studentId;
            IKernel kernel = new StandardKernel(new NinjectBinding());
            this._student = kernel.Get<IStudentLogic>();
            this._course = kernel.Get<ICourseLogic>();
        }

        public void LoadFormData()
        {
            StudentViewModel model = new StudentViewModel();

            model = _student.GetOneStudentWithCourses(studentId);
            nameTxt.Text = model.Student.Name;
            emailTxt.Text = model.Student.Email;
            passwordTxt.Text = model.Student.Password;
            confirmPasswordTxt.Text = model.Student.ConfirmPassword;
            phoneNoTxt.Text = model.Student.PhoneNo.ToString();

            coursesList.DataSource = _course.Get();
            coursesList.DisplayMember = "CourseName";
            coursesList.ValueMember = "CourseId";
            coursesList.SelectionMode = SelectionMode.MultiExtended;
            coursesList.SelectedItems.Clear();

            foreach (var courseId in model.Courses)
            {
                foreach (var course in _course.Get())
                {
                    if (course.CourseId.ToString() == courseId)
                    {
                        coursesList.SetSelected(course.CourseId - 1, true);
                        break;
                    }
                }
            }
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            Student studentModel = _student.GetOne(studentId);
            StudentViewModel model = new StudentViewModel();
            List<string> coursesIdList = new List<string>();

            studentModel.StudentId = studentId;
            studentModel.Name = nameTxt.Text;
            studentModel.Email = emailTxt.Text;
            studentModel.Password = passwordTxt.Text;
            studentModel.ConfirmPassword = confirmPasswordTxt.Text;
            studentModel.PhoneNo = Convert.ToInt32(phoneNoTxt.Text);
            model.Student = studentModel;

            foreach (var selected_courses in coursesList.SelectedItems)
            {
                var courseProperty = (Course)selected_courses;
                coursesIdList.Add(courseProperty.CourseId.ToString());
            }

            model.Courses = coursesIdList;
            _student.Update(studentModel);
            _student.UpdatestudentCourse(model);
            this.Close();
            StudentListing studentListing = new StudentListing();
            studentListing.Show();
        }
    }
}
