using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using ContentToggleButton.ViewModel;

namespace Spec
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public ButtonView ToggleButtonView { get; set; }

		public ButtonView PlainButtonView { get; set; }

		public MainWindow ()
		{
			// View Models for xaml bindings
			ToggleButtonView = new ButtonView(new List<string>
				{"bound Checked", "bound UnChecked"}, null);
			PlainButtonView = new ButtonView("bound Button");

			InitializeComponent();

			Toggle.Loaded += (o, e) =>
			{
				Debug.Print("Toggle Loaded");
				e.Handled = true;
			};

			//// CLR Bindings
			ButtonView.Bind(clrToggle,
				new List<string> { "clr Checked", "clr UnChecked" }, true);
			ButtonView.Bind(clrPlain, "clr Button");

			Plain.Click += (s, e) =>
			{
				Toggle.IsEnabled = !Toggle.IsEnabled;
			};
			clrPlain.Click += (s, e) =>
			{
				clrToggle.IsEnabled = !clrToggle.IsEnabled;
			};

			var cbBound = false;
			cbButton.Click += (s, e) =>
			{
				if (cbBound)
				{
					CB.SetCurrentValue(ToggleButton.IsCheckedProperty, null);
				}
				cbBound = !cbBound;
			};
		}

		void LogEvent (object o, RoutedEventArgs e, [CallerMemberName] string receiver = null)
		{
			Log.Text += String.Format(
				"{4} from {0}\n\tRouted Event {1}\n\tSourced from {2} : {3}\n",
				o.GetType().Name, e.RoutedEvent, e.Source.GetType().Name, 
				((FrameworkElement)e.Source).Name, receiver);
			Log.ScrollToEnd();
		}

		void PanelButtonClick (object o, RoutedEventArgs e)
		{
			LogEvent(o,e);
		}

		void StyleClick (object o, RoutedEventArgs e)
		{
			LogEvent(o, e);
		}

		private void Log_Clear(object sender, MouseButtonEventArgs e)
		{
			((TextBox)sender).Clear();
			e.Handled = true;
		}
	}
}
