// Decompiled with JetBrains decompiler
// Type: System.Windows.Input.CommandManager
// Assembly: PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 21C941D8-B1F8-4BF2-940D-B5B9EA76B03B
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_32\PresentationCore\v4.0_4.0.0.0__31bf3856ad364e35\PresentationCore.dll

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security;
using System.Windows.Threading;

namespace System.Windows.Input
{
  /// <summary>Provides command related utility methods that register <see cref="T:System.Windows.Input.CommandBinding" /> and <see cref="T:System.Windows.Input.InputBinding" /> objects for class owners and commands, add and remove command event handlers, and provides services for querying the status of a command.</summary>
  public sealed class CommandManager
  {
    /// <summary>Identifies the <see cref="E:System.Windows.Input.CommandManager.PreviewExecuted" /> attached event.</summary>
    public static readonly RoutedEvent PreviewExecutedEvent = EventManager.RegisterRoutedEvent("PreviewExecuted", RoutingStrategy.Tunnel, typeof (ExecutedRoutedEventHandler), typeof (CommandManager));
    /// <summary>Identifies the <see cref="E:System.Windows.Input.CommandManager.Executed" /> attached event.</summary>
    public static readonly RoutedEvent ExecutedEvent = EventManager.RegisterRoutedEvent("Executed", RoutingStrategy.Bubble, typeof (ExecutedRoutedEventHandler), typeof (CommandManager));
    /// <summary>Identifies the <see cref="E:System.Windows.Input.CommandManager.PreviewCanExecute" /> attached event.</summary>
    public static readonly RoutedEvent PreviewCanExecuteEvent = EventManager.RegisterRoutedEvent("PreviewCanExecute", RoutingStrategy.Tunnel, typeof (CanExecuteRoutedEventHandler), typeof (CommandManager));
    /// <summary>Identifies the <see cref="E:System.Windows.Input.CommandManager.CanExecute" /> attached event.</summary>
    public static readonly RoutedEvent CanExecuteEvent = EventManager.RegisterRoutedEvent("CanExecute", RoutingStrategy.Bubble, typeof (CanExecuteRoutedEventHandler), typeof (CommandManager));
    private static HybridDictionary _classCommandBindings = new HybridDictionary();
    private static HybridDictionary _classInputBindings = new HybridDictionary();
    [ThreadStatic]
    private static CommandManager _commandManager;
    private DispatcherOperation _requerySuggestedOperation;

    private static CommandManager Current
    {
      get
      {
        if (CommandManager._commandManager == null)
          CommandManager._commandManager = new CommandManager();
        return CommandManager._commandManager;
      }
    }

    /// <summary>Occurs when the <see cref="T:System.Windows.Input.CommandManager" /> detects conditions that might change the ability of a command to execute.</summary>
    public static event EventHandler RequerySuggested
    {
      add
      {
        CommandManager.RequerySuggestedEventManager.AddHandler((CommandManager) null, value);
      }
      remove
      {
        CommandManager.RequerySuggestedEventManager.RemoveHandler((CommandManager) null, value);
      }
    }

    private event EventHandler PrivateRequerySuggested;

    private CommandManager()
    {
    }

    /// <summary>Attaches the specified <see cref="T:System.Windows.Input.ExecutedRoutedEventHandler" /> to the specified element.</summary>
    /// <param name="element">The element to attach <paramref name="handler" /> to.</param>
    /// <param name="handler">The can execute handler.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> or <paramref name="handler" /> is null.</exception>
    public static void AddPreviewExecutedHandler(UIElement element, ExecutedRoutedEventHandler handler)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      if (handler == null)
        throw new ArgumentNullException("handler");
      element.AddHandler(CommandManager.PreviewExecutedEvent, (Delegate) handler);
    }

    /// <summary>Detaches the specified <see cref="T:System.Windows.Input.ExecutedRoutedEventHandler" /> from the specified element.</summary>
    /// <param name="element">The element to remove <paramref name="handler" /> from.</param>
    /// <param name="handler">The executed handler.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> or <paramref name="handler" /> is null.</exception>
    public static void RemovePreviewExecutedHandler(UIElement element, ExecutedRoutedEventHandler handler)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      if (handler == null)
        throw new ArgumentNullException("handler");
      element.RemoveHandler(CommandManager.PreviewExecutedEvent, (Delegate) handler);
    }

    /// <summary>Attaches the specified <see cref="T:System.Windows.Input.ExecutedRoutedEventHandler" /> to the specified element. </summary>
    /// <param name="element">The element to attach <paramref name="handler" /> to.</param>
    /// <param name="handler">The executed handler.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> or <paramref name="handler" /> is null.</exception>
    public static void AddExecutedHandler(UIElement element, ExecutedRoutedEventHandler handler)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      if (handler == null)
        throw new ArgumentNullException("handler");
      element.AddHandler(CommandManager.ExecutedEvent, (Delegate) handler);
    }

    /// <summary>Detaches the specified <see cref="T:System.Windows.Input.ExecutedRoutedEventHandler" /> from the specified element.</summary>
    /// <param name="element">The element to remove <paramref name="handler" /> from.</param>
    /// <param name="handler">The executed handler.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> or <paramref name="handler" /> is null.</exception>
    public static void RemoveExecutedHandler(UIElement element, ExecutedRoutedEventHandler handler)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      if (handler == null)
        throw new ArgumentNullException("handler");
      element.RemoveHandler(CommandManager.ExecutedEvent, (Delegate) handler);
    }

    /// <summary>Attaches the specified <see cref="T:System.Windows.Input.CanExecuteRoutedEventHandler" /> to the specified element.</summary>
    /// <param name="element">The element to attach <paramref name="handler" /> to.</param>
    /// <param name="handler">The can execute handler.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> or <paramref name="handler" /> is null.</exception>
    public static void AddPreviewCanExecuteHandler(UIElement element, CanExecuteRoutedEventHandler handler)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      if (handler == null)
        throw new ArgumentNullException("handler");
      element.AddHandler(CommandManager.PreviewCanExecuteEvent, (Delegate) handler);
    }

    /// <summary>Detaches the specified <see cref="T:System.Windows.Input.CanExecuteRoutedEventHandler" /> from the specified element.</summary>
    /// <param name="element">The element to remove <paramref name="handler" /> from.</param>
    /// <param name="handler">The can execute handler.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> or <paramref name="handler" /> is null.</exception>
    public static void RemovePreviewCanExecuteHandler(UIElement element, CanExecuteRoutedEventHandler handler)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      if (handler == null)
        throw new ArgumentNullException("handler");
      element.RemoveHandler(CommandManager.PreviewCanExecuteEvent, (Delegate) handler);
    }

    /// <summary>Attaches the specified <see cref="T:System.Windows.Input.CanExecuteRoutedEventHandler" /> to the specified element.</summary>
    /// <param name="element">The element to attach <paramref name="handler" /> to.</param>
    /// <param name="handler">The can execute handler.  </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> or <paramref name="handler" /> is null.</exception>
    public static void AddCanExecuteHandler(UIElement element, CanExecuteRoutedEventHandler handler)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      if (handler == null)
        throw new ArgumentNullException("handler");
      element.AddHandler(CommandManager.CanExecuteEvent, (Delegate) handler);
    }

    /// <summary>Detaches the specified <see cref="T:System.Windows.Input.CanExecuteRoutedEventHandler" /> from the specified element.</summary>
    /// <param name="element">The element to remove <paramref name="handler" /> from.</param>
    /// <param name="handler">The can execute handler.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> or <paramref name="handler" /> is null.</exception>
    public static void RemoveCanExecuteHandler(UIElement element, CanExecuteRoutedEventHandler handler)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      if (handler == null)
        throw new ArgumentNullException("handler");
      element.RemoveHandler(CommandManager.CanExecuteEvent, (Delegate) handler);
    }

    /// <summary>Registers the specified <see cref="T:System.Windows.Input.InputBinding" /> with the specified type. </summary>
    /// <param name="type">The type to register <paramref name="inputBinding" /> with.</param>
    /// <param name="inputBinding">The input binding to register.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> or <paramref name="inputBinding" /> is null.</exception>
    public static void RegisterClassInputBinding(Type type, InputBinding inputBinding)
    {
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      if (inputBinding == null)
        throw new ArgumentNullException("inputBinding");
      lock (CommandManager._classInputBindings.SyncRoot)
      {
        InputBindingCollection local_0 = CommandManager._classInputBindings[(object) type] as InputBindingCollection;
        if (local_0 == null)
        {
          local_0 = new InputBindingCollection();
          CommandManager._classInputBindings[(object) type] = (object) local_0;
        }
        local_0.Add(inputBinding);
        if (inputBinding.IsFrozen)
          return;
        inputBinding.Freeze();
      }
    }

    /// <summary>Registers a <see cref="T:System.Windows.Input.CommandBinding" /> with the specified type. </summary>
    /// <param name="type">The class with which to register <paramref name="commandBinding" />.</param>
    /// <param name="commandBinding">The command binding to register.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> or <paramref name="commandBinding" /> is null.</exception>
    public static void RegisterClassCommandBinding(Type type, CommandBinding commandBinding)
    {
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      if (commandBinding == null)
        throw new ArgumentNullException("commandBinding");
      lock (CommandManager._classCommandBindings.SyncRoot)
      {
        CommandBindingCollection local_0 = CommandManager._classCommandBindings[(object) type] as CommandBindingCollection;
        if (local_0 == null)
        {
          local_0 = new CommandBindingCollection();
          CommandManager._classCommandBindings[(object) type] = (object) local_0;
        }
        local_0.Add(commandBinding);
      }
    }

    /// <summary>Forces the <see cref="T:System.Windows.Input.CommandManager" /> to raise the <see cref="E:System.Windows.Input.CommandManager.RequerySuggested" /> event.</summary>
    public static void InvalidateRequerySuggested()
    {
      CommandManager.Current.RaiseRequerySuggested();
    }

    [SecurityCritical]
    internal static void TranslateInput(IInputElement targetElement, InputEventArgs inputEventArgs)
    {
      if (targetElement == null || inputEventArgs == null)
        return;
      ICommand command = (ICommand) null;
      IInputElement target = (IInputElement) null;
      object parameter = (object) null;
      DependencyObject o = targetElement as DependencyObject;
      bool flag1 = InputElement.IsUIElement(o);
      bool flag2 = !flag1 && InputElement.IsContentElement(o);
      bool flag3 = !flag1 && !flag2 && InputElement.IsUIElement3D(o);
      InputBindingCollection bindingCollection1 = (InputBindingCollection) null;
      if (flag1)
        bindingCollection1 = ((UIElement) targetElement).InputBindingsInternal;
      else if (flag2)
        bindingCollection1 = ((ContentElement) targetElement).InputBindingsInternal;
      else if (flag3)
        bindingCollection1 = ((UIElement3D) targetElement).InputBindingsInternal;
      if (bindingCollection1 != null)
      {
        InputBinding match = bindingCollection1.FindMatch((object) targetElement, inputEventArgs);
        if (match != null)
        {
          command = match.Command;
          target = match.CommandTarget;
          parameter = match.CommandParameter;
        }
      }
      if (command == null)
      {
        lock (CommandManager._classInputBindings.SyncRoot)
        {
          for (Type local_7 = targetElement.GetType(); local_7 != (Type) null; local_7 = local_7.BaseType)
          {
            InputBindingCollection local_16 = CommandManager._classInputBindings[(object) local_7] as InputBindingCollection;
            if (local_16 != null)
            {
              InputBinding local_6 = local_16.FindMatch((object) targetElement, inputEventArgs);
              if (local_6 != null)
              {
                command = local_6.Command;
                target = local_6.CommandTarget;
                parameter = local_6.CommandParameter;
                break;
              }
            }
          }
        }
      }
      if (command == null)
      {
        CommandBindingCollection bindingCollection2 = (CommandBindingCollection) null;
        if (flag1)
          bindingCollection2 = ((UIElement) targetElement).CommandBindingsInternal;
        else if (flag2)
          bindingCollection2 = ((ContentElement) targetElement).CommandBindingsInternal;
        else if (flag3)
          bindingCollection2 = ((UIElement3D) targetElement).CommandBindingsInternal;
        if (bindingCollection2 != null)
          command = bindingCollection2.FindMatch((object) targetElement, inputEventArgs);
      }
      if (command == null)
      {
        lock (CommandManager._classCommandBindings.SyncRoot)
        {
          for (Type local_5 = targetElement.GetType(); local_5 != (Type) null; local_5 = local_5.BaseType)
          {
            CommandBindingCollection local_14 = CommandManager._classCommandBindings[(object) local_5] as CommandBindingCollection;
            if (local_14 != null)
            {
              command = local_14.FindMatch((object) targetElement, inputEventArgs);
              if (command != null)
                break;
            }
          }
        }
      }
      if (command == null || command == ApplicationCommands.NotACommand)
        return;
      if (target == null)
        target = targetElement;
      bool continueRouting = false;
      RoutedCommand routedCommand = command as RoutedCommand;
      if (routedCommand != null)
      {
        if (routedCommand.CriticalCanExecute(parameter, target, inputEventArgs.UserInitiated, out continueRouting))
        {
          continueRouting = false;
          CommandManager.ExecuteCommand(routedCommand, parameter, target, inputEventArgs);
        }
      }
      else if (command.CanExecute(parameter))
        command.Execute(parameter);
      inputEventArgs.Handled = !continueRouting;
    }

    [SecurityCritical]
    [SecurityTreatAsSafe]
    private static bool ExecuteCommand(RoutedCommand routedCommand, object parameter, IInputElement target, InputEventArgs inputEventArgs)
    {
      return routedCommand.ExecuteCore(parameter, target, inputEventArgs.UserInitiated);
    }

    internal static void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
      if (sender == null || e == null || e.Command == null)
        return;
      CommandManager.FindCommandBinding(sender, (RoutedEventArgs) e, e.Command, false);
      if (e.Handled || e.RoutedEvent != CommandManager.CanExecuteEvent)
        return;
      DependencyObject dependencyObject = sender as DependencyObject;
      if (dependencyObject == null || !FocusManager.GetIsFocusScope(dependencyObject))
        return;
      IInputElement scopeFocusedElement = CommandManager.GetParentScopeFocusedElement(dependencyObject);
      if (scopeFocusedElement == null)
        return;
      CommandManager.TransferEvent(scopeFocusedElement, e);
    }

    private static bool CanExecuteCommandBinding(object sender, CanExecuteRoutedEventArgs e, CommandBinding commandBinding)
    {
      commandBinding.OnCanExecute(sender, e);
      if (!e.CanExecute)
        return e.Handled;
      return true;
    }

    internal static void OnExecuted(object sender, ExecutedRoutedEventArgs e)
    {
      if (sender == null || e == null || e.Command == null)
        return;
      CommandManager.FindCommandBinding(sender, (RoutedEventArgs) e, e.Command, true);
      if (e.Handled || e.RoutedEvent != CommandManager.ExecutedEvent)
        return;
      DependencyObject dependencyObject = sender as DependencyObject;
      if (dependencyObject == null || !FocusManager.GetIsFocusScope(dependencyObject))
        return;
      IInputElement scopeFocusedElement = CommandManager.GetParentScopeFocusedElement(dependencyObject);
      if (scopeFocusedElement == null)
        return;
      CommandManager.TransferEvent(scopeFocusedElement, e);
    }

    [SecurityTreatAsSafe]
    [SecurityCritical]
    private static bool ExecuteCommandBinding(object sender, ExecutedRoutedEventArgs e, CommandBinding commandBinding)
    {
      ISecureCommand command = e.Command as ISecureCommand;
      bool flag = e.UserInitiated && command != null && command.UserInitiatedPermission != null;
      if (flag)
        command.UserInitiatedPermission.Assert();
      try
      {
        commandBinding.OnExecuted(sender, e);
      }
      finally
      {
        if (flag)
          CodeAccessPermission.RevertAssert();
      }
      return e.Handled;
    }

    internal static void OnCommandDevice(object sender, CommandDeviceEventArgs e)
    {
      if (sender == null || e == null || e.Command == null)
        return;
      CanExecuteRoutedEventArgs e1 = new CanExecuteRoutedEventArgs(e.Command, (object) null);
      e1.RoutedEvent = CommandManager.CanExecuteEvent;
      e1.Source = sender;
      CommandManager.OnCanExecute(sender, e1);
      if (!e1.CanExecute)
        return;
      ExecutedRoutedEventArgs e2 = new ExecutedRoutedEventArgs(e.Command, (object) null);
      e2.RoutedEvent = CommandManager.ExecutedEvent;
      e2.Source = sender;
      CommandManager.OnExecuted(sender, e2);
      if (!e2.Handled)
        return;
      e.Handled = true;
    }

    private static void FindCommandBinding(object sender, RoutedEventArgs e, ICommand command, bool execute)
    {
      CommandBindingCollection commandBindings = (CommandBindingCollection) null;
      DependencyObject o = sender as DependencyObject;
      if (InputElement.IsUIElement(o))
        commandBindings = ((UIElement) o).CommandBindingsInternal;
      else if (InputElement.IsContentElement(o))
        commandBindings = ((ContentElement) o).CommandBindingsInternal;
      else if (InputElement.IsUIElement3D(o))
        commandBindings = ((UIElement3D) o).CommandBindingsInternal;
      if (commandBindings != null)
        CommandManager.FindCommandBinding(commandBindings, sender, e, command, execute);
      Tuple<Type, CommandBinding> tuple = (Tuple<Type, CommandBinding>) null;
      List<Tuple<Type, CommandBinding>> tupleList = (List<Tuple<Type, CommandBinding>>) null;
      lock (CommandManager._classCommandBindings.SyncRoot)
      {
label_19:
        for (Type local_2 = sender.GetType(); local_2 != (Type) null; local_2 = local_2.BaseType)
        {
          CommandBindingCollection local_7 = CommandManager._classCommandBindings[(object) local_2] as CommandBindingCollection;
          if (local_7 != null)
          {
            int local_13 = 0;
            while (true)
            {
              CommandBinding local_6 = local_7.FindMatch(command, ref local_13);
              if (local_6 != null)
              {
                if (tuple == null)
                {
                  tuple = new Tuple<Type, CommandBinding>(local_2, local_6);
                }
                else
                {
                  if (tupleList == null)
                  {
                    tupleList = new List<Tuple<Type, CommandBinding>>();
                    tupleList.Add(tuple);
                  }
                  tupleList.Add(new Tuple<Type, CommandBinding>(local_2, local_6));
                }
              }
              else
                goto label_19;
            }
          }
        }
      }
      if (tupleList != null)
      {
        ExecutedRoutedEventArgs e1 = execute ? (ExecutedRoutedEventArgs) e : (ExecutedRoutedEventArgs) null;
        CanExecuteRoutedEventArgs e2 = execute ? (CanExecuteRoutedEventArgs) null : (CanExecuteRoutedEventArgs) e;
        for (int index = 0; index < tupleList.Count; ++index)
        {
          if (execute && CommandManager.ExecuteCommandBinding(sender, e1, tupleList[index].Item2) || !execute && CommandManager.CanExecuteCommandBinding(sender, e2, tupleList[index].Item2))
          {
            Type type = tupleList[index].Item1;
            do
              ;
            while (++index < tupleList.Count && tupleList[index].Item1 == type);
            --index;
          }
        }
      }
      else
      {
        if (tuple == null)
          return;
        if (execute)
          CommandManager.ExecuteCommandBinding(sender, (ExecutedRoutedEventArgs) e, tuple.Item2);
        else
          CommandManager.CanExecuteCommandBinding(sender, (CanExecuteRoutedEventArgs) e, tuple.Item2);
      }
    }

    private static void FindCommandBinding(CommandBindingCollection commandBindings, object sender, RoutedEventArgs e, ICommand command, bool execute)
    {
      int index = 0;
      CommandBinding match;
      do
      {
        match = commandBindings.FindMatch(command, ref index);
      }
      while (match != null && (!execute || !CommandManager.ExecuteCommandBinding(sender, (ExecutedRoutedEventArgs) e, match)) && (execute || !CommandManager.CanExecuteCommandBinding(sender, (CanExecuteRoutedEventArgs) e, match)));
    }

    private static void TransferEvent(IInputElement newSource, CanExecuteRoutedEventArgs e)
    {
      RoutedCommand command = e.Command as RoutedCommand;
      if (command == null)
        return;
      try
      {
        e.CanExecute = command.CanExecute(e.Parameter, newSource);
      }
      finally
      {
        e.Handled = true;
      }
    }

    [SecurityCritical]
    [SecurityTreatAsSafe]
    private static void TransferEvent(IInputElement newSource, ExecutedRoutedEventArgs e)
    {
      RoutedCommand command = e.Command as RoutedCommand;
      if (command == null)
        return;
      try
      {
        command.ExecuteCore(e.Parameter, newSource, e.UserInitiated);
      }
      finally
      {
        e.Handled = true;
      }
    }

    private static IInputElement GetParentScopeFocusedElement(DependencyObject childScope)
    {
      DependencyObject parentScope = CommandManager.GetParentScope(childScope);
      if (parentScope != null)
      {
        IInputElement focusedElement = FocusManager.GetFocusedElement(parentScope);
        if (focusedElement != null && !CommandManager.ContainsElement(childScope, focusedElement as DependencyObject))
          return focusedElement;
      }
      return (IInputElement) null;
    }

    private static DependencyObject GetParentScope(DependencyObject childScope)
    {
      DependencyObject element = (DependencyObject) null;
      UIElement uiElement = childScope as UIElement;
      ContentElement contentElement = uiElement == null ? childScope as ContentElement : (ContentElement) null;
      UIElement3D uiElement3D = uiElement != null || contentElement != null ? (UIElement3D) null : childScope as UIElement3D;
      if (uiElement != null)
        element = uiElement.GetUIParent(true);
      else if (contentElement != null)
        element = contentElement.GetUIParent(true);
      else if (uiElement3D != null)
        element = uiElement3D.GetUIParent(true);
      if (element != null)
        return FocusManager.GetFocusScope(element);
      return (DependencyObject) null;
    }

    private static bool ContainsElement(DependencyObject scope, DependencyObject child)
    {
      if (child != null)
      {
        for (DependencyObject childScope = FocusManager.GetFocusScope(child); childScope != null; childScope = CommandManager.GetParentScope(childScope))
        {
          if (childScope == scope)
            return true;
        }
      }
      return false;
    }

    private void RaiseRequerySuggested()
    {
      if (this._requerySuggestedOperation != null)
        return;
      Dispatcher currentDispatcher = Dispatcher.CurrentDispatcher;
      if (currentDispatcher == null || currentDispatcher.HasShutdownStarted || currentDispatcher.HasShutdownFinished)
        return;
      this._requerySuggestedOperation = currentDispatcher.BeginInvoke(DispatcherPriority.Background, (Delegate) new DispatcherOperationCallback(this.RaiseRequerySuggested), (object) null);
    }

    private object RaiseRequerySuggested(object obj)
    {
      this._requerySuggestedOperation = (DispatcherOperation) null;
      // ISSUE: reference to a compiler-generated field
      if (this.PrivateRequerySuggested != null)
      {
        // ISSUE: reference to a compiler-generated field
        this.PrivateRequerySuggested((object) null, EventArgs.Empty);
      }
      return (object) null;
    }

    private class RequerySuggestedEventManager : WeakEventManager
    {
      private static CommandManager.RequerySuggestedEventManager CurrentManager
      {
        get
        {
          Type managerType = typeof (CommandManager.RequerySuggestedEventManager);
          CommandManager.RequerySuggestedEventManager suggestedEventManager = (CommandManager.RequerySuggestedEventManager) WeakEventManager.GetCurrentManager(managerType);
          if (suggestedEventManager == null)
          {
            suggestedEventManager = new CommandManager.RequerySuggestedEventManager();
            WeakEventManager.SetCurrentManager(managerType, (WeakEventManager) suggestedEventManager);
          }
          return suggestedEventManager;
        }
      }

      private RequerySuggestedEventManager()
      {
      }

      public static void AddHandler(CommandManager source, EventHandler handler)
      {
        if (handler == null)
          return;
        CommandManager.RequerySuggestedEventManager.CurrentManager.ProtectedAddHandler((object) source, (Delegate) handler);
      }

      public static void RemoveHandler(CommandManager source, EventHandler handler)
      {
        if (handler == null)
          return;
        CommandManager.RequerySuggestedEventManager.CurrentManager.ProtectedRemoveHandler((object) source, (Delegate) handler);
      }

      protected override WeakEventManager.ListenerList NewListenerList()
      {
        return new WeakEventManager.ListenerList();
      }

      protected override void StartListening(object source)
      {
        CommandManager.Current.PrivateRequerySuggested += new EventHandler(this.OnRequerySuggested);
      }

      protected override void StopListening(object source)
      {
        CommandManager.Current.PrivateRequerySuggested -= new EventHandler(this.OnRequerySuggested);
      }

      private void OnRequerySuggested(object sender, EventArgs args)
      {
        this.DeliverEvent(sender, args);
      }
    }
  }
}
