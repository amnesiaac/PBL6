﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PBL6b;

namespace PBL6b.Controllers
{
    public class EmpregosController : Controller
    {
        private db_pbl6Entities1 db = new db_pbl6Entities1();

        // GET: Empregos
        public ActionResult Index()
        {
            return View(db.Emprego.ToList());
        }

        // GET: Empregos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprego emprego = db.Emprego.Find(id);
            if (emprego == null)
            {
                return HttpNotFound();
            }
            return View(emprego);
        }

        // GET: Empregos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empregos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Empresa,Salario")] Emprego emprego)
        {
            if (ModelState.IsValid)
            {
                db.Emprego.Add(emprego);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(emprego);
        }

        // GET: Empregos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprego emprego = db.Emprego.Find(id);
            if (emprego == null)
            {
                return HttpNotFound();
            }
            return View(emprego);
        }

        // POST: Empregos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Empresa,Salario")] Emprego emprego)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emprego).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(emprego);
        }

        // GET: Empregos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprego emprego = db.Emprego.Find(id);
            if (emprego == null)
            {
                return HttpNotFound();
            }
            return View(emprego);
        }

        // POST: Empregos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Emprego emprego = db.Emprego.Find(id);
            db.Emprego.Remove(emprego);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
