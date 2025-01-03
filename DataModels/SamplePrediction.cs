using Microsoft.ML.Data;

namespace RMP.ML.DataModels;

public class SamplePrediction
{
    [ColumnName("PredictedLabel")]
    public bool IsToxic { get; set; }

    [ColumnName("Score")]
    public float Score { get; set; }
}