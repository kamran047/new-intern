using DataLayer;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BusinessLogic
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> dbSet;
        private IContext _context;

        public BaseRepository(IContext context)
        {
            _context = context;
            this.dbSet = _context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get()
        {
            return dbSet.ToList();
        }

        public TEntity GetOne(int id)
        {
            return dbSet.Find(id);
        }

        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entityToDelete = dbSet.Find(id);
            dbSet.Remove(entityToDelete);
            _context.SaveChanges();
        }
    }
}
