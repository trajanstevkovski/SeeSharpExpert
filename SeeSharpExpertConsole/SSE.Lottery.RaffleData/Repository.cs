using Microsoft.EntityFrameworkCore;
using SSE.Lottery.RaffleData.Model;
using System.Linq;

namespace SSE.Lottery.RaffleData
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected DbSet<T> DbSet;

        public Repository(DbContext dbContext)
        {
            DbSet = dbContext.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public void Insert(T entity)
        {
            DbSet.Add(entity);
        }
    }
}
