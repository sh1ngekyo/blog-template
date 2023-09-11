using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogTemplate.Tests.Common.Extensions.Queriable
{
    public static class ICollectionExtensions
    {
        public static IQueryable<T> AsAsyncQueryable<T>(this ICollection<T> source) =>
            new AsyncQueryable<T>(source.AsQueryable());
    }
}
