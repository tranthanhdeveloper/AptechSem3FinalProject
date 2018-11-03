using System;
using System.Collections.Generic;
using System.Linq;
using Context.Database;
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