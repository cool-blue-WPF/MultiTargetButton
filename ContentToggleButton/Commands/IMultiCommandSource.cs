using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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