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
public class ElementoListaController : ControllerBase
{
    private readonly IService<ElementoLista, int> _elementos;
    private readonly IMapper _mapper;

    public ElementoListaController(IService<ElementoLista, int> elementos, IMapper mapper)
    {
        _elementos = elementos;
        _mapper = mapper;
    }

    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _elementos.GetAllAsync();
        var response = _mapper.Map<IEnumerable<ElementoListaResponse>>(result);
        return Ok(response);
    }

    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (id <= 0)
            return BadRequest("El ID debe ser un número positivo.");

        var result = await _elementos.GetByIdAsync(id);
        var response = _mapper.Map<ElementoListaResponse>(result);
        return Ok(response);
    }

    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ElementoListaCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var entity = _mapper.Map<ElementoLista>(dto);
        entity.AddAt = DateTime.UtcNow;

        var created = await _elementos.CreateAsync(entity);
        var response = _mapper.Map<ElementoListaResponse>(created);

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

   
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ElementoListaUpdateDto dto)
    {
        if (id <= 0)
            return BadRequest("El ID debe ser un número positivo.");

        var existing = await _elementos.GetByIdAsync(id);
        if (existing == null)
            return NotFound($"Elemento con ID {id} no encontrado.");

        _mapper.Map(dto, existing);
        await _elementos.UpdateAsync(existing);

        return Ok(new { message = $"Elemento con ID {id} actualizado correctamente." });
    }

   
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
            return BadRequest("El ID debe ser un número positivo.");

        await _elementos.DeleteAsync(id);
        return Ok(new { message = $"Elemento con ID {id} eliminado correctamente." });
    }
}
