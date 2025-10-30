using ApiPrueba.src.Api.Extensions;
using ApiPrueba.src.Domain.Mappers;
using ApiPrueba.src.Domain.Mappings;
using ApiPrueba.src.Infrastructure.Data;
using ApiPrueba.src.Infrastructure.Middleware;
;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers();
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = null;
});

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<UserProfile>();
    cfg.AddProfile<ListaSeguimientoProfile>();
    cfg.AddProfile<TituloProfile>();
    cfg.AddProfile<ElementoListaProfile>();
});
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddAppServices(builder.Configuration)
                .AddJwtAuth(builder.Configuration)
                .AddDefaultCors(builder.Configuration)
                .AddSwaggerDocs();

var app = builder.Build();
app.UseSwaggerDocs();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ExceptionMiddleware>();

// Seed DB
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await Seed.EnsureAsync(db);
}

app.Run();
