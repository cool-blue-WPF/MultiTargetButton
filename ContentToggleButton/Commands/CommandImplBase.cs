using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace ContentToggleButton
{
	public abstract class CommandImplBase : CommandBinding, ICommand, ISupportInitialize
	{
		public IInputElement Target { get; set; }
		
		public void BeginInit()
		{

		}

		public void EndInit()
		{
			base.Executed += (sender, args) =>
			{
				((ICommand)this).Execute(args.Parameter);
				args.Handled = true;
			};
			base.CanExecute += (sender, args) =>
			{
				args.CanExecute =
					((ICommand)this).CanExecute(args.Parameter);
			};
		}

		protected CommandImplBase()
		{
			
		}

		protected CommandImplBase (IInputElement target, RoutedUICommand cmd)
			: base(cmd)
		{
			Target = target;
			EndInit();
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