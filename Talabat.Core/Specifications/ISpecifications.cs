using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Specifications
{
    public interface ISpecifications<T> where T : BaseEntity
    {
        //_dbContext.Products.Where(P => P.Id == id).Include(P => P.ProductType).Include(P => P.ProductBrand);

        // Signature for property for where condition [where(P ==>P.Id == id)]
        public Expression<Func<T , bool>> Criteria { get; set; }

        //Signature for property for List of Include [.Include(P => P.ProductType).Include(P => P.ProductBrand)]
        public List<Expression<Func<T , object>>> Includes { get; set; }

        //prop for order by  [OrderBy(p=>p.name)] 
        public Expression<Func<T, object>> OrderBy { get; set; }

        //prop for orderByDesc [OrderByDesc(p=>pname)]
        public Expression<Func<T , object>> OrderByDescending { get; set; }
    }
}
