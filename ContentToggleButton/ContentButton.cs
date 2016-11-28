using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace ContentToggleButton
{

	public class ContentButton : Button, IContent
	{
		private static readonly List<string> DefaultOptions =
			new List<string> { "", "Button" };

		public static readonly DependencyProperty
			OptionsProperty = DependencyProperty.Register(
			"Options", typeof(List<string>), typeof(ContentButton),
			new PropertyMetadata(DefaultOptions));

		public List<string> Options
		{
			get { return (List<string>)GetValue(OptionsProperty); }
			set { SetValue(OptionsProperty, value); }
		}

		public static readonly DependencyProperty
			InitialStateProperty = DependencyProperty.Register(
			"InitialState", typeof(bool?), typeof(ContentButton),
			new PropertyMetadata(false));

		public bool? InitialState
		{
			get { return (bool?)GetValue(InitialStateProperty); }
			set { SetValue(InitialStateProperty, value); }
		}

		//this also works for single set of value
		//public bool InitialState { get; set; }
		//public List<string> Options { get; set; }

		static ContentButton ()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ContentButton),
			   new FrameworkPropertyMetadata(typeof(ContentButton)));
		}

	}
}
