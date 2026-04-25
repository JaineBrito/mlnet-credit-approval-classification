using MachineLearning.ML;
using MachineLearning.Models;

ProjetoClassificacaoBinaria();

void ProjetoClassificacaoBinaria()
{
    var trainer = new CreditoModelTrainer();
    trainer.CarregarDadosCSV(Path.Combine(AppContext.BaseDirectory, "aprovacao_credito.csv" ));
    trainer.TreinarModelo();
    trainer.AvaliarModelo();
    trainer.AvaliarMelhorModelo();

    var pathModelo = Path.Combine(AppContext.BaseDirectory, "modelo_treinado_classificacao_binaria.zip");
    trainer.SalvarModelo(pathModelo);

    var predictor = new CreditoModelPredictor();
    predictor.CarregarModelo(pathModelo);

    var novoCredito = new CreditoInputData()
    {
        RendaMensal = 4200f,
        EstadoCivil = 1,
        NumeroDependentes = 2,
        PossuiVeiculo = 1,
        JaNegadoAntes = 0
    };
    var resultado = predictor.Prever(novoCredito);

    Console.WriteLine($"Aprovado? {(resultado.PredicaoAprovacao ? "Sim" : "Não")}");
    Console.WriteLine($"Probablilidade: {resultado.Probalility:P2}");
    
}

