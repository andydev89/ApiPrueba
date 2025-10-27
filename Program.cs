using ApiPrueba.src.Api.Extensions;
using ApiPrueba.src.Infrastructure.Data;
using SQLitePCL;

SQLitePCL.Batteries.Init();

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers();

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

// Seed DB
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await Seed.EnsureAsync(db);
}

app.Run();
