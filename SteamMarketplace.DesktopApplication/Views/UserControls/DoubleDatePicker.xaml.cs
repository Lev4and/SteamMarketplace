using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace SteamMarketplace.DesktopApplication.Views.UserControls
{
    public partial class DoubleDatePicker : UserControl, INotifyPropertyChanged
    {
        public DateTime? MinSelectedDate
        {
            get { return (DateTime?)GetValue(MinSelectedDateProperty); }
            set { SetValue(MinSelectedDateProperty, value); BeginSelectedDate = value; OnPropertyChanged("MinSelectedDate"); }
        }

        public static readonly DependencyProperty MinSelectedDateProperty =
            DependencyProperty.Register("MinSelectedDate", typeof(DateTime?), typeof(DoubleDatePicker), new PropertyMetadata((DateTime?)DateTime.MinValue));


        public DateTime? BeginSelectedDate
        {
            get { return (DateTime?)GetValue(BeginSelectedDateProperty); }
            set { SetValue(BeginSelectedDateProperty, value); OnPropertyChanged("BeginSelectedDate"); }
        }

        public static readonly DependencyProperty BeginSelectedDateProperty =
            DependencyProperty.Register("BeginSelectedDate", typeof(DateTime?), typeof(DoubleDatePicker), new PropertyMetadata((DateTime?)DateTime.MinValue));

        public DateTime? EndSelectedDate
        {
            get { return (DateTime?)GetValue(EndSelectedDateProperty); }
            set { SetValue(EndSelectedDateProperty, value); OnPropertyChanged("EndSelectedDate"); }
        }

        public static readonly DependencyProperty EndSelectedDateProperty =
            DependencyProperty.Register("EndSelectedDate", typeof(DateTime?), typeof(DoubleDatePicker), new PropertyMetadata((DateTime?)DateTime.MaxValue));

        public DateTime? MaxSelectedDate
        {
            get { return (DateTime?)GetValue(MaxSelectedDateProperty); }
            set { SetValue(MaxSelectedDateProperty, value); EndSelectedDate = value; OnPropertyChanged("MaxSelectedDate"); }
        }

        public static readonly DependencyProperty MaxSelectedDateProperty =
            DependencyProperty.Register("MaxSelectedDate", typeof(DateTime?), typeof(DoubleDatePicker), new PropertyMetadata((DateTime?)DateTime.MaxValue));

        public event PropertyChangedEventHandler PropertyChanged;

        public DoubleDatePicker()
        {
            InitializeComponent();
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
