using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace ContentToggleButton
{
	public static class Behaviours
	{
		#region AP CommandReceivers

		public static readonly DependencyProperty CommandReceiversProperty =
			DependencyProperty.RegisterAttached(
				"CommandReceivers", typeof(CommandBindingCollection),
				typeof(Behaviours),
				new PropertyMetadata(default(CommandBindingCollection),
					(t, a) => UpdateCollection<CommandBindingCollection, UIElement>
						(t, a, "CommandBindings")));

		public static void SetCommandReceivers(DependencyObject element,
			CommandBindingCollection value)
		{
			element.SetValue(CommandReceiversProperty, value);
		}

		public static CommandBindingCollection GetCommandReceivers(
			DependencyObject element)
		{
			return (CommandBindingCollection) element
				.GetValue(CommandReceiversProperty);
		}

		#endregion

		#region AP StyleSetters

		public static readonly DependencyProperty StyleSettersProperty =
			DependencyProperty.RegisterAttached(
				"StyleSetters", typeof(MyStyleSetters),
				typeof(Behaviours),
				new PropertyMetadata(default(MyStyleSetters),
					ButtonSettersChanged));

		private static void ButtonSettersChanged (DependencyObject d,
			DependencyPropertyChangedEventArgs args)
		{
			var fe = d as FrameworkElement;
			if (fe == null) return;
			var ui = d as UIElement;

			var newValue = args.NewValue as SetterBaseCollection;
			if (newValue != null)
			{
				foreach (var member in newValue)
				{
					var setter = member as Setter;
					if(setter != null)
					{
						fe.SetValue(setter.Property, setter.Value);
						continue;
					}
					var eventSetter = member as EventSetter;
					if (eventSetter == null) continue;
					if (ui == null || eventSetter.Event == null) continue;
					ui.AddHandler(eventSetter.Event, eventSetter.Handler);
				}
			}
		}

		public static void SetStyleSetters(DependencyObject element,
			MyStyleSetters value)
		{
			element.SetValue(StyleSettersProperty, value);
		}

		public static MyStyleSetters GetStyleSetters (
			DependencyObject element)
		{
			return (MyStyleSetters)element
				.GetValue(StyleSettersProperty);
		}


		#endregion

		private static void UpdateCollection<TCollection, THost>(
			object target, DependencyPropertyChangedEventArgs args,
			string targetCollection)
			where TCollection : class, IList
			where THost : class
		{
			var host = target as THost;
			if (host == null) return;

			var collection = typeof(THost).GetProperty(targetCollection)
				.GetValue(host) as IList;
			if (collection == null) return;

			var oldValue = args.OldValue as TCollection;
			if (oldValue != null)
			{
				foreach (var member in oldValue)
				{
					collection.Remove(member);
				}
			}
			try
			{
				var newValue = args.NewValue as TCollection;
				if (newValue != null)
				{
					foreach (var member in newValue)
					{
						collection.Add(member);
					}
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine("{0} in UpdateCollection<TCollection, THost>");
			}
		}
	}

	public class MyStyleSetters : List<SetterBase>
	{
	}

	public class TestList : List<string>
	{
		
	}
}