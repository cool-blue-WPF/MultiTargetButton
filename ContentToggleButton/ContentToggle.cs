using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;


namespace ContentToggleButton
{

	public class ContentToggle : ToggleButton, IContent
	{
		private static readonly List<string> DefaultOptions =
			new List<string> { "Checked", "UnChecked" };

		public static readonly DependencyProperty
			OptionsProperty = DependencyProperty.Register(
			"Options", typeof(List<string>), typeof(ContentToggle),
			new PropertyMetadata(DefaultOptions));

		public List<string> Options
		{
			get { return (List<string>)GetValue(OptionsProperty); }
			set { SetValue(OptionsProperty, value); }
		}

		//Inherited
		//public new bool? IsChecked

		public new bool IsEnabled
		{
			get { return base.IsEnabled; }
			set
			{
				base.IsEnabled = value;
				if (value && IsChecked == null)
					IsChecked = false;
			}
		}

		static ContentToggle ()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ContentToggle),
			   new FrameworkPropertyMetadata(typeof(ContentToggle)));
		}

	}
}
