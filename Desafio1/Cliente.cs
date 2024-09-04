using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1
{
    internal class Cliente : Pessoa
    {
        public int Id { get; set; }
        public int Pontuacao { get; set; }

        public Cliente() { }

        public Cliente(string nome, string endereco, string telefone, string cpf, int pontuacao) : base(nome, endereco, telefone, cpf)
        {
            this.cpf = cpf;
            Pontuacao = pontuacao;
        }

        public void AddPontuacao(int pontos)
        {
            Pontuacao += pontos;
        }
    }
}
