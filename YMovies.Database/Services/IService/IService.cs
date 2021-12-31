using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YMovies.Database.Services.IService
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
