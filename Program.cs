using COOPGO.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//services cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder
            .WithOrigins("http://localhost:8080") // coloque a origem que está rodando seu front
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

//app cors
//app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("corsapp");
app.UseCors("AllowAllOrigins");
app.UseAuthorization();
app.UseStaticFiles();

//app.UseCors(prodCorsPolicy);

app.MapControllers();

app.Run();