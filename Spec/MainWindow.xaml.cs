using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using ContentToggleButton;
using ContentToggleButton.ViewModel;

namespace Spec
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public static List<string> CommandButton = new List<string>{"Coupled to CB"};

		public ButtonView ToggleButtonView { get; set; }

		public ButtonView PlainButtonView { get; set; }

		public bool? InitialState = true;

		public MainWindow ()
		{
			// View Models for xaml bindings
			ToggleButtonView = new ButtonView(new List<string>
				{"bound Checked", "bound UnChecked"}, null);
			PlainButtonView = new ButtonView("bound Button");

			InitializeComponent();

			//// CLR Bindings
			ButtonView.Bind(clrToggle, new List<string> { "clr Checked", "clr UnChecked" }, 
				null);
			ButtonView.Bind(clrPlain, "clr Button");

			//Button wiring

			Plain.Click += (s, e) =>
			{
				Toggle.IsEnabled = !Toggle.IsEnabled;
			};

			clrPlain.Click += (s, e) =>
			{
				clrToggle.IsEnabled = !clrToggle.IsEnabled;
			};

			var cbBound = true;
			//cbButton.Click += (s, e) =>
			//{
			//	if (cbBound)
			//	{
			//		CB.SetCurrentValue(ToggleButton.IsCheckedProperty, null);
			//	}
			//	cbBound = !cbBound;
			//};
		}

		private void LogEvent (object o, RoutedEventArgs e, [CallerMemberName] string receiver = null)
		{
			//return;
			Log.Text += String.Format(
				"{4} from {0}\n\tRouted Event {1}\n\tSourced from {2} : {3}\n",
				o.GetType().Name, e.RoutedEvent, e.Source.GetType().Name, 
				((FrameworkElement)e.Source).Name, receiver);
			Log.ScrollToEnd();
		}

		private void PanelButtonClick (object o, RoutedEventArgs e)
		{
			LogEvent(o,e);
		}

		private void StyleClick (object o, RoutedEventArgs e)
		{
			LogEvent(o, e);
		}

		private void Log_Clear(object sender, MouseButtonEventArgs e)
		{
			((TextBox)sender).Clear();
			e.Handled = true;
		}

		private void OnButtonPlay(object sender, ExecutedRoutedEventArgs e)
		{
			var cb = e.Source as ToggleButton;
			var contentB = e.Source as ContentButton;
			var contC = contentB.Targets[0] as ContentControl;
			var target = contC.Content as ToggleButton;

			if (target == null) return;
			var flag = target.IsChecked ?? false;
			target.IsChecked = !flag;
			e.Handled = true;
		}

	}
}
