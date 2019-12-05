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
    public class CarritoesController : Controller
    {
        private ecommerceContext db = new ecommerceContext();

        // GET: Carritoes
        public ActionResult Index()
        {
            ViewBag.bodyclass = "product-page sidebar-collapse";
            var carrito = db.Carrito.Include(c => c.Producto);
            return View(carrito.ToList());
        }

        // GET: Carritoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrito carrito = db.Carrito.Find(id);
            if (carrito == null)
            {
                return HttpNotFound();
            }
            return View(carrito);
        }

        // GET: Carritoes/Create
        public ActionResult Create()
        {
            ViewBag.producto_id = new SelectList(db.Producto, "producto_id", "nombre");
            return View();
        }

        // POST: Carritoes/Create
        
        [HttpPost]
        public JsonResult Create(int producto_id, int cantidad)
        {
            var result = new jsonMessage();
            try
            {

                //define the model of cart  
                Carrito lista = new Carrito();
                lista.producto_id = producto_id;
                lista.cantidad = cantidad;
                lista.ApplicationUserId = User.Identity.GetUserId();


                db.Carrito.Add(lista);
                result.Message = "Producto agregado al carrito";
                result.Status = true;
                result.Class = "success";

                db.SaveChanges();


            }
            catch (Exception ex)
            {
                // ErrorLogers.ErrorLog(ex);
                result.Message = "Ocurrio un error guardando el carrito, favor intentar luego";
                result.Status = false;
                result.Class = "danger";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: Carritoes/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.bodyclass = "product-page sidebar-collapse";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrito carrito = db.Carrito.Find(id);
            if (carrito == null)
            {
                return HttpNotFound();
            }
            ViewBag.producto_id = new SelectList(db.Producto, "producto_id", "nombre", carrito.producto_id);
            return View(carrito);
        }

        // POST: Carritoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "carrito_id,producto_id,ApplicationUserId,cantidad")] Carrito carrito)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carrito).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.producto_id = new SelectList(db.Producto, "producto_id", "nombre", carrito.producto_id);
            return View(carrito);
        }

        // GET: Carritoes/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.bodyclass = "product-page sidebar-collapse";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrito carrito = db.Carrito.Find(id);
            if (carrito == null)
            {
                return HttpNotFound();
            }
            return View(carrito);
        }

        // POST: Carritoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Carrito carrito = db.Carrito.Find(id);
            db.Carrito.Remove(carrito);
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
