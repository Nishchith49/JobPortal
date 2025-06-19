using JobPortal.API.Swagger;
using JobPortal.Application;
using JobPortal.Infrastructure.Concrete.IServices;
using JobPortal.Infrastructure.Concrete.Services;
using JobPortal.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment.EnvironmentName ?? "Development";
//var defaultPort = env.Equals("QA", StringComparison.CurrentCultureIgnoreCase) ? 5001 : env.Equals("Production", StringComparison.CurrentCultureIgnoreCase) ? 5002 : 5000;
//var port = Environment.GetEnvironmentVariable("PORT") ?? defaultPort.ToString();
//builder.WebHost.UseUrls($"http://localhost:{port}");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(
            builder.Configuration.GetConnectionString("DefaultConnection"),
            ServerVersion.Parse("8.0.28"))
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors();
});

builder.Services.AddHttpClient();

builder.Services.AddControllers()
    .AddNewtonsoftJson(o =>
    {
        o.SerializerSettings.ContractResolver = new DefaultContractResolver();
        o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        o.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
        o.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ssZ";
    });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("JobPortal Admin", new OpenApiInfo
    {
        Title = $"JobPortal Admin {env}",
        Version = "1.0",
        Description = "JobPortal using ASP.NET CORE 9",
        Contact = new OpenApiContact
        {
            Name = "",
            Email = "",
        },
    });
    c.SwaggerDoc("JobPortal User", new OpenApiInfo
    {
        Title = $"JobPortal User {env}",
        Version = "1.0",
        Description = "JobPortal using ASP.NET CORE 9",
        Contact = new OpenApiContact
        {
            Name = "",
            Email = "",
        },
    });
    c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme.",
    });
    c.OperationFilter<AuthOperationFilter>();
});

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var securityKey = builder.Configuration.GetValue<string>("JwtOptions:SecurityKey")
                      ?? throw new InvalidOperationException("JWT SecurityKey is not configured");
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetValue<string>("JwtOptions:Issuer"),
        ValidAudience = builder.Configuration.GetValue<string>("JwtOptions:Audience"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey))
    };
});

builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", policyBuilder =>
{
    policyBuilder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithOrigins("http://localhost:4200", "https://localhost:4200")
        .SetIsOriginAllowedToAllowWildcardSubdomains()
        .AllowCredentials();
}));

builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IEmailServices, EmailServices>();
builder.Services.AddScoped<IFileExtensionService, FileExtensionService>();
builder.Services.AddScoped<IStorageServices, StorageServices>();


builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyMarker).Assembly);
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStatusCodePages("text/plain", "Status code page, status code: {0}");
app.UseHttpsRedirection();
app.UseCookiePolicy();
app.UseStaticFiles();
app.UseDefaultFiles();
app.UseRouting();
app.UseHsts();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/JobPortal Admin/swagger.json", "Admin API");
    c.SwaggerEndpoint("/swagger/JobPortal User/swagger.json", "User API");
    c.RoutePrefix = "swagger";
    //c.SwaggerEndpoint("/swagger/JobPortal/swagger.json", "JobPortal");
    c.DocExpansion(DocExpansion.None);
    c.DefaultModelsExpandDepth(-1);
    c.DisplayRequestDuration();
});

app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
#pragma warning restore ASP0014 // Suggest using top level route registrations

app.Use(async (context, next) =>
{
    if (context.Request.Path.HasValue && context.Request.Path.Value != "/")
    {
        context.Response.ContentType = "text/html";
        await context.Response.SendFileAsync(
            builder.Environment.ContentRootFileProvider.GetFileInfo("wwwroot/index.html")
        );
        return;
    }
    await next();
});

app.Run();