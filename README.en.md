# ML.NET Credit Approval Classification

Language: [Portugues](README.md) | **English**

Study project in C# with ML.NET to classify credit approval (yes/no).

The goal is to practice the full Machine Learning workflow in .NET:
- load data;
- train a binary classification model;
- evaluate metrics;
- run AutoML;
- save/load model artifacts;
- predict new examples.

## Project structure

- `mlnet-credit-approval-classification`
  Class library with ML logic (training, evaluation, persistence, and prediction).
- `mlnet-credit-approval-classification-console`
  Console app that runs the end-to-end workflow.

### Main files

- `mlnet-credit-approval-classification-console/Program.cs`
  Orchestrates the full process (train -> evaluate -> AutoML -> save -> predict).
- `mlnet-credit-approval-classification/ML/CreditoModelTrainer.cs`
  Contains training, evaluation, AutoML, and model-saving methods.
- `mlnet-credit-approval-classification/ML/CreditoModelPrediction.cs`
  Loads the saved model and performs predictions.
- `mlnet-credit-approval-classification/Models/CreditoInputData.cs`
  Defines CSV input columns and the label (`Aprovado`).
- `mlnet-credit-approval-classification/Models/CreditoPredictionResult.cs`
  Defines prediction output (`PredicaoAprovacao` and `Probalility`).

## How data is loaded

The CSV dataset is loaded with `LoadFromTextFile<CreditoInputData>()` using:
- header row (`hasHeader: true`);
- comma separator (`separatorChar: ','`).

Column mapping in `CreditoInputData`:
- column `0`: `RendaMensal`;
- column `1`: `EstadoCivil`;
- column `2`: `NumeroDependentes`;
- column `3`: `PossuiVeiculo`;
- column `4`: `JaNegadoAntes`;
- column `5`: `Aprovado` (classification label).

## Training pipeline

Inside `CreditoModelTrainer`:

1. Concatenate input fields into `Features`.
2. Train binary classification with `LbfgsLogisticRegression`.
3. Generate predictions on the dataset.
4. Compute metrics:
   - `Accuracy`;
   - `PositivePrecision`;
   - `PositiveRecall`;
   - `F1Score`.

## AutoML

`AvaliarMelhorModelo()` runs a binary classification AutoML experiment for 60 seconds:
- tests multiple algorithms automatically;
- selects the best run (`BestRun`);
- prints trainer name and metrics;
- updates `modeloTreinado` with the best model found.

## Save and load model

- `SalvarModelo(path)` saves the trained model to a `.zip` file.
- `CarregarModelo(path)` loads that model for future use.

This allows training and inference to be separated.

## Prediction

Prediction is done with:
- `CreatePredictionEngine<CreditoInputData, CreditoPredictionResult>()`
- `Predict(novoCredito)`

Project example:
- input: customer profile with income, marital status, dependents, vehicle ownership, and denial history;
- output: `PredicaoAprovacao` and positive-class probability.

## How to run

From repository root:

```bash
dotnet restore "mlnet-credit-approval-classification.sln"
dotnet build "mlnet-credit-approval-classification.sln" -c Debug
dotnet run --project "mlnet-credit-approval-classification-console/mlnet-credit-approval-classification-console.csproj"
```

## Dependencies

In the class library project:
- `Microsoft.ML`
- `Microsoft.ML.LightGbm`
- `Microsoft.ML.AutoML`

## Study notes

- This project is learning-focused, not production-ready.
- To evolve it further, you can:
  - add `TrainTestSplit`;
  - use cross-validation;
  - test normalization and feature engineering;
  - compare manual models with AutoML results.
