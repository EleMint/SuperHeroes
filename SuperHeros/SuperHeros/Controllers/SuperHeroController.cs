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
            return View(db.SuperHeros.ToList());
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
        public ActionResult Details(int id)
        {
            var Hero = db.SuperHeros.Where(s => s.Id == id).FirstOrDefault();
            return View(Hero);
        }
        public ActionResult Edit(int id)
        {
            var Hero = db.SuperHeros.Where(s => s.Id == id).FirstOrDefault();
            return View(Hero);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.SuperHeros superhero)
        {
            var hero = db.SuperHeros.Where(s => s.Id == superhero.Id).FirstOrDefault();
            hero.Name = superhero.Name;
            hero.AlterEgo = superhero.AlterEgo;
            hero.PrimaryAbility = superhero.PrimaryAbility;
            hero.SecondaryAbility = superhero.SecondaryAbility;
            hero.CatchPhrase = superhero.CatchPhrase;
            db.SaveChanges();
            var Heroes = db.SuperHeros.ToList();
            return RedirectToAction("Index", Heroes);
        }
        public ActionResult Delete(int id)
        {
            var deletedHero = db.SuperHeros.Where(s => s.Id == id).Single();
            return View(deletedHero);
        }
        public ActionResult OnDelete(int id)
        {
            var deletedHero = db.SuperHeros.Where(s => s.Id == id).Single();
            db.SuperHeros.Remove(deletedHero);
            db.SaveChanges();
            var Heroes = db.SuperHeros.ToList();
            return View("Index", Heroes);
        }
    }
}