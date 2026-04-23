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
        public Agregar_Editar()
        {
            InitializeComponent();
            DataTable dtMarca = rellenarCbMarca();
            DataTable dtCategoria= rellenarCbCategoria();
            if (dtMarca.Rows.Count > 0) {

                cbMarca.DataSource = dtMarca;
                cbMarca.ValueMember = "Id";
                cbMarca.DisplayMember = "Descripcion";
                
            }
            if (dtCategoria.Rows.Count > 0) {
            
                cbCategoria.DataSource = dtCategoria;
                cbCategoria.ValueMember="Id";
                cbCategoria.DisplayMember = "Descripcion";
            }
        }

       public DataTable rellenarCbCategoria()
        {

            
            string consulta = "select id,Descripcion from CATEGORIAS";
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ruta)) 
            {
                try
                {
                    SqlDataAdapter Da = new SqlDataAdapter(consulta,ruta);
                    Da.Fill(dt);
                }
                catch (Exception ex)
                {

                    throw ex;
                }


            }
            return dt;
        }

        public DataTable rellenarCbMarca()
        {

            
            string consulta = "select id,Descripcion from MARCAS";
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ruta))
            {
                try
                {
                    SqlDataAdapter Da = new SqlDataAdapter(consulta, ruta);
                    Da.Fill(dt);
                }
                catch (Exception ex)
                {

                    throw ex;
                }


            }
            return dt;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!verificarVacio())
            {
                MessageBox.Show("Tiene que completar todos los campos");
                return;
            }
            articulos a1 = new articulos();
            a1.nombreArticulo=txtNombre.Text.Trim();
            a1.descripcionArticulo=txtDescripcion.Text.Trim();
            a1.codigoArticulo=txtCodigo.Text.Trim();
            a1.precioArticulo=float.Parse(txtPrecio.Text.Trim());
            a1.idMarca = new marcas(int.Parse(cbMarca.SelectedValue.ToString()));
            a1.idCategoria = new categorias(int.Parse(cbCategoria.SelectedValue.ToString()));

            string consulta = "insert into ARTICULOS values (@codigo, @nombre, @descripcion, @idMarca, @idCategoria, @Precio)";

            using (SqlConnection con = new SqlConnection(ruta))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(consulta, con);
                    cmd.Parameters.AddWithValue("@codigo", a1.codigoArticulo);
                    cmd.Parameters.AddWithValue("@nombre", a1.nombreArticulo);
                    cmd.Parameters.AddWithValue("@descripcion",a1.descripcionArticulo);
                    cmd.Parameters.AddWithValue("@idMarca", a1.idMarca.IdMarcas);
                    cmd.Parameters.AddWithValue("@idCategoria", a1.idCategoria.idCategoria);
                    cmd.Parameters.AddWithValue("@precio", a1.precioArticulo);

                    MessageBox.Show(a1.nombreArticulo);
                    int fila =cmd.ExecuteNonQuery();
                    if (fila > 0)
                    {
                        MessageBox.Show("Articulo agregado correctamente");

                    }
                    else
                    {
                        MessageBox.Show("El articulo no se pudo agregar correctamente");
                    }
                    vaciarTxtBox();
                    con.Close();

                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private bool verificarVacio()
        {

            if (string.IsNullOrWhiteSpace(txtNombre.Text) || 
                string.IsNullOrWhiteSpace(txtDescripcion.Text) ||
                string.IsNullOrWhiteSpace (txtCodigo.Text) ||
                string.IsNullOrEmpty(txtPrecio.Text) || cbMarca.SelectedIndex==-1 ||cbCategoria.SelectedIndex==-1)
            {
                return false;
            }
            return true;
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
