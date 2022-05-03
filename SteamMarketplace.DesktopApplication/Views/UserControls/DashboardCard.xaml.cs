using System.Windows;
using System.Windows.Controls;

namespace SteamMarketplace.DesktopApplication.Views.UserControls
{
    public partial class DashboardCard : UserControl
    {
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(DashboardCard), new PropertyMetadata(""));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(DashboardCard), new PropertyMetadata(""));

        public DashboardCard()
        {
            InitializeComponent();
        }
    }
}
