using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1
{
    internal interface IRepositorio<T, t>
    {
        IEnumerable<T> GetAll();
        T Get(t id);
        T Save(T entity);
        bool Update(T entity);
        void Delete(T entity);

    }
}
