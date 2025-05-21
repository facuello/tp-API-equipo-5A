using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BaseDeDatos;
using Categorias;

namespace Articulos
{
    public class CatalogoArticulo
    {
        private List<Articulo> articulos = null;
        private Catalogo datos = null;

        public List<Articulo> listar(){
            articulos = new List<Articulo>();
            datos = new Catalogo(); 
            datos.Conectar();
            datos.Consultar("select A.ID, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, A.Precio, I.ImagenUrl from ARTICULOS A left join MARCAS M on A.IDMarca = M.ID left join CATEGORIAS C on A.IdCategoria = C.Id left join IMAGENES I on A.Id = I.IdArticulo\r\n");
            datos.Leer();

            try
            {
                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Marc = new Marca();
                    aux.Categ = new Categoria();

                    aux.ID = datos.validarNullInt(datos.Lector["ID"]);
                    aux.Codigo = datos.validarNullString(datos.Lector["Codigo"]);
                    aux.Nombre = datos.validarNullString(datos.Lector["Nombre"]);
                    aux.Descripcion = datos.validarNullString(datos.Lector["Descripcion"]);
                    aux.Marc.Descripcion = datos.validarNullString(datos.Lector["Marca"]);
                    aux.Categ.Descripcion = datos.validarNullString(datos.Lector["Categoria"]);
                    aux.Imagen = datos.validarNullString(datos.Lector["ImagenUrl"]);
                    aux.Precio = datos.validarNullDecimal(datos.Lector["Precio"]);

                    articulos.Add(aux);
                }
            }
            catch (Exception er)
            {

               throw er;
            }
            datos.Cerrar();
            return articulos;
        }
        public void agregarArticulo(Articulo aux) { 
            datos = new Catalogo();
            try
            {
                datos.Conectar();
                datos.Consultar("insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) values (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @IdPrecio)");
                datos.setearParametro("@Codigo", aux.Codigo);
                datos.setearParametro("@Nombre", aux.Nombre);
                datos.setearParametro("@Descripcion", aux.Descripcion);
                datos.setearParametro("@IdMarca", aux.Marc.Id);
                datos.setearParametro("@IDCategoria", aux.Categ.Id);
                datos.setearParametro("@IdPrecio", aux.Precio);
                datos.EjecutarNonQuery();
                datos.Cerrar();

                datos.Conectar();
                List<Articulo> art = listar();
                datos.Consultar("insert into Imagenes (IdArticulo, ImagenUrl) values (@IdArticulo, @ImagenUrl)");
                datos.setearParametro("@IdArticulo", art[art.Count - 1].ID);
                datos.setearParametro("@ImagenUrl", aux.Imagen);
                datos.EjecutarNonQuery();
            }
            catch (Exception er)
            {

                throw er;
            }
            finally {
                datos.Cerrar();
            }
        }
        public void EliminarArticulo(int id)
        {
            datos = new Catalogo();
            try
            {
                Catalogo datos = new Catalogo();
                datos.Conectar();
                datos.Consultar("DELETE FROM ARTICULOS WHERE ID = @IdArticulo");
                datos.setearParametro("IdArticulo", id);
                datos.EjecutarNonQuery();

                datos.Cerrar();

                datos.Conectar();
                datos.Consultar("DELETE FROM IMAGENES WHERE IdArticulo = @IdArticulo");
                datos.setearParametro("IdArticulo", id);
                datos.EjecutarNonQuery();

                datos.Cerrar();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void modificar(Articulo art) {
            datos = new Catalogo();
            try
            {
                datos.Conectar();
                datos.Consultar("update ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, Precio = @Precio where Id = @ID");
                datos.setearParametro("@Codigo",art.Codigo);
                datos.setearParametro("@Nombre", art.Nombre);
                datos.setearParametro("@Descripcion", art.Descripcion);
                datos.setearParametro("@IdMarca", art.Marc.Id);
                datos.setearParametro("@IdCategoria", art.Categ.Id);
                datos.setearParametro("@Precio", art.Precio);
                datos.setearParametro("@ID",art.ID);
                datos.EjecutarNonQuery();
                datos.Cerrar();

                datos.Conectar();
                datos.Consultar("update IMAGENES set ImagenUrl = @ImagenUrl where IdArticulo = @IdArticulo");
                datos.setearParametro("@IdArticulo",art.ID);
                datos.setearParametro("@ImagenUrl",art.Imagen);
                datos.EjecutarNonQuery();
                datos.Cerrar();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<Articulo> buscarArticulo(string campo, string criterio, string valor) {
            datos = new Catalogo();
            articulos = new List<Articulo>();
            try
            {
                datos.Conectar();
                string consulta = "select A.ID, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, A.Precio, I.ImagenUrl from ARTICULOS A left join MARCAS M on A.IDMarca = M.ID left join CATEGORIAS C on A.IdCategoria = C.Id left join IMAGENES I on A.Id = I.IdArticulo where ";
                switch (campo)
                {
                    case "ID":
                        switch (criterio)
                        {
                            case "Mayor a":
                                consulta += "A.ID > " + valor;
                                break;
                            case "Menor a":
                                consulta += "A.ID < " + valor;
                                break;
                            case "Igual a":
                                consulta += "A.ID = " + valor;
                                break;
                        }
                        break;
                    case "Codigo":
                        switch (criterio)
                        {
                            case "Empieza con":
                                consulta += "A.Codigo like '" + valor + "%'";
                                break;
                            case "Termina con":
                                consulta += "A.Codigo like '%" + valor + "'";
                                break;
                            case "Contiene":
                                consulta += "A.Codigo like '%" + valor + "%'";
                                break;
                        }
                        break;
                    case "Nombre":
                        switch (criterio)
                        {
                            case "Empieza con":
                                consulta += "A.Nombre like '" + valor + "%'";
                                break;
                            case "Termina con":
                                consulta += "A.Nombre like '%" + valor + "'";
                                break;
                            case "Contiene":
                                consulta += "A.Nombre like '%" + valor + "%'";
                                break;
                        }
                        break;
                    case "Descripcion":
                        switch (criterio)
                        {
                            case "Empieza con":
                                consulta += "A.Descripcion like '" + valor + "%'";
                                break;
                            case "Termina con":
                                consulta += "A.Descripcion like '%" + valor + "'";
                                break;
                            case "Contiene":
                                consulta += "A.Descripcion like '%" + valor + "%'";
                                break;
                        }
                        break;
                    case "Marca":
                        switch (criterio)
                        {
                            case "Empieza con":
                                consulta += "M.Descripcion like '" + valor + "%'";
                                break;
                            case "Termina con":
                                consulta += "M.Descripcion like '%" + valor + "'";
                                break;
                            case "Contiene":
                                consulta += "M.Descripcion like '%" + valor + "%'";
                                break;
                        }
                        break;
                    case "Categoria":
                        switch (criterio)
                        {
                            case "Empieza con":
                                consulta += "C.Descripcion like '" + valor + "%'";
                                break;
                            case "Termina con":
                                consulta += "C.Descripcion like '%" + valor + "'";
                                break;
                            case "Contiene":
                                consulta += "C.Descripcion like '%" + valor + "%'";
                                break;
                        }
                        break;
                    case "Precio":
                        switch (criterio)
                        {
                            case "Mayor a":
                                consulta += "A.Precio > " + valor;
                                break;
                            case "Menor a":
                                consulta += "A.Precio < " + valor;
                                break;
                            case "Igual a":
                                consulta += "A.precio = " + valor;
                                break;
                        }
                        break;
                    default:
                        break;
                }

                datos.Consultar(consulta);
                datos.Leer();
                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Marc = new Marca();
                    aux.Categ = new Categoria();

                    aux.ID = datos.validarNullInt(datos.Lector["ID"]);
                    aux.Codigo = datos.validarNullString(datos.Lector["Codigo"]);
                    aux.Nombre = datos.validarNullString(datos.Lector["Nombre"]);
                    aux.Descripcion = datos.validarNullString(datos.Lector["Descripcion"]);
                    aux.Marc.Descripcion = datos.validarNullString(datos.Lector["Marca"]);
                    aux.Categ.Descripcion = datos.validarNullString(datos.Lector["Categoria"]);
                    aux.Imagen = datos.validarNullString(datos.Lector["ImagenUrl"]);
                    aux.Precio = datos.validarNullDecimal(datos.Lector["Precio"]);

                    articulos.Add(aux);
                }

                return articulos;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
