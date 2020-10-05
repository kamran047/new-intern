using DataLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class BaseRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> dbSet;
        Context context;

        public BaseRepository(Context context)
        {
            this.context=context;
            this.dbSet = context.Set<TEntity>();

        }

        public IEnumerable<TEntity> Get()
        {
                 return dbSet.ToList();
            
        }

        public void Add(TEntity entity)
        {
                    dbSet.Add(entity);
                    context.SaveChanges();
        }

        public void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
            context.SaveChanges();
               
        }

        public void Delete(int id)
        {
            var entityToDelete = dbSet.Find(id);
            dbSet.Remove(entityToDelete);
            context.SaveChanges();
        }
    }
}
