using Onion.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Core.Specification
{
    public interface ISpecification<T> where T:BaseEntity
    {
        //_dbContext.Products
        //            .Include(p => p.ProductBrand)
        //            .Include(p => p.ProductType)
        //            .ToListAsync();
        public Expression <Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T,object>>> Includes { get; set; }
        //OrderBy
        public Expression<Func<T,object>> OrderBy { get; set; }
        //OrderByDes
        public Expression<Func<T,object>> OrderByDesc{ get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPagingEnabled { get; set; }

    }
}
