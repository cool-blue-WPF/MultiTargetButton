using System.Reflection;
using System.Windows;

namespace RelayControls
{
	public class Button : System.Windows.Controls.Button
	{
		#region EventToCommand

		private static readonly RoutedEventHandler TriggerCommand = (o, args) =>
		{
			if (o == args.OriginalSource) return;
			args.Handled = true;
			typeof(System.Windows.Controls.Button).GetMethod("OnClick",
				BindingFlags.NonPublic | BindingFlags.Instance)
				.Invoke(o, new object[0]);
		};

		#endregion

		#region Constructor

		static Button ()
		{
			EventManager.RegisterClassHandler(typeof(Button), ClickEvent,
				TriggerCommand);
		}

		#endregion
	}
}
