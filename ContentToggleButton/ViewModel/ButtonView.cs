using System.Collections.Generic;

namespace ContentToggleButton.ViewModel
{
	public class ButtonView
	{
		public List<string> Options { get; set; }
		public bool? InitialState { get; set; }
		
		// BINDINGS

		// CLR Post Initialize binding
		public static void Bind (IContent control, object options, 
			bool? state0 = null)
		{
			control.Bind(options, state0);	// method injection
		}
		
		// VIEW MODEL CONSTRUCTORS

		public ButtonView (List<string> options, object state0)
		{
			Options = options;
			InitialState = (bool?)state0;
		}
		public ButtonView (string label)
		{
			Options = new List<string> {label};
		}

		public ButtonView()
		{
			
		}
	}
}
