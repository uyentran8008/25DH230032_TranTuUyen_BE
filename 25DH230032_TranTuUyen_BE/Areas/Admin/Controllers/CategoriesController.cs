using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _25DH230032_TranTuUyen_BE.Models;

namespace _25DH230032_TranTuUyen_BE.Areas.Admin.Controllers
{
    public class CategoriesController : Controller
    {
        private MyStoresEntities db = new MyStoresEntities();

        // GET: Admin/Categories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());// show data khi lấy về từ (db.Categories)
        }

        // GET: Admin/Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);// nếu ko có id thì nó sẽ báo lỗi (ko có giá trị trước đó truyền vào ) 400
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();// nếu có id mà ko thấy sản phẩm đâu sẽ hiện báo lỗi 404
            }
            return View(category);// vừa có id vừa sp 
        }

        // GET: Admin/Categories/Create
        // đây là hàm get 
        [HttpGet]
        public ActionResult Create()// lấy 1 bản ghi từ database và hiển thị lên from create
        {
            return View();
        }
        // đây là hàm post
        // POST: Admin/Categories/Create
        // lưu dữ liệu nhập vào từ form vào database
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] // => nó sẽ phân biệt cái nào đc nhấn 1 lần hay nhiều lần nếu mà nó nhấn nhiều lần sẽ bị xem là ( tấn công )
        public ActionResult Create([Bind(Include = "CategoryID,CategoryName")] Category category)//bind là ss trường dữ liệu gửi đi ( ở đây thì có categoryID và categoryName)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Admin/Categories/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,CategoryName")] Category category ) // lấy dữ liệu của danh mục đã có copy cho CategoryID=id 
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Admin/Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
