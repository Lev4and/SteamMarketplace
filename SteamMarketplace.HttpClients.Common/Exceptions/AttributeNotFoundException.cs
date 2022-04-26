namespace SteamMarketplace.HttpClients.Common.Exceptions
{
    public class AttributeNotFoundException : Exception
    {
        public string PropertyName { get; }

        public string AttributeName { get; }

        public AttributeNotFoundException(string propertyName, string attributeName) : base($"The attribute {attributeName} not found in property {propertyName}.")
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName", "The property name should not be empty.");
            }

            if (string.IsNullOrEmpty(attributeName))
            {
                throw new ArgumentNullException("attributeName", "The attribute name should not be empty.");
            }

            PropertyName = propertyName;
            AttributeName = attributeName;
        }
    }
}
