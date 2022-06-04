using Microsoft.AspNetCore.Mvc;
using NSE.Web.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Web.MVC.Controllers
{
    public class MainController : Controller
    {
        protected bool HasResponseErrors (ResponseResult response)
        {
            if(response != null && response.Errors.Messages.Any())
            {
                foreach(var message in response.Errors.Messages)
                {
                    ModelState.AddModelError(string.Empty, message);
                }
                return true;
            }
            return false;
        }
    }
}
