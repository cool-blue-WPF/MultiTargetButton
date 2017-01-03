using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ContentToggleButton
{
	/// <summary>
	/// This is a standard ICommand object (i.e. not a routed command)so, no 
	/// target. The host simply invokes Execute on it's Click event and the 
	/// Command is distributed to the targets stored in the base.Items 
	/// collection.
	/// 
	/// </summary>
	///
	/// Command is normally a routed command 
	/// base.Items is the collection of targets that can be composed in xaml
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