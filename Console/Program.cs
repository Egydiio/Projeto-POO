using System;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks.Dataflow;


static public class GetTotalSemImposto
{
    public static double SomaTotalSemImposto;
}

class Program
{
    public static int UsuarioLogado;
    public static StreamWriter sw = File.AppendText("Tabelas/Consumidores.txt");
    
    static bool ProcessarContas(string caminhoArquivo)
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

            string caminhoArquivoSaidaContaAgua = "Tabelas/ContaAgua.txt";
            string caminhoArquivoSaidaContaEnergia = "Tabelas/ContaEnergia.txt";

            for (int i = 0; i < linhas.Length; i++)
            {
                string[] tipoConta = linhas[i].Split(',');


                if (tipoConta.Length > 0)
                {
                    if (tipoConta[0] == "ContaAgua")
                    {
                        GravarContaEmArquivo(linhas[i], caminhoArquivoSaidaContaAgua);
                    } else
                    {
                        GravarContaEmArquivo(linhas[i], caminhoArquivoSaidaContaEnergia);
                    }
                }
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

    static void GravarContaEmArquivo(string linhaConta, string caminhoArquivoSaida)
    {
        string[] dados = linhaConta.Split(',');

        if (dados.Length >= 6)
        {
            string novaLinha = $"{dados[1]},{dados[2]},{dados[3]},{dados[4]},{dados[5]},{UsuarioLogado}";

            // Gravar em caminhoArquivoSaida
            File.AppendAllText(caminhoArquivoSaida, novaLinha + Environment.NewLine);
        }
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
                caminhoCompleto = Path.Combine("Arquivos", nomeArq);
            } while (ProcessarContas(caminhoCompleto) == false);
            
            tables.ConsultTable();
            
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

            // Validar o tipo do consumidor
            if (proximoID == -1)
            {
                Console.WriteLine("Erro: Tipo de consumidor inválido. Use 'Residencial' ou 'Comercial'.");
                return "false";
            }

            // Escrever no arquivo usando o próximo ID e o tipo do consumidor
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
        try
        {
            string[] linhas = File.ReadAllLines("Tabelas/Consumidores.txt");

            if (linhas.Length == 0)
            {
                // Se o arquivo estiver vazio, retorna 1 como o próximo ID
                return 1;
            }

            // Se o arquivo não estiver vazio, continua com a lógica original
            int maiorID = linhas
                            .Select(line => int.Parse(line.Split(',')[0]))
                            .DefaultIfEmpty(0)
                            .Max();

            return maiorID + 1;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao obter próximo ID: " + ex.Message);
            return -1; // Retorna um valor que indica um erro (-1, por exemplo)
        }
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
                        UsuarioLogado = id;
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
                        break;
                    }
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
                    Console.WriteLine("Obrigado por usar nossa aplicação !!!");
                    run = false;
                    sw.Close();
                    Environment.Exit(1);
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
        Dashboard();
    }
}