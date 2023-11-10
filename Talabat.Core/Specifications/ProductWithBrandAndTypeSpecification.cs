using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Specifications
{
    public class ProductWithBrandAndTypeSpecification : BaseSpecifications<Product>
    {
        //for get all products
        public ProductWithBrandAndTypeSpecification(string sort):base()
        {
            Includes.Add(p => p.ProductType);
            Includes.Add(p => p.ProductBrand);
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "PriceAcs":
                        AddOrderBy(p => p.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
        }

        //for get product by id
        public ProductWithBrandAndTypeSpecification(int id):base(p=>p.Id==id)
        {
            Includes.Add(p => p.ProductType);
            Includes.Add(p => p.ProductBrand);
        }
    }
}
