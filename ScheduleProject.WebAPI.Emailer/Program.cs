using AutoMapper;
using Business;
using Emailer;
using Emailer.Consumers;
using Emailer.Interfaces;
using Emailer.Models;
using Emailer.Services;
using FacultyService.DbAccess;
using FacultyService.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using ScheduleService.Consumers;
using SimpleService.Interfaces;
using TokenService.RabbitMQModels;

var builder = WebApplication.CreateBuilder(args);

var emailConfig = builder.Configuration
           .GetSection("EmailConfiguration")
           .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);

builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.Configure<FormOptions>(o => {
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});

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
    x.AddConsumer<AddUserToEIConsumer>();
    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
    {
        config.Host(new Uri(RabbitMqConsts.RabbitMqRootUri), h =>
        {
            h.Username(RabbitMqConsts.UserName);
            h.Password(RabbitMqConsts.Password);
        });
        config.ReceiveEndpoint("addUserToEIQueue", oq =>
        {
            oq.PrefetchCount = 20;
            oq.ConfigureConsumer<AddUserToEIConsumer>(provider);
        });
        config.ReceiveEndpoint("subscriptionQueue", oq =>
        {
            oq.PrefetchCount = 20;
            oq.ConfigureConsumer<SubscriptionConsumer>(provider);
        });
    }));
    x.AddConsumer<SubscriptionConsumer>();
});

builder.Services.AddMassTransitHostedService();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IEmailSenderRepository, EmailSenderDbRepository>();
builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddCors();

var emailerConnectionString = builder.Configuration.GetConnectionString("EmailerDb");
builder.Services.AddDbContext<EmailerDbContext>(x => x.UseSqlServer(emailerConnectionString));
builder.Services.AddTransient<EmailerDbContext>();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutomapperProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();
app.ApplyMigrations();

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

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();