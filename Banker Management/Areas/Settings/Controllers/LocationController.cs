using BM.DataAccess;
using BM.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BM.Web.Areas.Settings.Controllers
{
    public class LocationController : Controller
    {
        IRepository<Location> repository;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public LocationController()
        {
            repository = new Repository<Location>();
        }
        //
        // GET: /Settings/Location/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Settings/Location/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Settings/Location/Create
        public ActionResult Create()
        {
            //IEnumerable<LocationType> actionTypes = Enum.GetValues(typeof(LocationType))
            //                                           .Cast<LocationType>();
            //ViewBag.LocationType = (from action in actionTypes
            //                    select new SelectListItem
            //                    {
            //                        Text = action.ToString(),
            //                        Value = ((int)action).ToString()
            //                    }).ToList();
            return View();
        }

        //
        // POST: /Settings/Location/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(Location model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (repository.Get<Location>().Where(L => L.Name.ToLower() == model.Name.ToLower()).ToList().Count == 0)
                    {
                        repository.Insert<Location>(model);
                        repository.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", "This Location Already Exists");
                }
                catch (Exception exception)
                {
                    logger.Error(exception.Message);
                    ModelState.AddModelError("", exception.Message);
                }
            }
            return View(model);
        }

        //
        // GET: /Settings/Location/Edit/5
        public ActionResult Edit(Guid id)
        {
            return View(repository.GetById<Location>(id));
        }

        //
        // POST: /Settings/Location/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(Location model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (repository.Get<Location>().Where(L => L.Name.ToLower() == model.Name.ToLower()).ToList().Count <= 1)
                    {
                        //Location location = new Location();
                        //location.Id = model.Id;
                        //location.
                        model.ParentLocation = null;
                        repository.Update<Location>(model);
                        repository.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    ModelState.AddModelError("", "This Location Already Exists");
                }
                catch (Exception exception)
                {
                    logger.Log(LogLevel.Error, exception.Message);
                    ModelState.AddModelError("", exception.Message);
                }
            }
            return View(model);
        }

        //
        // GET: /Settings/Location/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View(repository.GetById<Location>(id));
        }

        //
        // POST: /Settings/Location/Delete/5
       [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                repository.Delete<Location>(id);
                repository.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                logger.Error(exception.Message);
            }
            return View();
        }

        [HttpPost]
        public JsonResult LookupCity(string Id)
        {
            return Json((from b in repository.Get<Location>()//.Where(L => L.ParentId != null)
                         where b.Name.ToLower().Contains(Id.ToLower())
                         select new { Id = b.Id, Name = b.Name }).ToList().Take(10), JsonRequestBehavior.AllowGet);
        }

        // [HttpGet]
        public JsonResult GetLocations()
        {
            var query = from c1 in repository.Get<Location>()
                        //where c1.ParentId == null
                        join c2 in repository.Get<Location>() on c1.Id equals c2.ParentId
                        select new { Id = c2.Id, Name = c2.Name, ParentName = c1.Name };
            return Json(query.OrderBy(L => L.Name), JsonRequestBehavior.AllowGet);
        }
    }
}
