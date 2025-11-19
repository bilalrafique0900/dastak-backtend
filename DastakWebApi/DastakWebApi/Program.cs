using DastakWebApi.Data;
using DastakWebApi.Extentions;
using DastakWebApi.HelperMethods;
using DastakWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
string policyName = "AllowAngularOrigins";
// Add services to the container.
builder.Services.AddDbContext<DastakDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Configure CORS to allow any origin
builder.Services.AddCors(builder =>
{
    builder.AddPolicy(policyName,
        builder => builder.WithOrigins(configuration.GetSection("Cors").GetValue<string>("AllowedOrigins"))
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
        .SetIsOriginAllowed((hosts) => true)
        );
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
//builder.Services.AddSwaggerGen();
//Swagger Configuration
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Dastak_System",
        Version = "v2",
        Description = "Dastak_System",
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "bearer",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {{new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }},new List<string>()}});
});
builder.Services.AddScoped<JwtHandler>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICallersService, CallersService>();
builder.Services.AddScoped<IVisitorService, VisitorService>();
builder.Services.AddScoped<ILegalAssistanceService, LegalAssistanceService>();
builder.Services.AddScoped<IInterventionService, InterventionService>();
builder.Services.AddScoped<IFollowService, FollowService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddJwtAuthentication(configuration);
builder.Services.AddAuthorization(option =>
{
    option.FallbackPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
});
var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();
//app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
        ctx.Context.Response.Headers.Append("Access-Control-Allow-Headers",
          "Origin, X-Requested-With, Content-Type, Accept");
    },

});

app.UseFileServer(new FileServerOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "wwwroot")),
    EnableDirectoryBrowsing = true
});

//app.UseFileServer(new FileServerOptions
//{
//    FileProvider = new PhysicalFileProvider(
//           Path.Combine(builder.Environment.ContentRootPath, "wwwroot")),
//    RequestPath = "/wwwroot",
//    EnableDirectoryBrowsing = true
//});

//app.UseHttpsRedirection();

app.UseAuthentication();
// Enable the CORS policy
app.UseCors(policyName);
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
