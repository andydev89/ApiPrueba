using ApiPrueba.src.Api.Dtos;
using ApiPrueba.src.Application.Dtos;
using ApiPrueba.src.Application.Interfaces;
using ApiPrueba.src.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiPrueba.src.Api.Controllers;

[ApiController]
[Route("api/usuarios")]
[Authorize]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarios;
    private readonly IMapper _mapper;

    public UsuarioController(IUsuarioService usuarios, IMapper mapper)
    {
        _usuarios = usuarios;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _usuarios.GetAllAsync();
        var usuarios = _mapper.Map<IEnumerable<UserResponse>>(result);
        return Ok(usuarios);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _usuarios.GetByIdAsync(id);
        var usuario = _mapper.Map<UserResponse>(result);
        return Ok(usuario);
    }

    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var result = await _usuarios.GetByEmailAsync(email);
        var usuario = _mapper.Map<UserResponse>(result);
        return Ok(usuario);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UsuarioDto dto)
    {
        var entity = _mapper.Map<User>(dto);
        var created = await _usuarios.CreateAsync(entity);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UsuarioUpdateDto dto)
    {
        if (id <= 0)
            return BadRequest("El ID debe ser un número positivo.");

        
        var existingUser = await _usuarios.GetByIdAsync(id);
        if (existingUser == null)
            return NotFound($"Usuario con ID {id} no encontrado.");

       
        _mapper.Map(dto, existingUser);

       
        await _usuarios.UpdateAsync(existingUser);
        return Ok(new { message = $"Usuario con ID {id} actualizado correctamente." });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _usuarios.DeleteAsync(id);
        return NoContent();
    }
}
