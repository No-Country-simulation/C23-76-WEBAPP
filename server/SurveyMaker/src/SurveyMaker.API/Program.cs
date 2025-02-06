using Microsoft.EntityFrameworkCore;
using SurveyMaker.API.Extensions;
using SurveyMaker.API.Middlewares;
using SurveyMaker.Application.Extensions;
using SurveyMaker.Application.Hubs;
using SurveyMaker.Domain.Entities;
using SurveyMaker.Infrastructure.EF;
using SurveyMaker.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configurar CORS para permitir el acceso desde el frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", builder =>
    {
        builder
            .WithOrigins("http://localhost:4200")  // URL de tu frontend
            .AllowCredentials()  // Permitir credenciales (cookies, autenticación)
            .AllowAnyHeader()  // Permitir cualquier encabezado
            .AllowAnyMethod();  // Permitir cualquier método HTTP
    });
});


// Add services to the container.
builder.Services
    .AddApiServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices();

builder.Services.AddHttpContextAccessor();

builder.Services.AddProblemDetails();

builder.Services.AddExceptionHandler<GlobalExceptionHandlingMiddleware>();

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// Ensure Db is created and migrated
using (var dbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<SurveyMakerDbContext>())
{
    if (dbContext.Database.GetPendingMigrations().Any())
    {
        dbContext.Database.Migrate();
    }

    dbContext.Database.EnsureCreated();
}

app.UseHttpsRedirection();
app.UseCors("AllowLocalhost");
app.UseAuthorization();

app.MapHub<VoteHub>("/voteHub"); // Ahora está correctamente mapeado
app.MapIdentityApi<SurveyMakerUser>();

app.MapControllers();

app.Run();
