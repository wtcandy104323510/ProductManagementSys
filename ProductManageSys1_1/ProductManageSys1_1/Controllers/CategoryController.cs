using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductManageSys1_1.Models;

namespace ProductManageSys1_1.Controllers
{
    public class CategoryController : Controller
    {
        dbProductEntities db = new dbProductEntities();
        // GET: Category
        /// <summary>
        /// 類別管理頁面
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Index()
        {
            string uid = User.Identity.Name;
            string Permission = db.會員.Where(m => m.帳號 == uid).FirstOrDefault().權限;
            // 使用 ViewBag.Permission 動態屬性儲存現在使用者的權限，回傳給 View 顯示介面
            ViewBag.Permission = Permission;

            List<產品類別> category = new List<產品類別>();

            foreach(var item in db.產品類別.OrderByDescending(m => m.修改日))
            {
                category.Add(new 產品類別()
                {
                    類別編號 = item.類別編號,
                    類別名稱 = item.類別名稱,
                    編輯者 = item.編輯者,
                    修改日 = DateTransSys.StringConvertDateTimeString(item.修改日),
                    建立日 = DateTransSys.StringConvertDateTimeString(item.建立日)
                });
            }

            return View(category);
        }

        /// <summary>
        /// 類別新增功能
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Create()
        {
            string uid = User.Identity.Name;
            string Permission = db.會員.Where(m => m.帳號 == uid).FirstOrDefault().權限;

            if (!Permission.Contains("C"))
            {
                return RedirectToAction("Index", "PermissionErrorMessage", new { msg = "您的身分無新增的權限" });
            }

            return View();
        }


        [Authorize]
        [HttpPost, ActionName("Create")]
        public ActionResult CreateConfirm(string 類別名稱)
        {
            string editdate = DateTime.Now.ToString("yyyyMMddHHmmss");
            產品類別 category = new 產品類別();
            category.類別名稱 = 類別名稱;
            category.編輯者 = User.Identity.Name;
            category.建立日 = editdate;
            category.修改日 = editdate;
            db.產品類別.Add(category);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        /// <summary>
        /// 類別刪除功能
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        [Authorize]
        // 回傳的資料 cid 為類別編號
        public ActionResult Delete(int cid)
        {
            string uid = User.Identity.Name;
            string Permission = db.會員.Where(m => m.帳號 == uid).FirstOrDefault().權限;

            if (!Permission.Contains("D"))
            {
                return RedirectToAction("Index", "PermissionErrorMessage", new { msg = "您的身分無刪除的權限" });
            }

            var products = db.產品資料.Where(m => m.類別編號 == cid).ToList();
            var category = db.產品類別.Where(m => m.類別編號 == cid).FirstOrDefault();
            db.產品資料.RemoveRange(products);
            db.產品類別.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        [Authorize]
        public ActionResult Edit(int cid)
        {
            string uid = User.Identity.Name;
            string Permission = db.會員.Where(m => m.帳號 == uid).FirstOrDefault().權限;

            if (!Permission.Contains("U"))
            {
                return RedirectToAction("Index", "PermissionErrorMessage", new { msg = "您的身分無修改的權限" });
            }

            var category = db.產品類別.Where(m => m.類別編號 == cid).FirstOrDefault();
            return View(category);

        }

        [Authorize]
        [HttpPost, ActionName("Edit")]
        public ActionResult EditConfirm(int 類別編號, string 類別名稱)
        {
            string editdate = DateTime.Now.ToString("yyyyMMddHHmmss");
            var category = db.產品類別.Where(m => m.類別編號 == 類別編號).FirstOrDefault();
            category.類別名稱 = 類別名稱;
            category.編輯者 = User.Identity.Name;
            category.修改日 = editdate;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}