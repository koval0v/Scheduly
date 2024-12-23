using AutoMapper;
using FluentValidation;
using TokenService.Extentions;
using TokenService.Repositories;
using TokenService.Services;
using TokenService;
using TokenService.DbAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FluentValidation.AspNetCore;
using Services.Validation.FluentValidation;
using Microsoft.EntityFrameworkCore;
using TokenService.Interfaces;
using MassTransit;
using TokenService.RabbitMQModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<ExceptionMiddleware>();

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

builder.Services.AddControllers().AddFluentValidation(fv =>
{
    fv.RegisterValidatorsFromAssemblyContaining<RegistrationModelValidator>(lifetime: ServiceLifetime.Singleton);

    fv.RegisterValidatorsFromAssemblyContaining<LoginModelValidator>(lifetime: ServiceLifetime.Singleton);

    fv.RegisterValidatorsFromAssemblyContaining<UserModelValidator>(lifetime: ServiceLifetime.Singleton);
});
ValidatorOptions.Global.LanguageManager.Enabled = false;

builder.Services.AddTransient<ICredentialsRepository, CredentialsRepository>();
builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IEIRepository, EIRepository>();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IEIService, EIService>();

builder.Services.AddCors();

var usersConnectionString = builder.Configuration.GetConnectionString("UsersDb");
builder.Services.AddDbContext<UserDbContext>(x => x.UseSqlServer(usersConnectionString));
builder.Services.AddTransient<UserDbContext>();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutomapperProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

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