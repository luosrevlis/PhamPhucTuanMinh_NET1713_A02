using DAOs;
using Microsoft.EntityFrameworkCore;
using PhamPhucTuanMinhRazorPages.Constants;
using PhamPhucTuanMinhRazorPages.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<FuminiHotelManagementContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString(ConfigConst.ConnectionStringKey)),
    ServiceLifetime.Singleton);
builder.Services.ConfigureServices();
builder.Services.AddSession();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
