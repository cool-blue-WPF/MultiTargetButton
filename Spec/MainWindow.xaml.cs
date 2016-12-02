using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
				//else
				//{
				//	CB.ClearValue(ToggleButton.IsCheckedProperty);
				//}
				cbBound = !cbBound;
			};
		}

		void PanelButtonClick (object o, RoutedEventArgs e)
		{
			Debug.Print("PanelButtonClick from {0}\tRouted Event {1}\tSourced from {2}", 
				o.GetType().Name, e.RoutedEvent, e.Source.GetType().Name);
		}

		void StyleClick (object o, RoutedEventArgs e)
		{
			Debug.Print("StyleClick from {0}\tSourced from {1}", 
				o.GetType().Name, e.Source.GetType().Name);
		}

	}

}
