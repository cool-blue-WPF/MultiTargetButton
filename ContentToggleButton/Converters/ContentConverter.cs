using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ContentToggleButton.Converters
{
	public class ContentConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, 
			object parameter, CultureInfo culture)
		{
			try
			{
				var ic = values[0] as IContent;
				var content = ic == null ? null : ic.Content;
				if(ic == null || content != null) return content;
				var options = ic.Options;

				return ic.IsChecked ?? true 
					? options[0] ?? DependencyProperty.UnsetValue.ToString()
					: options[1] ?? DependencyProperty.UnsetValue.ToString();
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
				return DependencyProperty.UnsetValue.ToString();
			}
		}

		public object[] ConvertBack(object value, Type[] targetTypes, 
			object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
