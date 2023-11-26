using System;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks.Dataflow;


class Program
{
    public static int id = 0;
    public static StreamWriter sw = File.AppendText("Tabelas/Consumidores.txt");
    static bool ProcessarContas(string caminhoArquivo = "Arquivos/ContaPrincipal/Arquivo.txt")
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

    public static string escreveConsumidores(string nome)
    {
        try
        {
            int proximoID = ObterProximoID();

            // Escrever no arquivo usando o próximo ID
            sw.WriteLine(proximoID + "," + nome);

            // Fechar o StreamWriter
            sw.Close();

            Console.WriteLine("Seu ID é: " + proximoID);

            return "true";
        }
        catch (Exception ex)
        {
            // Certifique-se de fechar o StreamWriter em caso de exceção
            sw.Close();

            // Retorna a mensagem de erro
            return "Erro ao escrever no arquivo: " + ex.Message;
        }
    }

    public static int ObterProximoID()
    {
        // Lê todas as linhas do arquivo para determinar o maior ID atual
        int maiorID = File.ReadAllLines("Tabelas/Consumidores.txt")
                        .Select(line => int.Parse(line.Split(',')[0]))
                        .DefaultIfEmpty(0)
                        .Max();

        // Incrementa o maior ID para obter o próximo ID
        return maiorID + 1;
    }

    public static bool VerificarExistenciaID(int id)
    {
        try
        {
            // Lê todas as linhas do arquivo
            string[] linhas = File.ReadAllLines("Tabelas/Consumidores.txt");

            // Verifica se o ID está presente em alguma linha
            foreach (string linha in linhas)
            {
                string[] dados = linha.Split(',');

                if (dados.Length > 0 && int.TryParse(dados[0], out int consumidorID))
                {
                    if (consumidorID == id)
                    {
                        // ID encontrado
                        return true;
                    }
                }
            }

            // ID não encontrado
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao verificar existência do ID: " + ex.Message);
            return false;
        }
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
                    string consu = escreveConsumidores(nome);
                    if (consu == "true")
                    {
                        goto case "2";
                    } else 
                    {
                        Console.WriteLine(consu);
                    }
                    break;
                case "2":
                    tables.LoginTable();
                    Console.Write("Escreva seu ID: ");
                    string idString = Console.ReadLine();
                    if (int.TryParse(idString, out int id))
                    {
                        if (VerificarExistenciaID(id))
                        {
                            // ID existe, permitir login
                            Console.WriteLine("Login bem-sucedido!");
                            run = false;
                        }
                        else
                        {
                            // ID não existe, mostrar mensagem de erro
                            Console.WriteLine("Erro: ID não encontrado. Tente novamente.");
                        }
                    }
                    else
                    {
                        // Entrada inválida para ID, mostrar mensagem de erro
                        Console.WriteLine("Erro: ID inválido. Tente novamente.");
                    }
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
        Dashboard();
    }
}
