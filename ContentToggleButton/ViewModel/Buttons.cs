using System.Collections.Generic;

namespace ContentToggleButton.ViewModel
{
	
	public class ButtonView
	{
		public List<string> Options { get; set; }
		public bool? InitialState { get; set; }

		
		// BINDINGS

		// CLR Post Initialize binding
		public static void Bind (IContent control, List<string> options, bool? state0)
		{
			control.Options = options;
			control.IsChecked = state0;
		}
		public static void Bind (IContent control,string label)
		{
			control.Options = new List<string> { "", label };
		}

		
		// VIEW MODEL CONSTRUCTORS

		public ButtonView (List<string> options, bool? state0)
		{
			Options = options;
			InitialState = state0;
		}
		public ButtonView (string label)
		{
			Options = new List<string> {"", label};
		}
	}
}
