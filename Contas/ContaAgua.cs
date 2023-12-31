using System.Runtime.CompilerServices;

public class ContaAgua : Conta
{
    public Consumidor? Consumidor { get; set; }
    public double LeituraMesAnterior { get; set; }
    public double LeituraMesAtual { get; set; }
    public double Consumo { get; set; }
    public double TarifaAgua { get; set; }
    public double TarifaEsgoto { get; set; }
    public double Cofins { get; set; }
    public double ValorTotal { get; set; }
    public double count { get; set; }
    public double TarifaAguaTemp { get; set; }
    public double TarifaEsgotoTemp { get; set; }
    public string TipoImovel { get; set; }
    public double ValorSemImposto { get; set; }



    public void CalcularConta(double Consumo, string tipo)
    {
        count = 0;

        // Lógica de cálculo da tarifa escalonada para água
        if (tipo == "residencial")
        {
            if (Consumo >= 6)
            {
                if(Consumo >= 10){
                    TarifaAgua = 2.241; // Exemplo de tarifa para a faixa 6-10 m³
                    TarifaEsgoto = 1.122; // Exemplo de tarifa para a faixa 6-10 m³ de esgoto
                    count += 10;
                    TarifaAguaTemp += 10 * TarifaAgua;
                    TarifaEsgotoTemp += 10 * TarifaEsgoto;
                } else {
                    TarifaAgua = 2.241; // Exemplo de tarifa para a faixa 6-10 m³
                    TarifaEsgoto = 1.122; // Exemplo de tarifa para a faixa 6-10 m³ de esgoto
                    TarifaAguaTemp += Consumo * TarifaAgua;
                    TarifaEsgotoTemp += Consumo * TarifaEsgoto;
                    count = Consumo;
                }
                if((Consumo - count) > 0){
                    if((Consumo - count) >= 5){
                        TarifaAgua = 5.447; // Exemplo de tarifa para a faixa 10-15 m³
                        TarifaEsgoto = 2.724; // Exemplo de tarifa para a faixa 10-15 m³ de esgoto
                        count += 5;
                        TarifaAguaTemp += 5 * TarifaAgua;
                        TarifaEsgotoTemp += 5 * TarifaEsgoto;
                    } else {
                        TarifaAgua = 5.447; // Exemplo de tarifa para a faixa 10-15 m³
                        TarifaEsgoto = 2.724; // Exemplo de tarifa para a faixa 10-15 m³ de esgoto
                        TarifaAguaTemp += (Consumo - count) * TarifaAgua;
                        TarifaEsgotoTemp += (Consumo - count) * TarifaEsgoto;
                        count = Consumo;
                    }
                }
                if((Consumo - count) > 0){
                    if((Consumo - count) >= 5){
                        TarifaAgua = 5.461; // Exemplo de tarifa para a faixa 15-20 m³
                        TarifaEsgoto = 2.731; // Exemplo de tarifa para a faixa 15-20 m³ de esgoto
                        count += 5;
                        TarifaAguaTemp += 5 * TarifaAgua;
                        TarifaEsgotoTemp += 5 * TarifaEsgoto;
                    } else {
                        TarifaAgua = 5.461; // Exemplo de tarifa para a faixa 15-20 m³
                        TarifaEsgoto = 2.731; // Exemplo de tarifa para a faixa 15-20 m³ de esgoto
                        TarifaAguaTemp += (Consumo - count) * TarifaAgua;
                        TarifaEsgotoTemp += (Consumo - count) * TarifaEsgoto;
                        count = Consumo;
                    }
                }
                if((Consumo - count) > 0){
                    if((Consumo - count) >= 20){
                        TarifaAgua = 5.487; // Exemplo de tarifa para a faixa 20-40 m³
                        TarifaEsgoto = 2.744; // Exemplo de tarifa para a faixa 20-40 m³ de esgoto
                        count += 20;
                        TarifaAguaTemp += 20 * TarifaAgua;
                        TarifaEsgotoTemp += 20 * TarifaEsgoto;
                    } else {
                        TarifaAgua = 5.487; // Exemplo de tarifa para a faixa 20-40 m³
                        TarifaEsgoto = 2.744; // Exemplo de tarifa para a faixa 20-40 m³ de esgoto
                        TarifaAguaTemp += (Consumo - count) * TarifaAgua;
                        TarifaEsgotoTemp += (Consumo - count) * TarifaEsgoto;
                        count = Consumo;
                    }
                } 
                if((Consumo - count) > 0){
                    TarifaAgua = 10.066; // Exemplo de tarifa para a faixa +40 m³
                    TarifaEsgoto = 5.035; // Exemplo de tarifa para a faixa +40 m³ de esgoto
                    TarifaAguaTemp += (Consumo - count) * TarifaAgua;
                    TarifaEsgotoTemp += (Consumo - count) * TarifaEsgoto;
                }
            }
            else {
                TarifaAgua = 10.08; // Tarifa fixa para a faixa 0-6 m³
                TarifaEsgoto = 5.05; // Tarifa fixa para a faixa 0-6 m³ de esgoto
            }
        }
        else if (tipo == "comercial")
        {
            if (Consumo >= 6)
            {
                if(Consumo >= 10){
                    TarifaAgua = 4.299; // Exemplo de tarifa para a faixa 6-10 m³
                    TarifaEsgoto = 2.149; // Exemplo de tarifa para a faixa 6-10 m³ de esgoto
                    count += 10;
                    TarifaAguaTemp += 10 * TarifaAgua;
                    TarifaEsgotoTemp += 10 * TarifaEsgoto;
                } else {
                    TarifaAgua = 4.299; // Exemplo de tarifa para a faixa 6-10 m³
                    TarifaEsgoto = 2.149; // Exemplo de tarifa para a faixa 6-10 m³ de esgoto
                    TarifaAguaTemp += Consumo * TarifaAgua;
                    TarifaEsgotoTemp += Consumo * TarifaEsgoto;
                    count = Consumo;
                }
                if((Consumo - count) > 0){
                    if((Consumo - count) >= 30){
                        TarifaAgua = 8.221; // Exemplo de tarifa para a faixa 10-40 m³
                        TarifaEsgoto = 4.111; // Exemplo de tarifa para a faixa 10-40 m³ de esgoto
                        count += 30;
                        TarifaAguaTemp += 30 * TarifaAgua;
                        TarifaEsgotoTemp += 30 * TarifaEsgoto;
                    } else {
                        TarifaAgua = 8.221; // Exemplo de tarifa para a faixa 10-40 m³
                        TarifaEsgoto = 4.111; // Exemplo de tarifa para a faixa 10-40 m³ de esgoto
                        TarifaAguaTemp += (Consumo - count) * TarifaAgua;
                        TarifaEsgotoTemp += (Consumo - count) * TarifaEsgoto;
                        count = Consumo;
                    }
                }
                if((Consumo - count) > 0){
                    if((Consumo - count) >= 60){
                        TarifaAgua = 8.288; // Exemplo de tarifa para a faixa 40-100 m³
                        TarifaEsgoto = 4.144; // Exemplo de tarifa para a faixa 40-100 m³ de esgoto
                        count += 60;
                        TarifaAguaTemp += 60 * TarifaAgua;
                        TarifaEsgotoTemp += 60 * TarifaEsgoto;
                    } else {
                        TarifaAgua = 8.288; // Exemplo de tarifa para a faixa 40-100 m³
                        TarifaEsgoto = 4.144; // Exemplo de tarifa para a faixa 40-100 m³ de esgoto
                        TarifaAguaTemp += (Consumo - count) * TarifaAgua;
                        TarifaEsgotoTemp += (Consumo - count) * TarifaEsgoto;
                        count = Consumo;
                    }
                }
                if((Consumo - count) > 0){
                    TarifaAgua = 8.329; // Exemplo de tarifa para a faixa +100 m³
                    TarifaEsgoto = 4.165; // Exemplo de tarifa para a faixa +100 m³ de esgoto
                    TarifaAguaTemp += (Consumo - count) * TarifaAgua;
                    TarifaEsgotoTemp += (Consumo - count) * TarifaEsgoto;
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
        GetTotalSemImposto.SomaTotalSemImposto += ValorTotal - Cofins;

        Console.WriteLine("Valor Total Agua: {0:F2}" , ValorTotal);

    }

    public override string ToString()
    {
        return $"Consumidor: {Consumidor?.Id}, Tipo: {Consumidor?.Tipo}, Valor Total Água: {ValorTotal:C}";
    }

    public void calcularConsumo(){
        try
        {
            string[] linhas = File.ReadAllLines("Tabelas/ContaAgua.txt");

            if (linhas.Length == 0)
            {
                Console.WriteLine($"Erro: O arquivo está vazio.");
            } else
            {
                int id = Program.UsuarioLogado;
                int qualLinha = 0;
                for(int i = 0; i < linhas.Length; i++){
                    string[] temp = linhas[i].Split(',');
                    if(int.Parse(temp[5]) == id){
                        qualLinha = i;
                    }
                }
                string[] splitada = linhas[qualLinha].Split(",");
                double anterior = double.Parse(splitada[3]);
                double atual = double.Parse(splitada[4]);
                double consumo = atual - anterior;
                Console.WriteLine("Consumo Agua: " + consumo);
            }
        } catch (IOException e)
        {
            Console.WriteLine($"Ocorreu um erro ao ler o arquivo: {e.Message}");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine($"Erro: Sem permissão para acessar o arquivo.");
        }
    }

    public double calcularTotal(){
        try
        {
            string[] linhas = File.ReadAllLines("Tabelas/ContaAgua.txt");

            if (linhas.Length == 0)
            {
                Console.WriteLine($"Erro: O arquivo está vazio.");
                return 0;
            } else
            {
                int id = Program.UsuarioLogado;
                int qualLinha = 0;
                for(int i = 0; i < linhas.Length; i++){
                    string[] temp = linhas[i].Split(',');
                    if(int.Parse(temp[5]) == id){
                        qualLinha = i;
                    }
                }
                string[] splitada = linhas[qualLinha].Split(",");
                double anterior = double.Parse(splitada[3]);
                double atual = double.Parse(splitada[4]);
                string tipo = splitada[2];
                double consumo = atual - anterior;

                CalcularConta(consumo, tipo);

                return ValorTotal;
            }
        } catch (Exception ex)
        {
            Console.WriteLine($"Ocorreu um erro ao ler o arquivo: {ex.Message}");
            return 0;
        }
    }

}