namespace EntityFrameworkCore.LiteDB.Metadata.Internal
{
    public class LiteDBFullAnnotationsNames
    {
        public LiteDBFullAnnotationsNames(string prefix)
        {
            FieldName = prefix + LiteDBAnnotationsNames.FieldName;
            FieldType = prefix + LiteDBAnnotationsNames.FieldType;
            DefaultValue = prefix + LiteDBAnnotationsNames.DefaultValue;
            DatabaseName = prefix + LiteDBAnnotationsNames.DatabaseName;
            CollectionName = prefix + LiteDBAnnotationsNames.CollectionName;
            Name = prefix + LiteDBAnnotationsNames.Name;
        }

        public static LiteDBFullAnnotationsNames Instance { get; } = new LiteDBFullAnnotationsNames(LiteDBAnnotationsNames.Prefix);

        public readonly string FieldName;

        public readonly string FieldType;

        public readonly string DefaultValue;

        public readonly string DatabaseName;

        public readonly string CollectionName;

        public readonly string Name;
    }
}
