using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ecommerce.Models
{
    public class Venta_Detalle
    {

        [Key]
        public int venta_detalle_id { get; set; }
    }
}