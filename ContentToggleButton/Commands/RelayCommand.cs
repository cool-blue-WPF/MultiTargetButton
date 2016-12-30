using System;
using System.Windows.Input;

namespace ContentToggleButton
{
	public class RelayCommand<T> : ICommand
	{
		// CALLBACK DELEGATES

		private static bool DefaultCanExecute(T parameter)
		{
			return true;
		}

		#region ICommand

		#region bool CheckCanExecute

		readonly Func<T, bool> _canExecute;
		public bool CanExecute (object parameter)
		{
			return _canExecute(TranslateParameter(parameter));
		}

		#endregion

		#region Action<T> Execute

		readonly Action<T> _execute;
		public void Execute (object parameter)
		{
			_execute(TranslateParameter(parameter));
		}

		#endregion

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

		#endregion

		//CONSTRUCTOR
		public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
		{
			if (execute == null)
				throw new ArgumentNullException("execute");
			_execute = execute;
			_canExecute = canExecute ?? DefaultCanExecute;
		}


		//SERVICES

		// marshal from string to enum
		private T TranslateParameter(object parameter)
		{
			T value = default(T);
			if (parameter != null && typeof(T).IsEnum)
				value = (T) Enum.Parse(typeof(T), (string) parameter);
			else
				value = (T) parameter;
			return value;
		}
	}
}