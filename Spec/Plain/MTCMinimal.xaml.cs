using System.Diagnostics;
using System.Windows;
using System.Windows.Controls.Primitives;
using ContentToggleButton;

namespace Spec.Plain
{
	/// <summary>
	/// Interaction logic for MTCMinimal.xaml
	/// </summary>
	public partial class MTCMinimal : Window
	{
		//public MyStyleSetters setters { get; set; }
		
		public MTCMinimal()
		{
			//setters = new MyStyleSetters();

			InitializeComponent();

			//setters.Add(new Setter
			//{
			//	Property = FrameworkElement.HeightProperty,
			//	Value = this.Resources["ButtonHeight"]
			//});

			//setters.Add(new Setter
			//{
			//	Property = FrameworkElement.MarginProperty,
			//	Value = this.Resources["ButtonMargin"]
			//});

			//setters.Add(new EventSetter
			//{
			//	Event = ButtonBase.ClickEvent,
			//	Handler = (RoutedEventHandler)StyleClick
			//});

		}

		private void StyleClick(object sender, RoutedEventArgs e)
		{
			Debug.WriteLine("StyleClick");
		}
	}
}