using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using ContentToggleButton;
using ContentToggleButton.ViewModel;

namespace Spec
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public static List<string> CommandButton = new List<string>{"Coupled to CB"};

		public ButtonView ToggleButtonView { get; set; }

		public ButtonView PlainButtonView { get; set; }

		public bool? InitialState = true;

		public MainWindow ()
		{
			// View Models for xaml bindings
			ToggleButtonView = new ButtonView(new List<string>
				{"bound Checked", "bound UnChecked"}, null);
			PlainButtonView = new ButtonView("bound Button");

			InitializeComponent();

			//// CLR Bindings
			ButtonView.Bind(clrToggle, new List<string> { "clr Checked", "clr UnChecked" }, 
				null);
			ButtonView.Bind(clrPlain, "clr Button");

			//Button wiring

			Plain.Click += (s, e) =>
			{
				Toggle.IsEnabled = !Toggle.IsEnabled;
			};

			clrPlain.Click += (s, e) =>
			{
				clrToggle.IsEnabled = !clrToggle.IsEnabled;
			};

		}

		private void LogEvent (object o, RoutedEventArgs e, [CallerMemberName] string receiver = null)
		{
			//return;
			Log.Text += String.Format(
				"{4} from {0}\n\tRouted Event {1}\n\tSourced from {2} : {3}\n",
				o.GetType().Name, e.RoutedEvent, e.Source.GetType().Name, 
				((FrameworkElement)e.Source).Name, receiver);
			Log.ScrollToEnd();
		}

		private void PanelButtonClick (object o, RoutedEventArgs e)
		{
			LogEvent(o,e);
		}

		private void StyleClick (object o, RoutedEventArgs e)
		{
			LogEvent(o, e);
		}

		private void Log_Clear(object sender, MouseButtonEventArgs e)
		{
			((TextBox)sender).Clear();
			e.Handled = true;
		}

		private bool ButtonPauseTargets (RoutedEventArgs e, 
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

		private void OnButtonPause(object sender, ExecutedRoutedEventArgs e)
		{
			e.Handled = ButtonPauseTargets(e, delegate(ToggleButton target)
			{
				if (!target.IsEnabled) return false;
				var flag = target.IsChecked ?? false;
				target.IsChecked = !flag;
				return true;
			});
		}

		private void OnPauseCanExecute (object sender, CanExecuteRoutedEventArgs e)
		{
			e.Handled = ButtonPauseTargets(e, delegate(ToggleButton target)
			{
				e.CanExecute = target.IsEnabled;
				return e.CanExecute;
			});
		}

		private void OnButtonStopExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			e.Handled = ButtonPauseTargets(e, delegate(ToggleButton target)
			{
				var flag = target.IsEnabled;
				target.IsEnabled = !flag;
				return true;
			});
		}

		private void OnCanExecute (object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}
	}
}
