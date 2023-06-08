using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductManageSys1_1.Models;

namespace ProductManageSys1_1.Controllers
{
    public class MemberController : Controller
    {
        // 建立資料庫 dbProductEntities 型別的 db 物件
        dbProductEntities db = new dbProductEntities();
        // GET: Member
        /// <summary>
        /// 會員管理權限功能
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Index()
        {
            string uid = User.Identity.Name;
            string role = db.會員.Where(m => m.帳號 == uid).FirstOrDefault().角色;

            if(role != "管理者")
            {
                return RedirectToAction("Index", "PermissionErrorMessage", new { msg = "您的身分無管理會員的權限" });
            }

            // 建立會員的串列物件
            List<會員> members = new List<會員>();

            foreach(var item in db.會員)
            {
                // 先定義一個變數 member 準備存放會員的基本資料
                var member = new 會員();
                member.帳號 = item.帳號;
                member.密碼 = item.密碼;
                // item.權限.Contains("R") ? "讀取" : "" 代表 會員權限包含 R 功能時，用「讀取」字串取代空字串
                // ? 問號代表可以為 null 值，因為每個會員不見得擁有此權限
                member.權限 =
                    (item.權限.Contains("R") ? "讀取" : " ") +
                    (item.權限.Contains("C") ? "新增" : " ") +
                    (item.權限.Contains("U") ? "修改" : " ") +
                    (item.權限.Contains("D") ? "刪除" : " ");
                member.角色 = item.角色;
                members.Add(member);

            }

            return View(members);
        }


        /// <summary>
        /// 會員新增功能
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Create()
        {
            string uid = User.Identity.Name;
            string role = db.會員.Where(m => m.帳號 == uid).FirstOrDefault().角色;

            if (role != "管理者")
            {
                return RedirectToAction("Index", "PermissionErrorMessage", new { msg = "您的身分無管理會員的權限" });
            }

            return View();
        }


        [Authorize]
        [HttpPost]
        public ActionResult Create(string 帳號, string 密碼, string 角色, string[] 權限)
        {
            string userid = 帳號;
            var tempMember = db.會員.Where(m => m.帳號 == userid).FirstOrDefault();
            if (tempMember != null)
            {
                ViewBag.IsMember = true;
                return View();
            }

            string Permission = "R";
            for (int i = 0; i < 權限.Length; i++)
            {
                Permission += 權限[i];
            }

            會員 member = new 會員();
            member.帳號 = 帳號;
            member.密碼 = 密碼;
            member.角色 = 角色;
            member.權限 = Permission;
            db.會員.Add(member);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        /// <summary>
        /// 會員刪除功能
        /// 變數 userid 為接收會員管理頁面的 Html 刪除按鍵功能，userid = 會員帳號
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Delete(string userid)
        {
            string uid = User.Identity.Name;
            string role = db.會員.Where(m => m.帳號 == uid).FirstOrDefault().角色;

            if (role != "管理者")
            {
                return RedirectToAction("Index", "PermissionErrorMessage", new { msg = "您的身分無管理會員的權限" });
            }

            var member = db.會員.Where(m => m.帳號 == userid).FirstOrDefault();
            db.會員.Remove(member);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 會員編輯功能
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Edit(string userid)
        {
            string uid = User.Identity.Name;
            string role = db.會員.Where(m => m.帳號 == uid).FirstOrDefault().角色;

            if (role != "管理者")
            {
                return RedirectToAction("Index", "PermissionErrorMessage", new { msg = "您的身分無管理會員的權限" });
            }

            var member = db.會員.Where(m => m.帳號 == userid).FirstOrDefault();
            return View(member);

        }

        [Authorize]
        [HttpPost, ActionName("Edit")]
        public ActionResult EditConfirm(string 帳號, string 密碼, string 角色, string[] 權限)
        {
            string Permission = "R";
            if (權限 != null)
            {
                for (int i = 0; i < 權限.Length; i++)
                {
                    Permission += 權限[i];
                }
            }

            var member = db.會員.Where(m => m.帳號 == 帳號).FirstOrDefault();
            member.密碼 = 密碼;
            member.角色 = 角色;
            member.權限 = Permission;
            // 為什麼可以直接儲存回資料庫的資料
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}