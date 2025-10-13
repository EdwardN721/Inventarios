using Inventarios.Infrastructure;
using Inventarios.Mapper;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddContextPostgressServer(builder.Configuration, "DefaultConnection");
builder.Services.AddRepositories();

builder.Services.AddControllers();

// Agregar swagger Swashbukle.AspNetCore

builder.Services.AddSwagger();

// Agregar Mapper
builder.Services.AddAutoMapper(configuration =>
    {
        configuration.AddProfile<MappingProfiles>();
    },
    AppDomain.CurrentDomain.GetAssemblies());

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