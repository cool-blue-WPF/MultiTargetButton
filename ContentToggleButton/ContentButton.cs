using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;


namespace ContentToggleButton
{

	public class ContentButton : ButtonBase, IContent
	{
		private static readonly List<string> DefaultOptions =
			new List<string> { "Button", "" };

		public static readonly DependencyProperty
			OptionsProperty = DependencyProperty.Register(
			"Options", typeof(List<string>), typeof(ContentButton),
			new PropertyMetadata(DefaultOptions));

		public List<string> Options
		{
			get { return (List<string>)GetValue(OptionsProperty); }
			set { SetValue(OptionsProperty, value); }
		}

		//stub for IContent
		public bool? IsChecked
		{
			get { return null; }
			set { }
		}

		//Protective wrapper to stop setting a local value from replacing bindings
		//i.e. the binding can work in parallel with local setters 
		public new bool IsEnabled
		{
			get { return (bool)GetValue(IsEnabledProperty); }
			set
			{
				SetCurrentValue(IsEnabledProperty, value);
			}
		}

		//COMMANDS

		public static readonly DependencyProperty TargetsProperty = 
			DependencyProperty.Register(
			"targets", typeof(IList), typeof(ContentButton), 
			new PropertyMetadata(default(List<object>)));

		public IList Targets
		{
			get { return (IList)GetValue(TargetsProperty); }
			set { SetValue(TargetsProperty, value); }
		}

		//EVENTS

		public new static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent(
			"Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ContentButton));

		public new event RoutedEventHandler Click
		{
			add { AddHandler(ClickEvent, value); }
			remove { RemoveHandler(ClickEvent, value); }
		}

		void RaiseClickEvent (object o, RoutedEventArgs e)
		{
			RoutedEventArgs newEventArgs = new RoutedEventArgs(ClickEvent);
			RaiseEvent(newEventArgs);
		}

		//Services

		public void Bind (object options, object state0)
		{
			this.Options = new List<string> { (string)options };
		}

		//Constructers

		static ContentButton ()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ContentButton),
			   new FrameworkPropertyMetadata(typeof(ContentButton)));
		}

		public ContentButton()
		{
			base.Click += RaiseClickEvent;
			Targets = new List<object>();
		}

	}
}
