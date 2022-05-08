namespace SteamMarketplace.Model.Common
{
    public class DbConfig
    {
        public static string ConnectionString => "Data Source=DESKTOP-9CDGA5B;Initial Catalog=SteamMarketplace;User ID=sa;" +
            "Password=sa;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;" +
                "MultiSubnetFailover=False";

        //public static string ConnectionString => "Data Source=steam.marketplace.resource.api.u1321851.plsk.regruhosting.ru;Initial Catalog=u1321851_SteamMarketplace;User ID=u1321851_Admin;" +
        //    "Password=Andrey06032001!1973268450*;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;" +
        //        "MultiSubnetFailover=False";
    }
}
