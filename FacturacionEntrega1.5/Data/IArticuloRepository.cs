using Facturacion1._5.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion1._5.Data
{
    public interface IArticuloRepository
    {
        List<Articulo> GetAll();
        bool Update(Articulo articulo);
        bool Delete(int id);
    }
}
