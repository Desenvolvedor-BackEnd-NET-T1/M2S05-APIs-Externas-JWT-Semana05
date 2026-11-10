// MotoByte: a API da transportadora parceira da Pizzaria Byte.
//
// Ela faz o papel de uma API EXTERNA. Na aula, os alunos não precisam
// abrir este arquivo: ela só precisa estar rodando (dotnet run) em
// http://localhost:5300. O código fica aqui para quem tiver curiosidade.
//
// Rotas públicas:   GET  api/frete/{cep}       (aula 1)
//                   POST api/cotacoes          (aula 2)
//                   GET  api/status/lento      (aula 1, timeout)
//                   GET  ws/{cep}/json/        (cópia do ViaCEP para usar sem internet)
// Token (OAuth):    POST oauth/token           (aula 3, client credentials)
// Rotas protegidas: GET  api/entregas, GET api/entregas/{codigo}, POST api/entregas
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PizzariaByte.MotoByte;

var builder = WebApplication.CreateBuilder(args);

// A MotoByte responde em snake_case (valor_frete, prazo_minutos),
// como muitas APIs reais. É o que motiva o [JsonProperty] na aula 2.
builder.Services.ConfigureHttpJsonOptions(o =>
{
    o.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
    o.SerializerOptions.PropertyNameCaseInsensitive = true;
});

string emissor = builder.Configuration["Jwt:Emissor"]!;
string publico = builder.Configuration["Jwt:Publico"]!;
var chave = new SymmetricSecurityKey(
    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Chave"]!))
{
    KeyId = "motobyte-2026",
};

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.MapInboundClaims = false;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = emissor,
            ValidAudience = publico,
            IssuerSigningKey = chave,
            ClockSkew = TimeSpan.Zero,
        };
    });
builder.Services.AddAuthorization(o =>
    o.AddPolicy("entregas", p => p.RequireClaim("scope", "entregas")));

builder.Services.AddSingleton<Entregas>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "MotoByte no ar. Veja as rotas no README do projeto.");

// ---------------------------------------------------------------- públicas

app.MapGet("/api/frete/{cep}", (string cep) =>
{
    Frete? frete = Tabela.Calcular(cep, pizzas: 1);
    return frete is null
        ? Results.NotFound(new { mensagem = "CEP fora da área de entrega" })
        : Results.Ok(frete);
});

app.MapPost("/api/cotacoes", (PedidoCotacao pedido) =>
{
    if (pedido.Pizzas <= 0)
        return Results.BadRequest(new { mensagem = "Informe a quantidade de pizzas" });
    Frete? frete = Tabela.Calcular(pedido.Cep ?? "", pedido.Pizzas);
    return frete is null
        ? Results.NotFound(new { mensagem = "CEP fora da área de entrega" })
        : Results.Ok(frete);
});

app.MapGet("/api/status/lento", async () =>
{
    await Task.Delay(TimeSpan.FromSeconds(10));
    return Results.Ok(new { mensagem = "Respondi, mas demorei 10 segundos" });
});

// Mesmo formato do ViaCEP, para a aula funcionar sem internet.
app.MapGet("/ws/{cep}/json", (string cep) =>
{
    cep = cep.Replace("-", "");
    if (cep.Length != 8 || !cep.All(char.IsDigit))
        return Results.BadRequest();
    return ViaCepOffline.Enderecos.TryGetValue(cep, out string? json)
        ? Results.Text(json, "application/json")
        : Results.Text("{\n  \"erro\": \"true\"\n}", "application/json");
});

// ---------------------------------------------------------------- OAuth 2.0

// Fluxo client credentials: o sistema cliente se identifica com
// client_id e client_secret e recebe um access token (JWT).
app.MapPost("/oauth/token", async (HttpRequest req) =>
{
    if (!req.HasFormContentType)
        return Results.BadRequest(new { error = "invalid_request" });
    IFormCollection form = await req.ReadFormAsync();

    if (form["grant_type"] != "client_credentials")
        return Results.BadRequest(new { error = "unsupported_grant_type" });

    string id = form["client_id"].ToString();
    string segredo = form["client_secret"].ToString();
    string? esperado = app.Configuration[$"Clientes:{id}:Segredo"];
    if (esperado is null || esperado != segredo)
        return Results.Json(new { error = "invalid_client" }, statusCode: 401);

    string nome = app.Configuration[$"Clientes:{id}:Nome"]!;
    DateTime agora = DateTime.UtcNow;
    var token = new JwtSecurityToken(
        issuer: emissor,
        audience: publico,
        claims: new[]
        {
            new Claim("sub", id),
            new Claim("name", nome),
            new Claim("scope", "entregas"),
        },
        notBefore: agora,
        expires: agora.AddHours(1),
        signingCredentials: new SigningCredentials(chave, SecurityAlgorithms.HmacSha256));

    return Results.Ok(new
    {
        access_token = new JwtSecurityTokenHandler().WriteToken(token),
        token_type = "Bearer",
        expires_in = 3600,
        scope = "entregas",
    });
});

// ---------------------------------------------------------------- protegidas

var entregas = app.MapGroup("/api/entregas").RequireAuthorization("entregas");

entregas.MapGet("/", (ClaimsPrincipal user, Entregas banco) =>
    banco.DoCliente(user.FindFirst("sub")!.Value));

entregas.MapGet("/{codigo}", (string codigo, ClaimsPrincipal user, Entregas banco) =>
{
    Entrega? e = banco.Buscar(user.FindFirst("sub")!.Value, codigo);
    return e is null
        ? Results.NotFound(new { mensagem = $"Entrega {codigo} não encontrada" })
        : Results.Ok(e);
});

entregas.MapPost("/", (NovaEntrega nova, ClaimsPrincipal user, Entregas banco) =>
{
    if (string.IsNullOrWhiteSpace(nova.Cliente) || nova.Pizzas <= 0)
        return Results.BadRequest(new { mensagem = "Informe o cliente e a quantidade de pizzas" });
    Frete? frete = Tabela.Calcular(nova.Cep ?? "", nova.Pizzas);
    if (frete is null)
        return Results.NotFound(new { mensagem = "CEP fora da área de entrega" });
    Entrega e = banco.Criar(user.FindFirst("sub")!.Value, nova, frete);
    return Results.Created($"/api/entregas/{e.Codigo}", e);
});

app.Run();
