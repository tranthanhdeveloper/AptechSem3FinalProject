using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Context.Database;
using Model.Enum;
using PagedList;
using Service.Service;

namespace Web.Areas.Admin.Controllers
{
    [Helper.Sercurity.Authorize(RoleEnum.Admin)]
    public class UserController : AdminController
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: Admin/User
        public ActionResult ManageUser(int? page)
        {
            var listUser = _userService.GetAll(u => u.Status == (int) EntityStatus.Visible).OrderByDescending(u => u.Id) as IEnumerable<User>;
            var pageSize =int.Parse(WebConfigurationManager.AppSettings["PageSize"]);
            pageSize = pageSize == 0 ? 1 : pageSize;
            int pageNumber = page ?? 1;
            return View(listUser.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult FilterUser(int? page, string Model_Name)
        {
            var pageSize = int.Parse(WebConfigurationManager.AppSettings["PageSize"]);
            pageSize = pageSize == 0 ? 1 : pageSize;
            int pageNumber = page ?? 1;
            var listUser = _userService.GetAll(u => u.Status == (int) EntityStatus.Visible)
                .OrderByDescending(u => u.Id) as IEnumerable<User>;
            if (Model_Name != "")
            {
                listUser = listUser.Where(l => l.Name.ToLower().Contains(Model_Name.ToLower()));
            }

            return PartialView("~/Areas/Admin/Views/User/_partial_Search_User.cshtml", listUser.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult EditUser(User model)
        {
            var user = _userService.GetById(model.Id);
            if (user != null)
            {
                user.Name = model.Name;
                user.Email = model.Email;
                user.Phone = model.Phone;
                user.BirthDay = model.BirthDay;
                user.Address = model.Address;

                _userService.Update(user);
            }

            return Success();
        }

        public ActionResult DeleteUser(int id)
        {
            var user = _userService.GetById(id);
            user.Status = (int) EntityStatus.Invisible;
            _userService.Update(user);
            return Success();
        }
        
    }
}