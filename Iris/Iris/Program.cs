using Iris.Configuration;
using Iris.Database;
using Iris.Services.AuthService;
using Iris.Stores;
using Iris.Stores.AuthRequestStore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System.Reflection;

// Logger Configuration
Serilog.Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateBootstrapLogger();

var config = Config.BuildConfig();

var builder = WebApplication.CreateBuilder(args);

// Add serilog logger
builder.Host.UseSerilog((context, services, configuration) => configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .WriteTo.File(
                        path: config.Logger.FileName,
                        shared: true,
                        rollingInterval: RollingInterval.Day,
                        fileSizeLimitBytes: config.Logger.LimitFileSize
                        )
                    );

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Iris-Api",
        Version = "v1"
    });

    var executingLocation = Assembly.GetExecutingAssembly().Location;
    var xmlName = $"{Path.GetFileNameWithoutExtension(executingLocation)}.xml";
    var xmlPath = Path.Combine(Path.GetDirectoryName(executingLocation), xmlName);
    c.IncludeXmlComments(xmlPath);
});

var dbContext = new DatabaseContext();

builder.Services.AddSingleton(config);
builder.Services.AddSingleton(dbContext);

builder.Services.AddSingleton<IAuthRequestsStore, AuthRequestsStore>();
builder.Services.AddSingleton<TokensStore>();
builder.Services.AddSingleton<IAuthService, AuthService>();

var app = builder.Build();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Iris-Api");
});


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
