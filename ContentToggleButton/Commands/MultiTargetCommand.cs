using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ContentToggleButton
{

	public class MultiTargetCommand : ItemsControl, ICommand, ICommandSource
	{
		#region ICommand

		public bool CanExecute(object parameter)
		{
			return Commands.QueryCanExecute(this);
		}

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public void Execute(object parameter)
		{
			Commands.Distribute(this);
		}

		#endregion

		#region ICommandSource

		//todo make a bindable command class and use that for the command property
		// maybe this just needs a DP Command property...

		public ICommand Command { get; set; }

		public object CommandParameter { get; set; }

		public IInputElement CommandTarget
		{
			get { throw new NotImplementedException(); }
		}

		#endregion
	}
}