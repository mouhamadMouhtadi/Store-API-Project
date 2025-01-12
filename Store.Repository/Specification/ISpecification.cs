﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specification
{
    public interface ISpecification<T>
    {
        Expression<Func<T,bool>> Criteria { get; } // For Where COnditions 
        List<Expression<Func<T,object>>> Includes { get; }// For Includes
                                                          //order
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
         
        // Pagination
        int Take { get; }
        int Skip { get; }
        bool IsPaginated { get; }
    }
}
