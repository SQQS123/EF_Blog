using BlogSystem.IDAL;
using BlogSystem.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DAL
{
    public class BaseService<T>:IBaseService<T> where T:BaseEntity,new()
    {
        private readonly BlogContext _Db;
        public BaseService(Models.BlogContext db)
        {
            _Db = db; 
        }
        public async Task CreateAsync(T model, bool saved = true)
        {
            _Db.Set<T>().Add(model);
            if (saved) await _Db.SaveChangesAsync();
        }

        public async Task EditAsync(T mode, bool saved)
        {
            _Db.Configuration.ValidateOnSaveEnabled = false;
            _Db.Entry(mode).State = EntityState.Modified;
            if (saved)
            {
                await _Db.SaveChangesAsync();
                _Db.Configuration.ValidateOnSaveEnabled = true;
            }
        }

        public async Task RemoveAsync(Guid id, bool saved = true)
        {
            _Db.Configuration.ValidateOnSaveEnabled = false;
            var t = new T() {Id = id};
            _Db.Entry(t).State = EntityState.Unchanged;
            t.IsRemoved = true;
            if (saved)
            {
                await _Db.SaveChangesAsync();
                _Db.Configuration.ValidateOnSaveEnabled = true;
            }
        }

        public async Task RemoveAsync(T model, bool saved = true)
        {
            await RemoveAsync(model.Id, saved);
        }

        public async Task Save()
        {
            await _Db.SaveChangesAsync();
            _Db.Configuration.ValidateOnSaveEnabled = true;
        }

        public async Task<T> GetOneByIdAsync(Guid id)
        {
            return await GetAllAsync().FirstAsync(m => m.Id == id);
        }

        public IQueryable<T> GetAllAsync()
        {
            return _Db.Set<T>().Where(m => !m.IsRemoved).AsNoTracking();
        }

        public IQueryable<T> GetAllByPageAsync(int pageSize = 10, int pageIndex = 0)
        {
            return GetAllAsync().Skip(pageSize * pageIndex).Take(pageSize);
        }

        public IQueryable<T> GetAllOrderAsync(bool asc = true)
        {
            var datas = GetAllAsync();
            datas = asc ? datas.OrderBy(m => m.CreateTime) : datas.OrderByDescending(m => m.CreateTime);

            return datas;
        }

        public IQueryable<T> GetAllByPageOrderAsync(int pageSize = 10, int pageIndex = 0, bool asc = true)
        {
            return GetAllOrderAsync(asc).Skip(pageSize * pageIndex).Take(pageSize);
        }

        public void Dispose()
        {
            _Db.Dispose();
        }
    }
}
