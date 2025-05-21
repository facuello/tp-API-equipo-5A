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
using System.IO;

namespace TpWinform
{
    public partial class frmPrincipal : Form
    {

        private List<Articulo> articulos;
        private CatalogoArticulo catalogo;
        public frmPrincipal()
        {
            InitializeComponent();
        }
        private void cargarDatos()
        {
            catalogo = new CatalogoArticulo();
            try
            {
                articulos = catalogo.listar();
                dgvDatos.DataSource = articulos;
                ocultarColumnas();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        public void cargarImagen(string imagen) {
            try
            {
                pbxImg.Load(imagen);
            }
            catch (Exception)
            {
                pbxImg.Load("https://img.freepik.com/vector-premium/imagen-no-es-conjunto-iconos-disponibles-simbolo-vectorial-stock-fotos-faltante-defecto-estilo-relleno-delineado-negro-signo-no-encontro-imagen_268104-6708.jpg");
            }
        }
        public void ocultarColumnas() {
            dgvDatos.Columns["ID"].Visible = false;
            dgvDatos.Columns["Imagen"].Visible = false;
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregar frm = new frmAgregar();
            frm.ShowDialog();
            cargarDatos();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.CurrentRow != null)
            {
                Articulo seleccion = new Articulo();
                seleccion = (Articulo)dgvDatos.CurrentRow.DataBoundItem;
                frmAgregar frm = new frmAgregar(seleccion);
                frm.Text = "Modificar";
                frm.ShowDialog();
                cargarDatos();
            }
            else 
            {
                MessageBox.Show(("No hay ningun elemnto seleccionado."));
            }
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            cargarDatos();
            cmbCampo.Items.Add("ID");
            cmbCampo.Items.Add("Codigo");
            cmbCampo.Items.Add("Nombre");
            cmbCampo.Items.Add("Descripcion");
            cmbCampo.Items.Add("Marca");
            cmbCampo.Items.Add("Categoria");
            cmbCampo.Items.Add("Precio");
        }
        private void dgvDatos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDatos.CurrentRow != null)
            {
                Articulo aux = (Articulo)dgvDatos.CurrentRow.DataBoundItem;
                cargarImagen(aux.Imagen);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.CurrentRow != null)
            {
                catalogo = new CatalogoArticulo();
                Articulo seleccionado;
                try
                {
                    seleccionado = (Articulo)dgvDatos.CurrentRow.DataBoundItem;
                    catalogo.EliminarArticulo(seleccionado.ID);
                    cargarDatos();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                MessageBox.Show(("No hay ningun elemnto seleccionado."));
            }
            
            
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDatos.CurrentRow != null) {
                frmDetalle nuevo = new frmDetalle((Articulo)dgvDatos.CurrentRow.DataBoundItem);
                nuevo.ShowDialog();
            }
        }

        private void cmbCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCampo.Text == "ID" || cmbCampo.Text == "Precio") {
                cmbCriterio.Items.Clear();
                cmbCriterio.Items.Add("Mayor a");
                cmbCriterio.Items.Add("Menor a");
                cmbCriterio.Items.Add("Igual a");
            } else {
                cmbCriterio.Items.Clear();
                cmbCriterio.Items.Add("Empieza con");
                cmbCriterio.Items.Add("Termina con");
                cmbCriterio.Items.Add("Contiene");
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            catalogo = new CatalogoArticulo();
            try
            {
                dgvDatos.DataSource = catalogo.buscarArticulo(cmbCampo.Text,cmbCriterio.Text,txtBuscar.Text);
            }
            catch (Exception)
            {

                MessageBox.Show("Elemento ingresado no valido.");
            }
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            cargarDatos();
        }
    }
}
