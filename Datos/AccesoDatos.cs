using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestionDeArticulos.Datos
{

    public class AccesoDatos
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private string rutaConexion= "Server=localhost\\SQLEXPRESS;database=CATALOGO_P3_DB;integrated security=true";
        private SqlDataReader reader;

        public AccesoDatos()
        {
            con= new SqlConnection(rutaConexion);
            cmd = new SqlCommand();

        }
        
        public void setearConsulta(string consulta)
        {
            cmd.CommandType= System.Data.CommandType.Text;
            cmd.CommandText= consulta;
        }

        public SqlDataReader ejecutarLectura()
        {
            cmd.Connection = con;
            try
            {
                con.Open();
                reader= cmd.ExecuteReader();
                return reader;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void cerrarConexion() { 
            
            if(reader!=null)
            {
                reader.Close();
            }
            if(con!=null && con.State==System.Data.ConnectionState.Open)
            {
                con.Close();
            }

        }


    }
}
