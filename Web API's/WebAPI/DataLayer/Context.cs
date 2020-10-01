using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Context : DbContext
    {
        public Context (): base("name=studentdatabasestring")
        {

        }

        public DbSet <Student> Students { get; set; } 
    }
}
