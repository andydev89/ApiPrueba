using ApiPrueba.src.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static ApiPrueba.src.Application.DTOs.ProductDtos;

namespace ApiPrueba.src.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productservice;
    public ProductsController(IProductService productservice) => _productservice = productservice;

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? q = null)
    {
        var (items, total) = await _productservice.GetPagedAsync(page, pageSize, q);
        return Ok(new { total, items });
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest req)
    {
        var created = await _productservice.CreateAsync(req);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var p = await _productservice.GetByIdAsync(id);
        return p is null ? NotFound() : Ok(p);
    }

    [Authorize(Roles = "admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductRequest req)
    {
        var ok = await _productservice.UpdateAsync(id, req);
        return ok ? NoContent() : NotFound();
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _productservice.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }
}
