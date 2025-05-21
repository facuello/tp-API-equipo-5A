using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BaseDeDatos;
using Categorias;

namespace Articulos
{
    public class Articulo
    {
        public int ID { get; set; }
        public string Codigo { get;set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        [DisplayName("Marca")]
        public Marca Marc { get; set; }
        [DisplayName("Categoria")]
        public Categoria Categ { get; set; }
        public string Imagen { get; set; }
        public System.Decimal Precio { get; set; }
    }
}
