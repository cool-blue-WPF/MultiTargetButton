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
		public StaticButton MyToggleButton { get; set; }

		public StaticButton MyPlainButton { get; set; }

		public MainWindow ()
		{
			MyToggleButton = new StaticButton(new List<string> { "bound Checked", "bound UnChecked" }, true);

			MyPlainButton = new StaticButton(new List<string> { "", "bound Button" });

			InitializeComponent();

			//StaticButton.Bind(clrToggleToggle, new List<string> { "clr Checked", "clr UnChecked" }, true);
			//StaticButton.Bind(clrPlain, new List<string> { "", "clr Button" }, false);
		}

	}

}
