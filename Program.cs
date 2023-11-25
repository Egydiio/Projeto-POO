using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks.Dataflow;

interface Conta{
    void CalcularConta();
}

public enum TipoConsumidor
{
    Residencial,
    Comercial
}

public enum TipoServico
{
    Agua,
    Energia
}

public class Consumidor
{
    public int Id { get; set; }
    public TipoConsumidor Tipo { get; set; }
}

public class Tables
{

    public void DashboardTable()
    {
        string dashTable = "+----------------------------------------+\n";
        dashTable += "|               Bem Vindo!!              |\n";
        dashTable += "|----------------------------------------|\n";
        dashTable += "| 1 - Cadastrar                          |\n";
        dashTable += "| 2 - Login                              |\n";
        dashTable += "| 0 - Sair                               |\n";
        dashTable += "+----------------------------------------+";
        Console.WriteLine(dashTable);
        this.Quest();
    }

    public void LoginTable()
    {
        string loginTable = "+----------------------------------------+\n";
        loginTable += "|                 Login                  |\n";
        loginTable += "+----------------------------------------+";
        Console.WriteLine(loginTable);
    }

    public void RegisterTable()
    {
        string registerTable = "+----------------------------------------+\n";
        registerTable += "|                Cadastrar               |\n";
        registerTable += "+----------------------------------------+";
        Console.WriteLine(registerTable);
    }

    public void FileTable()
    {
        string fileTable = "+----------------------------------------+\n";
        fileTable += "|           Adicionar Arquivo            |\n";
        fileTable += "|----------------------------------------|\n";
        fileTable += "| 1 - Você deseja adicionar um arquivo ? |\n";
        fileTable += "| 0 - Sair                               |\n";
        fileTable += "+----------------------------------------+";
        Console.WriteLine(fileTable);
    }

    public void AddFileTable()
    {
        string addfileTable = "Mova o arquivo para pasta\n";
        addfileTable += "Digite o nome do arquivo: ";
        Console.Write(addfileTable);
    }
    public void ConsultTable()
    {
        string opcao;
        do
        {
            string tablePrinc = "+---------------------------------------------------------+\n";
            tablePrinc += "|                    Consulte sua conta                   |\n";
            tablePrinc += "|---------------------------------------------------------|\n";
            tablePrinc += "| 1 - Qual foi meu consumo de energia/água no último mês? |\n";
            tablePrinc += "| 2 - Qual é o valor total da minha conta?                |\n";
            tablePrinc += "| 3 - Qual é o valor da minha conta sem impostos?         |\n";
            tablePrinc += "| 4 - Perguntas Adicionais                                |\n";
            tablePrinc += "| 0 - Sair                                                |\n";
            tablePrinc += "+---------------------------------------------------------+";
            Console.WriteLine(tablePrinc);
            this.Quest();
            opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    // Lógica para consultar o consumo de energia/água no último mês
                    break;

                case "2":
                    // Lógica para consultar o valor total da conta
                    break;

                case "3":
                    // Lógica para consultar o valor total da conta
                    break;

                case "4":
                    this.AdcTable();
                    break;

                case "0":
                    // Sair da consulta
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        } while (opcao != "0");
    }

    public void AdcTable()
    {
        string opcao;
        do
        {
            string tableAdc = "+------------------------------------------------------------------------------------+\n";
            tableAdc += "|                                Perguntas Adicionais                                |\n";
            tableAdc += "|------------------------------------------------------------------------------------|\n";
            tableAdc += "| 1 - Quanto variou minha conta, em reais e em consumo, entre dois meses escolhidos? |\n";
            tableAdc += "| 2 - Qual é o valor médio da minha conta de energia/água?                           |\n";
            tableAdc += "| 3 - Em que mês houve a conta de maior valor, em reais e em consumo?                |\n";
            tableAdc += "| 0 - Sair                                                                           |\n";
            tableAdc += "+------------------------------------------------------------------------------------+";
            Console.WriteLine(tableAdc);
            this.Quest();
            opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    // Lógica para consultar o consumo de energia/água no último mês
                    break;

                case "2":
                    // Lógica para consultar o valor total da conta
                    break;

                case "3":
                    // Lógica para consultar o valor total da conta
                    break;

                case "0":
                    this.ConsultTable();
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        } while(opcao != "0");
    }

    public void Quest()
    {
        Console.Write("Escolha uma opção: ");
    }
}
public class ContaEnergia : Conta
{
    public Consumidor? Consumidor { get; set; }
    public double LeituraMesAnterior { get; set; }
    public double LeituraMesAtual { get; set; }
    public double Consumo { get; set; }
    public double Tarifa { get; set; }
    public double ContribuicaoIluminacao { get; set; }
    public double Imposto { get; set; }
    public double ValorTotal { get; set; }

    public void CalcularConta()
    {
        Consumo = LeituraMesAtual - LeituraMesAnterior;

        if (Consumidor?.Tipo == TipoConsumidor.Residencial)
            Tarifa = 0.46;
        else if (Consumidor?.Tipo == TipoConsumidor.Comercial)
            Tarifa = 0.41;

        ContribuicaoIluminacao = 13.25;

        ValorTotal = (Consumo * Tarifa) + ContribuicaoIluminacao;

        if (Consumidor?.Tipo == TipoConsumidor.Residencial)
            if(Consumo < 90) {
                Imposto = 0; // Se o consumo de um consumidor residencial for abaixo de 90KW/h, há isenção do imposto.
            } else {
                Imposto = ValorTotal * 0.4285;
            }
        else if (Consumidor?.Tipo == TipoConsumidor.Comercial)
            Imposto = ValorTotal * 0.2195;

        ValorTotal += Imposto;
    }

    public override string ToString()
    {
        return $"Consumidor: {Consumidor?.Id}, Tipo: {Consumidor?.Tipo}, Valor Total Energia: {ValorTotal:C}";
    }
}

public class ContaAgua : Conta
{
    public Consumidor? Consumidor { get; set; }
    public double LeituraMesAnterior { get; set; }
    public double LeituraMesAtual { get; set; }
    public double ConsumoAgua { get; set; }
    public double TarifaAgua { get; set; }
    public double TarifaEsgoto { get; set; }
    public double Cofins { get; set; }
    public double ValorTotal { get; set; }
    public double count { get; set; }
    public double TarifaAguaTemp { get; set; }
    public double TarifaEsgotoTemp { get; set; }


    public void CalcularConta()
    {
        ConsumoAgua = LeituraMesAtual - LeituraMesAnterior;
        count = 0;

        // Lógica de cálculo da tarifa escalonada para água
        if (Consumidor?.Tipo == TipoConsumidor.Residencial)
        {
            if (ConsumoAgua >= 6)
            {
                if(ConsumoAgua >= 10){
                    TarifaAgua = 2.241; // Exemplo de tarifa para a faixa 6-10 m³
                    TarifaEsgoto = 1.122; // Exemplo de tarifa para a faixa 6-10 m³ de esgoto
                    count += 10;
                    TarifaAguaTemp += 10 * TarifaAgua;
                    TarifaEsgotoTemp += 10 * TarifaEsgoto;
                } else {
                    TarifaAgua = 2.241; // Exemplo de tarifa para a faixa 6-10 m³
                    TarifaEsgoto = 1.122; // Exemplo de tarifa para a faixa 6-10 m³ de esgoto
                    TarifaAguaTemp += ConsumoAgua * TarifaAgua;
                    TarifaEsgotoTemp += ConsumoAgua * TarifaEsgoto;
                    count = ConsumoAgua;
                }
                if((ConsumoAgua - count) > 0){
                    if((ConsumoAgua - count) >= 5){
                        TarifaAgua = 5.447; // Exemplo de tarifa para a faixa 10-15 m³
                        TarifaEsgoto = 2.724; // Exemplo de tarifa para a faixa 10-15 m³ de esgoto
                        count += 5;
                        TarifaAguaTemp += 5 * TarifaAgua;
                        TarifaEsgotoTemp += 5 * TarifaEsgoto;
                    } else {
                        TarifaAgua = 5.447; // Exemplo de tarifa para a faixa 10-15 m³
                        TarifaEsgoto = 2.724; // Exemplo de tarifa para a faixa 10-15 m³ de esgoto
                        TarifaAguaTemp += (ConsumoAgua - count) * TarifaAgua;
                        TarifaEsgotoTemp += (ConsumoAgua - count) * TarifaEsgoto;
                        count = ConsumoAgua;
                    }
                }
                if((ConsumoAgua - count) > 0){
                    if((ConsumoAgua - count) >= 5){
                        TarifaAgua = 5.461; // Exemplo de tarifa para a faixa 15-20 m³
                        TarifaEsgoto = 2.731; // Exemplo de tarifa para a faixa 15-20 m³ de esgoto
                        count += 5;
                        TarifaAguaTemp += 5 * TarifaAgua;
                        TarifaEsgotoTemp += 5 * TarifaEsgoto;
                    } else {
                        TarifaAgua = 5.461; // Exemplo de tarifa para a faixa 15-20 m³
                        TarifaEsgoto = 2.731; // Exemplo de tarifa para a faixa 15-20 m³ de esgoto
                        TarifaAguaTemp += (ConsumoAgua - count) * TarifaAgua;
                        TarifaEsgotoTemp += (ConsumoAgua - count) * TarifaEsgoto;
                        count = ConsumoAgua;
                    }
                }
                if((ConsumoAgua - count) > 0){
                    if((ConsumoAgua - count) >= 20){
                        TarifaAgua = 5.487; // Exemplo de tarifa para a faixa 20-40 m³
                        TarifaEsgoto = 2.744; // Exemplo de tarifa para a faixa 20-40 m³ de esgoto
                        count += 20;
                        TarifaAguaTemp += 20 * TarifaAgua;
                        TarifaEsgotoTemp += 20 * TarifaEsgoto;
                    } else {
                        TarifaAgua = 5.487; // Exemplo de tarifa para a faixa 20-40 m³
                        TarifaEsgoto = 2.744; // Exemplo de tarifa para a faixa 20-40 m³ de esgoto
                        TarifaAguaTemp += (ConsumoAgua - count) * TarifaAgua;
                        TarifaEsgotoTemp += (ConsumoAgua - count) * TarifaEsgoto;
                        count = ConsumoAgua;
                    }
                } 
                if((ConsumoAgua - count) > 0){
                    TarifaAgua = 10.066; // Exemplo de tarifa para a faixa +40 m³
                    TarifaEsgoto = 5.035; // Exemplo de tarifa para a faixa +40 m³ de esgoto
                    TarifaAguaTemp += (ConsumoAgua - count) * TarifaAgua;
                    TarifaEsgotoTemp += (ConsumoAgua - count) * TarifaEsgoto;
                }
            }
            else {
                TarifaAgua = 10.08; // Tarifa fixa para a faixa 0-6 m³
                TarifaEsgoto = 5.05; // Tarifa fixa para a faixa 0-6 m³ de esgoto
            }
        }
        else if (Consumidor?.Tipo == TipoConsumidor.Comercial)
        {
            if (ConsumoAgua >= 6)
            {
                if(ConsumoAgua >= 10){
                    TarifaAgua = 4.299; // Exemplo de tarifa para a faixa 6-10 m³
                    TarifaEsgoto = 2.149; // Exemplo de tarifa para a faixa 6-10 m³ de esgoto
                    count += 10;
                    TarifaAguaTemp += 10 * TarifaAgua;
                    TarifaEsgotoTemp += 10 * TarifaEsgoto;
                } else {
                    TarifaAgua = 4.299; // Exemplo de tarifa para a faixa 6-10 m³
                    TarifaEsgoto = 2.149; // Exemplo de tarifa para a faixa 6-10 m³ de esgoto
                    TarifaAguaTemp += ConsumoAgua * TarifaAgua;
                    TarifaEsgotoTemp += ConsumoAgua * TarifaEsgoto;
                    count = ConsumoAgua;
                }
                if((ConsumoAgua - count) > 0){
                    if((ConsumoAgua - count) >= 30){
                        TarifaAgua = 8.221; // Exemplo de tarifa para a faixa 10-40 m³
                        TarifaEsgoto = 4.111; // Exemplo de tarifa para a faixa 10-40 m³ de esgoto
                        count += 30;
                        TarifaAguaTemp += 30 * TarifaAgua;
                        TarifaEsgotoTemp += 30 * TarifaEsgoto;
                    } else {
                        TarifaAgua = 8.221; // Exemplo de tarifa para a faixa 10-40 m³
                        TarifaEsgoto = 4.111; // Exemplo de tarifa para a faixa 10-40 m³ de esgoto
                        TarifaAguaTemp += (ConsumoAgua - count) * TarifaAgua;
                        TarifaEsgotoTemp += (ConsumoAgua - count) * TarifaEsgoto;
                        count = ConsumoAgua;
                    }
                }
                if((ConsumoAgua - count) > 0){
                    if((ConsumoAgua - count) >= 60){
                        TarifaAgua = 8.288; // Exemplo de tarifa para a faixa 40-100 m³
                        TarifaEsgoto = 4.144; // Exemplo de tarifa para a faixa 40-100 m³ de esgoto
                        count += 60;
                        TarifaAguaTemp += 60 * TarifaAgua;
                        TarifaEsgotoTemp += 60 * TarifaEsgoto;
                    } else {
                        TarifaAgua = 8.288; // Exemplo de tarifa para a faixa 40-100 m³
                        TarifaEsgoto = 4.144; // Exemplo de tarifa para a faixa 40-100 m³ de esgoto
                        TarifaAguaTemp += (ConsumoAgua - count) * TarifaAgua;
                        TarifaEsgotoTemp += (ConsumoAgua - count) * TarifaEsgoto;
                        count = ConsumoAgua;
                    }
                }
                if((ConsumoAgua - count) > 0){
                    TarifaAgua = 8.329; // Exemplo de tarifa para a faixa +100 m³
                    TarifaEsgoto = 4.165; // Exemplo de tarifa para a faixa +100 m³ de esgoto
                    TarifaAguaTemp += (ConsumoAgua - count) * TarifaAgua;
                    TarifaEsgotoTemp += (ConsumoAgua - count) * TarifaEsgoto;
                }
            } else {
                TarifaAgua = 25.79; // Tarifa fixa para a faixa 0-6 m³ para Comercial
                TarifaEsgoto = 12.90; // Tarifa fixa para a faixa 0-6 m³ de esgoto para Comercial
            }
        }

        // Cálculo do valor total
        ValorTotal =  TarifaAguaTemp + TarifaEsgotoTemp;

        // Adição de 3% a título de COFINS
        Cofins = ValorTotal * 0.03;

        ValorTotal += Cofins;
    }

    public override string ToString()
    {
        return $"Consumidor: {Consumidor?.Id}, Tipo: {Consumidor?.Tipo}, Valor Total Água: {ValorTotal:C}";
    }
}

class Program
{

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
    static void Main()
    {
        
        Tables tables = new Tables();
        tables.DashboardTable();
        string opcao = Console.ReadLine();

        switch (opcao)
        {
            case "1":
                    tables.RegisterTable();
                    Console.Write("Escreva seu nome: ");
                    string nome = Console.ReadLine();
                    break;
            case "2":
                    tables.LoginTable();
                    Console.Write("Escreva seu ID: ");
                    string ID = Console.ReadLine();
                    break;
            case "0":
                    break;
            default:
                Console.WriteLine("Opção inválida.");
                    break;
        }
        


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
}
