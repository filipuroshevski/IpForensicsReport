using Data.Context;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    #region Fields

    private readonly ApplicationDbContext _context;

    #endregion

    #region Ctor

    public Repository(ApplicationDbContext context)
    {
        this._context = context;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Insert
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Update
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<TEntity?> DeleteAsync(int id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        if (entity == null)
        {
            return entity;
        }

        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    /// <summary>
    /// Deletes all asynchronous.
    /// </summary>
    /// <param name="entities">The entities.</param>
    /// <returns></returns>
    public async Task<List<TEntity>> DeleteAllAsync(List<TEntity> entities)
    {
        _context.RemoveRange(entities);
        await _context.SaveChangesAsync();

        return entities;
    }
    /// <summary>
    /// Get
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    /// <summary>
    /// GetAll
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    /// <summary>
    /// FindAsync
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().Where(predicate).ToListAsync();
    }

    /// <summary>
    /// GetAll
    /// </summary>
    /// <returns></returns>
    public async Task<IQueryable<TEntity>> GetAllAsyncQueryable()
    {
        return _context.Set<TEntity>().AsQueryable();
    }

    /// <summary>
    /// FindAsync
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public async Task<IQueryable<TEntity>> FindAsyncQueryable(Expression<Func<TEntity, bool>> predicate)
    {
        return _context.Set<TEntity>().Where(predicate).AsQueryable();
    }
    #endregion

}