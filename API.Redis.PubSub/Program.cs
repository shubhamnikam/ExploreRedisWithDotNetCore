using API.Redis.PubSub.Services;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//setup redis cache
builder.Services.AddSingleton<IConnectionMultiplexer>(x =>
ConnectionMultiplexer.Connect(builder.Configuration.GetValue<string>("RedisConfig:ConnectionString")));
//setup redis pubsub
builder.Services.AddHostedService<RedisSubscriberService>();
builder.Services.AddScoped<RedisPublisherService>();

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
