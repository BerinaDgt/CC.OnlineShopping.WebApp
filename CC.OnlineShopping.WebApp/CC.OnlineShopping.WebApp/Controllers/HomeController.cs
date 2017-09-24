using CC.OnlineShopping.Interfaces;
using CC.OnlineShopping.RepositoryEF;
using CC.OnlineShopping.RepositoryEF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CC.OnlineShopping.WebApp.Controllers
{
    public class HomeController : Controller
    {
        IProductRepository _productRepository = new ProductRepository();
        DatabaseOnlineShopping db = new DatabaseOnlineShopping();
        // GET: Home
        public ActionResult Index()
        {
            var products = _productRepository.GetAll().OrderByDescending(p => p.ProductId).Take(3).ToList();

            ViewBag.ProductImgs = _productRepository.GetAll().OrderByDescending(p => p.ProductId).Skip(3).ToList();

            return View(products);
        }

        public ActionResult Cart()
        {
            var theOne = db.Users.Single(u => u.Email == User.Identity.Name).ID;

            var user = db.Carts.Single(c => c.ID == theOne);


            ViewBag.Articles = user.ProductsToCart.Count();
            ViewBag.CartPrice = user.ProductsToCart.Sum(p => p.Product.Price);

            return PartialView();
        }
    }
}