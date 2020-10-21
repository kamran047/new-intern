using DataLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class UserLogic: IDisposable
    {
       Context context =new Context();
        public User ValidateUser(string username, string password)
        {
            {
                return context.Users.FirstOrDefault(user =>
                       user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
                       && user.UserPassword == password);
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
