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
    public class CategoriasController : Controller
    {
        private ecommerceContext db = new ecommerceContext();

        // GET: Categorias
        public ActionResult Index()
        {
            ViewBag.bodyclass = "product-page sidebar-collapse";
            return View(db.Categoria.ToList());
        }  
        // GET: ListaCategorias
        public JsonResult ListaCategorias()
        {
            //var result = new jsonMessage();
            //try
            //{

            //    string listado = ""; 
            //    List<Categoria> categoria = db.Categoria.ToList();
            //    foreach(var item in categoria) {

            //        //  listado += "<a class=\"dropdown - item\" href=\"/Productoes/Catalogo_Categoria?categoria_id=" + item.categoria_id+ "\"> " + item.nombre+"</a><br>";
            //    }
            //    result.Message = listado;
            //    result.Status = true;
            //}
            //catch (Exception ex)
            //{
            //    result.Message = "We are unable to process your request at this time. Please try again later.";
            //    result.Status = false;
            //}

            //return Json(result, JsonRequestBehavior.AllowGet);

            return Json(db.Categoria.Select(x => new
            {
                categoria_id = x.categoria_id
                ,
                categoria = x.nombre
            }).ToList(), JsonRequestBehavior.AllowGet);

        }

        // GET: Categorias/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.bodyclass = "product-page sidebar-collapse";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categoria.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // GET: Categorias/Create
        public ActionResult Create()
        {
            ViewBag.bodyclass = "product-page sidebar-collapse";
            return View();
        }

        // POST: Categorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "categoria_id,nombre")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                db.Categoria.Add(categoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoria);
        }

        // GET: Categorias/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.bodyclass = "product-page sidebar-collapse";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categoria.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "categoria_id,nombre")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        // GET: Categorias/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.bodyclass = "product-page sidebar-collapse";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categoria.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categoria categoria = db.Categoria.Find(id);
            db.Categoria.Remove(categoria);
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
