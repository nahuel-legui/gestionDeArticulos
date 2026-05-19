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
        private string ruta = "Server=localhost\\SQLEXPRESS;database=CATALOGO_P3_DB;integrated security=true";
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
            cbMarca.DataSource = rellenarCbMarca();
            cbMarca.ValueMember = "Id";
            cbMarca.DisplayMember = "Descripcion";

            cbCategoria.DataSource = rellenarCbCategoria();
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

        public DataTable rellenarCbCategoria()
        {
            string consulta = "select id,Descripcion from CATEGORIAS";
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ruta))
            {
                SqlDataAdapter Da = new SqlDataAdapter(consulta, ruta);
                Da.Fill(dt);
            }
            return dt;
        }

        public DataTable rellenarCbMarca()
        {
            string consulta = "select id,Descripcion from MARCAS";
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ruta))
            {
                SqlDataAdapter Da = new SqlDataAdapter(consulta, ruta);
                Da.Fill(dt);
            }
            return dt;
        }

        // BOTÓN GUARDAR
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!verificarVacio())
            {
                MessageBox.Show("Tiene que completar todos los campos");
                return;
            }

            if (articulo == null)
                agregarArticulo();
            else
                editarArticulo();

            this.Close();
        }

        private void agregarArticulo()
        {
            string consulta = "INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) " +
                              "VALUES (@codigo, @nombre, @descripcion, @idMarca, @idCategoria, @precio)";

            using (SqlConnection con = new SqlConnection(ruta))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(consulta, con);
                cmd.Parameters.AddWithValue("@codigo", txtCodigo.Text.Trim());
                cmd.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                cmd.Parameters.AddWithValue("@descripcion", txtDescripcion.Text.Trim());
                cmd.Parameters.AddWithValue("@idMarca", cbMarca.SelectedValue);
                cmd.Parameters.AddWithValue("@idCategoria", cbCategoria.SelectedValue);
                cmd.Parameters.AddWithValue("@precio", float.Parse(txtPrecio.Text.Trim()));

                int fila = cmd.ExecuteNonQuery();
                MessageBox.Show(fila > 0 ? "Artículo agregado correctamente" : "No se pudo agregar el artículo");
            }
        }

        private void editarArticulo()
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
    }
}
