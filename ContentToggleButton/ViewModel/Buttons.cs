using System.Collections.Generic;

namespace ContentToggleButton.ViewModel
{
	
	public class StaticButton
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

		public StaticButton (List<string> options, bool? state0)
		{
			Options = options;
			InitialState = state0;
		}
		public StaticButton (string label)
		{
			Options = new List<string> {"", label};
		}
	}
}
