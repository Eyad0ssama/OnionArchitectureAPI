using Onion.Core.Entities;
using Onion.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Core.Repositories
{
    public interface IGenaricRepository<T> where T:BaseEntity
    {
        #region without Spec
        Task<IEnumerable<T>> GetAll();
        Task<T> GetByIdAsync(int id);
        #endregion

        #region With spec
        Task<IEnumerable<T>> GetAllWithspec(ISpecification<T> spec );
        Task<T> GetByIdWithSpecAsync(ISpecification<T> Spec);

        #endregion
    }
}
