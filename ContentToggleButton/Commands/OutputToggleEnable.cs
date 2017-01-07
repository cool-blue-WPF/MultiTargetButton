using System.Windows;
using System.Windows.Input;

namespace ContentToggleButton
{

	public class OutputToggleEnabledBind : CommandBindBase
	{

		public OutputToggleEnabledBind ()
			: base(MultiCommands.OutputToggleEnabled)
		{

		}

		public override bool CheckCanExecute (object sender, CanExecuteRoutedEventArgs args)
		{
			var target = sender as IInputElement;
			return target != null;
		}

		public override void Execute (object sender, ExecutedRoutedEventArgs args)
		{
			var target = sender as UIElement;
			if (target == null) return;
			var flag = target.IsEnabled;
			target.IsEnabled = !flag;
		}
	}
}
