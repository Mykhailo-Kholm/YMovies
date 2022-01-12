using System.Collections.Generic;
using System.Linq;

namespace YMovies.MovieDbService.Repositories.IRepository
{
    public interface IRepository<T>
    {
        IEnumerable<T> Items { get; }
        T GetItem(int id);
        void AddItem(T item);
        void UpdateItem(T item);
        void DeleteItem(int id);
    }
}
