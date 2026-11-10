using Microsoft.AspNetCore.Mvc;
using PizzariaByte.Api.Models;
using PizzariaByte.Api.Services;

namespace PizzariaByte.Api.Controllers;

[ApiController]
[Route("api/enderecos")]
public class EnderecosController : ControllerBase
{
    private readonly CepService _cep;

    public EnderecosController(CepService cep)
    {
        _cep = cep;
    }

    // Aula 1. GET api/enderecos/88010000/json
    [HttpGet("{cep}/json")]
    public async Task<IActionResult> BuscarJson(string cep)
    {
        try
        {
            string json = await _cep.BuscarJsonAsync(cep);
            return Content(json, "application/json");
        }
        catch (HttpRequestException)
        {
            // 502: o erro foi no serviço de terceiros
            return StatusCode(502, "ViaCEP indisponível");
        }
    }

    // Aula 2. GET api/enderecos/88010000
    [HttpGet("{cep}")]
    public async Task<ActionResult<Endereco>> Buscar(string cep)
    {
        Endereco endereco = await _cep.BuscarAsync(cep);
        if (endereco is null)
            return NotFound($"CEP {cep} não encontrado");
        return endereco;
    }
}
