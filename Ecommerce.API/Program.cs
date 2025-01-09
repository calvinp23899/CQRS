using Ecommerce.API.ServiceExtensions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Environment.GetEnvironmentVariable("SYNCKEY", EnvironmentVariableTarget.Machine));
builder.Services.ConfigOptionsPattern(builder.Configuration);
builder.Services.ConfigMediatR();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.ConfigAutoMapper();
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.AddCustomServices();
builder.Services.AddSwaggerGen();
builder.Services.ConfigSwaggerGen();
builder.Services.ConfigAuthorization();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
string env = builder.Environment.EnvironmentName;
//builder.Configuration
//    .SetBasePath(Directory.GetCurrentDirectory())
//    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
//    .AddEnvironmentVariables();

var app = builder.Build();
app.ApplyMigration();
if (app.Environment.IsProduction())
    app.UseHsts();
// Configure the HTTP request pipeline.
app.UseExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
