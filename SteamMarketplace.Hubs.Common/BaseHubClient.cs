using Microsoft.AspNetCore.SignalR.Client;
using System.Net;

namespace SteamMarketplace.Hubs.Common
{
    public class BaseHubClient
    {
        protected internal bool IsBusy { get; set; }

        protected internal bool IsConnected { get; set; }

        protected internal HubConnection Connection { get; set; }

        public BaseHubClient(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url", "The server url must not be empty.");
            }

            Connection = new HubConnectionBuilder()
                .WithUrl(url, (options) =>
                {
                    options.HttpMessageHandlerFactory = (message) =>
                    {
                        if (message is HttpClientHandler clientHandler)
                        {
                            clientHandler.ServerCertificateCustomValidationCallback +=
                                (sender, certificate, chain, sslPolicyErrors) => { return true; };
                        }

                        return message;
                    };
                })
                .WithAutomaticReconnect()
                .Build();
        }

        public async Task Connect()
        {
            if (!IsConnected)
            {
                try
                {
                    await Connection.StartAsync();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    IsConnected = true;
                }
            }
        }

        public async IAsyncEnumerable<T> StreamAsync<T>(string streamName, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(streamName))
            {
                throw new ArgumentNullException(nameof(streamName));
            }

            if (IsConnected)
            {
                await foreach (var item in Connection.StreamAsync<T>(streamName, cancellationToken))
                {
                    yield return item;
                }
            }
        }

        public async Task Disconnect()
        {
            if (IsConnected)
            {
                try
                {
                    await Connection.StopAsync();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    IsConnected = false;
                }
            }
        }
    }
}
