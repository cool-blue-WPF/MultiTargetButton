using System.Collections.Generic;

namespace ContentToggleButton
{
	public interface IContent
	{
		List<string> Options { get; set; }
		bool? InitialState { get; set; } 
	}
}
