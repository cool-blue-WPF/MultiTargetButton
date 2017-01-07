using System.Windows;

namespace WpfApplication1
{
	public static class ClassGroups
	{
		public static readonly DependencyProperty CommandGroupProperty = 
			DependencyProperty.RegisterAttached(
			"CommandGroup", typeof(string), typeof(ClassGroups), 
			new PropertyMetadata(default(string)));

		public static void SetCommandGroup(DependencyObject element, string value)
		{
			element.SetValue(CommandGroupProperty, value);
		}

		public static string GetCommandGroup(DependencyObject element)
		{
			return (string) element.GetValue(CommandGroupProperty);
		}
	}
}
