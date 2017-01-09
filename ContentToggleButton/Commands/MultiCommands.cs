using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ContentToggleButton
{
	public static class MultiCommands
	{
		#region RoutedCommands

		#region RoutedUICommand OutputToggle

		private static readonly RoutedUICommand outputToggle =
			new RoutedUICommand("Toggle Target", "OutputToggle", typeof(MultiCommands));

		public static RoutedUICommand OutputToggle
		{
			get { return outputToggle; }
		}

		#endregion

		#region RoutedUICommand OutputToggleEnabled

		private static readonly RoutedUICommand outputToggleEnabled =
			new RoutedUICommand("Toggle Target Enabled", "OutputToggleEnabled",
				typeof(MultiCommands));

		public static RoutedUICommand OutputToggleEnabled
		{
			get { return outputToggleEnabled; }
		}

		#endregion

		#endregion

		#region Agregate CanExecute

		public static bool QueryCanExecute(object multiTargetCommand)
		{
			var source = multiTargetCommand as ICommandSource;

			var canExecute = false;

			foreach (var child in ((ItemsControl)multiTargetCommand).Items)
			{
				canExecute |= CanExecuteCommand(source.Command, source.CommandParameter,
					((CommandTarget) child).Target);
			}

			return canExecute;
		}

		public static bool CanExecuteCommand(ICommand command, object parameter,
			IInputElement target)
		{
			var routedCommand = command as RoutedCommand;
			return routedCommand != null
				? routedCommand.CanExecute(parameter, target)
				: command.CanExecute(parameter);
		}

		#endregion

		#region Agregate Execute

		public static void Distribute(object multiTargetCommand)
		{
			var source = multiTargetCommand as ICommandSource;

			foreach (var child in ((ItemsControl)multiTargetCommand).Items)
			{
				ExecuteCommand(source.Command, source.CommandParameter,
					((CommandTarget) child).Target);
			}
		}

		/// <summary>
		/// Based on https://referencesource.microsoft.com/#PresentationFramework/src/Framework/MS/Internal/Commands/CommandHelpers.cs
		/// </summary>
		/// <param name="command"></param>
		/// <param name="parameter"></param>
		/// <param name="target"></param>
		public static void ExecuteCommand(ICommand command, object parameter,
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

		#endregion
	}
}