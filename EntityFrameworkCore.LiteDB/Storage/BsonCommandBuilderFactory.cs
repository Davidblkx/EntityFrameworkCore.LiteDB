namespace EntityFrameworkCore.LiteDB.Storage
{
    public class BsonCommandBuilderFactory : IBsonCommandBuilderFactory
    {
        public IBsonCommandBuilder Create()
            => new BsonCommandBuilder();
    }
}
