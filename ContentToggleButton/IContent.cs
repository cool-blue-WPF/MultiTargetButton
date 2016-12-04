using System.Collections.Generic;

namespace ContentToggleButton
{
	public interface IContent
	{
		List<string> Options { get; set; }
		bool? IsChecked { get; set; }
		bool IsEnabled { get; set; }
		bool IsPressed { get; }
		void Bind (object options, object state0);
	}
}
