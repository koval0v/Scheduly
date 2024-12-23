using AutoMapper;
using Business;
using Business.Interfaces;
using FacultyService;
using FacultyService.DbAccess;
using FacultyService.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using SimpleService.Interfaces;
using TokenService.RabbitMQModels;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddMassTransit(x =>
{
    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
    {
        config.Host(new Uri(RabbitMqConsts.RabbitMqRootUri), h =>
        {
            h.Username(RabbitMqConsts.UserName);
            h.Password(RabbitMqConsts.Password);
        });
    }));
});
builder.Services.AddMassTransitHostedService();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IScheduleRepository, ScheduleDbRepository>();
builder.Services.AddTransient<IScheduleDisciplineRepository, ScheduleDisciplineDbRepository>();

builder.Services.AddTransient<IScheduleService, Business.Service.ScheduleService>();

builder.Services.AddCors();

var scheduleConnectionString = builder.Configuration.GetConnectionString("ScheduleDb");
builder.Services.AddDbContext<ScheduleDbContext>(x => x.UseSqlServer(scheduleConnectionString));
builder.Services.AddTransient<ScheduleDbContext>();

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
