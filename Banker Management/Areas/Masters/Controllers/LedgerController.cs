﻿using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BM.Models;
using BM.DataAccess;
using BM.Web.Filters;
using NLog;
using BM.Web.ViewModels;
using BM.Common;
using System.Collections;
using System.Collections.Generic;
namespace BM.Web.Areas.Masters.Controllers
{
    [CustomActionAttribute]
    public class LedgerController : Controller
    {
        IRepository<Ledger> repository;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public LedgerController()
        {
            repository = new Repository<Ledger>();
        }
        // GET: /Masters/Ledger/
        public ActionResult Index()
        {
            //var ledgers = repository.Get<Ledger>();
            // return View(ledgers.ToList());
            return View();
        }

        public JsonResult Get()
        {
            var ledgers = repository.Get<Ledger>().Select(G => new LedgerViewModel { Id = G.Id, Name = G.Name, GroupName = G.Group.Name, MailingName = G.MailingName, OpeningBalance = G.OpeningBalance });
            return Json(ledgers.ToList(), JsonRequestBehavior.AllowGet);
        }


        // GET: /Masters/Ledger/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ledger = repository.Find<Ledger>(id);
            if (ledger == null)
            {
                return HttpNotFound();
            }
            return View(ledger);
        }

        // GET: /Masters/Ledger/Create
        public ActionResult Create()
        {
            var groups = repository.Get<Group>();
            ViewBag.GroupId = new SelectList(groups.ToList(), "Id", "Name");
            LoadDropDown();
            Ledger ledger = new Ledger();
            ledger.CreateInterestParameters();
            return View(ledger);
        }

        // POST: /Masters/Ledger/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ledger ledger)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ledger.Group = null;
                    ledger.City = null;
                    repository.Insert<Ledger>(ledger);
                    repository.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    logger.Error(exception.Message);
                    ModelState.AddModelError("", exception.Message);
                }
            }

            var groups = repository.Get<Group>();
            ViewBag.GroupId = new SelectList(groups.ToList(), "Id", "Name", ledger.GroupId);
            LoadDropDown();
            return View(ledger);
        }

        void LoadDropDown()
        {
            ViewBag.InterestStyleId = new SelectList(GetInterestStyle().AsEnumerable(), "Key", "Value");
            ViewBag.InterestBalanceId = new SelectList(GetInterestBalance().AsEnumerable(), "Key", "Value");
        }

        // GET: /Masters/Ledger/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ledger ledger = repository.Find<Ledger>(id);
            if (ledger == null)
            {
                return HttpNotFound();
            }
            var groups = repository.Get<Group>();
            LoadDropDown();
            ViewBag.GroupId = new SelectList(groups.ToList(), "Id", "Name", ledger.GroupId);
            return View(ledger);
        }

        // POST: /Masters/Ledger/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ledger ledger)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ledger.Group = null;
                    ledger.City = null;
                    repository.Update<Ledger>(ledger);
                    repository.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    logger.Error(exception.Message);
                }
            }
            var ledgers = repository.Get<Ledger>();
            LoadDropDown();
            ViewBag.GroupId = new SelectList(ledgers.ToList(), "Id", "Name", ledger.GroupId);
            return View(ledger);
        }

        // GET: /Masters/Ledger/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ledger ledger = repository.Find<Ledger>(id);
            if (ledger == null)
            {
                return HttpNotFound();
            }
            return View(ledger);
        }

        // POST: /Masters/Ledger/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                repository.Delete<Ledger>(id);
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
            Ledger ledger = repository.Find<Ledger>(id);
            if (ledger == null)
            {
                return HttpNotFound();
            }
            return View(ledger);
        }

        public Dictionary<string, string> GetInterestStyle()
        {
            Dictionary<string, string> interestStyleList = new Dictionary<string, string>();
            foreach (var interestStyle in Enums.EnumToList<InterestStyle>())
            {
                interestStyleList.Add(interestStyle.ToString(), Enums.GetEnumDescription(interestStyle));
            }
            return interestStyleList;
        }

        public Dictionary<string, string> GetInterestBalance()
        {
            Dictionary<string, string> interestBalanceList = new Dictionary<string, string>();
            foreach (var interestBalance in Enums.EnumToList<InterestBalance>())
            {
                interestBalanceList.Add(interestBalance.ToString(), Enums.GetEnumDescription(interestBalance));
            }
            return interestBalanceList;
        }
    }
}
