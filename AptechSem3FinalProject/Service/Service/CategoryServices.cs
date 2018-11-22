using System;
using System.Collections.Generic;
using System.Linq;
using Context.Database;
using Context.Repository;

namespace Service.Service
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        public CategoryService(IUow uow, IRepository<Category> repository) : base(uow, repository)
        {
        }
    }
}