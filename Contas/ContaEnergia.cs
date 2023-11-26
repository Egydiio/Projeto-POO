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