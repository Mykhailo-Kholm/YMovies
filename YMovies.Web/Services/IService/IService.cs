using System.Collections.Generic;

namespace YMovies.MovieDbService.Services.IService
{
    interface IService<T>
    {
        IEnumerable<T> Items { get; }
        T GetItem(int id);
        void AddItem(T item);
        void UpdateItem(T item);
        void DeleteItem(T item);
    }
}
