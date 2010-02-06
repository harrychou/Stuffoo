
using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using Stuffoo.Core.Models;

namespace Stuffoo.Core.DataAccess
{
    public class NHibernateRepository<T> : IRepository<T>
       where T : class, IIdentifiable
    {
        ISession _session;

        public NHibernateRepository(ISession session)
        {
            if (session == null)
                throw new ArgumentNullException("session");

            _session = session;
        }

        public IQueryable<T> Items
        {
            get { return _session.CreateCriteria(typeof(T)).List<T>().AsQueryable(); }
        }

        public void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            _session.Save(item);
        }

        public void Remove(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            _session.Delete(item);
        }

        public T FindById(int id)
        {
            return _session.Load<T>(id);
        }
    }
}