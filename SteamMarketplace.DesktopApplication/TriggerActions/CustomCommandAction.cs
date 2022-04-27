using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;


namespace SteamMarketplace.DesktopApplication.TriggerActions
{
    public class CustomCommandAction : TriggerAction<DependencyObject>
    {
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(CustomCommandAction), null);

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command", typeof(ICommand), typeof(CustomCommandAction), null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        protected override void Invoke(object parameter)
        {
            if (AssociatedObject != null)
            {
                var command = Command;

                if (command != null)
                {
                    if (CommandParameter != null)
                    {
                        if (command.CanExecute(CommandParameter))
                        {
                            command.Execute(CommandParameter);
                        }
                    }
                    else
                    {
                        if (command.CanExecute(parameter))
                        {
                            command.Execute(parameter);
                        }
                    }
                }
            }
        }
    }
}
