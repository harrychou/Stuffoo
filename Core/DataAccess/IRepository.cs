using System.Collections.Generic;
using System.Linq;
using Stuffoo.Core.Models;

namespace Stuffoo.Core.DataAccess
{
    public interface IRepository<T>
        where T : class, IIdentifiable
    {
        IQueryable<T> Items { get; }

        void Add(T item);

        void Remove(T item);

        T FindById(int id);

    }
}