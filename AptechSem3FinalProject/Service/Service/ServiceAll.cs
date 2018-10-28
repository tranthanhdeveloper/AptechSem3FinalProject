using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Context.Database;
using Context.Repository;

namespace Service.Service
{
    public interface IUserService : IService<User> 
    {

    }

    public class UserSevice : Service<User>, IUserService
    {
        public UserSevice(IUow uow, IRepository<User> repository) : base(uow, repository)
        {
        }
    }
}
