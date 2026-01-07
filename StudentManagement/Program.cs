using StudentManagement.Context;
using StudentManagement.Repository;
using StudentManagement.Repository.Interface;
using StudentManagement.Service;
using StudentManagement.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews()
    .AddSessionStateTempDataProvider();


var app = builder.Build();
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IStudentsService, StudentsService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
