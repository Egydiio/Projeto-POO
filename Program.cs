using System;
using System.IO;

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
            if(Consumo > 90) {
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
    public double ConsumoEsgoto { get; set; }
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
                    Console.WriteLine("1-IF-TarifaAgua: " + TarifaAguaTemp);
                    Console.WriteLine("1-IF-TarifaEsgoto: " + TarifaEsgotoTemp);
                } else {
                    TarifaAgua = 2.241; // Exemplo de tarifa para a faixa 6-10 m³
                    TarifaEsgoto = 1.122; // Exemplo de tarifa para a faixa 6-10 m³ de esgoto
                    TarifaAguaTemp += ConsumoAgua * TarifaAgua;
                    TarifaEsgotoTemp += ConsumoAgua * TarifaEsgoto;
                    count = ConsumoAgua;
                    Console.WriteLine("1-ELSE-TarifaAgua: " + TarifaAguaTemp);
                    Console.WriteLine("1-ELSE-TarifaEsgoto: " + TarifaEsgotoTemp);
                }
                if((ConsumoAgua - count) > 0){
                    if((ConsumoAgua - count) >= 10){
                        TarifaAgua = 5.447; // Exemplo de tarifa para a faixa 10-15 m³
                        TarifaEsgoto = 2.724; // Exemplo de tarifa para a faixa 10-15 m³ de esgoto
                        count += 5;
                        TarifaAguaTemp += 5 * TarifaAgua;
                        TarifaEsgotoTemp += 5 * TarifaEsgoto;
                        Console.WriteLine("2-IF-TarifaAgua: " + TarifaAguaTemp);
                        Console.WriteLine("2-IF-TarifaEsgoto: " + TarifaEsgotoTemp);
                    } else {
                        TarifaAgua = 5.447; // Exemplo de tarifa para a faixa 10-15 m³
                        TarifaEsgoto = 2.724; // Exemplo de tarifa para a faixa 10-15 m³ de esgoto
                        TarifaAguaTemp += (ConsumoAgua - count) * TarifaAgua;
                        TarifaEsgotoTemp += (ConsumoAgua - count) * TarifaEsgoto;
                        count = ConsumoAgua;
                        Console.WriteLine("2-ELSE-TarifaAgua: " + TarifaAguaTemp);
                        Console.WriteLine("2-ELSE-TarifaEsgoto: " + TarifaEsgotoTemp);
                    }
                }
                if((ConsumoAgua - count) > 0){
                    if((ConsumoAgua - count) >= 15){
                        TarifaAgua = 5.461; // Exemplo de tarifa para a faixa 15-20 m³
                        TarifaEsgoto = 2.731; // Exemplo de tarifa para a faixa 15-20 m³ de esgoto
                        count += 5;
                        TarifaAguaTemp += 5 * TarifaAgua;
                        TarifaEsgotoTemp += 5 * TarifaEsgoto;
                        Console.WriteLine("3-IF-TarifaAgua: " + TarifaAguaTemp);
                        Console.WriteLine("3-IF-TarifaEsgoto: " + TarifaEsgotoTemp);

                    } else {
                        TarifaAgua = 5.461; // Exemplo de tarifa para a faixa 15-20 m³
                        TarifaEsgoto = 2.731; // Exemplo de tarifa para a faixa 15-20 m³ de esgoto
                        TarifaAguaTemp += (ConsumoAgua - count) * TarifaAgua;
                        TarifaEsgotoTemp += (ConsumoAgua - count) * TarifaEsgoto;
                        count = ConsumoAgua;
                        Console.WriteLine("3-ELSE-TarifaAgua: " + TarifaAguaTemp);
                        Console.WriteLine("3-ELSE-TarifaEsgoto: " + TarifaEsgotoTemp);
                    }
                }
                if((ConsumoAgua - count) > 0){
                    if((ConsumoAgua - count) >= 20){
                        TarifaAgua = 5.487; // Exemplo de tarifa para a faixa 20-40 m³
                        TarifaEsgoto = 2.744; // Exemplo de tarifa para a faixa 20-40 m³ de esgoto
                        count += 20;
                        TarifaAguaTemp += 20 * TarifaAgua;
                        TarifaEsgotoTemp += 20 * TarifaEsgoto;
                        Console.WriteLine("4-IF-TarifaAgua: " + TarifaAguaTemp);
                        Console.WriteLine("4-IF-TarifaEsgoto: " + TarifaEsgotoTemp);
                    } else {
                        TarifaAgua = 5.487; // Exemplo de tarifa para a faixa 20-40 m³
                        TarifaEsgoto = 2.744; // Exemplo de tarifa para a faixa 20-40 m³ de esgoto
                        TarifaAguaTemp += (ConsumoAgua - count) * TarifaAgua;
                        TarifaEsgotoTemp += (ConsumoAgua - count) * TarifaEsgoto;
                        count = ConsumoAgua;
                        Console.WriteLine("4-ELSE-TarifaAgua: " + TarifaAguaTemp);
                        Console.WriteLine("4-ELSE-TarifaEsgoto: " + TarifaEsgotoTemp);
                    }
                } 
                if((ConsumoAgua - count) > 0){
                    TarifaAgua = 10.066; // Exemplo de tarifa para a faixa +40 m³
                    TarifaEsgoto = 5.035; // Exemplo de tarifa para a faixa +40 m³ de esgoto
                    TarifaAguaTemp += (ConsumoAgua - count) * TarifaAgua;
                    TarifaEsgotoTemp += (ConsumoAgua - count) * TarifaEsgoto;
                    Console.WriteLine("5-IF-TarifaAgua: " + TarifaAguaTemp);
                    Console.WriteLine("5-IF-TarifaEsgoto: " + TarifaEsgotoTemp);
                }
                ConsumoEsgoto = ConsumoAgua;
            }
            else {
                TarifaAgua = 10.08; // Tarifa fixa para a faixa 0-6 m³
                TarifaEsgoto = 5.05; // Tarifa fixa para a faixa 0-6 m³ de esgoto
                ConsumoAgua = 1;
				ConsumoEsgoto = ConsumoAgua;
            }
        }
        else if (Consumidor?.Tipo == TipoConsumidor.Comercial)
        {
            if (ConsumoAgua <= 6)
            {
                TarifaAgua = 25.79; // Exemplo de tarifa para a faixa 0-6 m³ para Comercial
                TarifaEsgoto = 12.90; // Exemplo de tarifa para a faixa 0-6 m³ de esgoto para Comercial
                ConsumoEsgoto = ConsumoAgua;
            }
            else if (ConsumoAgua > 6 && ConsumoAgua <= 10)
            {
                TarifaAgua = 4.299; // Exemplo de tarifa para a faixa 6-10 m³ para Comercial
                TarifaEsgoto = 2.149; // Exemplo de tarifa para a faixa 6-10 m³ de esgoto para Comercial
                ConsumoEsgoto = ConsumoAgua;
            }
            else if (ConsumoAgua > 10 && ConsumoAgua <= 40)
            {
                TarifaAgua = 8.221; // Exemplo de tarifa para a faixa 10-40 m³ para Comercial
                TarifaEsgoto = 4.111; // Exemplo de tarifa para a faixa 10-40 m³ de esgoto para Comercial
                ConsumoEsgoto = ConsumoAgua;
            }
            else if (ConsumoAgua > 40 && ConsumoAgua <= 100)
            {
                TarifaAgua = 8.288; // Exemplo de tarifa para a faixa 40-100 m³ para Comercial
                TarifaEsgoto = 4.144; // Exemplo de tarifa para a faixa 40-100 m³ de esgoto para Comercial
                ConsumoEsgoto = ConsumoAgua;
            }
            else if (ConsumoAgua > 100)
            {
                TarifaAgua = 8.329; // Exemplo de tarifa para a faixa +100 m³ para Comercial
                TarifaEsgoto = 4.165; // Exemplo de tarifa para a faixa +100 m³ de esgoto para Comercial
                ConsumoEsgoto = ConsumoAgua;
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
    static void Main()
    {
        string caminhoArquivo = "Contas/Arquivo.txt";

        try
        {
            string[] linhas = File.ReadAllLines(caminhoArquivo);

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

                Console.WriteLine(contaAgua);

                ContaEnergia contaEnergia = new ContaEnergia
                {
                    Consumidor = consumidor,
                    LeituraMesAnterior = double.Parse(dados[5]),
                    LeituraMesAtual = double.Parse(dados[6])
                };

                contaEnergia.CalcularConta();

                Console.WriteLine(contaEnergia);

                Console.WriteLine();
            }
        }
        catch (IOException e)
        {
            Console.WriteLine($"Ocorreu um erro ao ler o arquivo: {e.Message}");
        }
    }
}
