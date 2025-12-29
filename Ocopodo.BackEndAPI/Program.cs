using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ocopodo.Application.Catalog.Products;
using Ocopodo.Application.Common;
using Ocopodo.Data.EF;
using Ocopodo.Utilities.Constants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<OcopodoDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(SystemConstant.MainConnectionStringName)));

builder.Services.AddTransient<IStorageService, FileStorageService>();
builder.Services.AddTransient<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
