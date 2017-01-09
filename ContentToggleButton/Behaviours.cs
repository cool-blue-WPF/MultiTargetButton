using System.Windows;
using System.Windows.Input;

namespace ContentToggleButton
{
	public static class Behaviours
	{
		public static readonly DependencyProperty CommandReceiversProperty = 
			DependencyProperty.RegisterAttached(
			"CommandReceivers", typeof(CommandBindingCollection), 
			typeof(Behaviours),
			new PropertyMetadata(default(CommandBindingCollection), CommandReceiversChanged));

		private static void CommandReceiversChanged(DependencyObject target, 
			DependencyPropertyChangedEventArgs args)
		{
			var host = target as UIElement;
			if (args.OldValue != null)
			{
				foreach (CommandBinding commandReceiver in (CommandBindingCollection)args.OldValue)
				{
					host.CommandBindings.Remove(commandReceiver);
				}
				
			}
			if (args.NewValue != null)
			{
				foreach (CommandBinding commandReceiver in (CommandBindingCollection)args.NewValue)
				{
					host.CommandBindings.Add(commandReceiver);
				}
			}
		}

		public static void SetCommandReceivers(DependencyObject element, CommandBindingCollection value)
		{
			element.SetValue(CommandReceiversProperty, value);
		}

		public static CommandBindingCollection GetCommandReceivers (DependencyObject element)
		{
			return (CommandBindingCollection)element.GetValue(CommandReceiversProperty);
		}
	}
}
