using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;
        public GenericRepository(StoreContext dbContext)
        {
             _dbContext = dbContext;
        }

        #region without specefication
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            //SOLID => O => open for extention closed for modification
            //not compatable with solid 
            //solve it with Specefication Design Pattern
            return await _dbContext.Set<T>().ToListAsync();
        }


        public async Task<T> GetByIdAsync(int id)
        {
            //return await _dbContext.Set<T>().Where(x => x.Id == id).FirstOrDefaultAsync();
            return await _dbContext.Set<T>().FindAsync(id);
            //return await _dbContext.Set<T>().Where(p=>p.Id == id).Include(p => p.ProductBrand).Include(p => p.ProductType).ToListAsync();

        }

        #endregion


        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T> GetByIdWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
        
        private IQueryable<T>ApplySpecification(ISpecifications<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec);
        }

    }
}
