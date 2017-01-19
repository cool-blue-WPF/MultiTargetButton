using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ContentToggleButton;

namespace Spec
{
	/// <summary>
	/// Interaction logic for MTCMinimal.xaml
	/// </summary>
	public partial class MTCMinimal : Window
	{
		readonly List<ICommand> _commands;
		private readonly IEnumerator<ICommand> _ecommands;

		public MTCMinimal()
		{
			InitializeComponent();

			_commands = new List<ICommand>
			{
				(ICommand) Resources["MTC1"],
				(ICommand) Resources["MTC2"],
				MultiCommands.OutputToggleEnabled,
				MediaCommands.Pause,
				(ICommand) Resources["MTC0"],
			};
			_ecommands = _commands.GetEnumerator();
			refresh(MultiEnable);

			Targets.AddHandler(CommandManager.ExecutedEvent,
				(ExecutedRoutedEventHandler) (async (s, e) =>
						await Task.Run(() =>
						{
							var source = s as FrameworkElement;
							if (source == null) return;
							var elementName = "null";
							source.Dispatcher.Invoke(() =>
								elementName = source.Name
							);
							MessageBox.Show(string.Format("Targets {0} {1}",
								elementName, e.RoutedEvent.Name));
						})
				)
				, true
			);
		}

		private void ChangeMultiCommand_OnClick(object sender, RoutedEventArgs e)
		{
			if (MultiEnable == null) return;

			if (!_ecommands.MoveNext())
			{
				_ecommands.Reset();
				_ecommands.MoveNext();
			}
			MultiEnable.Command = _ecommands.Current;
			var multicommand = _ecommands.Current as MultiTargetCommand;
			if (multicommand == null)
			{
				MultiEnable.CommandTarget = Button1;
			}

			Log.Content = string.Format("{0}\t{1}", _commands.IndexOf(_ecommands.Current),
				_ecommands.Current);

			refresh(MultiEnable);
		}

		private void refresh(ICommandSource source)
		{
			var command = source.Command;
			var multicommand = command as MultiTargetCommand;
			if (multicommand == null && source.CommandTarget != null)
			{
				highlight((Control) source.CommandTarget);
				return;
			}

			var targets = ((ItemsControl) multicommand).Items;

			foreach (var button in Targets.Children)
			{
				((UIElement) button).IsEnabled = true;
				((Control) button).BorderBrush = null;
				((Control) button).BorderThickness = new Thickness(0);
				if (targets == null) continue;
				foreach (CommandTarget target in targets)
				{
					if (target.Target == button)
						highlight((Control) button);
				}
			}
		}

		private void highlight(Control control)
		{
			control.BorderBrush = Brushes.Crimson;
			control.BorderThickness = new Thickness(3);
		}

		private void Targets_OnExecuted(object s, ExecutedRoutedEventArgs e)
		{
			var source = s as FrameworkElement;
			var elementName = source.Name;
			Task.Run(() =>
			{
				MessageBox.Show(string.Format("Targets {0} {1}",
					elementName, e.RoutedEvent.Name));
			});
		}

		private void Pause_OnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			MessageBox.Show("Pause");
		}

		private void Pause_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}
	}
}