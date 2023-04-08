using StackExchange.Redis;

namespace FinalMS.Basket.Services.Redis;

public class RedisService
{
    private readonly string _host;

    private readonly int _port;

    private ConnectionMultiplexer _multiplexer;

    public RedisService(string host, int port)
    {
        _host = host;
        _port = port;
    }

    public void Connect() => _multiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");

    public IDatabase GetDb(int db = 1) => _multiplexer.GetDatabase(db);
}
