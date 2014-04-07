using BM.Common;
using BM.DataAccess;
using BM.Models;
using BM.Web.ViewModels;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BM.Web.Areas.Settings.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        IRepository<Company> repository; 
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public CompanyController()
        {
            repository = new Repository<Company>();
        }
        //
        // GET: /Settings/Company/
        public ActionResult Index()
        {
            return View(repository.Get<Company>().ToList());
        }

        public ActionResult Switch(Guid id)
        {
            Company company = repository.GetById<Company>(id);
            GlobalVariables.CompanyId = id.ToString();
            GlobalVariables.CompanyName = company.Name;
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Company model)
        {
            //ResponseItem ResponseItem = new ResponseItem();
            if (ModelState.IsValid)
            {
                try
                {
                    if (repository.Get<Location>().Where(L => L.Name.ToLower() == model.Name.ToLower()).ToList().Count == 0)
                    {
                        repository.Insert<Company>(model);
                        repository.SaveChanges();
                        return RedirectToAction("Index");
                        //ResponseItem.CssClass = "Success";
                        //ResponseItem.Success = true;
                        //ResponseItem.Message = "Company Created Successfully"; 
                    }
                    ModelState.AddModelError("", "This Location Already Exists");
                }
                catch (Exception exception)
                {
                    logger.Error(exception.Message);
                    //ResponseItem.CssClass = "Error";
                    //ResponseItem.Success = false;
                    //ResponseItem.Message = "A Error Occured while creating Company";
                }
            }
            return View(model);
           // return Json(ResponseItem, JsonRequestBehavior.AllowGet); ;
        }

        //
        // GET: /Settings/Location/Edit/5
        public ActionResult Edit(Guid id)
        {
            return View(repository.GetById<Company>(id));
        }

        //
        // POST: /Settings/Location/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(Company model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (repository.Get<Company>().Where(L => L.Name.ToLower() == model.Name.ToLower()).ToList().Count <= 1)
                    {
                        model.City = null;
                        repository.Update<Company>(model);
                        repository.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", "This Company Already Exists");
                }
                catch (Exception exception)
                {
                    logger.Error(exception.Message);
                }
            }
            return View(model);
        }

        //
        // GET: /Settings/Location/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View(repository.GetById<Company>(id));
        }

        //
        // POST: /Settings/Location/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                repository.Delete<Company>(id);
                repository.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                logger.Error(exception.Message);
            }
            return View();
        }

        public JsonResult GetCompanies()
        {
            return Json(repository.Get<Company>().ToList(), JsonRequestBehavior.AllowGet);
        }
	}
}