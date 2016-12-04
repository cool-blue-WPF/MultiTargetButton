using System;
using System.Windows.Input;

namespace ContentToggleButton
{
	public class OutputToggle : ICommand
	{

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			parameter = !(bool)parameter;
		}

		public event EventHandler CanExecuteChanged;
	}

	public static class Commands
	{
		static readonly RoutedUICommand _outputToggleCommand =
			new RoutedUICommand("Toggle Target", "OutputToggle", typeof(Commands));

		public static RoutedUICommand OutputToggleCommand
		{
			get { return _outputToggleCommand; }
		}
	}
}
