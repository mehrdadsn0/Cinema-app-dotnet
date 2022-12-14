using Microsoft.EntityFrameworkCore;
using CinemaApp.Data;
using Microsoft.Extensions.Options;
using CinemaApp.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectioString")));

builder.Services.AddScoped<IActorsService, ActorsService>();
builder.Services.AddScoped<ICinemasService, CinemasService>();
builder.Services.AddScoped<IDirectorsService, DirectorsService>();
builder.Services.AddScoped<IMoviesService, MoviesService>();

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication().AddCookie("CinemaAppCookie", options =>
{
    options.Cookie.Name = "CinemaAppCookie";
});

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//seed database

AppDbInitializer.seed(app);
app.Run();


