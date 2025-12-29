using DesafioMSA.Presentation.Helpers;
using DesafioMSA.Presentation.MiddleWare;

 var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.InstallServicesInAssembly(builder.Configuration);
builder.Services.AddSwaggerGen(cfg =>
{
    cfg.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Title = "Desafio MSA Tech",
        Version = "v1"
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
