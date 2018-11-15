using System;
using System.Collections.Generic;
using System.Linq;
using Context.Database;
using Context.Repository;

namespace Service.Service
{
    public class CategoryServices : Service<Category>, ICategoryService
    {
        public CategoryServices(IUow uow, IRepository<Category> repository) : base(uow, repository)
        {
        }
    }
}