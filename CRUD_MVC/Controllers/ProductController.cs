using CRUD_MVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_MVC.Controllers
{
    public class ProductController : Controller
    {
        private dbCrudMVCEntities dbProd = new dbCrudMVCEntities();

        // GET: Product
        public ActionResult Index()
        {
            
            List<ProductModel> productList = new List<ProductModel>();

            var product = (from p in dbProd.Products
                           join c in dbProd.Categories on p.CategoryId equals c.Id
                           select new ProductModel
                           {
                               Id = p.Id,
                               CategoryId = c.Id,
                               CategoryName = c.CategoryName,
                               Name = p.Name,
                               Description = p.Description,
                               Image = p.Image
                           }).ToList();

            productList = product;
            return View(productList);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id, string categoryName)
        {
            Product p = null;
            ViewBag.CategoryName = categoryName;
            p = GetProduct(id);
            return View(p);
        }

        // GET: Product/Create
        public ActionResult Create(Product prod)
        {
            List<Category> allCategory = new List<Category>();
            allCategory = dbProd.Categories.OrderBy(a => a.CategoryName).ToList();

            ViewBag.Category = new SelectList(allCategory, "Id", "CategoryName", prod.Id);

            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product prod, HttpPostedFileBase file)
        {
            try
            {
                List<Category> allCategory = new List<Category>();
                allCategory = dbProd.Categories.OrderBy(a => a.CategoryName).ToList();

                ViewBag.Category = new SelectList(allCategory, "Id", "CategoryName", prod.Id);


                if (file != null && file.ContentLength > 0)
                {
                    // extract only the fieldname
                    var fileName = Path.GetFileName(file.FileName);
                    // store the file inside ~/App_Data/uploads folder
                    var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                    file.SaveAs(path);
                }

                if (prod.CategoryId == null)
                {
                    ViewBag.ErrMsg = "Please select category!";
                }
                else if (file == null)
                {
                    ViewBag.ErrMsg = "Picture of product is required! Please upload the image!";
                }
                else
                {
                    using (var binaryReader = new BinaryReader(file.InputStream))
                        prod.Image = binaryReader.ReadBytes(file.ContentLength);


                    // TODO: Add insert logic here
                    if (ModelState.IsValid)
                    {
                        dbProd.Products.Add(prod);
                        dbProd.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    using (var binaryReader = new BinaryReader(file.InputStream))
                        prod.Image = binaryReader.ReadBytes(file.ContentLength);


                    // TODO: Add insert logic here
                    if (ModelState.IsValid)
                    {
                        dbProd.Products.Add(prod);
                        dbProd.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }

                return View(prod);
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.ToString());
                return View(prod);
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {

            // TODO: Add update logic here
            List<Category> allCategory = new List<Category>();
            allCategory = dbProd.Categories.OrderBy(a => a.CategoryName).ToList();

            ViewBag.Category = new SelectList(allCategory, "Id", "CategoryName", id);

            Product p = null;
            p = GetProduct(id);
            return View(p);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Product prod, HttpPostedFileBase file)
        {
            try
            {
                // TODO: Add update logic here
                List<Category> allCategory = new List<Category>();
                allCategory = dbProd.Categories.OrderBy(a => a.CategoryName).ToList();

                ViewBag.Category = new SelectList(allCategory, "Id", "CategoryName", prod.Id);


                if (file != null && file.ContentLength > 0)
                {
                    // extract only the fieldname
                    var fileName = Path.GetFileName(file.FileName);
                    // store the file inside ~/App_Data/uploads folder
                    var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                    file.SaveAs(path);


                    using (var binaryReader = new BinaryReader(file.InputStream))
                    prod.Image = binaryReader.ReadBytes(file.ContentLength);
                }


                if (prod.CategoryId == null)
                {
                    ViewBag.ErrMsg = "Please select category!";
                }
                else
                {

                    var result = dbProd.Products.Where(a => a.Id.Equals(prod.Id)).FirstOrDefault();
                    if (result != null)
                    {
                        result.CategoryId = prod.CategoryId;
                        result.Name = prod.Name;
                        result.Description = prod.Description;
                        result.Image = prod.Image;

                    }
                    dbProd.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(prod);

            }
            catch (Exception ex)
            {
                System.Console.Write(ex.ToString());
                return View(prod);
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            Product p = new Product();
            p = GetProduct(id);
            return View(p);
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Product product = dbProd.Products.Where(a => a.Id.Equals(id)).FirstOrDefault();
                dbProd.Products.Remove(product);
                dbProd.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private Product GetProduct(int id)
        {
            

            Product product = new Product();

            var result = (from a in dbProd.Products
                          where a.Id.Equals(id)
                          select new
                          {
                              a.Id,
                              a.CategoryId,
                              a.Name,
                              a.Description,
                              a.Image
                          }).FirstOrDefault();
            if (result != null)
            {
                product.Id = result.Id;
                product.CategoryId = result.CategoryId;
                product.Name = result.Name;
                product.Description = result.Description;
                product.Image = result.Image;
            }

            return product;
        }
    }
}
