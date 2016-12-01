using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;


namespace ContentToggleButton
{

	public class ContentToggle : ToggleButton, IContent
	{
		//IContent interface

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

		public new bool? IsChecked
		{
			get { return (bool?)GetValue(IsCheckedProperty); }
			set { SetCurrentValue(IsCheckedProperty, value); }
		}

		//Protective wrapper to stop setting a local value from replacing bindings
		public new bool IsEnabled
		{
			get { return (bool)GetValue(IsEnabledProperty); }
			set
			{
				SetCurrentValue(IsEnabledProperty, value);
			}
		}

		public new bool? IsPressed
		{
			get { return (bool?)base.GetValue(IsPressedProperty); }
			set { base.SetCurrentValue(IsPressedProperty, value); }
		}

		//EVENTS

		//Click
		public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent(
			"Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ContentToggle));

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
		
		//SERVICES

		public void Bind (object options, object state0)
		{
			this.Options = (List<string>)options;
			this.InitialState = (bool?)state0;
		}

		//OTHER DPs

		//Custom DP InitialState
		public static readonly DependencyProperty 
			InitialStateProperty = DependencyProperty.Register(
			"InitialState", typeof(bool?), typeof(ContentToggle), 
			new PropertyMetadata(false));
		
		public bool? InitialState
		{
			get { return (bool?) GetValue(InitialStateProperty); }
			set { SetValue(InitialStateProperty, value); }
		}
		
		//CONSTRUCTORS
		
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
			var tb = (ContentToggle)o;
			var value = (bool) e.NewValue;

			if ((bool)e.NewValue && !tb.IsChecked.HasValue)
			{
				// If IsChecked is null when enable becomes true, set IsChecked to false
				if (value && tb.IsChecked == null)
					tb.IsChecked = false;
			}
		}

		public ContentToggle ()
		{
			this.Loaded += OnElementLoaded;
			base.Click += RaiseClickEvent;

		}

		//INITIAL STATE

		// Set button to dissabled state if InitialState of IsChecked is null
		private void OnElementLoaded(object o, RoutedEventArgs e)
		{
			if (this.InitialState == null)
			{
				this.IsEnabled = false;
				this.IsChecked = false;
			}
			else
			{
				this.IsChecked = this.InitialState;
			}
		}
	}
}
