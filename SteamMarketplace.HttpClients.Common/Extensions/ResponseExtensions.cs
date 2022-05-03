using Newtonsoft.Json;
using System.Text;

namespace SteamMarketplace.HttpClients.Common.Extensions
{
    public static class ResponseExtensions
    {
        public async static Task<string> GetStringResultAsync(this HttpResponseMessage response)
        {
            if (response == null)
            {
                throw new ArgumentNullException("response", "The response should not be empty.");
            }

            var stream = await response.Content.ReadAsStreamAsync();

            if (stream != null)
            {
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return await reader.ReadToEndAsync();
                }
            }

            return "";
        }

        public async static Task<string> GetHtmlResultAsync(this HttpResponseMessage response)
        {
            if (response == null)
            {
                throw new ArgumentNullException("response", "The response should not be empty.");
            }

            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                if (stream != null)
                {
                    using (var reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        return await reader.ReadToEndAsync();
                    }
                }
            }

            return "";
        }

        public static T GetJsonResult<T>(this string responseContent)
        {
            if (string.IsNullOrEmpty(responseContent))
            {
                throw new ArgumentNullException("responseContent", "The response content not be empty.");
            }

            try
            {
                return JsonConvert.DeserializeObject<T>(responseContent, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            }
            catch
            {
                return Activator.CreateInstance<T>();
            }
        }

        public async static Task<T> GetJsonResultAsync<T>(this HttpResponseMessage response)
        {
            if (response == null)
            {
                throw new ArgumentNullException("response", "The response should not be empty.");
            }

            var stream = await response.Content.ReadAsStreamAsync();

            if (stream != null)
            {
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return JsonConvert.DeserializeObject<T>(await reader.ReadToEndAsync(), new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                }
            }

            return Activator.CreateInstance<T>();
        }
    }
}
