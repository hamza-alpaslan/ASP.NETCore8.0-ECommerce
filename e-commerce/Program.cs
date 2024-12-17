using e_commerce.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// MVC Controller deste�i
builder.Services.AddControllersWithViews();

// Session ve Cache deste�i
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum s�resi
    options.Cookie.HttpOnly = true; // �erez yaln�zca HTTP �zerinden eri�ilebilir
    options.Cookie.IsEssential = true; // �erez gerekli
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // HTTPS i�in zorunlu
    options.Cookie.SameSite = SameSiteMode.None; // �erez site d��� isteklerde g�nderilir
});


// Veritaban� ba�lam� i�in EF Core
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

app.UseSession(); // Oturum y�netimi
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
