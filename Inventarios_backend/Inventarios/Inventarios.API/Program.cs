using Inventarios.Extensions;
using Inventarios.Infrastructure;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddContextPostgressServer(builder.Configuration, "DefaultConnection");
builder.Services.AddInfrastructure();
builder.Services.AddBusiness();

builder.Services.AddControllers();

// Agregar swagger Swashbukle.AspNetCore

builder.Services.AddSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();