using Onion.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Core.Specification
{
    public class BaseSpecifications<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get ; set ; }
        public List<Expression<Func<T, object>>> Includes { get ; set ; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get ; set ; }
        public Expression<Func<T, object>> OrderByDesc { get ; set ; }
        public int Take { get ; set ; }
        public int Skip { get ; set ; }
        public bool IsPagingEnabled { get; set; }



        //Get All
        public BaseSpecifications()
        {
            //Includes = new List<Expression<Func<T, object>>>();
          
        }
        //Get By Id
        public BaseSpecifications(Expression<Func<T, bool>> criteriaExpression)
        {
            Criteria = criteriaExpression;
            
        }

        public void SetOrderBy(Expression<Func<T, object>>OrderByExpression)
        {
            OrderBy = OrderByExpression;
        }
        public void SetOrderByDesc(Expression<Func<T, object>>OrderByDescExpression)
        {
            OrderByDesc = OrderByDescExpression;
        }
        public void ApplayPagination(int skip, int take)
        {
            IsPagingEnabled = true;
            Skip = skip;
            Take = take;

        }
        

    }

}
