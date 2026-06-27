using OrderApi.Middleware;
using FluentValidation;
using FluentValidation.AspNetCore;
using OrderApi.Models;
using OrderApi.Filters;
using OrderApi.Services.Contracts;
using OrderApi.Services;
using OrderApi.Models.Request;
using OrderApi.Mappings;
using Microsoft.EntityFrameworkCore;
using OrderApi.Data;
using OrderApi.Models.Domain;
using OrderApi.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
builder.Services.AddValidatorsFromAssemblyContaining<GetProductsQueryValidator>();


//AutoMappers
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<OrderMappingProfile>();
    config.AddProfile<ProductMappingProfile>();
});

//Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes("this_is_a_very_strong_key")
                )
            };
        });


var app = builder.Build();

// using (var scope = app.Services.CreateScope())
// {
//     var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//     context.Database.EnsureCreated();
//     if (context.Products.Any() == false)
//     {
//         context.Products.AddRange(
//             new Product { Name = "Laptop", Price = 50000 },
//             new Product { Name = "Mouse", Price = 50000 },
//             new Product { Name = "Keyboard", Price = 50000 }
//         );
//         context.SaveChanges();
//     }
// }

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<RequestLoggingMiddleware>();
app.MapControllers();
app.Run();

