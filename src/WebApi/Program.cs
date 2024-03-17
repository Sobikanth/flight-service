var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddWebServices();
builder.Services.AddApplicationServices();
builder.Services.AddHttpClientServices(configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FlightService V1");
    c.RoutePrefix = "";
});


app.UseExceptionHandler(options => { });

app.MapControllers();

app.Run();