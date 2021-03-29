using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
         Expression<Func<T, bool>> Criteria {get;}    // this will be interpreted as our where criteria
         List<Expression<Func<T, object>>> Includes {get;}

         Expression<Func<T, object>> OrderBy{get;}
         Expression<Func<T, object>> OrderByDescending{get;}

        int Take { get; }                                       // pagination variable
        int Skip { get; }                                       // pagination
        bool IsPagingEnabled { get; }                       // pagination
    }
}