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

		public void Bind (object options, object state0)
		{
			this.Options = (List<string>)options;
			this.IsChecked = (bool?)state0;
		}

		static ContentToggle ()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ContentToggle),
			   new FrameworkPropertyMetadata(typeof(ContentToggle)));
			IsEnabledProperty.AddOwner(typeof(ContentToggle),
				new FrameworkPropertyMetadata(IsEnabledPropertyChanged));
		}

		private static void IsEnabledPropertyChanged (
			DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			var myToggleButton = (ContentToggle)o;
			var value = (bool) e.NewValue;

			if ((bool)e.NewValue && !myToggleButton.IsChecked.HasValue)
			{
				if (value && myToggleButton.IsChecked == null)
					myToggleButton.IsChecked = false;
			}
		}

		//INITIAL STATE

		// Set button to dissabled state if InitialState of IsChecked is null
		private void OnElementLoaded(object o, RoutedEventArgs e)
		{
			if (this.IsChecked == null)
			{
				this.IsEnabled = false;
				this.IsChecked = false;
			}
		}

		public ContentToggle()
		{
			this.Loaded += OnElementLoaded;
		}
	}
}
