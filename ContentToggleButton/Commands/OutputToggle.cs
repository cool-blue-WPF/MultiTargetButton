using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ContentToggleButton.Commands
{
	public class OutputToggle
	{
		public OutputToggle (ToggleButton toggleButton)
		{
			ToggleButton = toggleButton;
			
			OutputBinding = new CommandBinding(Ex,
				(sender, args) =>
				{
					var flag = ToggleButton.IsChecked ?? false;
					ToggleButton.IsChecked = !flag;
					args.Handled = true;
				},
				( sender, args) =>
				{
					args.CanExecute = true; // ToggleButton.IsEnabled;
				}
			);
		}

		public ToggleButton ToggleButton { get; set; }
		public CommandBinding OutputBinding { get; set; }

		static readonly RoutedUICommand _ex =
			new RoutedUICommand("Toggle Target", "Ex", typeof(OutputToggle));

		public static RoutedUICommand Ex
		{
			get { return _ex; }
		}
	}

	public class OutputToggleEnabled
	{
		static readonly RoutedUICommand _ex =
			new RoutedUICommand("Toggle Target Enabled", "Ex", 
				typeof(OutputToggleEnabled));

		public static RoutedUICommand Ex
		{
			get { return _ex; }
		}

		public ToggleButton ToggleButton { get; set; }
		public CommandBinding OutputBinding { get; set; }

		public OutputToggleEnabled (ToggleButton toggleButton)
		{
			ToggleButton = toggleButton;

			OutputBinding = new CommandBinding(Ex,
				(sender, args) =>
				{
					var flag = toggleButton.IsEnabled;
					toggleButton.IsChecked = !flag;
					args.Handled = true;
				},
				(sender, args) =>
				{
					args.CanExecute = true;
				}
			);
		}
	}
}
