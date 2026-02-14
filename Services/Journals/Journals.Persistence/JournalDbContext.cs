using Journals.Domain.Journals;
using Redis.OM;
using Redis.OM.Searching;
using StackExchange.Redis;

namespace Journals.Persistence;

public class JournalDbContext
{
    private readonly RedisConnectionProvider _provider;
    private readonly IDatabase _redisDb;

    public JournalDbContext(IConnectionMultiplexer redis, RedisConnectionProvider provider)
        => (_provider, _redisDb) = (provider, redis.GetDatabase());

    #region Collections

    public IRedisCollection<Journal> Journals => _provider.RedisCollection<Journal>();

    public IRedisCollection<Editor> Editors => _provider.RedisCollection<Editor>();

    #endregion

    public RedisConnectionProvider Provider => _provider;
}
