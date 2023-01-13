using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Model
{
    public class Conta
    {
        public double Saldo { get; private set; }

        static int nextID;
        public int ID { get; private set; }
        public string Cpf { get; private set; } = null!;
        public string Nome { get; private set; }
        public string Senha { get; private set; } = null!;

        public Conta(string cpf, string nome, string senha)
        {
            ID = Interlocked.Increment(ref nextID);
            Cpf = cpf;
            Nome = nome;
            Saldo = 0.0;
            Senha = senha;
        }

        public void Depositar(double quantia)
        {
            Saldo += quantia;
        }

        public bool Sacar(double quantia) 
        {
            if (Saldo < quantia) throw new SaldoInsuficienteException("Saldo insuficiente! ");

            Saldo -= quantia;
            return true;
        }
    }
}
