using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogTemplate.Tests.Common.Extensions.Queriable
{
    internal class AsyncQueryable<T> : IAsyncEnumerable<T>, IQueryable<T>
    {
        private IQueryable<T> Source;

        public AsyncQueryable(IQueryable<T> source)
        {
            Source = source;
        }

        public Type ElementType => typeof(T);

        public Expression Expression => Source.Expression;

        public IQueryProvider Provider => new AsyncQueryProvider<T>(Source.Provider);

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new AsyncEnumeratorWrapper<T>(Source.GetEnumerator());
        }

        public IEnumerator<T> GetEnumerator() => Source.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
