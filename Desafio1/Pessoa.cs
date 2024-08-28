using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1
{
    internal abstract class Pessoa
    {
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        private string Cpf;

        public string cpf { get { return Cpf; }
            set 
            {
                if (value.Length != 11)
                {
                    throw new ArgumentException("CPF Inválido.");
                }
                else
                {
                    Cpf = value;
                }
            } 
        }

        public Pessoa()
        {
            
        }

        public Pessoa(string nome, string endereco, string telefone, string cpf)
        {
            this.Nome = nome;
            this.Endereco = endereco;
            this.Telefone = telefone;
            this.Cpf = cpf;
        }
    }
}
