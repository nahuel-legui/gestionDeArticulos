using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestionDeArticulos
{
    public class marcas
    {

        public int IdMarcas { get; set; }

        public string descripcionMarca { get; set; }

        public marcas() 
        {
            IdMarcas = -1;
            descripcionMarca = "";
        }
        public marcas(int id )
        {
            IdMarcas = id;
            descripcionMarca = "";
        }


    }
}
