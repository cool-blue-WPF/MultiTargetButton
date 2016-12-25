using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using ContentToggleButton.Commands;
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

			var log = "MultiEnable";
			var children = LogicalTreeHelper.GetChildren(MultiEnable);
			var i = 0;
			foreach (var child in children)
			{
				log += string.Format("\nLogical Child {0} :\t{1}", i++, child.ToString());
			}

			children = LogicalTreeHelper.GetChildren((FrameworkElement)MultiEnable);

			log += "\nFrameworkElement";

			foreach (var child in children)
			{
				log += string.Format("\nLogical Child {0} :\t{1}", i++, child.ToString());
			}
			Dispatcher.InvokeAsync(() => MessageBox.Show(log));
		}


	}
}
