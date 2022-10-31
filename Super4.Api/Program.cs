using Super4.Application.Application;
using Super4.Application.Interfaces;
using Super4.Application.Mapper;
using Super4.Domain.Interfaces.Repositories;
using Super4.Domain.Interfaces.Repositories.DataConnector;
using Super4.Domain.Interfaces.Services;
using Super4.Domain.Services;
using Super4.Infra.DataConnector;
using Super4.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


builder.Services.AddAutoMapper(typeof(Core));


var builder2 = WebApplication.CreateBuilder(args);
string connString = builder2.Configuration.GetConnectionString("default");
builder.Services.AddScoped<IDbConnector>(db => new SqlConnector(connString));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ICustomerApplication, CustomerApplication>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
