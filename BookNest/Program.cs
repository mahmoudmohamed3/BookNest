using BookNest.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectiostring = builder.Configuration.GetConnectionString("DefaulConnection")
    ?? throw new InvalidOperationException("No Connection Sting");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(connectiostring));


builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddControllersWithViews();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
