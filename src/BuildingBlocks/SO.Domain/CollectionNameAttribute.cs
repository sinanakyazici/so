namespace SO.Domain
{
    public class CollectionNameAttribute : Attribute
    {
        public string Value { get; }

        public CollectionNameAttribute(string value)
        {
            Value = value;
        }
    }
}