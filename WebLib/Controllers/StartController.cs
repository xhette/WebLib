﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebLib.Controllers
{
    public class StartController : Controller
    {
        // GET: Start
        public ActionResult Index()
        {
            Utils.CreateAccounts.Up();
            return RedirectToAction("Index", "Home");
        }
    }
}