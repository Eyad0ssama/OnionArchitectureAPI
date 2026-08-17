using Onion.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Core.Specification
{
    public class ProductWithFiltrationForCountAsync : BaseSpecifications<Product>
    {
        public ProductWithFiltrationForCountAsync(ProductSpecParams Params) : base(p =>
        (!Params.BrandId.HasValue || p.ProductBrandId == Params.BrandId)

           &&

        (!Params.TypeId.HasValue || p.ProductTypeId == Params.TypeId)
        )
        {
            
        }
    }
}
