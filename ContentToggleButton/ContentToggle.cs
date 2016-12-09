using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;
using ContentToggleButton.Commands;


namespace ContentToggleButton
{

	public class ContentToggle : ToggleButton, IContent
	{
		private static readonly List<string> DefaultOptions =
			new List<string> { "Checked", "UnChecked" };

		//IContent interface

		public static readonly DependencyProperty
			OptionsProperty = DependencyProperty.Register(
			"Options", typeof(List<string>), typeof(ContentToggle),
			new PropertyMetadata(DefaultOptions));

		public List<string> Options
		{
			get { return (List<string>)GetValue(OptionsProperty); }
			set { SetValue(OptionsProperty, value); }
		}

		//Encapsulate 
		public new bool IsPressed
		{
			get { return (bool)base.GetValue(IsPressedProperty); }
			set { base.SetCurrentValue(IsPressedProperty, value); }
		}

		//EVENTS

		//Click
		public new static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent(
			"Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ContentToggle));

		public new event RoutedEventHandler Click
		{
			add { AddHandler(ClickEvent, value); }
			remove { RemoveHandler(ClickEvent, value); }
		}

		private void RaiseClickEvent (object o, RoutedEventArgs e)
		{
			var newEventArgs = new RoutedEventArgs(ClickEvent);
			RaiseEvent(newEventArgs);
		}
		
		//SERVICES

		public void Bind (object options, object state0)
		{
			this.Options = (List<string>)options;
			this.IsChecked = (bool?)state0;
		}

		//CONSTRUCTORS
		
		static ContentToggle ()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ContentToggle),
			   new FrameworkPropertyMetadata(typeof(ContentToggle)));
		}

		public ContentToggle ()
		{
			base.Click += RaiseClickEvent;

			CommandBindings.Add(new OutputToggle(this).OutputBinding);
			CommandBindings.Add(new OutputToggleEnabled(this).OutputBinding);
		}
	}
}
