using ApiPrueba.src.Api.Dtos;
using ApiPrueba.src.Application.Dtos;
using ApiPrueba.src.Application.Interfaces;
using ApiPrueba.src.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiPrueba.src.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TituloController : ControllerBase
{
    private readonly ITituloService _tituloService;
    private readonly IService<Titulo, int> _titulos;
    private readonly IMapper _mapper;

    public TituloController(IService<Titulo, int> titulos, IMapper mapper, ITituloService tituloService)
    {
        _titulos = titulos;
        _mapper = mapper;
        _tituloService = tituloService;
    }

  
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _titulos.GetAllAsync();
        var response = _mapper.Map<IEnumerable<TituloResponse>>(result);
        return Ok(response);
    }

  
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (id <= 0)
            return BadRequest("El ID debe ser un número positivo.");

        var result = await _titulos.GetByIdAsync(id);
        var response = _mapper.Map<TituloResponse>(result);
        return Ok(response);
    }

    
    [HttpGet("buscar")]
    public async Task<IActionResult> Buscar(
        [FromQuery] string? nombre,
        [FromQuery] string? tipo,
        [FromQuery] string? genero,
        [FromQuery] int? año,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string? sortBy = "nombre",
        [FromQuery] string sortDir = "asc")
    {
        if (page <= 0) page = 1;
        if (pageSize <= 0 || pageSize > 200) pageSize = 20;

        var desc = sortDir.Equals("desc", StringComparison.OrdinalIgnoreCase);

        var (items, total) = await _tituloService.BuscarPaginadoAsync(
            nombre, tipo, genero, año,
            page, pageSize, sortBy, desc
        );

        var response = _mapper.Map<IEnumerable<TituloResponse>>(items);

        return Ok(new
        {
            total,
            page,
            pageSize,
            sortBy,
            sortDir = desc ? "desc" : "asc",
            items = response
        });
    }

   

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TituloCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var entity = _mapper.Map<Titulo>(dto);
        var created = await _titulos.CreateAsync(entity);
        var response = _mapper.Map<TituloResponse>(created);

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

  
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] TituloUpdateDto dto)
    {
        if (id <= 0)
            return BadRequest("El ID debe ser un número positivo.");

        var existing = await _titulos.GetByIdAsync(id);
        if (existing == null)
            return NotFound($"Título con ID {id} no encontrado.");

        _mapper.Map(dto, existing);
        await _titulos.UpdateAsync(existing);

        return Ok(new { message = $"Título con ID {id} actualizado correctamente." });
    }

    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
            return BadRequest("El ID debe ser un número positivo.");

        await _titulos.DeleteAsync(id);
        return Ok(new { message = $"Título con ID {id} eliminado correctamente." });
    }
}