using amigos_dev.Infrastructure;
using amigos_dev.Infrastructure.InversionOfControl;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Setup Session
builder.Services.AddSession(options =>
options.Cookie.Name = "selectedFriends");

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<FDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("FriendDB"));
});

DependencyInjection.Inject(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//sessão
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
