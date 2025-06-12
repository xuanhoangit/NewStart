using System.Drawing;
using System.Text;
using DotNetEnv;
using IVY.Application.Interfaces.IRepository;
using IVY.Application.Interfaces.IServices.Product;
using IVY.Application.Services;
using IVY.Application.Services.Products;
using IVY.Domain.Models.Users;
using IVY.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SneakerAPI.Infrastructure.Repositories;


var  AllowHostSpecifiOrigins = "_allowHostSpecifiOrigins";
var builder = WebApplication.CreateBuilder(args);
Env.Load();
builder.Configuration
    .AddEnvironmentVariables()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowHostSpecifiOrigins,
                      policy  =>
                      {
                          policy
                            .WithOrigins(Environment.GetEnvironmentVariable("OriginHost"),"http://127.0.0.1:5500")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials(); // nếu bạn gửi cookie/token theo kiểu credentials
                      });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<IVYDbContext>(options =>
    options.UseSqlServer(
        Environment.GetEnvironmentVariable("ConnectionString"),
        sqlOptions =>
            sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery).
            MigrationsAssembly("IVYDashboard.API")
        )
         // <- đúng chỗ
);

// builder.Services.AddScoped<IVnpay, Vnpay>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOutfitService, OutfitService>();
builder.Services.AddScoped<ISizeService, SizeService>();
builder.Services.AddScoped<IColorService, ColorService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICollectionService, CollectionService>();
builder.Services.AddScoped<ISubColorService, SubColorService>();
builder.Services.AddScoped<IProductSubColorFileService, ProductSubColorFileService>();
builder.Services.AddScoped<IProductSubColorService, ProductSubColorService>();
builder.Services.AddTransient<IUnitOfWork,UnitOfWork>();
// builder.Services.AddTransient<IEmailSender,EmailSender>();
// builder.Services.AddTransient<IEmailSender,EmailSender>();
// builder.Services.AddTransient<IJwtService,JwtService>();
builder.Services.AddIdentity<EmployeeIdentity, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<IVYDbContext>()
    .AddDefaultTokenProviders();

//*************** Tất cả config**
var config=builder.Configuration;


//SetConfigEmailSettings
config["ConnectionStrings:SneakerAPIConnection"]=Environment.GetEnvironmentVariable("ConnectionString");
config["EmailSettings:SmtpServer"]=Environment.GetEnvironmentVariable("SmtpServer");
config["EmailSettings:SmtpPort"]=Environment.GetEnvironmentVariable("SmtpPort");
config["EmailSettings:SmtpUser"]=Environment.GetEnvironmentVariable("SmtpUser");
config["EmailSettings:SmtpPass"]=Environment.GetEnvironmentVariable("SmtpPass");
//SetConfigVNPAY
config["Vnpay:TmnCode"]=Environment.GetEnvironmentVariable("TmnCode");
config["Vnpay:HashSecret"]=Environment.GetEnvironmentVariable("HashSecret");
config["Vnpay:BaseUrl"]=Environment.GetEnvironmentVariable("BaseUrl");
config["Vnpay:ReturnUrl"]=Environment.GetEnvironmentVariable("ReturnUrl");
//SetDataEmailSettingModel
// builder.Services.Configure<EmailSettings>(config.GetSection("EmailSettings"));
// builder.WebHost.ConfigureKestrel(serverOptions =>
// {   
//     serverOptions.ListenAnyIP(5001); // HTTP
//     serverOptions.ListenAnyIP(444, listenOptions =>
//     {
//         listenOptions.UseHttps(Environment.GetEnvironmentVariable("FILECERT"), "mypassword");
//     });
// });
builder.Services.AddAuthentication(
    options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
)   
.AddJwtBearer(options =>
{   
    
    // Cấu hình JWT Bearer Authentication
    options.TokenValidationParameters = new TokenValidationParameters
    {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         
          // Cấu hình RoleClaimType đúng với token của bạn
        RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
         ValidIssuer = Environment.GetEnvironmentVariable("JWT__ValidIssuer"),
         ValidAudience = Environment.GetEnvironmentVariable("JWT__ValidAudience"),

         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT__Secret")))
    };


})
.AddCookie()
.AddGoogle(options =>
{
    options.ClientId = Environment.GetEnvironmentVariable("CLIENT_ID");
    options.ClientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET");
    options.CallbackPath="/signin-google";
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();
//End Cònig
// Đăng ký AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddHttpContextAccessor();

builder.Services.AddMemoryCache(); // Thêm dịch vụ MemoryCache
// builder.Services.AddSession(); // Thêm dịch vụ Session
builder.Services.AddDistributedMemoryCache(); // Cần thiết cho Session

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseRouting();
app.UseCors(AllowHostSpecifiOrigins);
// Sử dụng Authentication và Authorization


app.UseAuthentication();
app.UseAuthorization();
app.MapControllers(); 

app.Run();

