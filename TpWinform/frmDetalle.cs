using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Articulos;

namespace TpWinform
{
    public partial class frmDetalle : Form
    {
        public frmDetalle()
        {
            InitializeComponent();
        }
        public frmDetalle(Articulo art)
        {
            InitializeComponent();
            txtId.Text = art.ID.ToString();
            txtCodigo.Text = art.Codigo;
            txtNombre.Text = art.Nombre;
            txtDescripcion.Text = art.Descripcion;
            txtMarca.Text = art.Marc.ToString();
            txtCategoria.Text = art.Categ.ToString();
            txtPrecio.Text = art.Precio.ToString();
            txtImagenUrl.Text = art.Imagen;
            cargarImagen(art.Imagen);
        }
        private void cargarImagen(string URL)
        {
            try
            {
                pbxImg.Load(URL);
            }
            catch (Exception)
            {

                pbxImg.Load("https://img.freepik.com/vector-premium/imagen-no-es-conjunto-iconos-disponibles-simbolo-vectorial-stock-fotos-faltante-defecto-estilo-relleno-delineado-negro-signo-no-encontro-imagen_268104-6708.jpg");
            }

        }
    }
}
