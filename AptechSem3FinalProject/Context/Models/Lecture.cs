using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Enum;

namespace Context.Database
{
    public partial class Lecture
    {
        public IEnumerable<Video> GetVideos()
        {
            return this.Videos.Where(v => v.Status == (int) EntityStatus.Visible);
        }
    }
}
