using System.Collections.Generic;

namespace YMovies.Database.Repositories.IRepository
{
    interface IRepository<T>
    {
        IEnumerable<T> Items { get; }
        T GetItem(int id);
        void AddItem(T item);
        void UpdateItem(T item);
        void DeleteItem(int id);
    }
}
