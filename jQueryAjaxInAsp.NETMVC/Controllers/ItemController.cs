using jQueryAjaxInAsp.NETMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jQueryAjaxInAsp.NETMVC.Controllers
{
    public class ItemController : Controller
    {
        //
        // GET: /Item/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewAll()
        {
            return View(GetAllItems());
        }

        IEnumerable<ItemMst> GetAllItems()
        {
            using (DBModels db = new DBModels())
            {
                return db.ItemMsts.ToList<ItemMst>();
            }

        }

        public ActionResult AddOrEdit(int id = 0)
        {
            ItemMst itm = new ItemMst();
            if (id != 0)
            {
                using (DBModels db = new DBModels())
                {
                    itm = db.ItemMsts.Where(x => x.ItemId == id).FirstOrDefault<ItemMst>();
                }
            }
            return View(itm);
        }

        [HttpPost]
        public ActionResult AddOrEdit(ItemMst itm)
        {
            try
            {
                if (itm.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(itm.ImageUpload.FileName);
                    string extension = Path.GetExtension(itm.ImageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    itm.ItemImage = "~/AppFiles/Images/" + fileName;
                    itm.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName));
                }
                using (DBModels db = new DBModels())
                {
                    if (itm.ItemId == 0)
                    {
                        db.ItemMsts.Add(itm);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(itm).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllItems()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (DBModels db = new DBModels())
                {
                    ItemMst itm = db.ItemMsts.Where(x => x.ItemId == id).FirstOrDefault<ItemMst>();
                    db.ItemMsts.Remove(itm);
                    db.SaveChanges();
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllItems()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}