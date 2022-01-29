using Iris.Configuration;
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

builder.Services.AddSingleton(config);

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

/*app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");*/
app.Use(async (context, next) =>
{
    Endpoint endpoint = context.GetEndpoint();

    if (endpoint != null)
    {
        await next();
    }
    else
    {
        context.Response.Redirect("/swagger");
        await context.Response.WriteAsync("redirect to swagger");
    }
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

//app.MapFallbackToFile("index.html"); ;

app.Run();
