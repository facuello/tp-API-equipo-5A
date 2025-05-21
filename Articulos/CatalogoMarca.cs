using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseDeDatos;

namespace Articulos
{
    public class CatalogoMarca
    {
        private List<Marca> marcas = new List<Marca>();
        private Catalogo datos = new Catalogo();
        public List<Marca> Listar() {
            Marca aux;
            try
            {
                datos.Conectar();
                datos.Consultar("Select ID, Descripcion from Marcas");
                datos.Leer();
                while(datos.Lector.Read()){ 
                    aux = new Marca();
                    aux.Id = datos.validarNullInt(datos.Lector["Id"]);
                    aux.Descripcion = datos.validarNullString(datos.Lector["Descripcion"]);
                    marcas.Add(aux);
                }
            }
            catch (Exception er)
            {

                throw er;
            }
            
            return marcas;
        }
        public void agregarMarca(string valor) {
            if (!(validarRepetido(valor)))
                return;
            datos.Conectar();
            datos.Consultar("Insert into Marcas (Descripcion) values (@Descripcion)");
            datos.setearParametro("@Descripcion",valor);
            datos.EjecutarNonQuery();
            datos.Cerrar();
            
        }

        public bool validarRepetido(string valor) {
            List<Marca> obj = Listar();
            foreach (Marca lis in obj) {
                if (lis.Descripcion.ToUpper() == valor.ToUpper()) {
                    return false;
                }
            }
            return true;
        }
    }
}
