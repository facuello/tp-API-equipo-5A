using api_productos.Models;
using Articulos;
using BaseDeDatos;
using Categorias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls.WebParts;

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

        [HttpPost]
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
        public void Put(int id, [FromBody] ArticuloDto art)
        {
            CatalogoArticulo catalogo = new CatalogoArticulo();
            Articulo nuevo = new Articulo();
            nuevo.ID = id;
            nuevo.Codigo = art.Codigo;
            nuevo.Nombre = art.Nombre;
            nuevo.Descripcion = art.Descripcion;
            nuevo.Marc = new Marca { Id = art.IdMarca };
            nuevo.Categ = new Categoria { Id = art.IdCategoria };
            nuevo.Imagen = art.Imagen;
            nuevo.Precio = art.Precio;

            catalogo.modificar(nuevo);
        }

        //Agregar Imagenes: api/articulo/PostImg

        [HttpPost]
        [Route("api/articulo/PostImg")]
        public void PostImg([FromBody] ImagenDto img)
        {
            CatalogoImagen catalogo = new CatalogoImagen();
            Imagen nuevo = new Imagen();
            foreach (string aux in img.ImagenUrl)
            {
                nuevo.IdArticulo = img.IdArticulo;
                nuevo.ImagenUrl = aux;
                catalogo.AgregarImagen(nuevo);
            }
        }

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
