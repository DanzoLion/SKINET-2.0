using System.Linq;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity    // T or TEntity .. only from BaseEntity classes
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)  // static methods are created where we don't need to generate a new instance of the class SpecificationEvaluator   
    {
        var query = inputQuery;

        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria); // these are lambda expressions here .. eg:  p => p.ProductTypeId == id 
        }

        query = spec.Includes.Aggregate(query, (current, include) => current.Include(include)); // aggregates .Include() and passes to as list in query

        return query;
    }
    }
}