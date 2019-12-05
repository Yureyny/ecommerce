using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ecommerce.Models
{
    public class Producto
    {

        [Key]
        public int producto_id { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El Campo {0} debe tener una longitud maxima de 50 caracteres")]
        [Display(Name = "Producto")]
        public String nombre { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [DataType(DataType.Currency)]
        [Display(Name = "Precio")]
        public double precio { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [MaxLength(1, ErrorMessage = "El Campo {0} debe tener una longitud maxima de 1 caracteres")]
        [Display(Name = "Size")]
        public String size { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [MaxLength(1, ErrorMessage = "El Campo {0} debe tener una longitud maxima de 1 caracteres")]
        [Display(Name = "Sexo")]
        public String sexo { get; set; }


        [Display(Name = "Categoria")]
        public int categoria_id { get; set; }


        [Display(Name = "Marca")]
        public int marca_id { get; set; }

        [Display(Name = "Imagen")]
        public byte[] imagen { get; set; }

        public int stock { get; set; }

        // public HttpPostedFileWrapper ImageFile { get; set; }

        public virtual Categoria Categoria { get; set; }

        public virtual Marca Marca { get; set; }





    }
}