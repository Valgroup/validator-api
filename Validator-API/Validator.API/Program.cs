using Microsoft.EntityFrameworkCore;
using Validator.API.Filter;
using Validator.API.Middlewares;
using Validator.API.Resolvers;
using Validator.API.Services;
using Validator.Application.Emails;
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
using Validator.Service.Sendgrid;

var builder = WebApplication.CreateBuilder(args);

var cnConfig = builder.Configuration.GetConnectionString("CnValidator");
RuntimeConfigurationHelper.ConnectionString = cnConfig;
builder.Services.AddDbContext<ValidatorContext>(o => o.UseSqlServer(cnConfig));

RuntimeConfigurationHelper.Ambiente = builder.Configuration.GetValue<string>("Ambiente");

// Add services to the container.
builder.Services.AddTransient<IPlanilhaAppService, PlanilhaAppService>();
builder.Services.AddTransient<IDashAppService, DashAppService>();
builder.Services.AddTransient<IAuthAppService, AuthAppService>();
builder.Services.AddTransient<IUsuarioAppService, UsuarioAppService>();
builder.Services.AddTransient<ITemplateRazorService, TemplateRazorService>();
builder.Services.AddTransient<IEmailService, EmailService>();

//DOMAIN
builder.Services.AddTransient<IPlanilhaService, PlanilhaService>();
builder.Services.AddTransient<IParametroService, ParametroService>();
builder.Services.AddTransient(typeof(IServiceDomain<>), typeof(ServiceDomain<>));
builder.Services.AddTransient<IUsuarioService, UsuarioService>();
builder.Services.AddTransient<IProcessoService, ProcessoService>();
builder.Services.AddTransient<IUsuarioAvaliadorService, UsuarioAvaliadorService>();
builder.Services.AddTransient<IDivisaoService, DivisaoService>();
builder.Services.AddTransient<ISetorService, SetorService>();
builder.Services.AddTransient<IProgressoService, ProgressoService>();
//DOMAIN

//DATA
builder.Services.AddTransient<IUsuarioReadOnlyRepository, UsuarioReadOnlyRepository>();
builder.Services.AddTransient<IUsuarioAuthReadOnlyRepository, UsuarioAuthReadOnlyRepository>();
builder.Services.AddTransient<IDashReadOnlyRepository, DashReadOnlyRepository>();
builder.Services.AddTransient<IPlanilhaReadOnlyRepository, PlanilhaReadOnlyRepository>();
builder.Services.AddTransient<IUtilReadOnlyRepository, UtilReadOnlyRepository>();
builder.Services.AddTransient<INotificacaoReadOnlyRespository, NotificacaoReadOnlyRespository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ValidatorContext>();
builder.Services.AddTransient<IUserResolver, WebApiResolver>();

builder.Services.AddTransient<ISendGridService, SendGridService>();

builder.Services.AddRazorPages();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<AuthorizationHeaderOperationFilter>();
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(o =>
    o.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin()
    .SetIsOriginAllowed(origin => { return true; })

); ;

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(name: "default", pattern: "{controller=Email}/{action=Index}/{id?}");

app.MapRazorPages();

app.UseMiddleware<ExceptionGlobalHandlerMiddleware>();

app.Run();
