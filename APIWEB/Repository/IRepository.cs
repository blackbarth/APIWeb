using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace APIWEB.Repository
{
    public interface IRepository<T>
    {
        IQueryable<T> Get();
        T GetById(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        List<T> LocalizaPagina<Tipo>(int numPag, int tamPag) where Tipo : class;
        int GetTotalRegistros();
    }
}
