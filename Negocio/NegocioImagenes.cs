using gestionDeArticulos.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestionDeArticulos.Negocio
{
    public class NegocioImagenes
    {
        private DaoImagenes daoImg;
        public List<string> getImagenes(string id)
        {
            daoImg = new DaoImagenes();

            return daoImg.getImagenes(id);

        }
    }
}
