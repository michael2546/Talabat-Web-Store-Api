using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    public static class SpecificationEvaluator<T> where T : BaseEntity
    {
        //function to build dynamic query like this in generic repository
        //_dbContext.Set<T>().Where(p=>p.Id == id).Include(p => p.ProductBrand).Include(p => p.ProductType);

        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery , ISpecifications<T> specifications)
        {
            var query = inputQuery; //_dbContext.Products

            if(specifications.Criteria is not null) //p=>p.Id == id
                query = query.Where(specifications.Criteria); //_dbContext.Set<T>().Where(p=>p.Id == id)
        
            if(specifications.OrderBy is not null)
            {
                query = query.OrderBy(specifications.OrderBy);
            }
            if (specifications.OrderByDescending is not null)
            {
                query = query.OrderByDescending(specifications.OrderByDescending);
            }
            //p => p.ProductBrand , p => p.ProductType
            query = specifications.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            //_dbContext.Set<T>().Where(p=>p.Id == id).Include(p => p.ProductBrand)
            //_dbContext.Set<T>().Where(p=>p.Id == id).Include(p => p.ProductBrand).Include(p => p.ProductType);
            return query;
        }

    }
}
