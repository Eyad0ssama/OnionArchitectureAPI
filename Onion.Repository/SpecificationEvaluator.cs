using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Onion.Core.Entities;
using Onion.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Repository
{
    public class SpecificationEvaluator<T>where T:BaseEntity
    {
        public static IQueryable<T>GetQuery(IQueryable<T>inputQuery,ISpecification<T> spec)
        {
            var Query = inputQuery;
            if(spec.Criteria is not null)
            {
                Query = Query.Where(spec.Criteria);
            }

            if (spec.OrderBy is not null)
            {
                Query = Query.OrderBy(spec.OrderBy);
            }
            else if (spec.OrderByDesc is not null)
            {
                Query = Query.OrderByDescending(spec.OrderByDesc);
            }
            if (spec.IsPagingEnabled)
            {
                Query = Query.Skip(spec.Skip).Take(spec.Take);
            }


            Query = spec.Includes.Aggregate(Query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));
            return Query;
        }

        
    }
}
