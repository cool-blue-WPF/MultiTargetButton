using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ContentToggleButton
{
	public class OutputToggleBind : CommandBindBase
	{
		public OutputToggleBind ()
			: base(Commands.OutputToggle)
		{
				
		}

		public override bool CheckCanExecute (object sender, CanExecuteRoutedEventArgs args)
		{
			var target = sender as UIElement;
			return target != null && target.IsEnabled;
		}

		public override void Execute (object sender, ExecutedRoutedEventArgs args)
		{
			var target = sender as ToggleButton;
			if (target == null) return;
			var flag = target.IsChecked ?? false;
			target.IsChecked = !flag;
		}
	}
}
