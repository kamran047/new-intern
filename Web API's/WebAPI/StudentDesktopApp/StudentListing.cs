using BusinessLogic;
using DataLayer;
using DependencyInjection;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace StudentDesktopApp
{
    public partial class StudentListing : Form
    {
        private IStudentLogic _student;
        private ICourseLogic _course;
        public StudentListing()
        {
            InitializeComponent();
            IKernel kernel = new StandardKernel(new NinjectBinding());
            this._student = kernel.Get<IStudentLogic>();
            this._course = kernel.Get<ICourseLogic>();
        }

        private void addStudentBtn_Click(object sender, EventArgs e)
        {
            this.Dispose();
            AddStudentForm addStudent = new AddStudentForm();
            addStudent.Show();
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Login login = new Login();
            login.Show();
        }

        private void StudentListing_Load(object sender, EventArgs e)
        {
            StudentViewModel viewModel = new StudentViewModel();
            List<StudentViewModel> models = new List<StudentViewModel>();
            StudentCompositeModel compositeModel = new StudentCompositeModel();
            viewModel.Students = _student.Get().ToList();
            var studentCoursesList = _student.GetStudentCourse(viewModel.Students);
            showRecordGridView.DataSource = _student.GetStudentsWithCourses(studentCoursesList.ToList());
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(showRecordGridView.Rows[showRecordGridView.CurrentRow.Index].Cells[0].Value);
            _student.Delete(id);
            showRecordGridView.DataSource= _student.Get().ToList();
        }

        private void showRecordGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int studentId = Convert.ToInt32(showRecordGridView.Rows[showRecordGridView.CurrentRow.Index].Cells[0].Value);
            this.Close();
            UpdateForm updateForm = new UpdateForm(studentId);
            updateForm.Show();
            updateForm.LoadFormData();
        }
    }
}
