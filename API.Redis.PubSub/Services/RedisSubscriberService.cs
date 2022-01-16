using StackExchange.Redis;

namespace API.Redis.PubSub.Services
{
    public class RedisSubscriberService : BackgroundService
    {
        private readonly IConnectionMultiplexer connectionMultiplexer;
        public IConfiguration configuration { get; }

        public RedisSubscriberService(IConnectionMultiplexer connectionMultiplexer, IConfiguration configuration)
        {
            this.connectionMultiplexer = connectionMultiplexer;
            this.configuration = configuration;
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("RedisSubscriberService Started...");
            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //subscribe to redis channel
            var subscriber = connectionMultiplexer.GetSubscriber();
            var channel = configuration.GetValue<string>("RedisConfig:Channel");
            return subscriber.SubscribeAsync(channel, (ch, value) =>
            {
                Console.WriteLine($"Event triggered at subscriber, channel={ch}, message={value}");
            });
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("RedisSubscriberService Stopped...");
            return base.StopAsync(cancellationToken);
        }
    }
}
