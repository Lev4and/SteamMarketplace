namespace SteamMarketplace.Model.Common
{
    public class DbConfig
    {
        public static string ConnectionString => "Server=localhost;Database=SteamMarketplace;User Id=postgres;" +
            "Password=sa;Integrated Security=true;Pooling=true";
    }
}
