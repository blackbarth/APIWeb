﻿using APIWEB.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
namespace APIWEB.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected APPDBContext _context;

        public Repository(APPDBContext contexto)
        {
            _context = contexto;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public IQueryable<T> Get()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public T GetById(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().SingleOrDefault(predicate); ;
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Update(entity);
        }

        public List<T> LocalizaPagina<Tipo>(int numeroPagina, int quantidadeRegistro) where Tipo : class
        {
            return _context.Set<T>()
                .Skip(quantidadeRegistro * (numeroPagina - 1))
                .Take(quantidadeRegistro).ToList();
        }

        public int GetTotalRegistros()
        {
            return _context.Set<T>().AsNoTracking().Count();
        }
    }
}
