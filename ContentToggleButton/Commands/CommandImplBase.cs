using System;
using System.Windows;
using System.Windows.Input;

namespace ContentToggleButton
{
	public abstract class CommandImplBase : ICommand
	{
		public IInputElement Target { get; set; }

		public CommandBinding OutputBinding(IInputElement target, RoutedUICommand cmd)
		{
			Target = target;
			return new CommandBinding(cmd,
				(sender, args) =>
				{
					((ICommand) this).Execute(args.Parameter);
					args.Handled = true;
				},
				(sender, args) =>
				{
					args.CanExecute =
						((ICommand) this).CanExecute(args.Parameter);
				}
			);
		}

		#region ICommand

		public virtual bool CanExecute(object parameter)
		{
			return true;
		}

		public abstract void Execute(object parameter);

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		#endregion
	}
}