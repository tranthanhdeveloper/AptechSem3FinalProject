using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Context.Database;

namespace Context.Repository
{
    public interface IVideoRepository : IRepository<Video>
    {

    }

    public class VideoRepository : Repository<Video>, IVideoRepository
    {
        public VideoRepository(IDbFactory context) : base(context)
        {

        }
    }
    
    public interface IAccountRepository : IRepository<Account>
    {

    }

    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(IDbFactory context) : base(context)
        {

        }
    }

    public interface ICategoryRepository : IRepository<Category>
    {

    }

    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(IDbFactory context) : base(context)
        {

        }
    }
}