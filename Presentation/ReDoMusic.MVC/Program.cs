using ReDoMusic.Shared.Services;
using ReDoMusic.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Adding RequestCountService with dependency injection.
builder.Services.AddSingleton<RequestCountService>();

// Both the same thing but above one preferred.
builder.Services.AddSharedServices();
//builder.Services.AddSingleton<GuidGeneratorService>();

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
    pattern: "{controller=Brand}/{action=Index}");

app.Run();
