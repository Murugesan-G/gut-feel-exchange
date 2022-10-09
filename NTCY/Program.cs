using NTCY.Database;
using NTCY.Exceptions;
using NTCY.Services;
using NTCY.Services.Members;
using NTCY.Services.MemberService;
using NTCY.Utils;
using NTCY.Configuration;
using NTCY.Services.Club;
using NTCY.Services.Table;
using NTCY.Services.FoodService;
using NTCY.Services.RoomDetail;
using NTCY.Services.LiquorDetails;

var builder = WebApplication.CreateBuilder(args);

{
    var services = builder.Services;
    services.AddMvc();
    services.AddDbContext<DataContext>();

    services.AddCors();
    services.AddControllers();

    services.AddAutoMapper(typeof(Program));

    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    services.AddScoped<IJwtUtils, JwtUtils>();
    services.AddScoped<IMemberService, MemberService>();
    services.AddScoped<IClubService, ClubService>();
    services.AddScoped<IClubCommitteeService, ClubCommitteeService>();
    services.AddScoped<ICardTableService,CardTableService>();
    services.AddScoped<IFoodService, FoodService>();
    services.AddScoped<IRoomDetails, RoomDetails>();
    services.AddScoped<ILiquorCategoryService, LiquorCategoryService>();
    services.AddScoped<ILiquorService, LiquorService>();
}

// Add services to the container.
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
    pattern: "{controller=Member}/{action=ViewMembers}/{id?}");

// configure HTTP request pipeline
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // global error handler
    app.UseMiddleware<ErrorHandlerMiddleware>();

    // custom jwt auth middleware
    //app.UseMiddleware<JwtMiddleware>();

    app.MapControllers();
    app.UseRouting();
}

app.Run();
