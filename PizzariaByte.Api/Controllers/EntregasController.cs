using Microsoft.AspNetCore.Mvc;
using PizzariaByte.Api.Models;
using PizzariaByte.Api.Services;

namespace PizzariaByte.Api.Controllers;

// Aula 3. A pizzaria chama duas APIs externas: ViaCEP e MotoByte
[ApiController]
[Route("api/entregas")]
public class EntregasController : ControllerBase
{
    private readonly CepService _cep;
    private readonly MotoByteService _motoByte;

    public EntregasController(CepService cep, MotoByteService motoByte)
    {
        _cep = cep;
        _motoByte = motoByte;
    }

    [HttpGet]
    public async Task<ActionResult<List<Entrega>>> Listar()
    {
        return await _motoByte.ListarAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Criar(NovaEntrega nova)
    {
        // 1. O CEP existe? (ViaCEP)
        Endereco endereco =
            await _cep.BuscarAsync(nova.Cep);
        if (endereco is null)
            return BadRequest("CEP não encontrado");

        // 2. Chama o motoboy (MotoByte, com token)
        try
        {
            Entrega entrega =
                await _motoByte.CriarAsync(nova);
            return Ok(new { entrega, endereco });
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(502, ex.Message);
        }
    }
}
