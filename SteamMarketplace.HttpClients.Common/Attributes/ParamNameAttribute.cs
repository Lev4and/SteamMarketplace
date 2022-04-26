namespace SteamMarketplace.HttpClients.Common.Attributes
{
    public class ParamNameAttribute : Attribute
    {
        private string _name;

        public string Name
        {
            get { return _name; }
        }

        public ParamNameAttribute(string name)
        {
            _name = name;
        }
    }
}
