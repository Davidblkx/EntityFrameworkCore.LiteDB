using EntityFrameworkCore.LiteDB.Queries.Expressions;
using Microsoft.EntityFrameworkCore.Storage;
using LiteDB;

namespace EntityFrameworkCore.LiteDB.Queries.Internal
{
    public class ValueBufferFromBsonShaper : IValueBufferFromBsonShaper
    {
        private readonly FindExpression _findExpression;

        public ValueBufferFromBsonShaper(FindExpression findExpression)
        {
            _findExpression = findExpression;
        }

        public ValueBuffer Shape(ValueBuffer buffer)
        {
            return ConvertBsonValuesToClrTypes(ReorderFields(buffer));
        }

        private ValueBuffer ReorderFields(ValueBuffer buffer)
        {
            return buffer;
        }

        private ValueBuffer ConvertBsonValuesToClrTypes(ValueBuffer buffer)
        {
            for (int i = 0; i < buffer.Count; i++)
            {
                buffer[i] = ConvertToBaseClrType(buffer[i]);
            }
            return buffer;
        }

        private object ConvertToBaseClrType(object value)
        {
            if (value is ObjectId)
                return ((ObjectId)value).ToString();
            if (value is BsonValue)
                return ((BsonValue)value).RawValue;
            return value;
        }
    }
}
