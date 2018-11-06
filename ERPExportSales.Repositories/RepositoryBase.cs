using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ERPExportSales.Repositories
{
    public abstract class RepositoryBase<T> where T : class
    {
        private readonly IDbSet<T> dbset;
        private ApplicationDbContext dataContext;

        protected RepositoryBase(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            dbset = DataContext.Set<T>();
        }

        protected IDatabaseFactory DatabaseFactory { get; }

        protected ApplicationDbContext DataContext
        {
            get { return dataContext ?? (dataContext = DatabaseFactory.Get()); }
        }

        public virtual void Add(T entity)
        {
            dbset.Add(entity);
        }

        // http://www.cnblogs.com/scy251147/p/3688844.html
        public bool RefreshEntityInContext(T entity)
        {
            var objContext = ((IObjectContextAdapter)dataContext).ObjectContext;
            var objSet = objContext.CreateObjectSet<T>();
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);

            object foundEntity;
            var exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);

            if (exists)
            {
                objContext.Detach(foundEntity);
            }

            dbset.Attach(entity);

            return (exists);
        }

        public virtual void Update(T entity)
        {
            dbset.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            dbset.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            var objects = dbset.Where(where).AsEnumerable();
            foreach (var obj in objects)
                dbset.Remove(obj);
        }

        public virtual T GetById(long id)
        {
            return dbset.Find(id);
        }

        public virtual T GetById(string id)
        {
            return dbset.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbset.ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).ToList();
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return dbset.FirstOrDefault(where);
        }
    }

    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        T GetById(long id);
        T GetById(string id);
        T Get(Expression<Func<T, bool>> where);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
        bool RefreshEntityInContext(T entity);
    }
}