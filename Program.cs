using Microsoft.Extensions.ML;
using RMP.ML.DataModels;
using RMP.ML.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddPredictionEnginePool<SampleObservation, SamplePrediction>()
    .FromFile(builder.Configuration["MLModel:MLModelFilePath"]);

var app = builder.Build();

app.MapGrpcService<PredictionGrpcService>();

app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();