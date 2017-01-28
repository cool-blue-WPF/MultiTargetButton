using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;

namespace ContentToggleButton.Commands
{
	interface IMultiCommandSource
	{
		Collection<object> _logicalChildren { get; set; }

		IEnumerator LogicalChildren { get; }

		void CommandChangedCallback(DependencyObject d,
			DependencyPropertyChangedEventArgs e);

		void _changeLogicalMultiTargetCommand(MultiCommandButton b,
			object oldValue, object newValue);
	}

}