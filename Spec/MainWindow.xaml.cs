using System.Collections.Generic;
using System.Windows;
using ContentToggleButton;
using ContentToggleButton.ViewModel;

namespace Spec
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public StaticButton MyToggleButton { get; set; }

		public StaticButton MyPlainButton { get; set; }

		public MainWindow ()
		{
			// View Models
			MyToggleButton = new StaticButton(new List<string>
					{"bound Checked", "bound UnChecked"}, null);
			MyPlainButton = new StaticButton("bound Button");

			InitializeComponent();

			// CLR Bindings
			StaticButton.Bind(clrToggle,
				new List<string> { "clr Checked", "clr UnChecked" }, true);
			StaticButton.Bind(clrPlain, "clr Button");
		}

	}

}
