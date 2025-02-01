using System.Net;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(365); // Sessione di default di un anno
    options.Cookie.HttpOnly = true; // Cookie accessibile solo dal server
    options.Cookie.IsEssential = true; // Cookie essenziale (non richiede consenso)
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Cookie sicuro
    options.Cookie.MaxAge = TimeSpan.FromDays(365); // Cookie valido per un anno
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession(); // Add this line

app.Use(async (context, next) =>
{
    if (context.Session.TryGetValue("SessionTimeout", out var timeoutBytes))
    {
        var timeout = TimeSpan.FromTicks(BitConverter.ToInt64(timeoutBytes, 0));
        context.Session.Set("SessionTimeout", BitConverter.GetBytes(timeout.Ticks)); // Refresh session timeout
    }
    await next.Invoke();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
