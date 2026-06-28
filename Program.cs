using System.Reflection;
using System.Security.Claims;
using System.Text;
using Accounting.Application;
using Accounting.Infrastructure;
using Accounting_helal.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


// ============================================================================
// Serilog
// ============================================================================

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithThreadId()
    .Enrich.WithProcessId()
    .WriteTo.Console(
        outputTemplate:
        "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
    .WriteTo.File(
        "logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30,
        shared: true)
    .CreateLogger();

builder.Host.UseSerilog();


// ============================================================================
// Services
// ============================================================================

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddScoped<StandardApiResponseFilter>();
builder.Services.AddControllers(options =>
{
    options.Filters.AddService<StandardApiResponseFilter>();
});

builder.Services.AddEndpointsApiExplorer();


// ============================================================================
// JWT Authentication
// ============================================================================

var jwtSecret = builder.Configuration["Jwt:Secret"]
    ?? throw new InvalidOperationException(
        "Jwt:Secret is not configured.");

var jwtIssuer = builder.Configuration["Jwt:Issuer"];

var jwtAudience = builder.Configuration["Jwt:Audience"];

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme =
            JwtBearerDefaults.AuthenticationScheme;

        options.DefaultChallengeScheme =
            JwtBearerDefaults.AuthenticationScheme;

        options.DefaultScheme =
            JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;

        options.SaveToken = true;

        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwtIssuer,
                ValidAudience = jwtAudience,

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSecret)),

                RoleClaimType = ClaimTypes.Role,
                NameClaimType = ClaimTypes.Name
            };

        // ====================================================================
        // JWT Debug Logging
        // ====================================================================

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                Console.WriteLine("========== TOKEN RECEIVED ==========");
                Console.WriteLine(context.Token);

                return Task.CompletedTask;
            },

            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("========== AUTH FAILED ==========");
                Console.WriteLine(context.Exception);

                return Task.CompletedTask;
            },

            OnTokenValidated = context =>
            {
                Console.WriteLine("========== TOKEN VALIDATED ==========");

                foreach (var claim in context.Principal!.Claims)
                {
                    Console.WriteLine(
                        $"{claim.Type} = {claim.Value}");
                }

                return Task.CompletedTask;
            },

            OnChallenge = context =>
            {
                Console.WriteLine("========== ON CHALLENGE ==========");

                return Task.CompletedTask;
            },

            OnForbidden = context =>
            {
                Console.WriteLine("========== FORBIDDEN ==========");

                return Task.CompletedTask;
            }
        };
    });


// ============================================================================
// Authorization
// ============================================================================

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
    {
        policy
            .AddAuthenticationSchemes(
                JwtBearerDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser()
            .RequireRole("Admin");
    });

    options.AddPolicy("ManagerOrAdmin", policy =>
    {
        policy
            .AddAuthenticationSchemes(
                JwtBearerDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser()
            .RequireRole(
                "Admin",
                "Manager",
                "Owner",
                "Boss");
    });
});


// ============================================================================
// Swagger / OpenAPI
// ============================================================================

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Accounting Helal API",
        Version = "v1",
        Description =
            "Full-featured accounting backend for Helal."
    });

    // ========================================================================
    // JWT Auth for Swagger + Scalar
    // ========================================================================

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",

        Description =
            "Enter JWT token like: Bearer {your token}",

        In = ParameterLocation.Header,

        Type = SecuritySchemeType.Http,

        Scheme = "bearer",

        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference =
                        new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                },
                Array.Empty<string>()
            }
        });

    // ========================================================================
    // XML Comments
    // ========================================================================

    var xmlFile =
        $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    var xmlPath =
        Path.Combine(AppContext.BaseDirectory, xmlFile);

    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});


// ============================================================================
// Build App
// ============================================================================

var app = builder.Build();


// ============================================================================
// Middlewares
// ============================================================================

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSerilogRequestLogging(options =>
{
    options.MessageTemplate =
        "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";

    options.EnrichDiagnosticContext =
        (diagnosticContext, httpContext) =>
        {
            diagnosticContext.Set(
                "RequestHost",
                httpContext.Request.Host.Value);

            diagnosticContext.Set(
                "UserAgent",
                httpContext.Request.Headers.UserAgent.ToString());

            diagnosticContext.Set(
                "RemoteIp",
                httpContext.Connection.RemoteIpAddress?.ToString());
        };
});

app.UseMiddleware<RequestResponseLoggingMiddleware>();


// ============================================================================
// Swagger JSON
// ============================================================================

app.UseSwagger();


// ============================================================================
// Scalar
// ============================================================================

app.MapScalarApiReference(options =>
{
    options
        .WithTitle("Accounting Helal API")

        .WithTheme(ScalarTheme.BluePlanet)

        .WithOpenApiRoutePattern(
            "/swagger/v1/swagger.json")

        .AddPreferredSecuritySchemes("Bearer")

        .WithDefaultHttpClient(
            ScalarTarget.CSharp,
            ScalarClient.HttpClient);
});


// ============================================================================
// HTTP Pipeline
// ============================================================================

// app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();