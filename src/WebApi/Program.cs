var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddWebServices();
builder.Services.AddApplicationServices();
builder.Services.AddHttpClientServices(configuration);

var app = builder.Build();

app.UseExceptionHandler(options => { });

// app.MapGet("/", (_) => throw new NotFoundException());


app.MapEndpoints();


app.Run();

