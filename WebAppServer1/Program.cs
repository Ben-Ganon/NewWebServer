using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebAppServer1.Controllers;
using WebAppServer1.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WebAppServer1Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebAppServer1Context") ?? throw new InvalidOperationException("Connection string 'WebAppServer1Context' not found.")));


//builder.Services.AddDbContext<WebAppServer1Context>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("WebAppServer1Context") ?? throw new InvalidOperationException("Connection string 'WebAppServer1Context' not found.")));

HardContext.newDB();


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSwaggerGen();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(1);
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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
    pattern: "{controller=Reviews}/{action=Index}/{id?}");

app.UseSession();

app.Run();
