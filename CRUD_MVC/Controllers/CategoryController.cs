using CRUD_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CRUD_MVC.Controllers
{
    public class CategoryController : Controller
    {
        private dbCrudMVCEntities db = new dbCrudMVCEntities();

        // GET: Category
        public ActionResult Index()
        {
            List<CategoryModel> categoryList = new List<CategoryModel>();

                var Category = (from cat in db.Categories
                                select new CategoryModel
                                {
                                    Id = cat.Id,
                                    CategoryName = cat.CategoryName
                                }).ToList();
                categoryList = Category;
                return View(categoryList);
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {

            Category categoryModel = GetCategory(id);
            if (categoryModel == null)
            {
                return HttpNotFound();
            }
            return View(categoryModel);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }


        // GET: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category cat)
        {

            if (ModelState.IsValid)
            {
                db.Categories.Add(cat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }


        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            Category c = null;
            c = GetCategory(id); 
            return View(c);
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(Category model)
        {
            try
            {
                // TODO: Add update logic here

                var result = db.Categories.Where(a => a.Id.Equals(model.Id)).FirstOrDefault();
                if (result != null)
                {
                    result.CategoryName = model.CategoryName;
                    
                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            Category c = null;
            c = GetCategory(id);  
            return View(c);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //var contact = dc.Contacts.Where(a => a.ContactID.Equals(id)).FirstOrDefault();
            Category category = db.Categories.Where(a => a.Id.Equals(id)).FirstOrDefault() ;
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        private Category GetCategory(int id)
        {
            Category category = new Category();

                var result = (from a in db.Categories
                         where a.Id.Equals(id)
                         select new
                         {
                             a.Id,
                             a.CategoryName
                         }).FirstOrDefault();
                if (result != null)
                {
                    category.Id = result.Id;
                    category.CategoryName = result.CategoryName;
                }

            return category;
        }
    }
}
