using Facturacion1._5.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion1._5.Data
{
    public interface IClienteRepository
    {
        List<Cliente> GetAll();
    }
}
