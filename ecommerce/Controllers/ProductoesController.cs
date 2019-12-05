using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ecommerce.Models;
using Microsoft.AspNet.Identity;

namespace ecommerce.Controllers
{
    public class ProductoesController : Controller
    {
        private ecommerceContext db = new ecommerceContext();

        // GET: Productoes
        public ActionResult Index()
        {
            ViewBag.bodyclass = "product-page sidebar-collapse";
            var producto = db.Producto.Include(p => p.Categoria).Include(p => p.Marca);
            return View(producto.ToList());
        }
       
        // GET: Productoes/Catalogo
        public ActionResult Catalogo()
        {
            ViewBag.bodyclass = "ecommerce-page sidebar-collapse";
           var producto = db.Producto.Include(p => p.Categoria).Include(p => p.Marca);
            return View(producto.ToList());
        }
        // GET: Productoes/Producto_Parcial
        public ActionResult Producto_Parcial(int? categoria_id, int?marca_id)
        {
            if (categoria_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            } 
            if (marca_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var producto = db.Producto.Include(p => p.Categoria).Include(p => p.Marca).Where(p => (p.categoria_id == categoria_id || categoria_id==0)).Where(p=> p.marca_id==marca_id || marca_id==0); 
            return PartialView(producto.ToList());
        }
        // GET: Productoes/Producto/5
        public ActionResult Producto(int? id)
        {
            ViewBag.bodyclass = "product-page sidebar-collapse";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View("Producto",producto);
        }

        // GET: Productoes/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.bodyclass = "product-page sidebar-collapse";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Productoes/Create
        public ActionResult Create()
        {
            ViewBag.bodyclass = "product-page sidebar-collapse";
            ViewBag.categoria_id = new SelectList(db.Categoria, "categoria_id", "nombre");
            ViewBag.marca_id = new SelectList(db.Marca, "marca_id", "nombre");
            return View();
        }

        // POST: Productoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "producto_id,nombre,precio,size,sexo,categoria_id,marca_id,imagen")] Producto producto)
        {
            HttpPostedFileBase fb = Request.Files[0];
            WebImage image = new WebImage(fb.InputStream);
            producto.imagen = image.GetBytes();

            if (ModelState.IsValid)
            {
                db.Producto.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.categoria_id = new SelectList(db.Categoria, "categoria_id", "nombre", producto.categoria_id);
            ViewBag.marca_id = new SelectList(db.Marca, "marca_id", "nombre", producto.marca_id);
            return View(producto);
        }
        // POST: Productoes/save_list
        [HttpPost]
        public JsonResult save_list(int producto_id)
        {

            var result = new jsonMessage();
            try
            {

                //define the model of crt  
                ListaDeseos lista = new ListaDeseos();
                lista.producto_id = producto_id;
                lista.ApplicationUserId = User.Identity.GetUserId();


                db.Lista_Deseos.Add(lista);
                result.Message = "Producto agregado a la lista de deseos";
                result.Status = true;
                result.Class = "success";

                db.SaveChanges();


            }
            catch (Exception ex)
            {
               // ErrorLogers.ErrorLog(ex);
                result.Message = "Ocurrio un error guardando la lista, favor intentar luego";
                result.Status = false;
                result.Class = "danger";
            }

            return Json(result, JsonRequestBehavior.AllowGet);





        }

        // GET: Productoes/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.bodyclass = "product-page sidebar-collapse";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoria_id = new SelectList(db.Categoria, "categoria_id", "nombre", producto.categoria_id);
            ViewBag.marca_id = new SelectList(db.Marca, "marca_id", "nombre", producto.marca_id);
            return View(producto);
        }

        // POST: Productoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "producto_id,nombre,precio,size,sexo,categoria_id,marca_id,imagen")] Producto producto)
        {
            HttpPostedFileBase fb = Request.Files[0];
            WebImage image = new WebImage(fb.InputStream);
            producto.imagen = image.GetBytes();

            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categoria_id = new SelectList(db.Categoria, "categoria_id", "nombre", producto.categoria_id);
            ViewBag.marca_id = new SelectList(db.Marca, "marca_id", "nombre", producto.marca_id);
            return View(producto);
        }

        // GET: Productoes/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.bodyclass = "product-page sidebar-collapse";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Producto.Find(id);
            db.Producto.Remove(producto);
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

        public ActionResult getImage(int id)
        {
            Producto pro = db.Producto.Find(id);
            byte[] byteimage = pro.imagen;

            MemoryStream ms = new MemoryStream(byteimage);
            Image img = Image.FromStream(ms);
            ms = new MemoryStream();
            img.Save(ms,ImageFormat.Jpeg);
            ms.Position = 0;
            return File(ms,"image/jpg");
        }
    }

    //internal class jsonMessage
    //{
    //public string Message { get; set; }
    //public bool Status { get; set; }
    // public jsonMessage(){}
    //}
}
