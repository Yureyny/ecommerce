using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ecommerce.Models;
using Microsoft.AspNet.Identity;

namespace ecommerce.Controllers
{
    public class ListaDeseosController : Controller
    {
        private ecommerceContext db = new ecommerceContext();

        // GET: ListaDeseos
        public ActionResult Index()
        {
            ViewBag.bodyclass = "product-page sidebar-collapse";
            string id = User.Identity.GetUserId(); 
            var lista_Deseos = db.Lista_Deseos.Include(l => l.Producto).Where(l=> l.ApplicationUserId==id);
            return View(lista_Deseos.ToList());
        }

        // GET: ListaDeseos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListaDeseos listaDeseos = db.Lista_Deseos.Find(id);
            if (listaDeseos == null)
            {
                return HttpNotFound();
            }
            return View(listaDeseos);
        }

        // GET: ListaDeseos/Create
        public ActionResult Create()
        {
            ViewBag.producto_id = new SelectList(db.Producto, "producto_id", "nombre");
            return View();
        }

        // POST: ListaDeseos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "lista_id,producto_id,ApplicationUserId")] ListaDeseos listaDeseos)
        {
            if (ModelState.IsValid)
            {
                db.Lista_Deseos.Add(listaDeseos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.producto_id = new SelectList(db.Producto, "producto_id", "nombre", listaDeseos.producto_id);
            return View(listaDeseos);
        }

        // GET: ListaDeseos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListaDeseos listaDeseos = db.Lista_Deseos.Find(id);
            if (listaDeseos == null)
            {
                return HttpNotFound();
            }
            ViewBag.producto_id = new SelectList(db.Producto, "producto_id", "nombre", listaDeseos.producto_id);
            return View(listaDeseos);
        }

        // POST: ListaDeseos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "lista_id,producto_id,ApplicationUserId")] ListaDeseos listaDeseos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(listaDeseos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.producto_id = new SelectList(db.Producto, "producto_id", "nombre", listaDeseos.producto_id);
            return View(listaDeseos);
        }

        // GET: ListaDeseos/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.bodyclass = "product-page sidebar-collapse";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListaDeseos listaDeseos = db.Lista_Deseos.Find(id);
            if (listaDeseos == null)
            {
                return HttpNotFound();
            }
            return View(listaDeseos);
        }

        // POST: ListaDeseos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ListaDeseos listaDeseos = db.Lista_Deseos.Find(id);
            db.Lista_Deseos.Remove(listaDeseos);
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
