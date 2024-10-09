using KoiOrderingSystem.Data.Models;
using KoiOrderingSystem.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

#region Authen

//_ = builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//       .AddJwtBearer(options =>
//       {
//           options.SaveToken = true;
//           options.RequireHttpsMetadata = true;

//           options.TokenValidationParameters = new TokenValidationParameters
//           {
//               ValidateIssuer = false,
//               ValidateAudience = false,
//               ValidateLifetime = true,
//               ValidateIssuerSigningKey = false,
//               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
//                   builder.Configuration.GetValue<string>("JWT:Token") ?? string.Empty)),
//               ClockSkew = TimeSpan.Zero
//           };

//       });

//builder.Services.AddAuthorization();

#endregion


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<TravelService>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
