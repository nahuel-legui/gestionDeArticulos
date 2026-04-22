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

            string ruta = "Server=localhost\\SQLEXPRESS;database=CATALOGO_P3_DB;integrated security=true";
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

            string ruta = "Server=localhost\\SQLEXPRESS;database=CATALOGO_P3_DB;integrated security=true";
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




    }
}
