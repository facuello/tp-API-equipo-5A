using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseDeDatos;

namespace Articulos
{
    public class CatalogoImagen
    {
        private List<Imagen> imagenes = null;
        private Catalogo datos = null;

        public List<Imagen> Listar() { 
            imagenes = new List<Imagen>();
            datos = new Catalogo();
            datos.Conectar();
            datos.Consultar("Select Id, IdArticulo, ImagenUrl from IMAGENES");
            datos.Leer();

            try
            {
                while (datos.Lector.Read()) { 
                    Imagen aux = new Imagen();

                    aux.ID = datos.validarNullInt(datos.Lector["Id"]);
                    aux.IdArticulo = datos.validarNullInt(datos.Lector["IdArticulo"]);
                    aux.ImagenUrl = datos.validarNullString(datos.Lector["ImagenUrl"]);

                    imagenes.Add(aux);
                }
            }
            catch (Exception er)
            {

                throw er;
            }
            finally
            {
                datos.Cerrar();
            }

            return imagenes;
        }
        public void AgregarImagen(Imagen img) {
            datos = new Catalogo();

            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO IMAGENES (IdArticulo, ImagenUrl) VALUES (@IdArticulo, @ImagenUrl)");
                datos.setearParametro("@IdArticulo",img.IdArticulo);
                datos.setearParametro("@ImagenUrl",img.ImagenUrl);
                datos.EjecutarNonQuery();
            }
            catch (Exception er)
            {
                throw er;
            }
            finally 
            {
                datos.Cerrar();
            }
        }

    }
}
