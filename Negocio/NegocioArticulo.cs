using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using gestionDeArticulos.Datos;

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

    }
}
