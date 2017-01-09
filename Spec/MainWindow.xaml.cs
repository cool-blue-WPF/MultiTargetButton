using System;
using System.Windows;
using System.Windows.Controls;

namespace Spec
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		public MainWindow()
		{
			InitializeComponent();

		}

		private void Panel_OnClick(object sender, RoutedEventArgs e)
		{
			Button btn = e.OriginalSource as Button;
			var win = (Window)System.Windows.Application.LoadComponent(
				new Uri(btn.Name.Replace("_", "/") + ".xaml", UriKind.Relative));
			win.Show();
		}
	}
}