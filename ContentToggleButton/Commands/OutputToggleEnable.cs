using System.Windows;
using System.Windows.Input;

namespace ContentToggleButton
{

	public class OutputToggleEnabledImpl : CommandImplBase
	{
		public OutputToggleEnabledImpl() : base()
		{
			
		}
		
		public OutputToggleEnabledImpl (IInputElement target, RoutedUICommand cmd)
			: base(target, cmd)
		{

		}

		public override void Execute (object parameter)
		{
			var flag = Target.IsEnabled;
			((UIElement) Target).IsEnabled = !flag;
		}
	}
}
