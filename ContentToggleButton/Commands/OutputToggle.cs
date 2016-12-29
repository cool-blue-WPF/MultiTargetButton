using System.Windows.Controls.Primitives;

namespace ContentToggleButton
{
	public class OutputToggleImpl : CommandImplBase
	{
		public override void Execute (object parameter)
		{
			var target = Target as ToggleButton;
			if (target == null) return;
			var flag = target.IsChecked ?? false;
			target.IsChecked = !flag;
		}
	}
}
