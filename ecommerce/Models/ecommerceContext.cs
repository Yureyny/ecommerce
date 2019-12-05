using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ecommerce.Models
{
    public class ecommerceContext : DbContext
    {
        public ecommerceContext() : base("name=DefaultConnection") { }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Marca> Marca { get; set; }
        public DbSet<Producto> Producto { get; set; } 
        public DbSet<Venta> Venta { get; set; }
        public DbSet<Venta_Detalle> Venta_Detalle { get; set; }
        public DbSet<ListaDeseos> Lista_Deseos { get; set; }
        public DbSet<Carrito> Carrito { get; set; }
    }
}

