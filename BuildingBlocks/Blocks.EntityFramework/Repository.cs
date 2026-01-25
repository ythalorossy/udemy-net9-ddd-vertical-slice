using Blocks.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blocks.EntityFramework;

public interface IRepository<TEntity>
    where TEntity : class, IEntity
{
    Task<TEntity?> FindByIdAsync(int id);
    Task<TEntity?> GetByIdAsync(TEntity id);
    Task<TEntity> AddAsync(TEntity entity);
    TEntity Update(TEntity entity);
    void Remove(TEntity entity);
    Task<bool> DeleteByIdAsync(int id);
}

public class Repository<TContext, TEntity>
    where TContext : DbContext
    where TEntity : class, IEntity
{
    protected readonly TContext _dbContext;
    protected readonly DbSet<TEntity> _entity;

    public string TableName => _dbContext.Model.FindEntityType(typeof(TEntity))?.GetTableName()!;

    public Repository(TContext dcContext)
    {
        _dbContext = dcContext;
        _entity = _dbContext.Set<TEntity>();
    }

    public TContext Context => _dbContext;
    public virtual DbSet<TEntity> Entity => _entity;

    protected virtual IQueryable<TEntity> Query() => _entity;

    public virtual async Task<TEntity?> FindByIdAsync(int id)
        => await _entity.FindAsync(id);

    public virtual async Task<TEntity?> GetByIdAsync(int id)
        => await Query().FirstOrDefaultAsync(e => e.Id == id);

    public virtual async Task<TEntity> AddAsync(TEntity entity)
        => (await _entity.AddAsync(entity)).Entity;

    public virtual TEntity Update(TEntity entity)
        => _entity.Update(entity).Entity;

    public virtual void Remove(TEntity entity)
        => _entity.Remove(entity);

    //public virtual async Task<bool> DeleteByIdAsync(int id)
    //    => await Task.FromResult(_entity.Remove(
    //        await GetByIdAsync(id) ?? 
    //        throw new InvalidOperationException($"Entity with id {id} not found"))
    //        is not null);

    public virtual async Task<bool> DeleteByIdAsync(int id)
    {
        var rowsAffected = await _dbContext.Database
            .ExecuteSqlInterpolatedAsync($"DELETE FROM {TableName} WHERE Id = {id}");

        return rowsAffected > 0;
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        => await _dbContext.SaveChangesAsync(ct);
}
