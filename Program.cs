using OrderApi.Middleware;
using FluentValidation;
using FluentValidation.AspNetCore;
using OrderApi.Models;
using OrderApi.Filters;
using OrderApi.Services.Contracts;
using OrderApi.Services;
using OrderApi.Models.Request;
using OrderApi.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();

//Add controller support
//request passing controller will hit this filter
builder.Services.AddControllers(options =>
    options.Filters.Add<ValidateModelFilter>()
);
// register FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderRequest>();


//AutoMappers
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<OrderMappingProfile>();
});

//Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.UseMiddleware<RequestLoggingMiddleware>();
app.MapControllers();
app.Run();

