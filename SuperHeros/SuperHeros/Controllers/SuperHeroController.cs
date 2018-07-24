using SuperHeros.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperHeros.Controllers
{
    public class SuperHeroController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: SuperHero
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,AlterEgo,PrimaryAbility,SecondaryAbility,CatchPhrase")] Models.SuperHeros superHero)
        {
            if(ModelState.IsValid)
            {
                db.SuperHeros.Add(superHero);
                db.SaveChanges();
                return RedirectToAction("Index", "SuperHero");
            }

            ViewBag.Name = new SelectList(db.SuperHeros, "Id", "Name", superHero.Name);
            return View(superHero);
        }
        public ActionResult Details()
        {
            return View(db.SuperHeros.ToList());
        }
        public ActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.SuperHeros superHero)
        {
            var hero = db.SuperHeros.Where(s => s.Id == superHero.Id).Single();
            hero.Name = superHero.Name;
            hero.AlterEgo = superHero.AlterEgo;
            hero.PrimaryAbility = superHero.PrimaryAbility;
            hero.SecondaryAbility = superHero.SecondaryAbility;
            hero.CatchPhrase = superHero.CatchPhrase;
            db.SaveChanges();
            return RedirectToAction("Details");
        }
        public ActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string name)
        {
            var deletedHero = db.SuperHeros.Where(s => s.Name == name).Single();
            return View(deletedHero);
            //db.SuperHeros.Remove(deletedHero);
            //db.SaveChanges();
            //return RedirectToAction("Details");
        }
    }
}