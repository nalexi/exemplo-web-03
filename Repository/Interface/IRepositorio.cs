using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    interface IRepositorio<T>
    {
        List<T> ObterTodos(string busca);

        T ObterPeloId(int id);

        int Inserir(T objeto);

        void Alterar(T objeto);

        void Apagar(int id);
    }
}
