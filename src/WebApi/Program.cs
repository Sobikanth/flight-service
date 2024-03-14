using Infrastructure.FlightsHttpClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebServices();
builder.Services.AddApplicationServices();
builder.Services.AddHttpClientServices();

var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.MapEndpoints();


app.Run();
