using KCIAOGS24.NET.Infraestructure.Data.Repositories;
using KCIAOGS24.NET.Application.Services;
using KCIAOGS24.NET.Application.Interfaces;
using KCIAOGS24.NET.Domain.Interfaces;




using KCIAOGS24.NET.Infraestructure.Data.AppData;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(options => {

    options.UseOracle(builder.Configuration.GetConnectionString("Oracle"));

});

builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddTransient<IUsuarioApplicationService, UsuarioApplicationService>();

builder.Services.AddTransient<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddTransient<IEnderecoApplicationService, EnderecoApplicationService>();

builder.Services.AddTransient<IEnergiaSolarRepository, EnergiaSolarRepository>();
builder.Services.AddTransient<IEnergiaSolarApplicationService, EnergiaSolarApplicationService>();

builder.Services.AddTransient<IEnergiaEolicaRepository, EnergiaEolicaRepository>();
builder.Services.AddTransient<IEnergiaEolicaApplicationService, EnergiaEolicaApplicationService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
