using AutoMapper;
using Business.Service;
using Business.Interfaces;
using Data_access.Interfaces;
using SpecialtyService.DbAccess;
using Microsoft.EntityFrameworkCore;
using SpecialtyService.Repositories;
using SpecialtyService;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var authOptions = builder.Configuration.GetSection("Auth");
builder.Services.Configure<JwtOptions>(authOptions);
var auth = authOptions.Get<JwtOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {

        ValidateIssuer = true,
        ValidIssuer = auth.Issuer,

        ValidateAudience = false,
        ValidAudience = auth.Audience,

        ValidateLifetime = true,

        IssuerSigningKey = auth.GetSymmetricSecurityKey(),
        ValidateIssuerSigningKey = true,

    };
});

builder.Services.AddTransient<ISpecialtyRepository, SpecialtyDbRepository>();
builder.Services.AddTransient<ISpecialtyService, Business.Service.SpecialtyService>();

builder.Services.AddTransient<IFacultySpecialtyRepository, FacultySpecialtyDbRepository>();
builder.Services.AddTransient<IFacultySpecialtyService, FacultySpecialtyService>();

builder.Services.AddCors();

var specialtyConnectionString = "Server=localhost;Database=SpecialtiesDb;Trusted_Connection=True;Encrypt=False;";
builder.Services.AddDbContext<SpecialtyDbContext>(x => x.UseSqlServer(specialtyConnectionString));
builder.Services.AddTransient<SpecialtyDbContext>();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutomapperProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();
app.ApplyMigrations();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();