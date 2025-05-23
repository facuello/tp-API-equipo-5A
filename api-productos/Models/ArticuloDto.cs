using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Articulos;
using Categorias;

namespace api_productos.Models
{
    public class ArticuloDto
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdMarca { get; set; }
        public int IdCategoria { get; set; }
        public string Imagen { get; set; }
        public System.Decimal Precio { get; set; }
    }
}