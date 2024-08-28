using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1
{
    internal class Produto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }

        public Produto() { }

        public Produto(string descricao, double valor)
        {
            this.Descricao = descricao;
            this.Valor = valor;
        }
    }
}
