using System;
using System.Windows.Input;

namespace ContentToggleButton.Commands
{
	public class RelayCommand<T> : ICommand
	{
		// CALLBACK DELEGATES

		// default values
		private static bool CanExecute (T parameter) { return true; }

		// backing fields
		readonly Action<T> _execute;
		readonly Func<T, bool> _canExecute;

		// properties
		public void Execute (object parameter)
		{
			_execute(TranslateParameter(parameter));
		}
		public bool CanExecute (object parameter)
		{
			return _canExecute(TranslateParameter(parameter));
		}

		// notifiers
		public event EventHandler CanExecuteChanged
		{
			add
			{
				if (_canExecute != null)
					CommandManager.RequerySuggested += value;
			}
			remove
			{
				if (_canExecute != null)
					CommandManager.RequerySuggested -= value;
			}
		}

		
		//CONSTRUCTOR
		public RelayCommand (Action<T> execute, Func<T, bool> canExecute = null)
		{
			if (execute == null)
				throw new ArgumentNullException("execute");
			_execute = execute;
			_canExecute = canExecute ?? CanExecute;
		}

		
		//SERVICES

		// marshal from string to enum
		private T TranslateParameter (object parameter)
		{
			T value = default(T);
			if (parameter != null && typeof(T).IsEnum)
				value = (T)Enum.Parse(typeof(T), (string)parameter);
			else
				value = (T)parameter;
			return value;
		}
	}
}
