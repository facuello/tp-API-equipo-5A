using api_productos.Models;
using Articulos;
using BaseDeDatos;
using Categorias;
using Microsoft.Ajax.Utilities;
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
            bool confirmar = false;
            CatalogoArticulo articulo = new CatalogoArticulo();
            List<Articulo> lista = articulo.listar();

            foreach (Articulo aux in articulo.listar())
            {
                if (aux.ID == id)
                {
                    confirmar = true;
                }
            }

            if (!confirmar)
            {
                return null;
            }

            return lista.Find(x => x.ID == id);
        }

        // POST: api/Articulo

        [HttpPost]
        public IHttpActionResult Post([FromBody] ArticuloDto art)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(art.Codigo))
                {
                    return BadRequest("El código del artículo no puede estar vacío.");
                }
                else if (string.IsNullOrWhiteSpace(art.Nombre))
                {
                    return BadRequest("El nombre del artículo no puede estar vacío.");
                }
                else if (art.Precio <= 0)
                {
                    return BadRequest("El precio del artículo no puede estar vacío.");
                }
                else if (art.IdMarca <= 0)
                {
                    return BadRequest("el ID de marca debe ser mayor a 0.");
                }
                else if (art.IdCategoria <= 0)
                {
                    return BadRequest("El ID de categoria debe ser mayor a 0.");
                }
                CatalogoArticulo catalogo = new CatalogoArticulo();
                Articulo nuevo = new Articulo();
                {
                    nuevo.Codigo = art.Codigo;
                    nuevo.Nombre = art.Nombre;
                    nuevo.Descripcion = art.Descripcion;
                    nuevo.Marc = new Marca { Id = art.IdMarca };
                    nuevo.Categ = new Categoria { Id = art.IdCategoria };
                    nuevo.Imagen = art.Imagen;
                    nuevo.Precio = art.Precio;
                }

                catalogo.agregarArticulo(nuevo);
                ///CreatedAtRoute se usa para devolver una respuesta HTTP
                ///DefaultApi nombre de la ruta
                ///new { id = nuevo.ID} crea el nuevo objeto que se visualiza en postman
                return CreatedAtRoute("DefaultApi", new { id = nuevo.ID }, nuevo);

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        //public void Post([FromBody] Articulo value)
        //{
        //}

        // PUT: api/Articulo/5
        public IHttpActionResult Put(int id, [FromBody] ArticuloDto art)
        {
            CatalogoArticulo var = new CatalogoArticulo();
            bool confirmar = false;
            try
            {
                foreach (Articulo aux in var.listar())
                {
                    if (aux.ID == id)
                    {
                        confirmar = true;
                    }
                }

                if (!confirmar){
                    return BadRequest("El Id del artículo no existe.");
                }else if (string.IsNullOrWhiteSpace(art.Codigo))
                {
                    return BadRequest("El código del artículo no puede estar vacío.");
                }
                else if (string.IsNullOrWhiteSpace(art.Nombre))
                {
                    return BadRequest("El nombre del artículo no puede estar vacío.");
                }
                else if (art.Precio <= 0)
                {
                    return BadRequest("El precio del artículo no puede estar vacío.");
                }
                else if (art.IdMarca <= 0)
                {
                    return BadRequest("el ID de marca debe ser mayor a 0.");
                }
                else if (art.IdCategoria <= 0)
                {
                    return BadRequest("El ID de categoria debe ser mayor a 0.");
                }

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

                return CreatedAtRoute("DefaultApi", new { id = nuevo.ID }, nuevo);
            }
            catch (Exception er)
            {
                return InternalServerError(er);
            }

        }

        //Agregar Imagenes: api/articulo/PostImg

        [HttpPost]
        [Route("api/articulo/PostImg")]
        public IHttpActionResult PostImg([FromBody] ImagenDto img)
        {
            CatalogoArticulo var = new CatalogoArticulo();
            bool confirmar = false;
            try
            {
                foreach (Articulo art in var.listar())
                {
                    if (art.ID == img.IdArticulo)
                    {
                        confirmar = true;
                    }
                }

                if (!confirmar){
                    return BadRequest("El Id del artículo no existe.");
                } else if (img.ImagenUrl.Any(x => string.IsNullOrEmpty(x))) {
                    return BadRequest("No se pueden agregar imagenes vacias.");
                }

                CatalogoImagen catalogo = new CatalogoImagen();
                Imagen nuevo = new Imagen();
                List<string> retorno = new List<string>();
                foreach (string aux in img.ImagenUrl)
                {
                    nuevo.IdArticulo = img.IdArticulo;
                    nuevo.ImagenUrl = aux;
                    catalogo.AgregarImagen(nuevo);
                    retorno.Add(nuevo.ImagenUrl);
                }
                return Ok("Agregado exitosamente.");
            }
            catch (Exception er)
            {
                return InternalServerError(er);
            }
        }

        // DELETE: api/Articulo/5
        public HttpResponseMessage Delete(int id)
        {
            CatalogoArticulo var = new CatalogoArticulo();
            bool confirmar = false;
            try
            {
                foreach (Articulo art in var.listar())
                {
                    if (art.ID == id)
                    {
                        confirmar = true;
                    }
                }
                if (!confirmar) {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "El ID articulo no existe en la base de datos.");
                }
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
