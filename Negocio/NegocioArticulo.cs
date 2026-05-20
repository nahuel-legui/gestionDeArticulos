using gestionDeArticulos.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gestionDeArticulos
{
    class NegocioArticulo
    {
        private DaoArticulos daoArt;
        public List<articulos> listar()
        {
            daoArt = new DaoArticulos();
            return daoArt.listar();


        }

        public DataTable rellenarCbCategoria()
        {
            daoArt= new DaoArticulos();
            return daoArt.rellenarCbCategoria();
        }

        public DataTable rellenarCbMarca()
        {
            daoArt=new DaoArticulos();
            return daoArt.rellenarCbMarca();
        }
        public void agregarArticulo(articulos obj)
        {
            daoArt = new DaoArticulos();
            daoArt.agregarArticulo(obj);
        }

    }

}
