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


    public interface ICourseService : IService<Course> 
    {

    }

    public class CourseService : Service<Course>, ICourseService
    {
        public CourseService(IUow uow, IRepository<Course> repository) : base(uow, repository)
        {
        }
    }

    public interface ILectureService : IService<Lecture> 
    {

    }

    public class LectureService : Service<Lecture>, ILectureService
    {
        public LectureService(IUow uow, IRepository<Lecture> repository) : base(uow, repository)
        {
        }
    }

    public interface IOrderService : IService<Order> 
    {

    }

    public class OrderService : Service<Order>, IOrderService
    {
        public OrderService(IUow uow, IRepository<Order> repository) : base(uow, repository)
        {
        }
    }


    public interface IPaymentService : IService<Payment>
    {

    }

    public class PaymentService : Service<Payment>, IPaymentService
    {
        public PaymentService(IUow uow, IRepository<Payment> repository) : base(uow, repository)
        {
        }
    }


    public interface IPaymentMethodService : IService<PaymentMethod>
    {

    }

    public class PaymentMethodService : Service<PaymentMethod>, IPaymentMethodService
    {
        public PaymentMethodService(IUow uow, IRepository<PaymentMethod> repository) : base(uow, repository)
        {
        }
    }

    public interface IVideoService : IService<Video>
    {

    }

    public class VideoService : Service<Video>, IVideoService
    {
        public VideoService(IUow uow, IRepository<Video> repository) : base(uow, repository)
        {
        }
    }
}
