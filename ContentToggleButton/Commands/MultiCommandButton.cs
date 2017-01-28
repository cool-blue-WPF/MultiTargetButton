using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;


namespace ContentToggleButton
{
	public class MultiCommandButton : Button
	{

		#region Command

		private Collection<object> AdoptedLogicalChildren { get; set; }

		protected override IEnumerator LogicalChildren
		{
			get
			{
				if(AdoptedLogicalChildren == null) return base.LogicalChildren;
				return this.AdoptedLogicalChildren.GetEnumerator();
			}
		}

		private static  void CommandChangedCallback (DependencyObject d,
			DependencyPropertyChangedEventArgs e)
		{
			var multitarget = e.NewValue as MultiTargetCommand;
			if (multitarget == null) return;

			var b = d as MultiCommandButton;
			if (b == null) return;

			//test(b, e.OldValue, e.NewValue);
			CommandChangedCallbackHelper(b, e.OldValue, e.NewValue);

			if (e.OldValue == null) return;
			b.RemoveLogicalChild(e.OldValue);
		}

		private static void test(MultiCommandButton b,
			object oldValue, object newValue)
		{
			if(oldValue != null)
				b.RemoveLogicalChild(oldValue);
			if(newValue != null)
				b.AddLogicalChild(newValue);
		}

		private static  void CommandChangedCallbackHelper (MultiCommandButton b, 
			object oldValue, object newValue)
		{
			var children = b.AdoptedLogicalChildren;
			if (children != null && oldValue is MultiTargetCommand)
			{
				if (children.Contains(oldValue))
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

			b.AdoptedLogicalChildren = new Collection<object>();
			if (content != null)
				b.AdoptedLogicalChildren.Add(content);

			b.AdoptedLogicalChildren.Add(newValue);

			b.AddLogicalChild(newValue);
		}

		#endregion

		#region Constructor

		static MultiCommandButton ()
		{
			CommandProperty.AddOwner(typeof(MultiCommandButton),
				new FrameworkPropertyMetadata(CommandChangedCallback));
			ContentProperty.AddOwner(typeof(MultiCommandButton),
				new FrameworkPropertyMetadata(CommandChangedCallback));
		}

		#endregion
	}
}
