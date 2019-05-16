using System;
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
    public class FamiliasController : Controller
    {
        private db_pbl6Entities1 db = new db_pbl6Entities1();

        // GET: Familias
        public ActionResult Index()
        {
            var familia = db.Familia.Include(f => f.Membro);
            return View(familia.ToList());
        }

        // GET: Familias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Familia familia = db.Familia.Find(id);
            if (familia == null)
            {
                return HttpNotFound();
            }
            return View(familia);
        }

        // GET: Familias/Create
        public ActionResult Create()
        {
            ViewBag.MembroId = new SelectList(db.Membro, "Id", "Nome");
            return View();
        }

        // POST: Familias/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Sobrenome,MembroId,Renda")] Familia familia, List<int>MembroId)
        {
            if (ModelState.IsValid)
            {
                foreach(int membro in MembroId)
                {
                    Membro membro1 = db.Membro.Find(membro);
                    familia.Renda += membro1.Renda;
                }
                db.Familia.Add(familia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MembroId = new SelectList(db.Membro, "Id", "Nome", familia.MembroId);
            return View(familia);
        }

        // GET: Familias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Familia familia = db.Familia.Find(id);
            if (familia == null)
            {
                return HttpNotFound();
            }
            ViewBag.MembroId = new SelectList(db.Membro, "Id", "Nome", familia.MembroId);
            return View(familia);
        }

        // POST: Familias/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Sobrenome,MembroId,Renda")] Familia familia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(familia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MembroId = new SelectList(db.Membro, "Id", "Nome", familia.MembroId);
            return View(familia);
        }

        // GET: Familias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Familia familia = db.Familia.Find(id);
            if (familia == null)
            {
                return HttpNotFound();
            }
            return View(familia);
        }

        // POST: Familias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Familia familia = db.Familia.Find(id);
            db.Familia.Remove(familia);
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
