using Cine.Filters;
using Cine.ServiceFactory;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("SessionId", new OpenApiSecurityScheme
    {
        Description = "Session header required for protected endpoints. Example: X-Session-Id: {sessionId}",
        Name = "X-Session-Id",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "SessionId"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddScoped<ApiExceptionFilter>();
builder.Services.AddScoped<SessionAuthenticationFilter>();
builder.Services.AddControllers(options =>
{
    options.Filters.AddService<ApiExceptionFilter>();
});

builder.Services.AddCineDependencies(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();
app.UseHttpsRedirection();

await app.RunAsync();
