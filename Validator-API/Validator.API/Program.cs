using Microsoft.EntityFrameworkCore;
using Validator.API.Resolvers;
using Validator.Application.Interfaces;
using Validator.Application.Services;
using Validator.Data.Contexto;
using Validator.Data.Dapper;
using Validator.Data.Repositories;
using Validator.Data.UoW;
using Validator.Domain.Core.Helpers;
using Validator.Domain.Core.Interfaces;
using Validator.Domain.Core.Services;
using Validator.Domain.Interfaces;
using Validator.Domain.Interfaces.Repositories;
using Validator.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

var cnConfig = builder.Configuration.GetConnectionString("Test");
RuntimeConfigurationHelper.ConnectionString = cnConfig;
builder.Services.AddDbContext<ValidatorContext>(o => o.UseSqlServer(cnConfig));

// Add services to the container.
builder.Services.AddTransient<IPlanilhaAppService, PlanilhaAppService>();
builder.Services.AddTransient<IDashAppService, DashAppService>();
builder.Services.AddTransient<IAuthAppService, AuthAppService>();
builder.Services.AddTransient<IUsuarioAppService, UsuarioAppService>();

//DOMAIN
builder.Services.AddTransient<IPlanilhaService, PlanilhaService>();
builder.Services.AddTransient<IParametroService, ParametroService>();
builder.Services.AddTransient(typeof(IServiceDomain<>), typeof(ServiceDomain<>));
builder.Services.AddTransient<IUsuarioService, UsuarioService>();
//DOMAIN

//DATA
builder.Services.AddTransient<IUsuarioReadOnlyRepository, UsuarioReadOnlyRepository>();
builder.Services.AddTransient<IUsuarioAuthReadOnlyRepository, UsuarioAuthReadOnlyRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ValidatorContext>();
builder.Services.AddTransient<IUserResolver, WebApiResolver>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
