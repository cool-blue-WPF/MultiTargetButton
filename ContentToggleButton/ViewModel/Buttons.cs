using System.Collections.Generic;

namespace ContentToggleButton.ViewModel
{
	
	public class StaticButton
	{
		public List<string> Options { get; set; }
		public bool InitialState { get; set; }

		public static void Bind (IContent control, List<string> options, bool state0)
		{
			control.Options = options;
			control.IsChecked = state0;
		}

		public StaticButton (List<string> options, bool state0)
		{
			Options = options;
			InitialState = state0;
		}
		public StaticButton (List<string> options)
		{
			Options = options;
		}
		public void Bind (IContent control)
		{
			control.Options = Options;
			control.IsChecked = InitialState;
		}
		public void Initialise (IContent control)
		{
			control.IsChecked = InitialState;
		}
	}

}
