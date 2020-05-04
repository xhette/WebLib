using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.GeneralMethods;
using WebLib.Models;
using WebMatrix.WebData;

namespace WebLib.Controllers
{
    public class RegistrationController : Controller
    {
        public static SimpleRoleProvider roles = (SimpleRoleProvider)Roles.Provider;
        public static SimpleMembershipProvider membership = (SimpleMembershipProvider)Membership.Provider;

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult RegisterReader()
        {
            RegisterReaderModel model = new RegisterReaderModel();
            model.Reader.BirthDate = DateTime.Today;

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult RegisterReader(RegisterReaderModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (membership.GetUser(model.User.Login, false) == null)
                    {
                        membership.CreateUserAndAccount(model.User.Login, model.User.Password);
                        if (!roles.IsUserInRole(model.User.Login, "reader"))
                            roles.AddUsersToRoles(new[] { model.User.Login }, new[] { "reader" });

                        Registration registration = new Registration();
                        model.Reader.UserId = WebSecurity.GetUserId(model.User.Login);

                        var result = registration.RegisterReader((ReaderDataDTO)model.Reader);


                        if (result.Code == BusinessLayer.OperationStatusEnum.Success)
                        {
                            TempData["OperationStatus"] = true;
                            TempData["OpearionMessage"] = "Регистрация успешно завершена";

                            return RedirectToAction("Index", "Login");
                        }
                        else
                        {
                            TempData["OperationStatus"] = false;
                            TempData["OpearionMessage"] = result.Message;
                        }
                    }
                    else
                    {
                        TempData["OperationStatus"] = false;
                        TempData["OpearionMessage"] = "Пользователь с таким логином уже существует";
                    }
                }
                catch (Exception ex)
                {
                    TempData["OperationStatus"] = false;
                    TempData["OpearionMessage"] = ex.StackTrace;
                }
            }

            return View(model);
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult RegisterEmployee(int? type)
        {
            if (type.HasValue && (type.Value == 1 || type.Value == 2))
            {
                RegisterEmployeeModel model = new RegisterEmployeeModel
                {
                    EmployeeType = type.Value
                };

                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Registration");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult RegisterEmployee(RegisterEmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (membership.GetUser(model.User.Login, false) == null)
                    {
                        membership.CreateUserAndAccount(model.User.Login, model.User.Password);
                        if (model.EmployeeType == 1)
                        {
                            if (!roles.IsUserInRole(model.User.Login, "librarian"))
                                roles.AddUsersToRoles(new[] { model.User.Login }, new[] { "librarian" });
                        }
                        else if (model.EmployeeType == 2)
                        {
                            if (!roles.IsUserInRole(model.User.Login, "provider"))
                                roles.AddUsersToRoles(new[] { model.User.Login }, new[] { "provider" });
                        }

                        Registration registration = new Registration();

                        int id = WebSecurity.GetUserId(model.User.Login);

                        var result = registration.RegisterEmployee((BusinessLayer.UserTypeEnum)model.EmployeeType, id, model.EmployeeId);


                        if (result.Code == BusinessLayer.OperationStatusEnum.Success)
                        {
                            TempData["OperationStatus"] = true;
                            TempData["OpearionMessage"] = "Регистрация успешно завершена";

                            return RedirectToAction("Index", "Login");
                        }
                        else
                        {
                            TempData["OperationStatus"] = false;
                            TempData["OpearionMessage"] = result.Message;
                        }
                    }
                    else
                    {
                        TempData["OperationStatus"] = false;
                        TempData["OpearionMessage"] = "Пользователь с таким логином уже существует";
                    }
                }
                catch (Exception ex)
                {
                    TempData["OperationStatus"] = false;
                    TempData["OpearionMessage"] = ex.StackTrace;
                }
            }

            return View(model);
        }
    }
}