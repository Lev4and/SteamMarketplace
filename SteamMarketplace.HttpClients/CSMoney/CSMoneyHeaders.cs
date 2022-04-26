namespace SteamMarketplace.HttpClients.CSMoney
{
    public class CSMoneyHeaders
    {
        public static Dictionary<string, string> JsonHeaders => new Dictionary<string, string>()
        {
            { "Host", "inventories.cs.money" },
            { "Connection", "keep-alive" },
            { "sec-ch-ua", "\"Chromium\";v=\"94\", \"Yandex\";v=\"21\", \"; \"Not A Brand\";v=\"99\"" },
            { "Accept", "application/json, text/plain, */*" },
            { "sec-ch-ua-mobile", "?0" },
            { "X-Client-App", "web" },
            { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) " +
                "Chrome/94.0.4606.85 YaBrowser/21.11.4.730 Yowser/2.5 Safari/537.36" },
            { "sec-ch-ua-platform", "\"Windows\"" },
            { "Sec-Fetch-Site", "same-origin" },
            { "Sec-Fetch-Mode", "cors" },
            { "Sec-Fetch-Dest", "empty" },
            { "Accept-Encoding", "gzip, deflate, br" },
            { "Accept-Language", "ru,en;q=0.9" },
        };
    }
}
