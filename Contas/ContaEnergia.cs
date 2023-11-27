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
    public string Mes { get; set; }
    public string TipoImovel { get; set; }
    public double ValorSemImposto { get; set; }

    public void CalcularConta(double Consumo, string tipo)
    {

        if (tipo == "residencial")
            Tarifa = 0.46;
        else if (tipo == "comercial")
            Tarifa = 0.41;

        ContribuicaoIluminacao = 13.25;

        ValorTotal = (Consumo * Tarifa) + ContribuicaoIluminacao;

        if (tipo == "residencial")
            if(Consumo < 90) {
                Imposto = 0; // Se o consumo de um consumidor residencial for abaixo de 90KW/h, há isenção do imposto.
            } else {
                Imposto = ValorTotal * 0.4285;
            }
        else if (tipo == "comercial")
            Imposto = ValorTotal * 0.2195;

        ValorTotal += Imposto;
        GetTotalSemImposto.SomaTotalSemImposto += ValorTotal - Imposto;

        Console.WriteLine("Valor Total Energia: {0:F2}" , ValorTotal);
    }

    public override string ToString()
    {
        return $"Consumidor: {Consumidor?.Id}, Tipo: {Consumidor?.Tipo}, Valor Total Energia: {ValorTotal:C}";
    }

    public void calcularConsumo(){
        string[] linhas = File.ReadAllLines("Tabelas/ContaEnergia.txt");
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
        Console.WriteLine("Consumo Energia: " + consumo);
    }

    public double calcularTotal(){
        string[] linhas = File.ReadAllLines("Tabelas/ContaEnergia.txt");
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
}