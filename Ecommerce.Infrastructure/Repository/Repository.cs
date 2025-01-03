using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using Ecommerce.Core.Constants;
using Microsoft.AspNetCore.Http;


namespace Ecommerce.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly EcommerceDbContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly IUserService _userService;
        public Repository(EcommerceDbContext context, IUserService userService)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _userService = userService;
        }

        public async Task CreateAsync(T entity)
        {
            string username = _userService.GetUserName();
            entity.CreatedBy = username is null ? StringConstant.SystemDefault : username;
            entity.CreatedDate = DateTime.UtcNow;
            entity.IsDeleted = false;
            _context.Set<T>().Add(entity);
        }

        public async Task<IQueryable<T>> GetAllAsync(bool? isGetAll = false)
        {
            return await Task.Run(() =>
            {
                IQueryable<T> query = _dbSet;
                if (isGetAll == false)
                {
                    query = query.Where(x => x.IsDeleted == false);
                }

                return query;
            });
        }

        public async Task<T?> GetByIdAsync(int id, List<string>? relatedProperties = null, bool? isGetAll = false)
        {
            if (relatedProperties == null)
            {
                var entity = await _dbSet.FindAsync(id);
                if (isGetAll == false)
                {
                    return entity?.IsDeleted == false ? entity : null;
                }

                return entity;
            }

            var query = _dbSet.AsQueryable();
            if (isGetAll == false)
            {
                query = query.Where(x => x.IsDeleted == false);
            }

            foreach (var property in relatedProperties)
            {
                query = query.Include(property);
            }

            var result = await query.FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public Task HardDeleteAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task HardDeleteAsync(int id)
        {
            await Task.Run(() =>
            {
                T? entity = _dbSet.Find(id);
                if (entity != null)
                    _dbSet.Remove(entity);
            });
        }

        public async Task<T> InsertAsync(T entity)
        {
            string username = _userService.GetUserName();
            await Task.Run(() =>
            {
                entity.CreatedBy = username is null ? StringConstant.SystemDefault : username;
                entity.CreatedDate = DateTime.UtcNow;
                entity.IsDeleted = false;
                _dbSet.Add(entity);
            });
            await SaveAsync();
            return entity;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IQueryable<T>> SearchAsync(Expression<Func<T, bool>>? filter = null, bool? isGetAll = false)
        {
            return await Task.Run(() =>
            {
                IQueryable<T> query = _dbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (isGetAll == false)
                {
                    query = query.Where(x => x.IsDeleted == false);
                }

                return query;
            });
        }

        public async Task SoftDeletedAsync(int id)
        {
            T? entity = _dbSet.Find(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                await UpdateAsync(entity);
            }
        }

        public async Task UpdateAsync(T entity)
        {
            string username = _userService.GetUserName();
            await Task.Run(() =>
            {
                entity.UpdatedDate = DateTime.UtcNow;
                entity.UpdatedBy = username == null ? StringConstant.SystemDefault : username;
                _dbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            });
        }
    }
}
