using ECommerce_MW.DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DatabaseContext>(o =>         //Aqu� se especifica que es conexi�n a base de datos SQL
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));                        //Forma de inyectar dependencias, (o => o. ...) (o = options)lamdar ayuda a simplificar c�digos y m�todos
});

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();               //En tiempo de compilaci�n, puede agregar cambios de razor en las p�ginas sin tener que cerrar la aplicaci�n.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");             //Coge el controlador home y abre la acci�n Index

app.Run();