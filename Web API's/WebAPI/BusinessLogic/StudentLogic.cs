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
