using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Store.Data.Entity;

namespace Store.Repository.Specification
{
    public class SpecificationEvaluator<TEntity,Tkey> where TEntity: BaseEntity<Tkey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> InputQuery, ISpecification<TEntity> specs)
        {
            var query = InputQuery;
            if(specs.Criteria is not null)
            {
                query= query.Where(specs.Criteria); // x=>x.id == 3

            }
            if(specs.OrderBy is not null)
            {
                query= query.OrderBy(specs.OrderBy); // x=>x.Name 
            }
            if(specs.OrderByDescending is not null)
            {
                query= query.OrderBy(specs.OrderByDescending); // x=>x.Name 
            }
            if (specs.IsPaginated )
            {
                query = query.Skip(specs.Skip).Take(specs.Take);
            } 
            query = specs.Includes.Aggregate(query, (Current, includeEx) => Current.Include(includeEx));
            return query;
        }
    }
}
