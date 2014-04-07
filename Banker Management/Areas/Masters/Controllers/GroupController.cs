using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BM.Models;
using BM.DataAccess;
using BM.Web.Filters;
using NLog;
using BM.Web.ViewModels;

namespace BM.Web.Areas.Masters.Controllers
{
    [CustomActionAttribute]
    public class GroupController : Controller
    {
        IRepository<Group> repository;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public GroupController()
        {
            repository = new Repository<Group>();
        }
        // GET: /Masters/Group/
        public ActionResult Index()
        {
            //var groups = repository.Get<Group>();
           // return View(groups.ToList());
            return View();
        }

        public JsonResult Get()
        {
            var groups = repository.Get<Group>().Select(G => new GroupViewModel {Id=G.Id, Name = G.Name, Description = G.Description, ParentName = G.ParentGroup.Name });
            return Json(groups.ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: /Masters/Group/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var group = repository.Find<Group>(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // GET: /Masters/Group/Create
        public ActionResult Create()
        {
            var groups = repository.Get<Group>();
            ViewBag.ParentId = new SelectList(groups.ToList(), "Id", "Name");
            return View();
        }

        // POST: /Masters/Group/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Group group)
        {
            if (ModelState.IsValid)
            {
                repository.Insert<Group>(group);
                repository.SaveChanges();
                return RedirectToAction("Index");
            }

            var groups = repository.Get<Group>();
            ViewBag.ParentId = new SelectList(groups.ToList(), "Id", "Name", group.ParentId);
            return View(group);
        }

        // GET: /Masters/Group/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = repository.Find<Group>(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            var groups = repository.Get<Group>();
            ViewBag.ParentId = new SelectList(groups.ToList(), "Id", "Name", group.ParentId);
            return View(group);
        }

        // POST: /Masters/Group/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,ParentId,CreatedDate,ModifiedDate")] Group group)
        {
            if (ModelState.IsValid)
            {
                repository.Update<Group>(group);
                repository.SaveChanges();
                return RedirectToAction("Index");
            }
            var groups = repository.Get<Group>();
            ViewBag.ParentId = new SelectList(groups.ToList(), "Id", "Name", group.ParentId);
            return View(group);
        }

        // GET: /Masters/Group/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = repository.Find<Group>(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: /Masters/Group/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                repository.Delete<Group>(id);
                repository.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                logger.Error(exception.Message);
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ledger = repository.Find<Group>(id);
            if (ledger == null)
            {
                return HttpNotFound();
            }
            return View(ledger);
        }
    }
}
