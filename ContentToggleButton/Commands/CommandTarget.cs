using System.Dynamic;
using System.Windows;
using System.Windows.Markup;

namespace ContentToggleButton.Commands
{
	[ContentProperty("Target")]
	public class CommandTarget : DependencyObject
	{
		public IInputElement Target
		{
			get { return (IInputElement) GetValue(TargetProperty); }
			set { SetValue(TargetProperty, value); }
		}
		public static readonly DependencyProperty TargetProperty =
			DependencyProperty.Register(
				"Target", typeof(IInputElement), typeof(CommandTarget),
				new PropertyMetadata(default(IInputElement)));

	}
}
