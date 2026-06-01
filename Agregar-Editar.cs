using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gestionDeArticulos
{
    public partial class Agregar_Editar : Form
    {
        private NegocioArticulo neArt;
        private articulos articulo = null;

        // Constructor para agregar
        public Agregar_Editar()
        {
            InitializeComponent();
            cargarCombos();
        }

        // Constructor para editar
        public Agregar_Editar(articulos articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            cargarCombos();
            precargarDatos();
        }

        private void cargarCombos()
        {
            neArt = new NegocioArticulo();
            cbMarca.DataSource = neArt.rellenarCbMarca();
            cbMarca.ValueMember = "Id";
            cbMarca.DisplayMember = "Descripcion";

            cbCategoria.DataSource = neArt.rellenarCbCategoria();
            cbCategoria.ValueMember = "Id";
            cbCategoria.DisplayMember = "Descripcion";
        }

        private void precargarDatos()
        {
            if (articulo != null)
            {
                txtCodigo.Text = articulo.codigoArticulo;
                txtNombre.Text = articulo.nombreArticulo;
                txtDescripcion.Text = articulo.descripcionArticulo;
                txtPrecio.Text = articulo.precioArticulo.ToString();

                if (articulo.idMarca != null)
                    cbMarca.SelectedValue = articulo.idMarca.IdMarcas;

                if (articulo.idCategoria != null)
                    cbCategoria.SelectedValue = articulo.idCategoria.idCategoria;
            }
        }



        // BOTÓN GUARDAR
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            NegocioArticulo artNegocio = new NegocioArticulo();
            if (!verificarVacio())
            {
                MessageBox.Show("Tiene que completar todos los campos");
                return;
            }

                
            articulo.codigoArticulo = txtCodigo.Text;
            articulo.nombreArticulo = txtNombre.Text;
            articulo.descripcionArticulo = txtDescripcion.Text;
            articulo.idMarca= new marcas();
            articulo.idCategoria=new categorias();
            articulo.idMarca.IdMarcas = int.Parse(cbMarca.SelectedValue.ToString());
            articulo.idCategoria.idCategoria = int.Parse(cbCategoria.SelectedValue.ToString());
            articulo.precioArticulo = float.Parse(txtPrecio.Text);
            if (articulo == null)
            {
                artNegocio.agregarArticulo(articulo);

            }
            else
                artNegocio.editarArticulo(articulo);

            this.Close();
        }


        private bool verificarVacio()
        {
            return !(string.IsNullOrWhiteSpace(txtNombre.Text) ||
                     string.IsNullOrWhiteSpace(txtDescripcion.Text) ||
                     string.IsNullOrWhiteSpace(txtCodigo.Text) ||
                     string.IsNullOrEmpty(txtPrecio.Text) ||
                     cbMarca.SelectedIndex == -1 ||
                     cbCategoria.SelectedIndex == -1);
        }

        private void vaciarTxtBox()
        {
            txtNombre.Clear();
            txtDescripcion.Clear();
            txtCodigo.Clear();
            txtPrecio.Clear();
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && 
                e.KeyChar !='.' && e.KeyChar != ',')
            { 
                e.Handled = true;
            }

            if(e.KeyChar == '.' || e.KeyChar == ',')
            {
                if(txtPrecio.Text.Contains(".")|| txtPrecio.Text.Contains(","))
                {
                    e.Handled= true;
                }
            }
            
        }
    }
}
