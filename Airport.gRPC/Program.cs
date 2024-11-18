using Airport.gRPC.Services;
using Airport.Provider.Provider;
using Airport.Provider.Repository;
using Airport.Service.Commands;
using Airport.Service.Queries;
using Common.Core.Exceptions;
using FluentValidation;
using MediatR;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(c =>
{
    c.RegisterServicesFromAssembly(typeof(Program).Assembly);
    //c.AddOpenBehavior(typeof(ValidatorPipelineBehaviour<,>));
});

builder.Services.AddHttpClient();

builder.Services.AddTransient<IAirportProvider, AirportProvider>();
builder.Services.AddTransient<ICachedRepository, CachedRepository>();
builder.Services.AddTransient<IRequestHandler<AirportRequest, AirportResponse>, AirportQueryHandler>();

builder.Services.AddGrpc();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Host.UseSerilog();

builder.Services.AddStackExchangeRedisCache(op =>
{
    op.Configuration = builder.Configuration.GetConnectionString("redisStorage");
});

var app = builder.Build();

app.MapGrpcService<GreeterService>();
app.MapGrpcService<AirportService>();

app.UseSerilogRequestLogging();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.UseExceptionHandler(option => { });

app.Run();
