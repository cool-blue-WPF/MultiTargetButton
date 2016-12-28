using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ContentToggleButton.Commands
{
	//[ContentProperty("Targets")]
	public class MultiTargetCommand : ItemsControl, ICommand, ICommandSource
	{
		#region ICommand

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public event EventHandler CanExecuteChanged;

		public void Execute(object parameter)
		{
			CommandDistributor.Distribute(this);
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