using EntityFrameworkCore.LiteDB.Queries.Bson;
using Remotion.Linq.Parsing;
using System;
using EntityFrameworkCore.LiteDB.Storage;
using EntityFrameworkCore.LiteDB.Queries.Expressions;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;

namespace EntityFrameworkCore.LiteDB.Bson
{
    public class BsonQueryGenerator : ThrowingExpressionVisitor, IBsonQueryGenerator, IBsonExpressionVisitor
    {
        private FindExpression _findExpression;
        private readonly IBsonCommandBuilderFactory _bsonCommandBuilderFactory;
        private IBsonCommandBuilder _bsonCommandBuilder;

        public BsonQueryGenerator(IBsonCommandBuilderFactory bsonCommandBuilderFactory,
            FindExpression findExpression)
        {
            _bsonCommandBuilderFactory = bsonCommandBuilderFactory;
            _findExpression = findExpression;
        }

        public IValueBufferFactory CreateValueBufferFactory(IValueBufferFactoryFactory valueBufferFactoryFactory)
        {
            return valueBufferFactoryFactory.
                Create(_findExpression.GetProjectionTypes().ToArray(), indexMap: null);
        }

        public ILiteDBFindCommand GenerateBsonCommand()
        {
            _bsonCommandBuilder = _bsonCommandBuilderFactory.Create();
            Visit(_findExpression);
            return _bsonCommandBuilder.Build();
        }

        public Expression VisitCollection(CollectionExpression collectionExpression)
        {
            _bsonCommandBuilder.AddCollection(collectionExpression.Name, collectionExpression.EntityType);

            return collectionExpression;
        }

        public Expression VisitField(FieldExpression fieldExpression)
        {
            _bsonCommandBuilder.AddField(fieldExpression.Name);
            return fieldExpression;
        }

        public Expression VisitFind(FindExpression findExpression)
        {
            VisitCollection(findExpression.Collection);

            if (findExpression.Projection.Any())
                VisitProjection(findExpression.Projection);

            return findExpression;
        }

        private void VisitProjection(IReadOnlyList<FieldExpression> fieldExpressions)
            => fieldExpressions.ToList().ForEach(fieldExpr => VisitField(fieldExpr));

        protected override Exception CreateUnhandledItemException<T>(T unhandledItem, string visitMethod)
            => new NotImplementedException(visitMethod);
    }
}
