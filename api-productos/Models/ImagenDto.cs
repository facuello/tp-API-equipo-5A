using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_productos.Models
{
    public class ImagenDto
    {
        public System.Int32 IdArticulo { get; set; }
        public List<string> ImagenUrl { get; set; }
    }
}
