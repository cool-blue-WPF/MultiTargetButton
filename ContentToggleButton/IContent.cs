using System.Collections.Generic;

namespace ContentToggleButton
{
	internal interface IContent
	{
		List<string> Options { get; set; }
		bool InitialState { get; set; } 
	}
}
