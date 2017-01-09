using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls.Primitives;


namespace ContentToggleButton
{
	public class ContentButton : ButtonBase, IContent
	{
		#region IContent
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
			set { throw new InvalidOperationException("IsChecked is not implemented on a Button"); }
		}

		//Protective wrapper to stop setting of a local value from replacing bindings
		//i.e. the binding can work in parallel with local setters 
		//public new bool IsEnabled
		//{
		//	get { return (bool)GetValue(IsEnabledProperty); }
		//	set
		//	{
		//		SetCurrentValue(IsEnabledProperty, value);
		//	}
		//}

		public void Bind (object options, bool? state0 = false)
		{
			Options = new List<string> { (string)options };
		}

		#endregion

		#region Command

		private Collection<object> _logicalChildren = 
					(Collection<object>)null;

		protected override IEnumerator LogicalChildren
		{
			get
			{
				if(_logicalChildren == null) return base.LogicalChildren;
				return this._logicalChildren.GetEnumerator();
			}
		}

		public static readonly DependencyProperty CommandProxy =
			CommandProperty.AddOwner(typeof(ContentButton),
			new FrameworkPropertyMetadata(CommandChangedCallback));

		private static void CommandChangedCallback (DependencyObject d,
			DependencyPropertyChangedEventArgs e)
		{
			var multitarget = e.NewValue as MultiTargetCommand;
			if (multitarget == null) return;

			var b = d as ContentButton;
			if (b == null) return;

			_changeLogicalMultiTargetCommand(b, e.OldValue, e.NewValue);

			if (e.OldValue == null) return;
			b.RemoveLogicalChild(e.OldValue);
		}

		private static void _changeLogicalMultiTargetCommand (ContentButton b, 
			object oldValue, object newValue)
		{
			var children = b._logicalChildren;
			if (children != null && oldValue is MultiTargetCommand)
			{
				if (children.Contains(oldValue))	// 
				{
					children.Remove(oldValue);
					b.RemoveLogicalChild(oldValue);

					children.Add(newValue);
					b.AddLogicalChild(newValue);

					return;
				}
				throw new InvalidOperationException("Only one MultiCommand can be set");
			}

			var baseChildren = b.LogicalChildren;
			object content = null;
			if (baseChildren != null && baseChildren.MoveNext())
			{
				content = baseChildren.Current;
			}

			b._logicalChildren = new Collection<object>();
			if (content != null)
				b._logicalChildren.Add(content);

			b._logicalChildren.Add(newValue);

			b.AddLogicalChild(newValue);
		}

		#endregion

		#region Constructors

		static ContentButton ()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ContentButton),
			   new FrameworkPropertyMetadata(typeof(ContentButton)));
		}
		#endregion
	}
}
