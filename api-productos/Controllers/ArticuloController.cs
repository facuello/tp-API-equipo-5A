using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Articulos;
using BaseDeDatos;

namespace api_productos.Controllers
{
    public class ArticuloController : ApiController
    {
        // GET: api/Articulo
        public IEnumerable<Articulo> Get()
        {
            Catalogo catalogo = new Catalogo();

            //return catalogo.listar();
        }

        // GET: api/Articulo/5
        public Articulo Get(int id)
        {
            Catalogo catalogo = new Catalogo();
            //List<Articulo> lista = catalogo.listar();

            //return lista.Find(x=> x.Id == id);
        }

        // POST: api/Articulo
        public void Post([FromBody]Articulo value)
        {
        }

        // PUT: api/Articulo/5
        public void Put(int id, [FromBody]Articulo value)
        {
        }

        // DELETE: api/Articulo/5
        public void Delete(int id)
        {
        }
    }
}
