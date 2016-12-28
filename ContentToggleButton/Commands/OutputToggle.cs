using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ContentToggleButton.Commands
{
	public class OutputToggle : ICommand
	{
		public OutputToggle ()
		{
			OutputBinding = new CommandBinding(Ex,
				(sender, args) =>
				{
					((ICommand)this).Execute(args.Parameter);
					args.Handled = true;
				},
				(sender, args) =>
				{
					args.CanExecute =
						((ICommand)this).CanExecute(args.Parameter);
				}
			);
		}
		public OutputToggle (IInputElement target) : this()
		{
			Target = target;
		}

		public IInputElement Target { get; set; }
		public CommandBinding OutputBinding { get; set; }

		static readonly RoutedUICommand _ex =
			new RoutedUICommand("Toggle Target", "Ex", typeof(OutputToggle));

		public static RoutedUICommand Ex
		{
			get { return _ex; }
		}

		#region ICommand

		bool ICommand.CanExecute (object parameter)
		{
			return true;
		}

		void ICommand.Execute (object parameter)
		{
			var target = Target as ToggleButton;
			if (target == null) return;
			var flag = target.IsChecked ?? false;
			target.IsChecked = !flag;
		}

		public event EventHandler CanExecuteChanged;
		
		#endregion
	}

	public class OutputToggleEnabled : ICommand
	{
		static readonly RoutedUICommand _ex =
			new RoutedUICommand("Toggle Target Enabled", "Ex", 
				typeof(OutputToggleEnabled));

		public static RoutedUICommand Ex
		{
			get { return _ex; }
		}

		public UIElement Element { get; set; }
		public CommandBinding OutputBinding { get; set; }

		public OutputToggleEnabled ()
		{
			OutputBinding = new CommandBinding(Ex,
				(sender, args) =>
				{
					((ICommand)this).Execute(args.Parameter);
					args.Handled = true;
				},
				(sender, args) =>
				{
					args.CanExecute =
						((ICommand)this).CanExecute(args.Parameter);
				}
			);
		}

		public OutputToggleEnabled (UIElement element) : this()
		{
			Element = element;
		}

		public bool CanExecute (object parameter)
		{
			return true;
		}

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		// todo how to do this without direct references?
		public void Execute (object parameter)
		{
			var flag = Element.IsEnabled;
			Element.IsEnabled = !flag;
		}
	}
}
