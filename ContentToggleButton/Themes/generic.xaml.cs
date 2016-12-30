using System.Windows;
using System.Windows.Input;

namespace ContentToggleButton.Themes
{
	public partial class Generic : ResourceDictionary
	{
		public Generic()
		{
			CommandManager.RegisterClassCommandBinding(typeof(ContentToggle),
				new OutputToggleEnabledBind());
			CommandManager.RegisterClassCommandBinding(typeof(ContentToggle),
				new OutputToggleBind());
		}
	}
}
