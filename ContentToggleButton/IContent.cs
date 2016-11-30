using System.Collections.Generic;

namespace ContentToggleButton
{
	public interface IContent
	{
		List<string> Options { get; set; }
		bool? IsChecked { get; set; }
		void Bind(object options, object state0);
	}
}
