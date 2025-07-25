using DL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string connectionString = builder.Configuration.GetConnectionString("JGuevaraLogin");

builder.Services.AddDbContext<JguevaraProgramacionNcapasFebreroContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<BL.UsuarioLogin>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Form}/{id?}");

app.Run();
