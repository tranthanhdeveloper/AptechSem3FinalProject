using Context.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Context.Repository;

namespace Service.Service
{
    public class VideoService : Service<Video>, IVideoService
    {
        public VideoService(IUow uow, IRepository<Video> repository) : base(uow, repository)
        {
        }
    }
}