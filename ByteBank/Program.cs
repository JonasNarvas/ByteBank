using System.Security.Cryptography;
using System.Text.Json;

namespace byteBank;


    public class Projeto
    {   
        static int IDAux = 1;

        static void Menu()
        {
        Console.WriteLine("-----------------------------");
        Console.WriteLine("1 - Inserir novo usuário");
        Console.WriteLine("2 - Deletar um usuário");
        Console.WriteLine("3 - Listar todas as contas registradas");
        Console.WriteLine("4 - Detalhes de um usuário");
        Console.WriteLine("5 - Quantia armazenada no banco");
        Console.WriteLine("6 - Manipular a conta");
        Console.WriteLine("0 - Sair do programa");
        Console.WriteLine("-----------------------------");
        Console.Write("Digite a opção desejada: ");
        }

        static void menuConta()
        {
        Console.WriteLine("-----------------------------");
        Console.WriteLine("1 - Alterar o tipo de conta");
        Console.WriteLine("2 - Alterar senha");
        Console.WriteLine("3 - Fazer um depósito");
        Console.WriteLine("4 - Sacar dinheiro");
        Console.WriteLine("5 - Ver seu extrato bancário");
        Console.WriteLine("0 - Voltar para o menu anterior");
        Console.WriteLine("-----------------------------");
        Console.Write("Digite a opção desejada: ");
    }
        static void cadastrarCliente(List<string> nome, List<string> cpf, List<string>senha, List<double>saldo, List<int> ID)
        {
        Console.Write("Digite o nome do titular da conta: ");
        nome.Add(Console.ReadLine());
        Console.Write("Digite o cpf: ");
        cpf.Add(Console.ReadLine());
        Console.Write("Crie uma senha: ");
        senha.Add(Console.ReadLine());
        saldo.Add(0);
        ID.Add(IDAux);
        IDAux++;
        
       
        }
        static void versaoDaConta(List<string> tipoDaConta, List<string> instituicao)
        { int resposta = 0;
        do
        {
            Console.Write("Qual o tipo de conta? [1] - Conta Corrente [2] - Conta Universitária ");
            resposta = int.Parse(Console.ReadLine());
            if (resposta == 1)
            {
                tipoDaConta.Add("Conta Corrente");
            }
            else if (resposta == 2)
            {
                Console.Write("Digite o nome da instituição de ensino: ");
                instituicao.Add(Console.ReadLine());
                tipoDaConta.Add("Conta Universitária");
            }
            else
            {
                Console.WriteLine("Opção inválida, selecione novamente.");
            }
        } while (resposta != 1 && resposta != 2);
        }

        
        static void deletarCliente(List<string> nome, List<string> cpf,List<string> tipoDaConta, List<string> senha, List<double> saldo, List<int> ID)
        {
        Console.Write("Digite o cpf do usuário a ser deletado: ");
        string idDelete = Console.ReadLine();
        int indexParaValidar = cpf.FindIndex(cpf => cpf == idDelete);
        if (indexParaValidar == -1)
        {
            Console.WriteLine("CPF não encontrado");

        }
        else
        {
            cpf.Remove(idDelete);
            nome.RemoveAt(indexParaValidar);
            tipoDaConta.RemoveAt(indexParaValidar);
            senha.RemoveAt(indexParaValidar);
            saldo.RemoveAt(indexParaValidar);
            ID.RemoveAt(indexParaValidar);
        }
        }
                        
        static void detalhesDeUsuario(List<string> nome, List<string> cpf, List<string> tipoDaConta, List<double> saldo)
        {
        Console.Write("Digite o cpf do usuário a ser apresentado: ");
        string idDelete = Console.ReadLine();
        int indexParaValidar = cpf.FindIndex(cpf => cpf == idDelete);
        if (indexParaValidar == -1)
        {
            Console.WriteLine("CPF não encontrado");

        }
        Console.WriteLine($"CPF: {cpf[indexParaValidar]} | Titular da conta: {nome[indexParaValidar]} | Tipo da Conta:{tipoDaConta[indexParaValidar]} | Saldo: R${saldo[indexParaValidar]:F2}");
        }
        static void apresentarTodasAsContas(List<string> nome, List<string> cpf,List<string> tipoDaConta, List<double> saldo, List<int> ID)
        {
        for(int i = 0; i < ID.Count; i++)
        {
            Console.WriteLine($"CPF: {cpf[i]} | Titular da conta: {nome[i]} | Tipo da Conta:{tipoDaConta[i]} | Saldo: R${saldo[i]:F2}");
        }
        }
        static void quantiaNoBanco(List<double> saldo)
        {
        Console.WriteLine($"Quantia total armazenada no banco: R${saldo.Sum():F2}");
        }
        
        public static void Main(string[] args)
        {

        List<string> nome = new List<string>();
        List<string> cpf = new List<string>();
        List<int> ID = new List<int>();
        List<string> senha = new List<string>();
        List<double> saldo = new List<double>();
        List<string> tipoDaConta = new List<string>();
        List<string> instituicao = new List<string>();
        List<string> usuario = new List<string>();
        
        Console.WriteLine("SEJA BEM VINDO(A)");

        int opcao;

        do
        {
            Menu();
            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 0: Console.WriteLine("Encerrando o programa..."); break;
                case 1: versaoDaConta(tipoDaConta, instituicao); cadastrarCliente(nome, cpf, senha, saldo, ID); break;
                case 2: deletarCliente(nome, cpf, tipoDaConta, senha, saldo, ID); break;
                case 3: apresentarTodasAsContas(nome, cpf, tipoDaConta, saldo, ID); break;
                case 4: detalhesDeUsuario(nome, cpf, tipoDaConta, saldo); break;
                case 5: quantiaNoBanco(saldo); break;
                case 6:
                    Console.Write("Digite o cpf: ");
                    string cpfValidar = Console.ReadLine();
                    if (validarCPFSenha(cpf, senha, nome, cpfValidar) == true)
                    {
                        int opt;
                        do
                        {

                            menuConta(); opt = int.Parse(Console.ReadLine());
                            switch (opt)
                            {
                                case 0: Console.WriteLine("Voltando para o menu anterior..."); break;
                                case 1: break;
                                case 2: alterarSenha(cpf, senha, cpfValidar); break;
                                case 3: fazerDeposito(cpf, saldo, cpfValidar); break;
                                case 4: fazerSaque(cpf,saldo, cpfValidar); break;


                            }
                        } while (opt != 0);
                    }
                    else Console.WriteLine("Tente novamente"); opcao = 6;
                        break;
            }

            } while (opcao != 0) ;
                    
        }

     

    static bool validarCPFSenha(List<string> cpf, List<string> senha, List<string> nome,string cpfValidar)
        {
        int indexParaValidar = cpf.FindIndex(cpf => cpf == cpfValidar);
        if (indexParaValidar == -1)
        {
            Console.WriteLine("CPF não encontrado");
            return false;
        }
        Console.Write("Digite a senha do usuário: ");
        string senhaAtual = Console.ReadLine();
        if(senhaAtual == senha[indexParaValidar])
        {
            Console.WriteLine($"SEJA BEM VINDO(A), {nome[indexParaValidar]}");
            return true;
        }
        else
        {
            Console.WriteLine("Senha inválida");
            return false;
        }
        
    }
    static void fazerDeposito(List<string> cpf, List<double> saldo, string cpfValidar)
     {
        
        int indexParaValidar = cpf.FindIndex(cpf=> cpf == cpfValidar);
        if (indexParaValidar == -1)
        {
            Console.WriteLine("CPF não encontrado");
        }
        else
        {
            Console.Write("Qual é o valor do depósito? ");
            double valor = double.Parse(Console.ReadLine());
            saldo[indexParaValidar] = valor;
        }
            
     }
    static void fazerSaque(List<string> cpf, List<double> saldo, string cpfValidar)
    {
        int indexParaValidar = cpf.FindIndex(cpf => cpf == cpfValidar);
        if (indexParaValidar == -1)
        {
            Console.WriteLine("CPF não encontrado");
        }
        else
        {
            Console.Write("Qual é o valor do saque que quer fazer? ");
            double valor = double.Parse(Console.ReadLine());
            saldo[indexParaValidar] -= valor;

        }
    }
    static void alterarSenha(List<string> cpf, List<string> senha,string cpfValidar)
    {
        int indexParaValidar = cpf.FindIndex(cpf => cpf == cpfValidar);
        if (indexParaValidar == -1)
        {
            Console.WriteLine("CPF não encontrado");
        }
        else
        {
            string senhaAtual = "";
            do
            {
                Console.Write("Digite a senha atual: ");
                senhaAtual = Console.ReadLine();
                if (senhaAtual == senha[indexParaValidar])
                {
                    Console.Write("Digite a nova senha: ");
                    string novaSenha = Console.ReadLine();
                    senha[indexParaValidar] = novaSenha;
                    senhaAtual = novaSenha;
                    Console.WriteLine("###########################");
                    Console.WriteLine("SENHA ALTERADA COM SUCESSO");
                }
                else
                {
                    Console.WriteLine("ERRO");
                    Console.WriteLine("A senha digitada não é a mesma senha cadastrada");
                }
            }while(senhaAtual != senha[indexParaValidar]);
        }
        }
    
}

