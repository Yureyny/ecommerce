using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ecommerce.Models
{
    public class ListaDeseos
    {
        [Key]
        public int lista_id { get; set; }

        [Display(Name = "Producto")]
        public int producto_id { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual Producto Producto { get; set; }
       


    }
}