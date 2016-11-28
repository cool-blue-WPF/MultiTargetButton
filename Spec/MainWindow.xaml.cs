using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace Spec
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public List<string> options { get; set; }
		public bool initialState { get; set; }

		public List<string> label { get; set; }

		public string text { get; set; }

		public MainWindow ()
		{
			//options = new List<string> { "Checked", "UnChecked" };

			//label = new List<string> { "Button", "" };

			//initialState = false;

			//text = "set text";

			InitializeComponent();

		}

	}

}
