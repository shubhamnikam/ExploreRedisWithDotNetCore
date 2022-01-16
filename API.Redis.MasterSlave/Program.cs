using API.Redis.MasterSlave.Services;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//setup mem cache
//builder.Services.AddSingleton<MemoryCache>();
//builder.Services.AddSingleton<ICacheService, MemoryCacheService>();
//setup redis cache
builder.Services.AddSingleton<IConnectionMultiplexer>(x =>
ConnectionMultiplexer.Connect(builder.Configuration.GetValue<string>("RedisConfig:ConnectionString")));
builder.Services.AddSingleton<ICacheService, RedisCacheService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
