using System.Windows;

namespace ContentToggleButton
{

	public class OutputToggleEnabledImpl : CommandImplBase
	{
		public override void Execute (object parameter)
		{
			var flag = Target.IsEnabled;
			((UIElement) Target).IsEnabled = !flag;
		}
	}
}
