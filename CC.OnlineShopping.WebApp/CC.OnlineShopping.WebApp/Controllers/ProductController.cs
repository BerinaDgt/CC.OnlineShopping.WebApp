using CC.OnlineShopping.Entities;
using CC.OnlineShopping.Interfaces;
using CC.OnlineShopping.RepositoryEF;
using CC.OnlineShopping.RepositoryEF.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CC.OnlineShopping.WebApp.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        IProductRepository _productRepository = new ProductRepository();
        DatabaseOnlineShopping db = new DatabaseOnlineShopping();
        // GET: Product
        public ActionResult Index()
        {
            var products = _productRepository.GetAll().ToList();

            return View(products);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file, string email)
        {
            email = User.Identity.Name;

            var user = db.Users.Single(u => u.Email == email);
            var theOne = user.ID;

            if (ModelState.IsValid)
            {
                product.Image = Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("~/Images/ProductImages"), product.Image);
                file.SaveAs(path);

                product.UserId = theOne;

                if (_productRepository.Create(product))
                    return RedirectToAction("UserAccount", "Account");
            }

            return View();
        }

        public ActionResult Delete(Product product)
        {
            return View(_productRepository.GetById(product.ProductId));
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (_productRepository.Delete(id))
                return RedirectToAction("UserAccount", "Account");
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View(_productRepository.GetById(id));
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                if (_productRepository.Update(product))
                    return RedirectToAction("UserAccount", "Account");
            }
            return View(product);
        }
    }
}