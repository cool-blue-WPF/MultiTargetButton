using System.Collections.Generic;
using System.Windows;
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

			//// CLR Bindings
			ButtonView.Bind(clrToggle,
				new List<string> { "clr Checked", "clr UnChecked" }, true);
			ButtonView.Bind(clrPlain, "clr Button");

			Plain.Click += (s, e) => {
				Toggle.IsEnabled = !Toggle.IsEnabled;
			};
		}
	}
}
