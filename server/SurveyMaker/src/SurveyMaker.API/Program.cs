using Microsoft.EntityFrameworkCore;
using SurveyMaker.API.Extensions;
using SurveyMaker.API.Middlewares;
using SurveyMaker.Application.Extensions;
using SurveyMaker.Domain.Entities;
using SurveyMaker.Infrastructure.EF;
using SurveyMaker.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddApiServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Asegúrate de agregar tu middleware al pipeline antes de otros middlewares que podrían manejar excepciones (como UseRouting)
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

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

app.UseAuthorization();

app.MapIdentityApi<SurveyMakerUser>();

app.MapControllers();

app.Run();
