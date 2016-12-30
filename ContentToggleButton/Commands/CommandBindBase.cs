using System.Windows.Input;

namespace ContentToggleButton
{
	public abstract class CommandBindBase : CommandBinding
	{
		protected CommandBindBase (RoutedUICommand cmd)
			: base(cmd)
		{
			base.Executed += (sender, args) =>
			{
				this.Execute(sender, args);
				args.Handled = true;
			};
			base.CanExecute += (sender, args) =>
			{
				args.CanExecute =
					this.CheckCanExecute(sender, args);
			};
		}

		public virtual bool CheckCanExecute (object sender, CanExecuteRoutedEventArgs args)
		{
			return true;
		}

		public abstract void Execute(object sender, ExecutedRoutedEventArgs args);
	}
}