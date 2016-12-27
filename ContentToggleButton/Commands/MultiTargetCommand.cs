using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ContentToggleButton.Commands
{
	//[ContentProperty("Targets")]
	public class MultiTargetCommand : ItemsControl, ICommand, ICommandSource
	{
		private class DistributorBinding
		{
			private CommandBinding _binding;
			private readonly MultiTargetCommand _host;

			public DistributorBinding(MultiTargetCommand host, ICommand cmd)
			{
				_host = host;
				Binding = new CommandBinding(cmd,
					(s, e) =>
					{
						((ICommand) host).Execute(e.Parameter);
						e.Handled = true;
					},
					(s, e) =>
					{
						e.CanExecute =
							((ICommand) host).CanExecute(e.Parameter);
					});
			}

			private CommandBinding Binding
			{
				get { return _binding; }
				set
				{
					_binding = value;
					//_binding.PreviewExecuted += _host.Distributor;
				}
			}
		}

		static void ExecuteCommand(ICommand command, object parameter,
			IInputElement target)
		{
			RoutedCommand routedCommand = command as RoutedCommand;
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

		DistributorBinding _distributorBinding;

		void Distributor()
		{
			var source = this as ICommandSource;

			foreach (var child in this.Items)
			{
				ExecuteCommand(source.Command, source.CommandParameter,
					((CommandTarget) child).Target);
			}
		}

		#region ICommand

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public event EventHandler CanExecuteChanged;

		public void Execute(object parameter)
		{
			Distributor();
		}

		#endregion

		#region ICommandSource

		//todo make a bindable command class and use that for the command property
		// maybe this just needs a DP Command property...

		private ICommand _command;
		public ICommand Command
		{
			get { return _command; }
			set
			{
				if (value == _command) return;
				_command = value;
				_distributorBinding = new DistributorBinding(this, _command);
			}
		}

		public object CommandParameter { get; set; }

		public IInputElement CommandTarget
		{
			get { throw new NotImplementedException(); }
		}

		#endregion

	}
}