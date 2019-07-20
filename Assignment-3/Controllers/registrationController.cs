using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment_3.Models;



namespace Assignment_3.Controllers
{
    
    public class registrationController : Controller
    {
        private registrationDBContext db = new registrationDBContext();


        public static class MyGlobalVariables
        {
            public static registration obj { get; set; }
            public static int user_id { get; set; }
            public static string sign_in_name { get; set; }
            public static string sign_in_pass { get; set; }
            public static bool signin { set; get; }
    }


        // GET: registration
        public ActionResult Index()
        {
            return View();
        }

        // POST: diners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "ID,first_name,last_name,email,mobile_number,password")] registration registration)
        {
            if (ModelState.IsValid)
            {
                db.registration.Add(registration);
                db.SaveChanges();
                return RedirectToAction("updates");
            }

            return View(registration);
        }

        public ActionResult signin(string nam,string searchStrings)

        { 
            return View();
            
        }

        public ActionResult updates(string nam,string pass)
        {
            if (MyGlobalVariables.obj != null && MyGlobalVariables.signin==true)
            {
                var check = from m in db.registration
                            select m;

                if (!String.IsNullOrEmpty(MyGlobalVariables.sign_in_name) && !String.IsNullOrEmpty(MyGlobalVariables.sign_in_name) && !String.IsNullOrEmpty(MyGlobalVariables.sign_in_pass) && !String.IsNullOrEmpty(MyGlobalVariables.sign_in_pass))
                {
                    check = check.Where(s => s.password.Equals(MyGlobalVariables.sign_in_pass) && s.first_name.Equals(MyGlobalVariables.sign_in_name));

                    if (check.Any())
                    {
                        
                        return View(db.products.ToList());
                    }
                    else
                    {
                        MyGlobalVariables.signin = false;
                        return View("signin");
                    }
                }
                else
                {
                    MyGlobalVariables.signin = false;
                    return View("signin");
                }
            }
            else
            {

                var check = from m in db.registration
                            select m;

                if (!String.IsNullOrEmpty(nam) && !String.IsNullOrEmpty(nam) && !String.IsNullOrEmpty(pass) && !String.IsNullOrEmpty(pass))
                {
                    check = check.Where(s => s.password.Equals(pass) && s.first_name.Equals(nam));



                    if (check.Any())
                    {
                        registration rs = check.Single(w => w.first_name == nam);
                        MyGlobalVariables.obj = new registration();
                        MyGlobalVariables.signin = true;
                        MyGlobalVariables.obj = rs;
                        MyGlobalVariables.user_id = rs.ID;
                        MyGlobalVariables.sign_in_name = nam;
                        MyGlobalVariables.sign_in_pass = pass;

                        return View(db.products.ToList());
                    }
                    else
                    {
                        MyGlobalVariables.signin = false;
                        return View("signin");
                    }
                }
                else
                {
                    MyGlobalVariables.signin = false;
                    return View("signin");
                }

            }
        }

        

        public ActionResult profile()
        {
            if (MyGlobalVariables.obj == null || MyGlobalVariables.signin==false)
            {
                return View("Index");
            }
            else
            {
                var check = from m in db.registration
                            select m;
                registration rs = check.Single(w => w.ID == MyGlobalVariables.user_id);
                return View(rs);
            }
        }

        public ActionResult change()
        {
            var check = from m in db.registration
                        select m;
            registration rs = check.Single(w => w.ID == MyGlobalVariables.user_id);
            return View(rs);
        }

        [HttpPost]
        
        public ActionResult changesave([Bind(Include = "ID,first_name,last_name,email,mobile_number,password")] registration registration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("profile");
            }
            return View(registration);
        }

        public ActionResult rating(string rating)

        {
            if (!String.IsNullOrEmpty(rating) && !String.IsNullOrEmpty(rating))
            {
                
                var std = db.products.Where(s => s.title == rating)
                        .FirstOrDefault<product>();
                if (std.rating < 5)
                {
                    int rating_value = std.rating + 1;
                    std.rating = rating_value;
                    db.SaveChanges();
                }
                ViewBag.data = db.products.ToList();
                return View("updates", db.products.ToList());
            }
            
            
           
            return View("signin");
        }

        public ActionResult searchIndex(String searchString)
        {
            var food = from m in db.products
                       select m;

            Console.WriteLine(searchString);
            if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(searchString))
            {
                food = food.Where(s => s.title.Contains(searchString));

            }
            return View("updates",food);
        }


    }
}