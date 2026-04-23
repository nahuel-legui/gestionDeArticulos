using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestionDeArticulos
{
    public class categorias
    {
        public int idCategoria { get; set; }

        public string descripcionCategoria { get; set; }  


        public categorias()
        {
            idCategoria = -1;
            descripcionCategoria = "";

        }

        public categorias(int id)
        {
            idCategoria = id;
            descripcionCategoria = "";

        }


    }
}
