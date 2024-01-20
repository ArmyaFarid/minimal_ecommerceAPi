using API.Config;
using API.Context;
using API.Services;
using Microsoft.Extensions.Configuration;
using Serilog;

var build = new ConfigurationBuilder();
build.AddJsonFile("appsettings.json");
var configuration = build.Build();



var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
               .ReadFrom.Configuration(configuration)
               .CreateLogger();


builder.Services.AddSingleton(Log.Logger);
builder.Services.Configure<DbContextSettings>(configuration);
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IDbContextFactory, DbContextFactory>();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
                          options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


// Add services to the container.


//builder.Services.AddControllers();

//Cors origin
var allowedOrigin = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("myAppCors", policy =>
    {
        policy.WithOrigins(allowedOrigin)
                .AllowAnyHeader()
                .AllowAnyMethod();
    });
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("myAppCors");
}

app.UseRouting();


app.UseAuthorization();

app.MapControllers();

app.Run();
