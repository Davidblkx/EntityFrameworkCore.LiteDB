using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkCore.LiteDB.Queries.Internal
{
    public class QueryingEnumerable : IEnumerable<ValueBuffer>
    {
        private readonly LiteDBQueryContext _queryContext;
        private readonly ShaperCommandContext _shaperCommandContext;
        private readonly IValueBufferFromBsonShaper _valueBufferShaper;

        public QueryingEnumerable(
            LiteDBQueryContext queryContext,
            ShaperCommandContext shaperCommandContext,
            IValueBufferFromBsonShaper valueBufferShaper)
        {
            _queryContext = queryContext;
            _shaperCommandContext = shaperCommandContext;
            _valueBufferShaper = valueBufferShaper;
        }

        public virtual IEnumerator<ValueBuffer> GetEnumerator() => new Enumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private sealed class Enumerator : IEnumerator<ValueBuffer>
        {
            private readonly QueryingEnumerable _queryingEnumerable;

            private Queue<ValueBuffer> _buffer;
            private bool _disposed;
            private ValueBuffer _current;
            private bool _queryExecuted = false;

            public Enumerator(QueryingEnumerable queryingEnumerable)
            {
                _queryingEnumerable = queryingEnumerable;
            }

            public bool MoveNext()
            {
                if (_buffer == null)
                {
                    if (!_queryExecuted)
                    {
                        _queryingEnumerable._queryContext.Connection.Open();

                        _buffer = new Queue<ValueBuffer>(ExecuteQuery());

                        _queryExecuted = true;
                    }

                    var hasNext = _buffer.Any();
                    _current = hasNext ? _buffer.Dequeue() : default(ValueBuffer);

                    return hasNext;
                }

                if (_buffer.Count > 0)
                {
                    _current = _buffer.Dequeue();

                    return true;
                }

                return false;
            }

            private IEnumerable<ValueBuffer> ExecuteQuery()
            {
                var command = _queryingEnumerable._shaperCommandContext
                                .GetCommand();

                var results = command.ExecuteFind(
                                    _queryingEnumerable._queryContext.Connection);

                _queryingEnumerable._shaperCommandContext.NotifyReaderCreated();
                var valueBufferFactory = _queryingEnumerable._shaperCommandContext
                                                            .ValueBufferFactory;
                return results.Select(
                                    record => valueBufferFactory.Create(
                                             record,
                                             _queryingEnumerable._valueBufferShaper));

            }

            public ValueBuffer Current => _current;
            object IEnumerator.Current => Current;

            public void Dispose()
            {
                if (!_disposed)
                {
                    _queryingEnumerable._queryContext.Connection?.Close();

                    _disposed = true;
                }
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }
    }
}
