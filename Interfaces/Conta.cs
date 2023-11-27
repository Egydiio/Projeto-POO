interface Conta
{
    Consumidor Consumidor { get; set; }
    string Mes { get; set; }
    string TipoImovel { get; set; }
    double LeituraMesAnterior { get; set; }
    double LeituraMesAtual { get; set; }
    double Consumo { get; set; }
    double ValorSemImposto { get; set; }
    void CalcularConta();
}