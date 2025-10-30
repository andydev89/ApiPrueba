using ApiPrueba.src.Api.Dtos;
using ApiPrueba.src.Application.Dtos;
using ApiPrueba.src.Application.Interfaces;
using ApiPrueba.src.Application.Services;
using ApiPrueba.src.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiPrueba.src.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ListaSeguimientoController : ControllerBase
{
    private readonly IListaSeguimientoService _listas;
    private readonly IMapper _mapper;

    public ListaSeguimientoController(IListaSeguimientoService listas, IMapper mapper)
    {
        _listas = listas;
        _mapper = mapper;
    }

    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _listas.GetAllAsync();
        var response = _mapper.Map<IEnumerable<ListaSeguimientoResponse>>(result);
        return Ok(response);
    }

    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _listas.GetByIdAsync(id);
        var response = _mapper.Map<ListaSeguimientoResponse>(result);
        return Ok(response);
    }

   
    [HttpGet("usuario/{usuarioId:int}")]
    public async Task<IActionResult> GetByUsuario(int usuarioId)
    {
        var result = await _listas.GetByUsuarioAsync(usuarioId);
        var response = _mapper.Map<IEnumerable<ListaSeguimientoResponse>>(result);
        return Ok(response);
    }

   
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ListaSeguimientoCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var entity = _mapper.Map<ListaSeguimiento>(dto);
        var created = await _listas.CreateAsync(entity);
        var response = _mapper.Map<ListaSeguimientoResponse>(created);

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ListaSeguimientoUpdateDto dto)
    {
        if (id <= 0)
            return BadRequest("El ID debe ser un número positivo.");

        var existing = await _listas.GetByIdAsync(id);
        if (existing == null)
            return NotFound($"Lista con ID {id} no encontrada.");

        _mapper.Map(dto, existing);
        await _listas.UpdateAsync(existing);

        return Ok(new { message = $"Lista con ID {id} actualizada correctamente." });
    }

    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _listas.DeleteAsync(id);
        return Ok(new { message = $"Lista con ID {id} eliminada correctamente." });
    }

   
    [HttpPost("{listaId:int}/titulos/{tituloId:int}")]
    public async Task<IActionResult> AddTitulo(int listaId, int tituloId)
    {
        await _listas.AgregarTituloAsync(listaId, tituloId);
        return Ok(new { message = "Título agregado correctamente" });
    }

    [HttpDelete("{listaId:int}/titulos/{tituloId:int}")]
    public async Task<IActionResult> RemoveTitulo(int listaId, int tituloId)
    {
        await _listas.EliminarTituloAsync(listaId, tituloId);
        return Ok(new { message = "Título eliminado correctamente de la lista" });
    }
}

