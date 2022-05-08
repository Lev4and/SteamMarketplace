using System.Net;

namespace SteamMarketplace.Model.Common
{
    public static class Statuses
    {
        public static Status Success => new Status(HttpStatusCode.OK, StatusName.Success, "Успех");

        public static Status NotFound => new Status(HttpStatusCode.NotFound, StatusName.Error, "Запрашиваемый ресурс не был найден.");

        public static Status Forbidden => new Status(HttpStatusCode.Forbidden, StatusName.Error, "Доступ к ресурсу запрещён.");

        public static Status InvalidData => new Status(HttpStatusCode.BadRequest, StatusName.Error, "Неверные данные.");

        public static Status Unauthorized => new Status(HttpStatusCode.Unauthorized, StatusName.Error, "Вы неавторизованы.");

        public static Status FatalUnauthorized => new Status(HttpStatusCode.Unauthorized, StatusName.Error, "Неверный логин или пароль.");
    }
}
