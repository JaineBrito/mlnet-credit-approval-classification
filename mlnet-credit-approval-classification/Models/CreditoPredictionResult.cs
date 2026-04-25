using System;
using Microsoft.ML.Data;

namespace MachineLearning.Models;

public class CreditoPredictionResult
{
    [ColumnName("PredictedLabel")]
    public bool PredicaoAprovacao { get; set; }

    [ColumnName("Probability")]
    public float Probalility { get; set; }

}
