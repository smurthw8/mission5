using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FilmFanatic.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmFanatic.Controllers
{
    public class HomeController : Controller
    {

        private FilmCollectionContext _filmAdder { get; set; }

        public HomeController( FilmCollectionContext someName )
        {
            _filmAdder = someName;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Podcasts()
        {
            return View();
        }

        //ADD TO COLLECTION controllers
        [HttpGet]
        public IActionResult AddtoCollection()
        {
            ViewBag.Categories = _filmAdder.Categories.ToList();

            return View();
        }

        [HttpPost]
        //cr = what being passed from the form
        public IActionResult AddtoCollection(CollectionResponse cr)
        {
            ViewBag.Categories = _filmAdder.Categories.ToList();

            if (ModelState.IsValid)
            {
                //get data from form
                _filmAdder.Add(cr);
                //save changes
                _filmAdder.SaveChanges();

                return RedirectToAction("FilmCollection");
            }
            else //if data doesn't meet requirements
            {
                return View(cr);
            }
        }

        public IActionResult FilmCollection()
        {
            var collection = _filmAdder.Films
                .Include(x => x.Category)
                .Where(x => x.Edited == false)
                .OrderBy(x => x.Year)
                .ToList();

            return View(collection);
        }

        //EDIT page controllers
        [HttpPost]
        public IActionResult Edit(CollectionResponse cr)
        {
            if (ModelState.IsValid)
            {
                _filmAdder.Update(cr);
            _filmAdder.SaveChanges();

            //need to redirecttoaction, or pass in all Film Collection data again
            return RedirectToAction("FilmCollection");
            }
            else
            {
                return View(cr);
            }

        }

        [HttpGet]
        public IActionResult Edit(int filmid)
        {
            ViewBag.Categories = _filmAdder.Categories.ToList();

            //ToString pull record using Find (then do itar by ID), or Single (expects criteria to find 1 entry)
            //finds Films object where FilmId == paramater filmid
            var collection = _filmAdder.Films.Single(x => x.FilmId == filmid);

            return View("AddtoCollection", collection);
        }

        //DELETE page controllers
        [HttpGet]
        public IActionResult Delete(int filmid)
        {
            var collection = _filmAdder.Films.Single(x => x.FilmId == filmid);

            return View(collection);
        }

        [HttpPost]
        //pass in model feilds for specific row > need to pass in Id as hidden feild on delete page
        public IActionResult Delete(CollectionResponse cr)
        {
            _filmAdder.Films.Remove(cr);
            _filmAdder.SaveChanges();

            return RedirectToAction("FilmCollection");
        }


    }
}
