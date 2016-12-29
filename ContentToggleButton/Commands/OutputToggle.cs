using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ContentToggleButton
{
	public class OutputToggleImpl : CommandImplBase
	{
		public OutputToggleImpl()
		{
				
		}
		public OutputToggleImpl(IInputElement target, RoutedUICommand cmd)
			: base(target, cmd)
		{
			
		}

		public override void Execute (object parameter)
		{
			var target = Target as ToggleButton;
			if (target == null) return;
			var flag = target.IsChecked ?? false;
			target.IsChecked = !flag;
		}
	}
}
