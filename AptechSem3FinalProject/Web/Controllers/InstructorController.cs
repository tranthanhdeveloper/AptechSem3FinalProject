using AutoMapper;
using Model.Enum;
using Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class InstructorController : Controller
    {       
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;

        public InstructorController(IAccountService accountService, IUserService userService)
        {
            this._accountService = accountService;
            this._userService = userService;
        }

        // GET: Instructor
        [Helper.Sercurity.Authorize]
        public ActionResult Index()
        {
            ApplyAuthorViewModel applyAuthorViewModel = new ApplyAuthorViewModel();
            var user = _userService.GetById(Helper.Sercurity.SessionPersister.AccountInformation.UserId);
            applyAuthorViewModel = Mapper.Map<ApplyAuthorViewModel>(user);
            return View(applyAuthorViewModel);
        }

        [HttpPost]
        public ActionResult Apply(ApplyAuthorViewModel formData)
        {
            if (ModelState.IsValid)
            {
                var loggedAccount = Helper.Sercurity.SessionPersister.AccountInformation;
                var loggedUser = _userService.GetById(loggedAccount.UserId);
                loggedUser.Company = formData.Company;
                loggedAccount.RoleId = (int)RoleEnum.Author;
                _userService.Update(loggedUser);
                _accountService.Update(loggedAccount);
                return RedirectToAction("Index", "Dashboard", new { area= "Instructors" }); ;
            }
            return View("index", formData);
        }
    }
}