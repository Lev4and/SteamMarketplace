using System.Windows;
using System.Windows.Markup;

namespace SteamMarketplace.DesktopApplication.Behaviors
{
    public class LanguageBehavior
    {
        public static DependencyProperty LanguageProperty = DependencyProperty.RegisterAttached("Language", typeof(string),
            typeof(LanguageBehavior), new UIPropertyMetadata(LanguageBehavior.OnLanguageChanged));

        private static void OnLanguageChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = target as FrameworkElement;
            element.Language = XmlLanguage.GetLanguage(e.NewValue?.ToString() != "???" ? e.NewValue?.ToString() ?? "" : "");
        }

        public static void SetLanguage(FrameworkElement target, string value)
        {
            target.SetValue(LanguageBehavior.LanguageProperty, value);
        }

        public static string GetLanguage(FrameworkElement target)
        {
            return (string)target.GetValue(LanguageBehavior.LanguageProperty);
        }
    }
}
