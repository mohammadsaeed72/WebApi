using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SampleWebApi._1.Entities;
using SampleWebApi._2.Database;
using SampleWebApi._2.Database.Repositories;
using SampleWebApi._3.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMyAuthenticationService, MyAuthenticationService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IBasketService, BasketService>();



builder.Services.AddScoped<IItemRepository, ItemRepository>();




builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr"));
});


// For Identity
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

builder.Services.AddCap(capConf =>
{
    capConf.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr"));
    capConf.UseRabbitMQ(conf =>
    {
        conf.HostName = "rabbitmq";
        conf.Port = 5672;
        //conf.UserName = "root";
        //conf.Password="password";
    });
});

builder.Services.AddStackExchangeRedisCache(op =>
{
    op.Configuration = "cache:6379,password=p@sword!2#";
});


var app = builder.Build();

// Configure the HTTP request pipeline.
using (var scope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();



app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
