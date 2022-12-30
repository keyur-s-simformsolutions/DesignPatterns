using Throttling.Data;
using Microsoft.EntityFrameworkCore;
using Throttling.Core;
using AspNetCoreRateLimit;
using Throttling.Core.IRepository;
using Throttling.Core.Services;
using Throttling.Core.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddMemoryCache();

// for Rate Limiting and Throttling
builder.Services.ConfigureRateLimiting();
builder.Services.AddHttpContextAccessor();

//services.AddResponseCaching();
builder.Services.ConfigureHttpCacheHeader();

builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);

builder.Services.AddCors(c =>
{
    c.AddPolicy("CorsPolicy-AllowAll", builder =>
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
});

builder.Services.ConfigureAutoMapper();

builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthManager, AuthManager>();

//services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelListing", Version = "v1" });
//});
builder.Services.ConfigureSwaggerGen(builder.Configuration);

//builder.Services.AddControllers().AddNewtonsoftJson(o =>
//    o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
//);

// for API versioning
builder.Services.ConfigureVersioning();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}
app.ConfigureSwagger(builder.Configuration);

app.ConfigureExceptionHandler();

app.UseHttpsRedirection();

app.UseCors("CorsPolicy-AllowAll");

// for Caching
app.UseResponseCaching();

// for Rate Limiting and Throttling
app.UseHttpCacheHeaders();
app.UseIpRateLimiting();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
