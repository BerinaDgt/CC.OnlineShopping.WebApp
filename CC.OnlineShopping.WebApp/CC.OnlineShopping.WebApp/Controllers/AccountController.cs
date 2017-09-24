using CC.OnlineShopping.Entities;
using CC.OnlineShopping.Interfaces;
using CC.OnlineShopping.RepositoryEF;
using CC.OnlineShopping.RepositoryEF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CC.OnlineShopping.WebApp.Controllers
{
    public class AccountController : Controller
    {
        DatabaseOnlineShopping db = new DatabaseOnlineShopping();
        IUserRepository _userRepository = new UserRepository();
        IProductRepository _productRepository = new ProductRepository();
        // GET: Account
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(string email, string password)
        {
            var dbUser = db.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (dbUser != null)
            {
                FormsAuthentication.SetAuthCookie(email, true);
                return RedirectToAction("Loading", "Account");
            }

            ViewBag.Error = "Invalid email or password, please try again";

            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (_userRepository.Create(user))
                    return RedirectToAction("LogIn");
            }

            return View();
        }

        [Authorize]
        public ActionResult UserAccount(string email)
        {
            email = User.Identity.Name;

            var user = db.Users.Single(u => u.Email == email);

            return View(user);
        }

        public ActionResult Loading()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public JsonResult AddCart(string username)
        {
            var dbUser = db.Users.Single(u => u.Email == username);

            if(dbUser.Cart == null) { 
            db.Carts.Add(new Cart { ID = dbUser.ID});
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddToCart(int prodId)
        {
            var dbUser = db.Users.Single(u => u.Email == User.Identity.Name);

            var dbCart = db.Carts.Single(c => c.ID == dbUser.ID).ID;
            

            if (dbUser != null)
            {
                //db.Entry(dbCart).Collection(c => c.Products).Load();
                //dbCart.Products.Add(dbProduct);

                db.ProductToCarts.Add(new ProductsToCart { CartId = dbCart, ProductId = prodId });
                db.SaveChanges();
                Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Cart()
        {
            var user = db.Users.Single(u => u.Email == User.Identity.Name);

            return View(user);
        }

        public JsonResult Remove(int prodId)
        {
            //.Cart.ProductsToCart.Select(pc => pc.Product)
            var dbUser = db.Users.Single(u => u.Email == User.Identity.Name);

            if (dbUser != null)
            {
                var remove = dbUser.Cart.ProductsToCart.Where(pc => pc.ProductId == prodId).FirstOrDefault();

                db.ProductToCarts.Remove(remove);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}