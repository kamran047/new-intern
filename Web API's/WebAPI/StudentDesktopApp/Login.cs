using BusinessLogic;
using DependencyInjection;
using Ninject;
using System;
using System.Windows.Forms;

namespace StudentDesktopApp
{
    public partial class Login : Form
    {
        private IUserLogic _user;

        public Login()
        {
            InitializeComponent();
            IKernel kernel = new StandardKernel(new NinjectBinding());
            this._user = kernel.Get<IUserLogic>(); 
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            string username = usernameTxt.Text;
            string password = passwordTxt.Text;
            if (_user.ValidateUser(username, password) != null)
            {
                StudentListing student_listing = new StudentListing();
                this.Hide();
                student_listing.Show();
            }
        }
    }
}
