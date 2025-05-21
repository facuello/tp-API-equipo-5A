using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BaseDeDatos;
using Articulos;
using Categorias;
using System.IO;
using System.Configuration;

namespace TpWinform
{
    public partial class frmAgregar : Form
    {
        private CatalogoArticulo datos = null;
        private Articulo nuevo = null;
        private CatalogoMarca datMarca = null;
        private CatalogoCategoria datCateg = null;
        private OpenFileDialog archivo = null;
        private bool modificar = false;
        public frmAgregar()
        {
            InitializeComponent();
        }
        public frmAgregar(Articulo art)
        {
            InitializeComponent();
            btnAgregar.Text = "Modificar";
            nuevo = art;
            modificar = true;
        }

        private void txtImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtImagen.Text);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!modificar) {
                    nuevo = new Articulo();
                }
                datos = new CatalogoArticulo();
                datMarca = new CatalogoMarca();
                datCateg = new CatalogoCategoria();
                if ((datCateg.validarRepetido(cboCategoria.Text)) && (datMarca.validarRepetido(cboMarca.Text)))
                {
                    MessageBox.Show("Agregue la marca y/o la categoria por favor (+)");
                    return;
                }

                if (validarVacio(txtCodigo) || validarVacio(txtNombre) || validarVacio(txtDescripcion) || validarVacio(txtPrecio))
                {
                    MessageBox.Show("Por favor llenar los campos faltantes.");
                    return;
                }

                nuevo.Codigo = txtCodigo.Text;
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.Marc = (Marca)cboMarca.SelectedItem;
                nuevo.Categ = (Categoria)cboCategoria.SelectedItem;
                nuevo.Imagen = txtImagen.Text;
                nuevo.Precio = Decimal.Parse(txtPrecio.Text);


                if (nuevo.ID != 0)
                {
                    datos.modificar(nuevo);
                    MessageBox.Show("Modificado Correctamente.");
                }
                else
                {
                    datos.agregarArticulo(nuevo);
                    MessageBox.Show("Agregado Correctamente.");
                }

                string path = @"C:\TpWinforms-Imagenes\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (archivo != null && !(txtImagen.Text.ToLower().Contains("http")) && !(File.Exists(path + archivo.SafeFileName)))
                    File.Copy(archivo.FileName, ConfigurationManager.AppSettings["images-folder"] + archivo.SafeFileName);


                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Dato ingresado no valido.");
            }
        }

        private void cargarImagen(string URL) {
            try
            {
                pboImgAgr.Load(URL);
            }
            catch (Exception)
            {

                pboImgAgr.Load("https://img.freepik.com/vector-premium/imagen-no-es-conjunto-iconos-disponibles-simbolo-vectorial-stock-fotos-faltante-defecto-estilo-relleno-delineado-negro-signo-no-encontro-imagen_268104-6708.jpg");
            }
            
        }

        private void frmAgregar_Load(object sender, EventArgs e)
        {
            try
            {
                CargarDatosMarca();
                CargarDatosCategoria();

                if (nuevo != null) { 
                    txtCodigo.Text = nuevo.Codigo.ToString();
                    txtNombre.Text = nuevo.Nombre;
                    txtDescripcion.Text = nuevo.Descripcion;
                    cboMarca.Text = nuevo.Marc.Descripcion;
                    cboCategoria.Text = nuevo.Categ.Descripcion;
                    txtImagen.Text = nuevo.Imagen;
                    txtPrecio.Text = nuevo.Precio.ToString();
                    cargarImagen(nuevo.Imagen);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnAgrMarca_Click(object sender, EventArgs e)
        {
            datMarca = new CatalogoMarca();
            string valor = cboMarca.Text;
            if (validarVacio(cboMarca))
            {
                MessageBox.Show("Faltan datos");
                return;
            }
            if (datMarca.validarRepetido(valor)) {
                datMarca.agregarMarca(valor);
                CargarDatosMarca();
                cboMarca.Text = valor;
            } else if(!(datMarca.validarRepetido(valor))){ 
                MessageBox.Show("Ya se encuentra en la base de datos");
                CargarDatosMarca();
            }
        }
        private void CargarDatosCategoria()
        {
            CatalogoCategoria categ = new CatalogoCategoria();
            try
            {
                cboCategoria.DataSource = categ.Listar();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void CargarDatosMarca()
        {
            CatalogoMarca marc = new CatalogoMarca();
            try
            {
                cboMarca.DataSource = marc.Listar();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnAgrCateg_Click(object sender, EventArgs e)
        {
            datCateg = new CatalogoCategoria();
            string valor = cboCategoria.Text;
            if (validarVacio(cboCategoria))
            {
                MessageBox.Show("Faltan datos");
                return;
            }
            if (datCateg.validarRepetido(valor)) { 
                datCateg.agregarCategoria(valor);
                CargarDatosCategoria();
                cboCategoria.Text = valor;
            }
            else if (!(datCateg.validarRepetido(valor)))
            {
                MessageBox.Show("Ya se encuentra en la base de datos");
                CargarDatosMarca();
            }
        }

        private void btnImagen_Click(object sender, EventArgs e)
        {
            archivo = new OpenFileDialog();
            archivo.Filter = "jpg|*jpg;|png|*png;|jpeg|*jpeg";
            if (archivo.ShowDialog() == DialogResult.OK) { 
                txtImagen.Text = archivo.FileName;
                cargarImagen(archivo.FileName);
            }
        }

        private bool validarVacio(TextBox txt) {
            if (string.IsNullOrEmpty(txt.Text)) {
                return true;
            }
            return false;
        }
        private bool validarVacio(ComboBox txt)
        {
            if (string.IsNullOrEmpty(txt.Text))
            {
                return true;
            }
            return false;
        }
    }
}
