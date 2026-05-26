using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestionDeArticulos.Datos
{
    public class DaoImagenes
    {

        public List<string> getImagenes(string id)

        {
            List<string> lista = new List<string>();

            AccesoDatos da=new AccesoDatos();
            string consulta= $"Select ImagenUrl from IMAGENES where IdArticulo={id}";
            try
            {

                da.setearConsulta(consulta);
                SqlDataReader lector = da.ejecutarLectura();
                while (lector.Read())
                {
                    string url = lector["ImagenUrl"].ToString();
                    lista.Add(url);
                }

                da.cerrarConexion();
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;

            }
        }
    }
}
