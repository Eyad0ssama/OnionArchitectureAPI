using Microsoft.EntityFrameworkCore;
using Onion.Core.Entities;
using Onion.Core.Repositories;
using Onion.Core.Specification;
using Onion.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Repository
{
    internal class GenaricRepository
    {
    }
    public class GenaricRepository<T> : IGenaricRepository<T> where T : BaseEntity
    {
        private readonly OnionContext _dbContext;

        public GenaricRepository(OnionContext dbContext)
        {
            _dbContext = dbContext;
        }
        #region WithoutSpec
        public async Task<IEnumerable<T>> GetAll()

        {
            if (typeof(T) == typeof(Product)) 
            {
                
                 var product = await _dbContext.Products
                    .Include(p => p.ProductBrand)
                    .Include(p => p.ProductType)
                    .ToListAsync();
                return (IEnumerable<T>)product;
                
            }
            return await _dbContext.Set<T>().ToListAsync();
        }

       

        public async Task<T> GetByIdAsync(int id)
        
            => await _dbContext.Set<T>().FindAsync(id);

        public Task<T> GetByIdAsync()
        {
            throw new NotImplementedException();
        }
        #endregion

        public async Task<IEnumerable<T>> GetAllWithspec(ISpecification<T> Spec)
        {
            return await ApplySpecification(Spec).ToListAsync();
        }
        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> Spec)
        {
            return await ApplySpecification(Spec).FirstOrDefaultAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> Spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>(), Spec);
        }

       
    }
}


