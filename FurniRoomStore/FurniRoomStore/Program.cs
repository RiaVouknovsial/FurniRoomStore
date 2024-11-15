using FurniRoomStore;
using FurniRoomStore.Interfaces;
using FurniRoomStore.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;


var builder = WebApplication.CreateBuilder(args);

// Настройка логирования
builder.Logging.ClearProviders();
builder.Logging.AddConsole();  // Добавление логирования в консоль
builder.Logging.AddDebug();    // Логирование в отладчик

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddScoped<IRepository<T>, Repository<T>();

//Регистрация репозиториев
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();


//Регистрация сервисов
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<OrderService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FurniRoomStore API", Version = "v1" });
    c.MapType<IFormFile>(() => new OpenApiSchema
    {
        Type = "file",
        Format = "binary"
    });
});

builder.Services.AddControllers();
builder.Services.AddDbContext<FurniRoomStoreContext>(options =>
   options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
   new MySqlServerVersion(new Version(8, 0, 25))));

//builder.Services.AddDbContext<FurniRoomStoreContext>(static options =>
//    options.UseInMemoryDatabase("InMemoryDb"));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FurniRoomStore API V1");
    }); 
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
