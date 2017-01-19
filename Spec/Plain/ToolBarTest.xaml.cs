using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using ContentToggleButton.ViewModel;
using SelectorEngine;

namespace Spec.Plain
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class ToolBarTest : Window
	{
		public static List<string> CommandButton = new List<string> {"Coupled to CB"};

		public ButtonView ToggleButtonView { get; set; }

		public ButtonView PlainButtonView { get; set; }

		public bool? InitialState = true;

		public ToolBarTest ()
		{
			// View Models for xaml bindings
			ToggleButtonView = new ButtonView(new List<string>
				{"bound Checked", "bound UnChecked"}, false);
			PlainButtonView = new ButtonView("Toggle -->");

			InitializeComponent();

			foreach (var logicalChild in this.FindChildren<ButtonBase>())
			{
				var toolTipBinding = new Binding("ToolTipProperty")
				{
					RelativeSource = new RelativeSource(RelativeSourceMode.Self),
					Path = new PropertyPath("Name")
				};
				logicalChild.SetBinding(ButtonBase.ToolTipProperty, toolTipBinding);
			}
		}

	}
}