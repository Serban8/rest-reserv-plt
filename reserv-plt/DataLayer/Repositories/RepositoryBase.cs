using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class RepositoryBase<T> where T : BaseModel
    {
        private readonly AppDbContext _context;
        protected readonly DbSet<T> DbSet;

        public RepositoryBase(AppDbContext context)
        {
            _context = context;
            DbSet = _context.Set<T>();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            try
            {
                return await DbSet.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T?> AddAsync(T entity)
        {
            try
            {
                entity.Id = Guid.NewGuid();
                var x = await DbSet.AddAsync(entity);
                return x.State == EntityState.Added ? x.Entity : null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            DbSet.Update(entity);
            return entity;
        }

        public void DeleteAsync(T entity)
        {
            DbSet.Remove(entity);
        }

        public async Task SaveAllChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
