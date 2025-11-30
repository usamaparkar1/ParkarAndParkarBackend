using Application.Services.ContactUs;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

#region Main
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Step 1: Configure environment-specific settings
ConfigureEnvironmentSettings(builder);

// Step 2: Add DbContext with SQL Server provider
AddDatabaseContextWithSqlServer(builder);

// Step 3: Register Dependencies and Services for API
RegisterDependenciesForApi(builder.Services);

// Step 4: Add Controllers and Swagger
AddControllersAndSwagger(builder);

// Final Step: Build the application
BuildAndRunApplication(builder);
#endregion

#region Methods
// Step 1: Configure Environment-Specific Settings
static void ConfigureEnvironmentSettings(WebApplicationBuilder builder)
{
    builder.Configuration
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables();
}

// Step 2: Add DbContext with SQL Server provider
static void AddDatabaseContextWithSqlServer(WebApplicationBuilder builder)
{
    string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    builder.Services.AddDbContext<ParkarAndParkarDbContext>(options =>
    {
        options.UseSqlServer(connectionString);
    });
}

// Step 3: Register Dependencies and Services for API
static void RegisterDependenciesForApi(IServiceCollection services)
{
    services.AddScoped<IContactUsService, ContactUsService>();
}

static void AddControllersAndSwagger(WebApplicationBuilder builder)
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Add CORS
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
        });
    });
}

static void BuildAndRunApplication(WebApplicationBuilder builder)
{
    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseCors("AllowAll");
    app.MapControllers();

    app.Run();
}
#endregion