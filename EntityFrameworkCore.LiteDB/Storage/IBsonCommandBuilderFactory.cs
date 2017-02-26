namespace EntityFrameworkCore.LiteDB.Storage
{
    public interface IBsonCommandBuilderFactory
    {
        IBsonCommandBuilder Create();
    }
}
