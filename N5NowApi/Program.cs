using Application.Bootstrapping;
using Infrastructure.Bootstrapping;
using N5NowApi.Bootstrapping;
using N5NowApi.Middlewares;
using Serilog;
using Serilog.Sinks.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Debug()
    .WriteTo.Console()
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"{configuration["ApplicationName"].ToLower()}-logs-{env.ToLower()}-{DateTime.UtcNow:yy-MM-dd}"
    })
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<FluentValidationMiddleware>();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

DatabaseInitial.ExecuteMigrate(app);

app.UseMiddleware<FluentValidationMiddleware>();
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();