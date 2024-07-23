using GrpcService1.Infrastructure.Persistence;
using GrpcService1.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using UOW.Repository;
using UOW.UoW;
using UOW1.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddGrpc().AddJsonTranscoding();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork<CustomerDbContext>>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));

    // Đăng ký DbContext
    builder.Services.AddDbContext<CustomerDbContext>(options =>
    {
        var mongoClientSettings = MongoClientSettings.FromUrl(new MongoUrl(Environment.GetEnvironmentVariable("ConnectionStringsMongodb")));
            
        mongoClientSettings.MaxConnectionPoolSize = 100;
        mongoClientSettings.MinConnectionPoolSize = 10;
        var mongoClient = new MongoClient(mongoClientSettings);
        options.UseMongoDB(mongoClient,"test");
    });
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.ListenAnyIP(5601, listenOptions => {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
    //add health check on a separate port instead of Http1AndHttp2 on same port
    options.ListenAnyIP(5600, listenOptions => {
        listenOptions.Protocols = HttpProtocols.Http1;
    });
});
var app = builder.Build();
app.UseMiddleware<AutoSave>();
// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<SumerService>();
app.MapGrpcService<CustomerService>();
app.UseHttpsRedirection();
app.Run();