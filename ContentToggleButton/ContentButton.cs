using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;


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
			"Targets", typeof(ObservableCollection<ContentControl>), 
			typeof(ContentButton),
			new PropertyMetadata(default(ObservableCollection<ContentControl>)));

		public ObservableCollection<ContentControl> Targets
		{
			get { return (ObservableCollection<ContentControl>)GetValue(TargetsProperty); }
			set { SetValue(TargetsProperty, value); }
		}

		private void refreshBindings (object sender,
			NotifyCollectionChangedEventArgs eventArgs)
		{
			if (eventArgs.NewItems != null)
			{
				foreach (var newItem in eventArgs.NewItems)
				{
					
				}
			}
			if (eventArgs.OldItems == null) return;
			foreach (var oldItem in eventArgs.OldItems)
			{
					
			}
		}

		private static bool ButtonPauseTargets(RoutedEventArgs e,
			Func<ToggleButton, bool> ex)
		{
			var handled = false;

			if (e.OriginalSource.GetType() == typeof(ContentButton))
			{
				var cb = e.OriginalSource as ContentButton;
				var containers = cb.Targets as IList<ContentControl>;
				if (containers == null || containers.Count == 0)
				{
					var target = e.OriginalSource as ToggleButton;
					if (target == null) return false;

					handled = ex(target);
				}
				else
				{
					foreach (var container in containers)
					{
						var target = container.Content as ToggleButton;
						if (target == null) continue;

						handled = ex(target);
					}
				}
			}
			else
			{
				var target = e.OriginalSource as ToggleButton;
				if (target == null) return false;
				handled = ex(target);
			}

			return handled;
		}

		private void commandTargets (object sender, ExecutedRoutedEventArgs e)
		{
			foreach (var target in Targets)
			{
				ButtonPauseTargets(e, delegate(ToggleButton tb)
				{
					CommandTarget = tb;
					Command.Execute(null);
					return true;
				});
			}
		}

		//EVENTS

		public new static readonly RoutedEvent ClickEvent = 
			EventManager.RegisterRoutedEvent(
			"Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), 
			typeof(ContentButton));

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
			Targets = new ObservableCollection<ContentControl>();
			Command.Execute();
		}

	}
}
