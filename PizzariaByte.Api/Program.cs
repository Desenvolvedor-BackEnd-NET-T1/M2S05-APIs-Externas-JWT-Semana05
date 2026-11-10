using PizzariaByte.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Os endereços das APIs ficam no appsettings.json
var config = builder.Configuration;

// Aula 1. O .NET cria e reaproveita os HttpClient
// (IHttpClientFactory) e entrega ao CepService.
builder.Services.AddHttpClient<CepService>(c =>
{
    c.BaseAddress = new Uri(config["Apis:ViaCep"]);
    c.Timeout = TimeSpan.FromSeconds(10);
});

// Aula 3. A transportadora, que exige token
builder.Services.AddHttpClient<MotoByteService>(c =>
{
    c.BaseAddress = new Uri(config["MotoByte:Url"]);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
