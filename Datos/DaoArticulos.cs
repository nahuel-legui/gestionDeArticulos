using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public DataTable rellenarCbCategoria()
        {
            AccesoDatos datos = new AccesoDatos();

            DataTable dt = new DataTable();

            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("descripcion", typeof(string));

            try
            {
                string consulta = "select id,Descripcion from CATEGORIAS";
                datos.setearConsulta(consulta);
                SqlDataReader lector= datos.ejecutarLectura();
                while (lector.Read())
                {
                    DataRow fila = dt.NewRow();
                    fila["id"] = lector[0];
                    fila["descripcion"] = lector[1];
                    dt.Rows.Add(fila);

                }
                datos.cerrarConexion();
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }



        }

        public DataTable rellenarCbMarca()
        {
            string consulta = "select id,Descripcion from MARCAS";
            AccesoDatos datos = new AccesoDatos();

            DataTable dt = new DataTable();

            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("descripcion", typeof(string));

            try
            {
                datos.setearConsulta(consulta);
                SqlDataReader lector = datos.ejecutarLectura();
                while (lector.Read())
                {
                    DataRow fila = dt.NewRow();
                    fila["id"] = lector[0];
                    fila["descripcion"] = lector[1];
                    dt.Rows.Add(fila);

                }
                datos.cerrarConexion();
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void agregarArticulo(articulos obj)
        {
            string consulta = "INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) " +
                              "VALUES (@codigo, @nombre, @descripcion, @idMarca, @idCategoria, @precio)";

           AccesoDatos datos =new AccesoDatos();
            try
            {
                datos.setearConsulta(consulta);
                datos.setearParametros("@codigo",obj.codigoArticulo);
                datos.setearParametros("@nombre", obj.nombreArticulo);
                datos.setearParametros("@descripcion", obj.descripcionArticulo);
                datos.setearParametros("@idMarca", obj.idMarca.IdMarcas);
                datos.setearParametros("@idCategoria", obj.idCategoria.idCategoria);
                datos.setearParametros("@precio", float.Parse(obj.precioArticulo.ToString()));

                datos.ejecutarAccion();
            }
            catch (Exception ex )
            {

                throw ex;
            }
            
                 
            
        }

    }
}
