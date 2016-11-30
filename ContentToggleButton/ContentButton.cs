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

		public bool? IsChecked
		{
			get { return null; }
			set { }
		}

		public void Bind(object options, object state0)
		{
			this.Options = new List<string> { "", (string)options };
		}

		static ContentButton ()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ContentButton),
			   new FrameworkPropertyMetadata(typeof(ContentButton)));
		}

	}
}
