using System.Collections;
using Microsoft.EntityFrameworkCore;
using Net8StarterAuthApi;
using Net8StarterAuthApi.Auth;
using Net8StarterAuthApi.Data;
using Net8StarterAuthApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
var connectionString = builder.Configuration["DbConnection"];
builder.Services.AddDbContext<ApiDbContext>(options =>
    {
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        options.UseSnakeCaseNamingConvention();
    }
);

//app dependency injection
builder.Services.AddScoped<IUserService, UserService>();




//custom extensions
builder.Services.ConfigureAuthWithAws(builder.Configuration);
builder.Services.AddCorsCustom(builder.Configuration);
builder.Services.AddSwaggerDocExtensions(builder.Configuration);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddHealthChecks();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
    //@todo Remove 
    foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
        Console.WriteLine("  {0} = {1}", de.Key, de.Value);
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/health");
app.UseHttpsRedirection();

//if using auth
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();