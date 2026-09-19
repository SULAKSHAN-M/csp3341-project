using StudentRecordManagement.Web.Data;
using StudentRecordManagement.Web.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// Session is used only to remember which administrator is logged in
// (Pages/Login.cshtml.cs) — it holds a staff ID string, nothing sensitive.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// The Registrar holds the whole in-memory catalogue + student roster for
// this demo, seeded once at startup and shared for the life of the app.
builder.Services.AddSingleton(SeedData.Build());

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
