using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ContentToggleButton.Commands
{
	public class OutputToggle
	{
		public ToggleButton ToggleButton { get; set; }
		public CommandBinding OutputToggleBinding { get; set; }

		public OutputToggle (ToggleButton toggleButton)
		{
			ToggleButton = toggleButton;
			
			OutputToggleBinding = new CommandBinding(RoutedCommands.OutputToggleCommand,
				delegate(object sender, ExecutedRoutedEventArgs args)
				{
					var flag = toggleButton.IsChecked ?? false;
					toggleButton.IsChecked = !flag;
					args.Handled = true;
				},
				delegate(object sender, CanExecuteRoutedEventArgs args)
				{
					args.CanExecute = toggleButton.IsEnabled;
				}
			);
		}
	}

	public static class RoutedCommands
	{
		static readonly RoutedUICommand _outputToggleCommand =
			new RoutedUICommand("Toggle Target", "OutputToggle", typeof(RoutedCommands));

		public static RoutedUICommand OutputToggleCommand
		{
			get { return _outputToggleCommand; }
		}


	}
}
