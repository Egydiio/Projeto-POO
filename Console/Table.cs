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
        fileTable += "| 0 - Não                                |\n";
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
            ContaEnergia energia = new ContaEnergia();
            ContaAgua agua = new ContaAgua();
            double temp = 0;

            switch (opcao)
            {
                case "1":
                    Console.Clear();
                    energia.calcularConsumo();
                    agua.calcularConsumo();
                    break;

                case "2":
                    Console.Clear();
                    temp += energia.calcularTotal();
                    temp += agua.calcularTotal();
                    Console.WriteLine("Total das contas: {0:F2}" , temp);

                    break;

                case "3":
                    Console.WriteLine("O valor total da sua conta sem imposto é: {0:F2}" , GetTotalSemImposto.SomaTotalSemImposto);
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