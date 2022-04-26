namespace SteamMarketplace.HttpClients.Common.Attributes
{
    public class DisplayValueAttribute : Attribute
    {
        private string _value;

        public string Value
        {
            get { return _value; }
        }

        public DisplayValueAttribute(string value)
        {
            _value = value;
        }
    }
}
