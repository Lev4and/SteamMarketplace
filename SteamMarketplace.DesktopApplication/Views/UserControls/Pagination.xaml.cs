using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SteamMarketplace.DesktopApplication.Views.UserControls
{
    public partial class Pagination : UserControl
    {
        public int Page
        {
            get { return (int)GetValue(PageProperty); }
            set { SetValue(PageProperty, value); }
        }

        public static readonly DependencyProperty PageProperty =
            DependencyProperty.Register("Page", typeof(int), typeof(Pagination), new PropertyMetadata(1));

        public ObservableCollection<int> Items
        {
            get { return (ObservableCollection<int>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ObservableCollection<int>), typeof(Pagination), new PropertyMetadata(new ObservableCollection<int>()));

        public Pagination()
        {
            InitializeComponent();
        }
    }
}
