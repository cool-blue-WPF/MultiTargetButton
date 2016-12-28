using System.Windows;
using System.Windows.Input;

namespace ContentToggleButton.Commands
{
	public static class CommandDistributor
	{
		public static void Distribute (MultiTargetCommand multiTargetCommand)
		{
			var source = multiTargetCommand as ICommandSource;

			foreach (var child in multiTargetCommand.Items)
			{
				ExecuteCommand(source.Command, source.CommandParameter,
					((CommandTarget)child).Target);
			}
		}

		public static void ExecuteCommand (ICommand command, object parameter,
			IInputElement target)
		{
			var routedCommand = command as RoutedCommand;
			if (routedCommand != null)
			{
				if (!routedCommand.CanExecute(parameter, target))
					return;
				routedCommand.Execute(parameter, target);
			}
			else
			{
				if (!command.CanExecute(parameter))
					return;
				command.Execute(parameter);
			}
		}
	}
}