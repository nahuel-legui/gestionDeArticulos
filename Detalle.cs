using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gestionDeArticulos
{
    public partial class Detalle : Form
    {
        private articulos articulo = null;
        private NegocioArticulo neArt;

        public Detalle(articulos articulo)
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
            txtCodigo.ReadOnly = true;
            txtNombre.ReadOnly = true;
            txtDescripcion.ReadOnly = true;
            txtPrecio.ReadOnly = true;
            cbMarca.Enabled = false;
            cbCategoria.Enabled = false;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}

