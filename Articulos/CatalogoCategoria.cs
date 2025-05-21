using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseDeDatos;
using Categorias;

namespace Articulos
{
    public class CatalogoCategoria
    {
        private List<Categoria> categorias = new List<Categoria>();
        private Catalogo datos = new Catalogo();
        public List<Categoria> Listar() { 
            try
            {
                datos.Conectar();
                datos.Consultar("Select Id, Descripcion from CATEGORIAS");
                datos.Leer();
                while (datos.Lector.Read()) { 
                    Categoria aux = new Categoria();
                    aux.Id = datos.validarNullInt(datos.Lector["Id"]);
                    aux.Descripcion = datos.validarNullString(datos.Lector["Descripcion"]);

                    categorias.Add(aux);
                }
            }
            catch (Exception er)
            {

                throw er;
            }
            datos.Cerrar();
            return categorias;
        }
        public void agregarCategoria(string valor)
        {
            if (!(validarRepetido(valor)))
                return;
            datos.Conectar();
            datos.Consultar("Insert into Categorias (Descripcion) values (@Descripcion)");
            datos.setearParametro("@Descripcion", valor);
            datos.EjecutarNonQuery();
            datos.Cerrar();

        }
        public bool validarRepetido(string valor) {
            List<Categoria> obj = Listar();
            foreach (Categoria lis in obj) {
                if (valor.ToUpper() == lis.Descripcion.ToUpper()) { 
                    return false;
                }
            }
            return true;
        }

    }
}
