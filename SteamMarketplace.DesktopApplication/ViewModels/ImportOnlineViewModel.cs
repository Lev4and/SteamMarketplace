using DevExpress.Mvvm;
using SteamMarketplace.Hubs;
using SteamMarketplace.Hubs.HubEventArgs;
using SteamMarketplace.Hubs.ResourceAPI.ResponseModels;
using SteamMarketplace.Model.Marketplace.CSMoney.Types;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SteamMarketplace.DesktopApplication.ViewModels
{
    public class ImportOnlineViewModel : BindableBase, IDisposable
    {
        private readonly HubContext _hubContext;

        public int Online { get; set; }

        public ObservableCollection<ImportedItemInfo> ImportedItems { get; set; }

        public ICommand Loaded => new AsyncCommand(() =>
        {
            return LoadedAsync();
        });

        public ImportOnlineViewModel(HubContext hubContext)
        {
            _hubContext = hubContext;

            Online = 0;
            ImportedItems = new ObservableCollection<ImportedItemInfo>();
        }

        private async Task LoadedAsync()
        {
            _hubContext.ResourceAPI.Import.OnlineChanged += Import_OnlineChanged;
            _hubContext.ResourceAPI.Import.ItemImported += Import_ItemImported;

            await _hubContext.ResourceAPI.Import.Connect();
        }

        private void Import_OnlineChanged(OnlineHubEventArgs onlineInfo)
        {
            Online = onlineInfo.Online;
        }

        private void Import_ItemImported(ImportHubEventArgs importInfo)
        {
            for (var i = 0; i < (importInfo.ImportedItemInfo.Item.StackSize ?? 1); i++)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ImportedItems.Insert(0, importInfo.ImportedItemInfo);
                });
            }
        }

        public void Dispose()
        {
            _hubContext.ResourceAPI.Import.ItemImported -= Import_ItemImported;
        }
    }
}
