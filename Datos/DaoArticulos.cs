using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestionDeArticulos.Datos
{
    public class DaoArticulos
    {
        public List<articulos> listar()
        {
            //tiene q devolver una lista 
            List<articulos> lista = new List<articulos>();
            
            AccesoDatos da = new AccesoDatos();

            try
            {
                string consulta= "Select Codigo,Nombre,Descripcion,id from ARTICULOS";

                da.setearConsulta(consulta);
                SqlDataReader lector= da.ejecutarLectura();

                while (lector.Read())
                {
                    articulos aux = new articulos();
                    aux.codigoArticulo = (string)lector["Codigo"];
                    aux.nombreArticulo = (string)lector["Nombre"];
                    aux.descripcionArticulo = (string)lector["Descripcion"];
                    aux.idArticulos = int.Parse(lector["id"].ToString());


                    lista.Add(aux);

                }
                da.cerrarConexion();
                return lista;
            }
            catch (Exception ex)
            {
                //Me permite no que se crashee la app y me diga el error que pueda tener
                throw ex;
            }


        }

    }
}
