using System;
using Microsoft.ML.Data;

namespace MachineLearning.Models;

public class CreditoInputData
{
    [LoadColumnAttribute(0)]
    public float RendaMensal { get; set; }
    [LoadColumnAttribute(1)]
    public float EstadoCivil { get; set; }
    // 0 - Solteiro, 1 - Casado, 2 - Divorciado, 3 - Viúvo, 4 - Uniao Estavel   
    [LoadColumnAttribute(2)]
    public float NumeroDependentes { get; set; }

    [LoadColumnAttribute(3)]
    public float PossuiVeiculo { get; set; }

    [LoadColumnAttribute(4)]
    public float JaNegadoAntes { get; set; }    

    [LoadColumnAttribute(5)]
    public bool Aprovado { get; set; }

}
