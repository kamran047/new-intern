using DataLayer;
using Model;
using System;
using System.Linq;

namespace BusinessLogic
{
    public class UserLogic : IDisposable, IUserLogic
    {
        private IContext _context;
        public UserLogic(IContext context)
        {
            _context = context;
        }

        public User ValidateUser(string username, string password)
        {
            {
                return _context.Users.FirstOrDefault(user =>
                       user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
                       && user.UserPassword == password);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
