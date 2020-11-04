using BusinessLogic;
using DataLayer;
using DependencyInjection;
using Model;
using Ninject;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace StudentDesktopApp
{
    public partial class AddStudentForm : Form
    {
        private IStudentLogic _student;
        private ICourseLogic _course;
        Label filePath = new Label();
        public AddStudentForm()
        {
            InitializeComponent();
            IKernel kernel = new StandardKernel(new NinjectBinding());
            this._student = kernel.Get<IStudentLogic>();
            this._course = kernel.Get<ICourseLogic>();
        }

        private void AddStudentForm_Load(object sender, EventArgs e)
        {
            coursesList.DataSource = _course.Get();           
            coursesList.DisplayMember = "CourseName";
            coursesList.ValueMember ="CourseId";
            coursesList.SelectionMode = SelectionMode.MultiExtended;
        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            Student studentModel = new Student();
            StudentViewModel model = new StudentViewModel();
            List<string> coursesIdList = new List<string>();
            File.Copy(filePath.Text, Path.Combine(@"E:\Projects\Internship\Web API's\WebAPI\StudentDesktopApp\Images\", Path.GetFileName(filePath.Text)));

            studentModel.Name = nameTxt.Text;
            studentModel.Email = emailTxt.Text;
            studentModel.Password = passwordTxt.Text;
            studentModel.ConfirmPassword = confirmPasswordTxt.Text;
            studentModel.PhoneNo = Convert.ToInt32(phoneNoTxt.Text);
            studentModel.ImagePath = fileName.Text;
            model.Student = studentModel;

            foreach (var selected_courses in coursesList.SelectedItems)
            {
                var courseProperty = (Course)selected_courses;
                coursesIdList.Add(courseProperty.CourseId.ToString());
            }

            model.Courses = coursesIdList;
            _student.Add(studentModel);
            _student.AddStudentCourse(model);
            this.Close();
            StudentListing studentListing = new StudentListing();
            studentListing.Show();
        }

        private void studentImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            PictureBox pictureBox = sender as PictureBox;
            if (pictureBox != null)
            {
                openFile.Filter = "(*.jpg; *.jpeg; *.png;) | *.jpg; *.jpeg; *.png;";
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    pictureBox.Image = Image.FromFile(openFile.FileName);
                    filePath.Text = openFile.FileName;
                    fileName.Text = Path.GetFileName(openFile.FileName);
                    fileName.Visible=true;
                }
            }
        }
    }
}
