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

            if (articulo == null)
            {
                articulos obj= new articulos();
                obj.codigoArticulo = txtCodigo.Text;
                obj.nombreArticulo = txtNombre.Text;
                obj.descripcionArticulo = txtDescripcion.Text;
                obj.idMarca.IdMarcas=int.Parse(cbMarca.SelectedValue.ToString());
                obj.idCategoria.idCategoria=int.Parse(cbCategoria.SelectedValue.ToString());
                obj.precioArticulo = float.Parse(txtPrecio.Text);
                artNegocio.agregarArticulo(obj);

            }
            else
                //editarArticulo();

            this.Close();
        }

        

        /*private void editarArticulo()
        {
            string consulta = "UPDATE ARTICULOS SET Codigo=@codigo, Nombre=@nombre, Descripcion=@descripcion, " +
                              "IdMarca=@idMarca, IdCategoria=@idCategoria, Precio=@precio WHERE Id=@id";

            using (SqlConnection con = new SqlConnection(ruta))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(consulta, con);
                cmd.Parameters.AddWithValue("@id", articulo.idArticulos); // tu clase tiene idArticulos, lo mapeamos a la columna Id
                cmd.Parameters.AddWithValue("@codigo", txtCodigo.Text.Trim());
                cmd.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                cmd.Parameters.AddWithValue("@descripcion", txtDescripcion.Text.Trim());
                cmd.Parameters.AddWithValue("@idMarca", cbMarca.SelectedValue);
                cmd.Parameters.AddWithValue("@idCategoria", cbCategoria.SelectedValue);
                cmd.Parameters.AddWithValue("@precio", float.Parse(txtPrecio.Text.Trim()));

                int fila = cmd.ExecuteNonQuery();
                MessageBox.Show(fila > 0 ? "Artículo editado correctamente" : "No se pudo editar el artículo");
            }
        }*/



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
    }
}
