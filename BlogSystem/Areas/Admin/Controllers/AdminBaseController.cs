using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSystem.Areas.Admin.Controllers
{

    [Authorize(Roles = "Admin")]
    public  abstract class AdminBaseController : Controller
    {

    }
}