using System;

namespace EntityFrameworkCore.LiteDB.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CollectionAttribute : Attribute
    {
        public CollectionAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
