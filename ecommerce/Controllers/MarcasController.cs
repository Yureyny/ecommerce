using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ecommerce.Models;

namespace ecommerce.Controllers
{
    public class MarcasController : Controller
    {
        private ecommerceContext db = new ecommerceContext();

        // GET: Marcas
        public ActionResult Index()
        {
            ViewBag.bodyclass = "product-page sidebar-collapse";
            return View(db.Marca.ToList());
        }
        // GET: ListaMarcas
        public JsonResult ListaMarcas()
        {
            return Json(db.Marca.Select(x => new
            {
                marca_id = x.marca_id,
                marca = x.nombre
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
            // GET: Marcas/Details/5
            public ActionResult Details(int? id)
        {
            ViewBag.bodyclass = "product-page sidebar-collapse";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marca marca = db.Marca.Find(id);
            if (marca == null)
            {
                return HttpNotFound();
            }
            return View(marca);
        }

        // GET: Marcas/Create
        public ActionResult Create()
        {
            ViewBag.bodyclass = "product-page sidebar-collapse";
            return View();
        }

        // POST: Marcas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "marca_id,nombre")] Marca marca)
        {
            if (ModelState.IsValid)
            {
                db.Marca.Add(marca);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(marca);
        }

        // GET: Marcas/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.bodyclass = "product-page sidebar-collapse";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marca marca = db.Marca.Find(id);
            if (marca == null)
            {
                return HttpNotFound();
            }
            return View(marca);
        }

        // POST: Marcas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "marca_id,nombre")] Marca marca)
        {
            if (ModelState.IsValid)
            {
                db.Entry(marca).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(marca);
        }

        // GET: Marcas/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.bodyclass = "product-page sidebar-collapse";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marca marca = db.Marca.Find(id);
            if (marca == null)
            {
                return HttpNotFound();
            }
            return View(marca);
        }

        // POST: Marcas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Marca marca = db.Marca.Find(id);
            db.Marca.Remove(marca);
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
