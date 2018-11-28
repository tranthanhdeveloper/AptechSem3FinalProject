
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Context.Database;
using Model.Sercurity;
using PagedList;
using Service.Service;
using Web.Areas.Admin.Models;
using Web.Helper;
using Web.Helper.Sercurity;

namespace Web.Areas.Admin.Controllers
{
    public class AccountController : AdminController
    {
        private IAccountService _accountService;
        private IUserService _userService;
        public AccountController(IAccountService accountService, IUserService userService)
        {
            _accountService = accountService;
            _userService = userService;
        }
        // GET: Admin/Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                model.Password = Encryptor.EncryptSHA1(model.Password);
                var accountLogin = _accountService.Login(model.UserName, model.Password);
                if (accountLogin == null)
                {
                    ModelState.AddModelError("", MessageConstants.LoginFail);
                    return View(model);
                }

                SessionPersister.AccountInformation = accountLogin;
                return RedirectToAction("Index", "Dashboard", new {area="Admin"});

            }

            return View(model);
        }

        public ActionResult ListAccount(int? page)
        {
            var listAccount = (IEnumerable<Account>) _userService.GetAll().OrderByDescending(u => u.Id);
            var pageSize =int.Parse(WebConfigurationManager.AppSettings["PageSize"]);
            pageSize = pageSize == 0 ? 1 : pageSize;
            int pageNumber = page ?? 1;
            return View(listAccount.ToPagedList(pageNumber, pageSize));
        }
    }
}