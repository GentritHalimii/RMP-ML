using Grpc.Core;
using Microsoft.Extensions.ML;
using RMP.ML.DataModels;
using RMP.ML.Extensions;

namespace RMP.ML.Services;

public class PredictionGrpcService(PredictionEnginePool<SampleObservation, SamplePrediction> predictionEnginePool)
    : RMP.ML.PredictionService.PredictionServiceBase
{
    public override Task<PredictionReply> PredictToxicity(PredictionRequest request, ServerCallContext context)
    {
        var sampleData = new SampleObservation { SentimentText = request.SentimentText };
        var prediction = predictionEnginePool.Predict(sampleData);
        
        var probability = CalculateMethods.CalculatePercentage(prediction.Score);
        var message = $"Prediction: Is Toxic?: '{prediction.IsToxic}' with {probability}% probability of toxicity for the text '{request.SentimentText}'";

        return Task.FromResult(new PredictionReply
        {
            IsToxic = prediction.IsToxic,
            Probability = probability,
            Message = message
        });
    }
}
