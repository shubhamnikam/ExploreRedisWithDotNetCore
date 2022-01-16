using StackExchange.Redis;

namespace API.Redis.PubSub.Services
{
    public class RedisPublisherService
    {
        private readonly IConnectionMultiplexer connectionMultiplexer;
        public IConfiguration configuration { get; }

        public RedisPublisherService(IConnectionMultiplexer connectionMultiplexer, IConfiguration configuration)
        {
            this.connectionMultiplexer = connectionMultiplexer;
            this.configuration = configuration;
        }


        internal Task PublishAsync(string message)
        {
            var publisher = connectionMultiplexer.GetSubscriber();
            var channel = configuration.GetValue<string>("RedisConfig:Channel");
            return publisher.PublishAsync(channel, message);
        }
    }
}
