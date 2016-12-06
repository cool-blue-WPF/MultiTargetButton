using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ContentToggleButton.Commands
{
	public class OutputToggle
	{
		static readonly RoutedUICommand _ex =
			new RoutedUICommand("Toggle Target", "Ex", typeof(OutputToggle));

		public static RoutedUICommand Ex
		{
			get { return _ex; }
		}

		public ToggleButton ToggleButton { get; set; }
		public CommandBinding OutputBinding { get; set; }

		public OutputToggle (ToggleButton toggleButton)
		{
			ToggleButton = toggleButton;
			
			OutputBinding = new CommandBinding(Ex,
				delegate(object sender, ExecutedRoutedEventArgs args)
				{
					var flag = ToggleButton.IsChecked ?? false;
					ToggleButton.IsChecked = !flag;
					args.Handled = true;
				},
				delegate(object sender, CanExecuteRoutedEventArgs args)
				{
					args.CanExecute = ToggleButton.IsEnabled;
				}
			);
		}
	}

	public class OutputToggleEnabled
	{
		static readonly RoutedUICommand _outputToggleEnabledCommand =
			new RoutedUICommand("Toggle Target Enabled", "OutputToggleEnabled", 
				typeof(OutputToggleEnabled));

		public static RoutedUICommand OutputToggleEnabledCommand
		{
			get { return _outputToggleEnabledCommand; }
		}

		public ToggleButton ToggleButton { get; set; }
		public CommandBinding OutputBinding { get; set; }

		public OutputToggleEnabled (ToggleButton toggleButton)
		{
			ToggleButton = toggleButton;

			OutputBinding = new CommandBinding(OutputToggleEnabledCommand,
				delegate(object sender, ExecutedRoutedEventArgs args)
				{
					var flag = toggleButton.IsEnabled;
					toggleButton.IsChecked = !flag;
					args.Handled = true;
				},
				delegate(object sender, CanExecuteRoutedEventArgs args)
				{
					args.CanExecute = true;
				}
			);
		}
	}
}
