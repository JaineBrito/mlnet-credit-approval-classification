# ML.NET Credit Approval Classification

Idioma: **Portugues** | [English](README.en.md)

Projeto de estudo em C# com ML.NET para classificar aprovacao de credito (sim/nao).

O objetivo e praticar o fluxo completo de Machine Learning no .NET:
- carregar dados;
- treinar modelo de classificacao binaria;
- avaliar metricas;
- executar AutoML;
- salvar/carregar modelo;
- prever novos exemplos.

## Estrutura do projeto

- `mlnet-credit-approval-classification`
  Biblioteca com a logica de ML (treino, avaliacao, persistencia e predicao).
- `mlnet-credit-approval-classification-console`
  Aplicacao de console que executa o fluxo completo.

### Arquivos principais

- `mlnet-credit-approval-classification-console/Program.cs`
  Orquestra todo o processo (treino -> avaliacao -> AutoML -> salvar -> prever).
- `mlnet-credit-approval-classification/ML/CreditoModelTrainer.cs`
  Contem os metodos de treino, avaliacao, AutoML e salvamento do modelo.
- `mlnet-credit-approval-classification/ML/CreditoModelPrediction.cs`
  Carrega o modelo salvo e faz previsoes.
- `mlnet-credit-approval-classification/Models/CreditoInputData.cs`
  Define as colunas de entrada do CSV e o rotulo (`Aprovado`).
- `mlnet-credit-approval-classification/Models/CreditoPredictionResult.cs`
  Define a saida da predicao (`PredicaoAprovacao` e `Probalility`).

## Como os dados sao lidos

O dataset CSV e carregado por `LoadFromTextFile<CreditoInputData>()` com:
- cabecalho (`hasHeader: true`);
- separador virgula (`separatorChar: ','`).

Mapeamento de colunas no `CreditoInputData`:
- coluna `0`: `RendaMensal`;
- coluna `1`: `EstadoCivil`;
- coluna `2`: `NumeroDependentes`;
- coluna `3`: `PossuiVeiculo`;
- coluna `4`: `JaNegadoAntes`;
- coluna `5`: `Aprovado` (rotulo da classificacao).

## Pipeline de treino

No `CreditoModelTrainer`:

1. Concatena os campos de entrada em `Features`.
2. Treina classificacao binaria com `LbfgsLogisticRegression`.
3. Gera previsoes sobre os dados.
4. Calcula metricas:
   - `Accuracy`;
   - `PositivePrecision`;
   - `PositiveRecall`;
   - `F1Score`.

## AutoML

O metodo `AvaliarMelhorModelo()` roda um experimento de classificacao binaria por 60 segundos:
- testa algoritmos automaticamente;
- escolhe o melhor `BestRun`;
- imprime nome do treinador e metricas;
- atualiza `modeloTreinado` para o melhor modelo encontrado.

## Salvar e carregar modelo

- `SalvarModelo(path)` salva o modelo treinado em arquivo `.zip`.
- `CarregarModelo(path)` recupera esse modelo para uso futuro.

Isso permite separar treino e inferencia.

## Predicao

A predicao e feita com:
- `CreatePredictionEngine<CreditoInputData, CreditoPredictionResult>()`
- `Predict(novoCredito)`

Exemplo do projeto:
- entrada: cliente com renda, estado civil, dependentes, veiculo e historico;
- saida: `PredicaoAprovacao` e probabilidade da classe positiva.

## Como executar

Na raiz do repositorio:

```bash
dotnet restore "mlnet-credit-approval-classification.sln"
dotnet build "mlnet-credit-approval-classification.sln" -c Debug
dotnet run --project "mlnet-credit-approval-classification-console/mlnet-credit-approval-classification-console.csproj"
```

## Dependencias

No projeto de biblioteca:
- `Microsoft.ML`
- `Microsoft.ML.LightGbm`
- `Microsoft.ML.AutoML`

## Observacoes de estudo

- O projeto e focado em aprendizado, nao em producao.
- Para evoluir, voce pode:
  - fazer `TrainTestSplit`;
  - usar validacao cruzada;
  - testar normalizacao e engenharia de atributos;
  - comparar modelos manuais com o resultado do AutoML.