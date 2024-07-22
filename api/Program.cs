using System.Text;
using API.Context;
using API.DTOs.Mappings;
using API.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


string mySqlConnection = builder.Configuration.GetConnectionString("SqlServerConnection");

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlServer(mySqlConnection)
);

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));

builder.Services.AddAutoMapper(typeof(ApiDTOMappingProfile));

builder.Services.AddAuthentication("Bearer").AddJwtBearer();


builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Viva TCE", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() 
    { 
        Description = "JWT Authorization header using the Bearer scheme.\r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"", 
        Name = "Authorization", 
        In = ParameterLocation.Header, 
        Type = SecuritySchemeType.ApiKey, 
        Scheme = "Bearer", 
        BearerFormat = "JWT", 
    }); 
    c.AddSecurityRequirement(new OpenApiSecurityRequirement 
    { 
        { 
                new OpenApiSecurityScheme 
                { 
                    Reference = new OpenApiReference 
                    { 
                        Type = ReferenceType.SecurityScheme, 
                        Id = "Bearer" 
                    } 
                }, 
                new string[] {} 
        } 
    }); 
});


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
