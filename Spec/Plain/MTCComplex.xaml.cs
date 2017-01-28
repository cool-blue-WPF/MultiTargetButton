using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using ContentToggleButton.ViewModel;
using SelectorEngine;

namespace Spec.Plain
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MTCComplex1 : Window
	{
		public static List<string> CommandButton = new List<string> {"Coupled to CB"};

		public ButtonView ToggleButtonView { get; set; }

		public ButtonView PlainButtonView { get; set; }

		public bool? InitialState = true;

		public MTCComplex1 ()
		{
			// View Models for xaml bindings
			ToggleButtonView = new ButtonView(new List<string>
				{"bound Checked", "bound UnChecked"}, false);
			PlainButtonView = new ButtonView("Toggle -->");

			InitializeComponent();

			foreach (var logicalChild in this.FindChildren<ButtonBase>())
			{
				var toolTipBinding = new Binding("ToolTipProperty")
				{
					RelativeSource = new RelativeSource(RelativeSourceMode.Self),
					Path = new PropertyPath("Name")
				};
				logicalChild.SetBinding(ButtonBase.ToolTipProperty, toolTipBinding);
			}
		}

		private void LogEvent (object o, RoutedEventArgs e, 
			[CallerMemberName] string receiver = null)
		{
			//return;
			Log.Text += String.Format(
				"{4} from element {0}\n\tRouted Event {1}\n\tSourced from {2} : {3}\n\tSourced from {5}\n",
				o.GetType().Name, e.RoutedEvent, e.Source.GetType().Name,
				((FrameworkElement)o).Name, receiver, ((FrameworkElement)e.Source).Name);
			Log.ScrollToEnd();
		}

		private void StyleClick (object o, RoutedEventArgs e)
		{
			LogEvent(o, e);
		}

		private void Clear_Log()
		{
			if(Log != null) Log.Clear();
		}

		private void Log_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
		{
			Clear_Log();
			e.Handled = true;
		}

		private void Button_Clear(object sender, ExecutedRoutedEventArgs e)
		{
			Clear_Log();
			e.Handled = true;
		}

		private void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = Log != null && !string.IsNullOrEmpty(Log.Text);
		}
	}
}