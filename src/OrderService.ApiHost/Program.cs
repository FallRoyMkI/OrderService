using Microsoft.AspNetCore.Diagnostics;
using OrderService.Application.Exceptions;
using OrderService.Application.Extensions;
using OrderService.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(configuration);
builder.Services.AddApplication();
builder.Services.AddCors(x => x.AddDefaultPolicy(o => o.AllowCredentials().AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:58978")));
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler(a => a.Run(async context =>
{
    var error = context.Features.Get<IExceptionHandlerFeature>();
    var exception = error.Error;
    context.Response.StatusCode = exception switch
    {
        OrderNotFoundExcpetion => StatusCodes.Status404NotFound,
        ValidationException => StatusCodes.Status400BadRequest,
        _ => StatusCodes.Status500InternalServerError
    };

    await context.Response.WriteAsync(exception.Message);
}));

app.Run();
