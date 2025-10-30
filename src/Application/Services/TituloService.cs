using ApiPrueba.src.Application.Interfaces;
using ApiPrueba.src.Domain.Entities;
using ApiPrueba.src.Domain.Repositories;
using ApiPrueba.src.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Globalization;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiPrueba.src.Application.Services;

public class TituloService : Service<Titulo, int>, ITituloService
{
    private readonly IRepository<Titulo, int> _repository;
    private readonly ILogger<TituloService> _logger;

    public TituloService(IRepository<Titulo, int> repository, ILogger<TituloService> logger)
        : base(repository, logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<Titulo>> BuscarPorGeneroAsync(string genero)
    {
        try
        {
            return await _repository.ListAsync(t => t.Genero != null && t.Genero.Contains(genero));
        }
        catch (Exception e)
        {
            _logger.LogError("Error al obtener las Listas de Seguimiento a partir del usuarioId", e.Message);
            throw new Exception("Error interno al buscar por Genero los titulos", e);
        }

    }



    public async Task<(IEnumerable<Titulo> Items, int Total)> BuscarPaginadoAsync(
        string? nombre, string? tipo, string? genero, int? año,
        int page, int pageSize, string? sortBy, bool desc)
    {

        try
        {
            Expression<Func<Titulo, bool>> filtro = t =>
            (string.IsNullOrEmpty(nombre) || t.Nombre.ToLower().Contains(nombre.ToLower())) &&
           (string.IsNullOrEmpty(tipo) || t.Tipo.ToLower() == tipo.ToLower()) &&
           (string.IsNullOrEmpty(genero) || (t.Genero != null && t.Genero.ToLower() == genero.ToLower())) &&
           (!año.HasValue || t.Año == año);

            Expression<Func<Titulo, object>>? orderBy = sortBy?.ToLower() switch
            {
                "nombre" => t => t.Nombre,
                "tipo" => t => t.Tipo,
                "genero" => t => t.Genero!,
                "año" => t => t.Año!,
                "createdat" => t => t.CreatedAt!,
                _ => t => t.Nombre // default
            };

            var (items, total) = await _repository.ListPagedAsync(
                predicate: filtro,
                page: page,
                pageSize: pageSize,
                orderBy: orderBy,
                descending: desc

            );

            return (items, total);
        }
        catch (Exception e)
        {
            _logger.LogError("Error al obtener las Listas de Seguimiento a partir del usuarioId", e.Message);
            throw new Exception("Error interno al buscar por Genero los titulos", e);
        }
    }
}
