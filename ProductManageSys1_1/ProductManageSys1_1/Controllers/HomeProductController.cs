using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ProductManageSys1_1.Models;

namespace ProductManageSys1_1.Controllers
{
    public class HomeProductController : Controller
    {
        // GET: HomeProduct
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ActionName("Index")]
        public ActionResult Index(string 帳號, string 密碼)
        {
            // 建立 dbProductEntities db 物件
            dbProductEntities db = new dbProductEntities();
            var member = db.會員
                .Where(m => m.帳號 == 帳號 && m.密碼 == 密碼)
                .FirstOrDefault();
            // member != null 代表已經存在此會員帳號，即導回 Controller 控制器 「Category」
            if (member != null)
            {
                FormsAuthentication.RedirectFromLoginPage
                    (member.帳號, true);
                return RedirectToAction("Index", "Category");
            }
            // 如果沒有此會員帳號，則將 ViewBag.IsLogin 設值為 true
            ViewBag.IsLogin = true;
            return View();
        }
    }
}