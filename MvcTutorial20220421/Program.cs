// 1. �w�� Nuget �M�� Microsoft.EntityFrameworkCore.SqlServer
// 2. �w�� Nuget �M�� Microsoft.EntityFrameworkCore.Tools
// 3. ���� DB model�G
//      Scaffold-DbContext "Server=���A����}�ΦW��;Initial Catalog=��Ʈw�W��;Integrated Security=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -f
// 4. �R�� DbContext ���g�����s�u�r��
// 5. �b appsettings.Development.json �W�[�s�u�r��
// 6. ���U Entity Framework service



using Microsoft.EntityFrameworkCore;
using MvcTutorial20220421.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DemoDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

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
