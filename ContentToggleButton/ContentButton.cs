using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;


namespace ContentToggleButton
{

	public class ContentButton : ButtonBase, IContent
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

		//EVENTS

		public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent(
			"Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ContentButton));

		public new event RoutedEventHandler Click
		{
			add { AddHandler(ClickEvent, value); }
			remove { RemoveHandler(ClickEvent, value); }
		}

		void RaiseClickEvent (object o, RoutedEventArgs e)
		{
			RoutedEventArgs newEventArgs = new RoutedEventArgs(ContentButton.ClickEvent);
			RaiseEvent(newEventArgs);
		}

		public void Bind (object options, object state0)
		{
			this.Options = new List<string> { "", (string)options };
		}

		static ContentButton ()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ContentButton),
			   new FrameworkPropertyMetadata(typeof(ContentButton)));
		}

		public ContentButton()
		{
			base.Click += RaiseClickEvent;
		}

	}
}
