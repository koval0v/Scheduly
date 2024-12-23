using AutoMapper;
using Business.Interfaces;
using Business.Service;
using Data_access.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Timeout;
using TeacherService;
using TeacherService.DbAccess;
using TeacherService.Repositories;

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
        ValidIssuer = auth?.Issuer,

        ValidateAudience = false,
        ValidAudience = auth?.Audience,

        ValidateLifetime = true,

        IssuerSigningKey = auth?.GetSymmetricSecurityKey(),
        ValidateIssuerSigningKey = true,

    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/cdOpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ITeacherRepository, TeacherDbRepository>();
builder.Services.AddHttpClient<ITeacherService, Business.Service.TeacherService>(a =>
{
    a.BaseAddress = new Uri("https://localhost:7215/api/disciplines/");
})
.AddTransientHttpErrorPolicy(b => b.Or<TimeoutRejectedException>().WaitAndRetryAsync(
    5,
    c => TimeSpan.FromSeconds(Math.Pow(2, c))
))
.AddTransientHttpErrorPolicy(b => b.Or<TimeoutRejectedException>().CircuitBreakerAsync(
    3,
    TimeSpan.FromSeconds(15)
    ))
.AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(1));

builder.Services.AddTransient<IDisciplineTeacherRepository, DisciplineTeacherDbRepository>();
builder.Services.AddTransient<IDisciplineTeacherService, DisciplineTeacherService>();

builder.Services.AddCors();

var teacherConnectionString = builder.Configuration.GetConnectionString("TeacherDb");
builder.Services.AddDbContext<TeacherDbContext>(x => x.UseSqlServer(teacherConnectionString));
builder.Services.AddTransient<TeacherDbContext>();

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
