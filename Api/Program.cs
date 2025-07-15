using System.Reflection;
using ApiRest.Middlewares;
using Application;
using Application.Service;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);



 
builder.Services.AddControllers()
        .AddJsonOptions(JsonOptions =>
                JsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{

    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Homework", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme",
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
                Array.Empty<string>()
            }

    });


    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssemblies(typeof(TareaHandler).GetTypeInfo().Assembly);
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // 👈 Permite Angular
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // Solo si usas autenticación basada en cookies o sesiones
    });
});


builder.Services.AddDbContext<PruebaContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BdEM"),
    sqlServerOptionsAction: options =>
    {
        options.EnableRetryOnFailure();
    });
 
});

builder.Services.AddHealthChecks();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.ConfigureRepository();
builder.Services.ConfigureServices();
var app = builder.Build();

app.UseCors("AllowAngular");
app.UseSwagger();
app.UseSwaggerUI();
app.MapHealthChecks("/health");
app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthorization();
app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
