using SSE.Lottery.RaffleData.Model;
using System.Linq;

namespace SSE.Lottery.RaffleData
{
    public interface IRepository<T> where T : IEntity
    {
        void Insert(T entity);
        void Delete(T entity);

        IQueryable<T> GetAll();
        T GetById(int id);
    }
}
