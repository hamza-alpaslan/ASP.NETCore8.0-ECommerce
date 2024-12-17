using e_commerce.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// MVC Controller desteði
builder.Services.AddControllersWithViews();

// Session ve Cache desteði
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum süresi
    options.Cookie.HttpOnly = true; // Çerez yalnýzca HTTP üzerinden eriþilebilir
    options.Cookie.IsEssential = true; // Çerez gerekli
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // HTTPS için zorunlu
    options.Cookie.SameSite = SameSiteMode.None; // Çerez site dýþý isteklerde gönderilir
});


// Veritabaný baðlamý için EF Core
builder.Services.AddDbContext<ApplicationDbcContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession(); // Oturum yönetimi
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
