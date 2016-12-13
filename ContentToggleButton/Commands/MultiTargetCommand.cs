using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Input;

namespace ContentToggleButton.Commands
{
	//[ContentProperty("Targets")]
	public class MultiTargetCommand : FrameworkElement, ICommand, ICommandSource
	{
		public MultiTargetCommand()
		{
			Targets = new ObservableCollection<CommandTarget>();
			Targets.CollectionChanged +=Targets_CollectionChanged;
		}

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

			foreach (var target in Targets)
			{
				ExecuteCommand(source.Command, source.CommandParameter,
					target.Target);
			}
		}

		#region DP Targets

		public static readonly DependencyProperty TargetsProperty =
			DependencyProperty.Register(
				"Targets", typeof(ObservableCollection<CommandTarget>),
				typeof(ContentButton),
				new PropertyMetadata(default(ObservableCollection<CommandTarget>)));

		public ObservableCollection<CommandTarget> Targets
		{
			get
			{
				return (ObservableCollection<CommandTarget>) GetValue(TargetsProperty);
			}
			set { SetValue(TargetsProperty, value); }
		}

		private void Targets_CollectionChanged (object sender,
			NotifyCollectionChangedEventArgs eventArgs)
		{
			if (eventArgs.NewItems != null)
			{
				foreach (var newItem in eventArgs.NewItems)
				{
					this.AddLogicalChild(newItem);
				}
			}
			if (eventArgs.OldItems == null) return;
			foreach (var oldItem in eventArgs.OldItems)
			{
				this.RemoveLogicalChild(oldItem);
			}
		}

		protected override IEnumerator LogicalChildren
		{
			get { return Targets.GetEnumerator(); }
		}

		#endregion

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