using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1
{
    internal interface IRepositorio<T, t>
    {
        T Add(T item);
        T Alterar(T item);
        T Consultar(t id);
        IEnumerable<T> Consultar();
        bool Remover(T item);

    }
}
