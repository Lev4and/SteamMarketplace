using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SteamMarketplace.DesktopApplication.Views.UserControls
{
    public partial class XListBox : ListBox
    {
        public List<object> XSelectedItems
        {
            get { return (List<object>)GetValue(XSelectedItemsProperty); }
            set { SetValue(XSelectedItemsProperty, value); }
        }

        public static readonly DependencyProperty XSelectedItemsProperty =
            DependencyProperty.Register("XSelectedItems", typeof(List<object>), typeof(XListBox), new PropertyMetadata(null));


        public XListBox() : base()
        {
            SelectionChanged += delegate { XSelectedItems = GetConvertedSelectedItems(); };
        }

        private List<object> GetConvertedSelectedItems()
        {
            if (SelectedItems != null)
            {
                if (!string.IsNullOrEmpty(SelectedValuePath))
                {
                    return SelectedItems.Cast<object>().Select(selectedItem =>
                        selectedItem.GetType().GetProperty(SelectedValuePath).GetValue(selectedItem)).ToList();
                }

                return SelectedItems.Cast<object>().ToList();
            }

            return new List<object>();
        }
    }
}
