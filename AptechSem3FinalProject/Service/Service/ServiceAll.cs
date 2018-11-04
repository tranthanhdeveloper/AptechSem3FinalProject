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

    public interface IAccountService : IService<Account>
    {
        Account Login(string userName, string password);
        Account GetByUserName(string username);
    }

    public class AccountSevice : Service<Account>, IAccountService
    {
        public AccountSevice(IUow uow, IRepository<Account> repository) : base(uow, repository)
        {
        }

        public Account GetByUserName(string username)
        {
            return GetAll(a =>
                a.UserName.ToLower().Contains(username.ToLower())).SingleOrDefault();
        }
        public Account Login(string userName, string password)
        {
            return GetAll(a =>
                a.UserName.ToLower().Contains(userName.ToLower()) && a.Password.Contains(password)).SingleOrDefault();           
        }
    }   
}
