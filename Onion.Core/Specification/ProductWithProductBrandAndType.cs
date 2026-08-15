using Onion.Core.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Core.Specification
{
    public class ProductWithProductBrandAndType:BaseSpecifications<Product>
    {
        public ProductWithProductBrandAndType(ProductSpecParams Params) : base(p=>
        (!Params.BrandId.HasValue||p.ProductBrandId== Params.BrandId)

           && 

        (!Params.TypeId.HasValue || p.ProductTypeId == Params.TypeId)


        )
        {

            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);

            if (!string.IsNullOrEmpty(Params.Sort))
            {
                switch(Params.Sort)
                {
                    case"priceAsc":
                        SetOrderBy(p => p.Price);
                        break;
                    case "PriceDesc":
                        SetOrderByDesc(p => p.Price);
                        break;
                    default:
                        SetOrderBy(p => p.Name);
                        break;
                }
            }
            ApplayPagination(Params.PageSize * (Params.PageIndex - 1), Params.PageSize);


        }
        
        public ProductWithProductBrandAndType(int id):base(p => p.Id == id)
        {

            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
            
        }
    }
}
