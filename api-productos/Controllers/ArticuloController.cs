using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Articulos;
using BaseDeDatos;
using api_productos.Models;
using Categorias;

namespace api_productos.Controllers
{
    public class ArticuloController : ApiController
    {

        //GET: api/Articulo
        public IEnumerable<Articulo> Get()
        {
            CatalogoArticulo catalogo = new CatalogoArticulo();

            return catalogo.listar();
        }

        // GET: api/Articulo/5
        public Articulo Get(int id)
        {
            CatalogoArticulo articulo = new CatalogoArticulo();
            List<Articulo> lista = articulo.listar();

            return lista.Find(x => x.ID == id);
        }

        // POST: api/Articulo
        public void Post([FromBody] ArticuloDto art)
        {
            CatalogoArticulo catalogo = new CatalogoArticulo();
            Articulo nuevo = new Articulo();
            nuevo.Codigo = art.Codigo;
            nuevo.Nombre = art.Nombre;
            nuevo.Descripcion = art.Descripcion;
            nuevo.Marc = new Marca { Id = art.IdMarca };
            nuevo.Categ = new Categoria { Id = art.IdCategoria };
            nuevo.Imagen = art.Imagen;
            nuevo.Precio = art.Precio;

            catalogo.agregarArticulo(nuevo);
        }
        //public void Post([FromBody] Articulo value)
        //{
        //}

        // PUT: api/Articulo/5
        //public void Put(int id, [FromBody] Articulo value)
        //{
        //}

        // DELETE: api/Articulo/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                CatalogoArticulo catalogo = new CatalogoArticulo();
                catalogo.EliminarArticulo(id);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Articulo borrado con exito.");

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "No se pudo borrar el Articulo.");
                throw ex;
            }

        }
    }
}
