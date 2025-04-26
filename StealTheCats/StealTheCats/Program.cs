using CatsLibrary.Extensions;
using DatabaseContext.DBHelper.Methods;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<Extensions.HttpClient.CatApiSettings>(
    builder.Configuration.GetSection("CatApiSettings"));


// Local Db SQL Configuration
builder.Services.AddDbContext<DatabaseContext.DBHelper.Methods.DbHelper>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsAssembly("StealTheCats")));

// Docker MSSQL Configuration

//var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
//var dbName = Environment.GetEnvironmentVariable("DB_NAME");
//var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");

//// Use SA credentials:

//var connectionString =
//  $"Data Source={dbHost};" +
//  $"Initial Catalog={dbName};" +
//  $"User ID=sa;Password={dbPassword};" +
//  "TrustServerCertificate=True;Encrypt=True;";

//builder.Services.AddDbContext<DbHelper>(opts =>
//    opts.UseSqlServer(connectionString));

builder.Services.AddHttpClient();
builder.Services.AddCatsLibraryServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// On Docker Check the DB
//app.Services.InitializeDatabase();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();