using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Enum;

namespace Context.Database
{
    public partial class Course
    {
        public IEnumerable<Lecture> GetLectures()
        {
            return this.Lectures;
        }
    }
}
