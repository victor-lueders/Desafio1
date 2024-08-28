using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1
{
    internal class FormaPagamento
    {
        public string Descricao { get; set; }

        public FormaPagamento() { }

        public FormaPagamento(string Descricao)
        {
            this.Descricao = Descricao;
        }
    }
}
