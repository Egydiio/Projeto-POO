using System;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks.Dataflow;


class Program
{
    public static int id = 0;
    public static StreamWriter sw = File.AppendText("Consumidores.txt");
    static bool ProcessarContas(string caminhoArquivo = "Contas/ContaPrincipal/Arquivo.txt")
    {
        try
        {
            if (!File.Exists(caminhoArquivo))
            {
                Console.WriteLine($"Erro: O arquivo '{caminhoArquivo}' não foi encontrado.");
                return false;
            }

            string[] linhas = File.ReadAllLines(caminhoArquivo);

            if (linhas.Length == 0)
            {
                Console.WriteLine($"Erro: O arquivo '{caminhoArquivo}' está vazio.");
                return false;
            }

            foreach (string linha in linhas)
            {
                string[] dados = linha.Split(';');

                Consumidor consumidor = new Consumidor
                {
                    Id = int.Parse(dados[0]),
                    Tipo = (TipoConsumidor)Enum.Parse(typeof(TipoConsumidor), dados[1])
                };

                ContaAgua contaAgua = new ContaAgua
                {
                    Consumidor = consumidor,
                    LeituraMesAnterior = double.Parse(dados[2]),
                    LeituraMesAtual = double.Parse(dados[3])
                };

                contaAgua.CalcularConta();
                /* Console.WriteLine(contaAgua); */

                ContaEnergia contaEnergia = new ContaEnergia
                {
                    Consumidor = consumidor,
                    LeituraMesAnterior = double.Parse(dados[5]),
                    LeituraMesAtual = double.Parse(dados[6])
                };

                contaEnergia.CalcularConta();
                /* Console.WriteLine(contaEnergia); */

                Console.WriteLine();
            }
            return true; // Retorna true se o processamento for bem-sucedido
        }
        catch (IOException e)
        {
            Console.WriteLine($"Ocorreu um erro ao ler o arquivo: {e.Message}");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine($"Erro: Sem permissão para acessar o arquivo '{caminhoArquivo}'.");
        }
        return false; // Retorna false em caso de erro
    }

    static void Dashboard()
    {
        Tables tables = new Tables();
        int login;

        // Exibir a tabela de login
        tables.Quest();
        login = int.Parse(Console.ReadLine());

        // Verificar se a pessoa quer adicionar um arquivo
        tables.FileTable();
        tables.Quest();
        int opcaoAdicionarArquivo = int.Parse(Console.ReadLine());

        if (opcaoAdicionarArquivo == 1)
        {
            // Exibir a tabela de adicionar arquivo
            string nomeArq;
            string caminhoCompleto;
            do
            {
                Console.WriteLine();
                tables.AddFileTable();
                nomeArq = Console.ReadLine();
                caminhoCompleto = Path.Combine("Contas", nomeArq);
            } while (ProcessarContas(caminhoCompleto) == false);
            
            Console.WriteLine(caminhoCompleto);
            if (ProcessarContas(caminhoCompleto))
            {
                tables.ConsultTable();
            }
        }
        else
        {
            tables.ConsultTable();
        } 
    }

    public static void escreveConsumidores(string nome){
        sw.WriteLine(id + "," + nome); 
        id++;
    }
    static void Main()
    {
        bool run = true;
        Tables tables = new Tables();

        while(run){
            Console.Clear();
            tables.DashboardTable();
            string opcao = Console.ReadLine();
            switch (opcao){
            case "1":
                Console.Clear();
                tables.RegisterTable();
                Console.Write("Escreva seu nome: ");
                string nome = Console.ReadLine();
                escreveConsumidores(nome);
                break;
            case "2":
                Console.Clear();
                tables.LoginTable();
                Console.Write("Escreva seu ID: ");
                string ID = Console.ReadLine();
                break;
            case "0":
                Console.Clear();
                Console.WriteLine("Obrigado por usar nossa aplicação !!!");
                run = false;
                sw.Close();
                break;
            default:
                Console.WriteLine("Opção inválida.");
                break;
        }
        }

        // int login;

        // // Exibir a tabela de login
        // tables.Quest();
        // login = int.Parse(Console.ReadLine());

        // // Verificar se a pessoa quer adicionar um arquivo
        // tables.FileTable();
        // tables.Quest();
        // int opcaoAdicionarArquivo = int.Parse(Console.ReadLine());

        // if (opcaoAdicionarArquivo == 1)
        // {
        //     // Exibir a tabela de adicionar arquivo
        //     string nomeArq;
        //     string caminhoCompleto;
        //     do
        //     {
        //         Console.WriteLine();
        //         tables.AddFileTable();
        //         nomeArq = Console.ReadLine();
        //         caminhoCompleto = Path.Combine("Contas", nomeArq);
        //     } while (ProcessarContas(caminhoCompleto) == false);
            
        //     Console.WriteLine(caminhoCompleto);
        //     if (ProcessarContas(caminhoCompleto))
        //     {
        //         tables.ConsultTable();
        //     }
        // }
        // else
        // {
        //     tables.ConsultTable();
        // }
    }
}
