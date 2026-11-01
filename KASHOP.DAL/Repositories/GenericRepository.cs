using KASHOP.DAL.Data;
using KASHOP.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _context.Set<T>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveAsync(T entity, CancellationToken cancellationToken = default)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<T>> GetAsync(bool track = false, Expression<Func<T, bool>> filter = null, List<Expression<Func<T,object>>> includes = null, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _context.Set<T>().AsQueryable();

            if (!track)
                query = query.AsNoTracking();

            if (filter is not null)
                query = query.Where(filter);

            if (includes is not null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            var entities = await query.ToListAsync(cancellationToken);
            return entities;
        }

        public async Task<T?> GetOneAsync(
            bool track = false,
            Expression<Func<T, bool>> filter = null, List<Expression<Func<T,
            object>>> includes = null,
            CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _context.Set<T>().AsQueryable();

            if (!track)
                query = query.AsNoTracking();

            if (filter is not null)
                query = query.Where(filter);

            if (includes is not null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            var entity = await query.FirstOrDefaultAsync(filter, cancellationToken);
            return entity;
        }
    }
}
