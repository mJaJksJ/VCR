using Iris.Configuration;
using Iris.Database;
using Iris.Services.AuthService;
using Iris.Services.MailServersService;
using Iris.Stores;
using Iris.Stores.AuthRequestStore;
using Iris.Stores.ServiceConnectionStore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
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

var log = Serilog.Log.ForContext<Program>();

var config = Config.BuildConfig();

var builder = WebApplication.CreateBuilder(args);

#region serilog configuration
var logTemplateConsole = "[{Level:u3}] <{ThreadId}> :: {Message:lj}{NewLine}{Exception}";
var logTemplateFile = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] <{ThreadId}> :: {Message:lj}{NewLine}{Exception}";

if (!Directory.Exists(config.Logger.FilePath))
{
    try
    {
        Directory.CreateDirectory(config.Logger.FilePath);
        log.Information($"create directory {config.Logger.FilePath} for logs");
    }
    catch
    {
        log.Error("Can't find and create directory for logs");
        return;
    }
}


builder.Host.UseSerilog((context, services, configuration) => configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext()
                    .Enrich.WithThreadId()
                    .WriteTo.Console(outputTemplate: logTemplateConsole)
                    .WriteTo.File(
                        outputTemplate: logTemplateFile,
                        path: Path.Combine(config.Logger.FilePath, config.Logger.FileName),
                        shared: true,
                        rollingInterval: RollingInterval.Day,
                        fileSizeLimitBytes: config.Logger.LimitFileSize
                        )
                    );

#endregion

// Add services to the container.
builder.Services.AddControllers();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(config.AuthConfig.JwtSecurityKey),

            ValidateIssuer = true,
            ValidIssuer = config.AuthConfig.JwtIssuer,

            ValidateAudience = true,
            ValidAudience = config.AuthConfig.JwtAudience,

            RequireExpirationTime = true,
            ValidateLifetime = true,

            ClockSkew = TimeSpan.FromMinutes(1)
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = _ =>
            {
                if (string.IsNullOrEmpty(_.Token))
                {
                    var fromAuth = _.Request.Query["auth"];
                    if (!string.IsNullOrEmpty(fromAuth))
                    {
                        _.Token = fromAuth;
                    }

                    var fromAccessToken = _.Request.Query["access_token"];
                    if (!string.IsNullOrEmpty(fromAccessToken))
                    {
                        _.Token = fromAccessToken;
                    }
                }

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization(
    options =>
    {
        options.DefaultPolicy = new AuthorizationPolicyBuilder()
            .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser()
            .Build();
    });

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
builder.Services.AddSingleton<IServerConnectionStore, ServerConnectionStore>();
builder.Services.AddSingleton<IAuthRequestsStore, AuthRequestsStore>();
builder.Services.AddSingleton<TokensStore>();
builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddScoped<IMailServersService, MailServersService>();

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

app.Use(async (context, next) =>
{
    try
    {
        if (context.Response.HasStarted)
        {
            Log.Information(
                "Current user identity: {Name} AuthenticationType: {AuthenticationType}",
                context.User.Identity.Name, context.User.Identity.AuthenticationType);
        }
    }
    catch (Exception exc)
    {
        Log.Error("{Message}", exc.Message);
    }

    await next();
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
