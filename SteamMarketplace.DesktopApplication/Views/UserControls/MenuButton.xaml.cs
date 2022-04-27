using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;

namespace SteamMarketplace.DesktopApplication.Views.UserControls
{
    public partial class MenuButton : Button
    {
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(MenuButton), new PropertyMetadata("Text"));

        public PackIconKind Kind
        {
            get { return (PackIconKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public static readonly DependencyProperty KindProperty =
            DependencyProperty.Register("Kind", typeof(PackIconKind), typeof(MenuButton), new PropertyMetadata(PackIconKind.Account));

        public MenuButton()
        {
            InitializeComponent();
        }
    }
}
