using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace gestionDeArticulos
{
    class ElectronicaNegocio
    {
        public List<articulos> listar()
        {
            //tiene q devolver una lista 
            List<articulos>lista=new List<articulos>();
            //para conectarse a esa conexion
            SqlConnection conexion = new SqlConnection();
            //Esto realiza acciones 
            SqlCommand comando = new SqlCommand();
            //Aca voy a obtener un set de datos y los voy a guardar en lector 
            SqlDataReader lector;

            try
            {
                //Configuramos la cadena de conexion
                conexion.ConnectionString = "Server=localhost\\SQLEXPRESS;database=CATALOGO_P3_DB;integrated security=true";
                comando.CommandType=System.Data.CommandType.Text;
                comando.CommandText = "Select Codigo,Nombre,Descripcion,id from ARTICULOS";
                comando.Connection = conexion;

                conexion.Open();
                lector =comando.ExecuteReader();

                while (lector.Read()) 
                { 
                    articulos aux = new articulos();
                    aux.codigoArticulo = (string)lector["Codigo"];
                    aux.nombreArticulo= (string)lector["Nombre"];
                    aux.descripcionArticulo=(string)lector["Descripcion"];
                    aux.idArticulos = int.Parse(lector["id"].ToString());


                    lista.Add(aux);

                }
                conexion.Close();
                return lista;
            }
            catch (Exception ex)
            {
                //Me permite no que se crashee la app y me diga el error que pueda tener
                throw ex ;
            }


        }

    }
}
