using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repository;
using Repository.Repository.IRepository;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddTransient<IItemRepository, ItemRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=items}/{action=Index}/{id?}");
});
app.MapRazorPages();

app.Run();
