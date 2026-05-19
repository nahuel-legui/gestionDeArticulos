using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace gestionDeArticulos
{
    public class articulos
    {
        public int idArticulos { get; set; }

        public string codigoArticulo { get; set; }

        public string nombreArticulo { get; set; }

        public string descripcionArticulo { get; set; }

        public marcas idMarca{ get; set; }
        public categorias idCategoria { get; set; }

        public float precioArticulo { get; set; }


    }
}
