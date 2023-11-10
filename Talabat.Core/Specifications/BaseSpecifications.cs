using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Specifications
{
    public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get ; set ; }
        public List<Expression<Func<T, object>>> Includes { get ; set  ; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get ; set ; }
        public Expression<Func<T, object>> OrderByDescending { get ; set ; }

        //Get All
        public BaseSpecifications()
        {
            //Includes = new List<Expression<Func<T, object>>> ();
            
        }

        //Get By Id
        public BaseSpecifications(Expression<Func<T , bool>> criteriaExpression)
        {
            Criteria = criteriaExpression;
            //Includes = new List<Expression<Func<T, object>>>();
        }

        public void AddOrderBy(Expression<Func<T , object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        public void AddOrderByDescending(Expression<Func<T , object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }
    }
}
