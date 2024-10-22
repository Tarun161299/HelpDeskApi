//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseAuthorization();

//app.MapControllers();

//app.Run();



//using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using HelpDeskAPI.Data;
using HelpDeskAPI.Data.Abstractions.Behaviors;
using HelpDeskAPI.Data.Abstractions.Models;
using HelpDeskAPI.Data.Business.Behaviors;
//using HelpDeskAPI.Data.Business.Services;
using HelpDeskAPI.Data.Interfaces;
using HelpDeskAPI.Data.Repositories;
using HelpDeskAPI.Service.Controllers;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Text;
using EfModels = HelpDeskAPI.Data.EF.Models;
using HelpDeskAPI.Data.Business.Services;
using SixLaborsCaptcha.Mvc.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
// DD
var connectionString = builder.Configuration.GetConnectionString("ConStr");
string domain = builder.Configuration.GetSection("Domain").Value.ToString();
var columnOptions = new ColumnOptions
{
    AdditionalColumns = new Collection<SqlColumn>
               {
                   new SqlColumn("UserName", SqlDbType.NVarChar)
                 }
};

var logger = new LoggerConfiguration()
               .Enrich.FromLogContext()
               .WriteTo.MSSqlServer(connectionString, sinkOptions: new MSSqlServerSinkOptions { TableName = "Log" }
               , null, null, LogEventLevel.Information, null, columnOptions: columnOptions, null, null)
               .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Services.AddDbContext<EfModels.HelpDeskDBContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Scoped);

builder.Services.Configure<MailService>(builder.Configuration.GetSection("MailSettings"));
//builder.Services.AddTransient<EfModels.OBSDBContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Scoped);
//builder.Services.Configure<MailService>(builder.Configuration.GetSection("MailSettings"));

// Add services to the container.

builder.Services.AddSixLabCaptcha(x =>
{
    x.DrawLines = 4;
});

builder.Services.AddControllers(config =>
{   //*to allow Authorize Globally*
    //var policy = new AuthorizationPolicyBuilder()
    //                 .RequireAuthenticatedUser()
    //                 .Build();
    //config.Filters.Add(new AuthorizeFilter(policy));
});
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "JWT Token Authentication API",
        Description = "ASP.NET Core 6.0 Web API"
    });
    //To Enable authorization using Swagger (JWT)  
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: Bearer 12345abcdef",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
    c.OperationFilter<AddCustomHeaderService>();
});

// DD
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
//DIRECTORSINJECT
builder.Services.AddTransient<IAppServiceRequestDirector, AppServiceRequestDirector>();
builder.Services.AddTransient<IAppTicketDirector, AppTicketDirector>();
builder.Services.AddTransient<IAppTicketHistoryDirector, AppTicketHistoryDirector>();
builder.Services.AddTransient<IAppServiceRequestHistoryDirector, AppServiceRequestHistoryDirector>();
builder.Services.AddTransient<IAppRemarksDirector, AppRemarksDirector>();
builder.Services.AddTransient<IAppRoleModulePermissionDirector, AppRoleModulePermissionDirector>();
builder.Services.AddTransient<IMdUserBoardMappingDirector, MdUserBoardMappingDirector>();
builder.Services.AddTransient<IMdUserBoardRoleMappingDirector, MdUserBoardRoleMappingDirector>();
builder.Services.AddTransient<IAppDocumentUploadedDetailDirector, AppDocumentUploadedDetailDirector>();
builder.Services.AddTransient<IAppLoginDetailsDirector, AppLoginDetailsDirector>();
builder.Services.AddTransient<IMdActionTypeDirector, MdActionTypeDirector>();
builder.Services.AddTransient<IMdAgencyDirector, MdAgencyDirector>();
builder.Services.AddTransient<IMdDocumentTypeDirector, MdDocumentTypeDirector>();
builder.Services.AddTransient<IMdModuleDirector, MdModuleDirector>();
builder.Services.AddTransient<IMdRoleDirector, MdRoleDirector>();
builder.Services.AddTransient<IMdSectionDirector, MdSectionDirector>();
builder.Services.AddTransient<IZmstProjectsDirector, ZmstProjectsDirector>();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<EncryptionDecryptionService, EncryptionDecryptionService>();
builder.Services.AddTransient<UtilityService, UtilityService>();
builder.Services.AddTransient<SMSService, SMSService>();
builder.Services.AddMvc();
builder.Services.AddTransient<IExtEndPointDirector, ExtEndPointDirector>();
builder.Services.AddTransient<ICaptchaDirector, CaptchaDirector>();
builder.Services.AddTransient<IMD_StatusDirector, MD_StatusDirector>();
builder.Services.AddTransient<IMD_PriorityDirector, MD_PriorityDirector>();
builder.Services.AddTransient<IJwtAuthenticationDirector, JwtAuthenticationDirector>();
builder.Services.AddTransient<IAppSettingDirector, AppSettingDataDirector>();
//builder.Services.AddTransient<RefreshTokenMiddlewareService, RefreshTokenMiddlewareService>();
builder.Services.AddSingleton<JWTTokenService, JWTTokenService>();
//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(
//        builder =>
//        {
//            builder.WithOrigins("https://localhost:44351", "http://localhost:4200", "http://localhost:54038", domain)
//                                .AllowAnyHeader()
//                                .AllowAnyMethod();
//        });
//});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyHeader()
                                .AllowAnyMethod().WithExposedHeaders("*");
        });
});
//builder.Configuration.GetConnectionString("HelpDeskAPI")
var key = "helpDeskSystemkey21072023";
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false,
        //ClockSkew = TimeSpan.Zero,

    };
}).AddCookie();



builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// DD E
builder.Services.AddDistributedMemoryCache();


builder.Services.AddSession(options =>
{
    // Set a short timeout for easy testing.
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//start region
builder.Services.AddHsts(options =>
{
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromDays(365);
});
//end region
var app = builder.Build();

// Configure the HTTP request pipeline.
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Access-Control-Expose-Headers", "NewToken");
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("X-XSS-Protection", "1;mode=block");
    //context.Response.Headers.Add("Strict-Transport-Security", "max-age=3153600000");Access-Control-Allow-Origin": "*",
    context.Response.Headers.Add("Referrer-Policy", "strict-origin-when-cross-origin");
    //context.Response.Headers.Add("Strict-Transport-Security", "max-age=3153600000");
    await next();
    });

////app.UseReferrerPolicy(options => options.NoReferrer());
////app.UseXContentTypeOptions();
////app.UseXXssProtection(options => options.EnabledWithBlockMode());
////app.UseXfo(options => options.Deny());
app.Use(async (context, next) =>
{
    if (!context.Response.Headers.ContainsKey("Feature-Policy"))
    {
        context.Response.Headers.Add("Feature-Policy", "accelerometer 'none'; camera 'none'; microphone 'none';");
    }
    await next();
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("v1/swagger.json", "HELPDESK");
    });
    app.UseCors();
    //Change
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseSession();
app.UseMiddleware<RefreshTokenMiddlewareService>();
app.Run();

