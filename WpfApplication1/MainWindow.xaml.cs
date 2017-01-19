using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using SelectorEngine;
using ContentToggleButton;

namespace WpfApplication1
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow ()
		{
			InitializeComponent();
			_list = new List<ICommand>
			{
				(ICommand)Resources["MTC1"],
				(ICommand)Resources["MTC2"],
				MultiCommands.OutputToggleEnabled,
				MediaCommands.Pause
			};
			_eList = _list.GetEnumerator();

			foreach (var logicalChild in this.FindChildren<ButtonBase>())
			{

			}
		}

		private void asyncmessage(string message)
		{
			Task.Run(() => MessageBox.Show(message));
		}

		private List<ICommand> _list;
		private IEnumerator<ICommand> _eList;

		private void ChangeMultiCommand_OnClick (object sender, RoutedEventArgs e)
		{
			if (MultiEnable == null) return;

			if (!_eList.MoveNext())
			{
				_eList.Reset();
				_eList.MoveNext();
			}
			MultiEnable.Command = _eList.Current;
			var multicommand = _eList.Current as MultiTargetCommand;
			if (multicommand == null)
			{
				MultiEnable.CommandTarget = Button1;
			}
		}

		private void Sealed_OnClick(object sender, RoutedEventArgs e)
		{
			Button btn = e.OriginalSource as Button;
			var win = (Window)System.Windows.Application.LoadComponent(
				new Uri(btn.Name.Replace("_", "/") + ".xaml", UriKind.Relative));
			win.Show();
		}
	}
}
