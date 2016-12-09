// Decompiled with JetBrains decompiler
// Type: System.Windows.Input.RoutedCommand
// Assembly: PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 21C941D8-B1F8-4BF2-940D-B5B9EA76B03B
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_32\PresentationCore\v4.0_4.0.0.0__31bf3856ad364e35\PresentationCore.dll

using MS.Internal;
using System.ComponentModel;
using System.Security;
using System.Security.Permissions;
using System.Windows.Markup;

namespace System.Windows.Input
{
  /// <summary>Defines a command that implements <see cref="T:System.Windows.Input.ICommand" /> and is routed through the element tree.</summary>
  [ValueSerializer("System.Windows.Input.CommandValueSerializer, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null")]
  [TypeConverter("System.Windows.Input.CommandConverter, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null")]
  public class RoutedCommand : ICommand
  {
    private string _name;
    private SecurityCriticalDataForSet<RoutedCommand.PrivateFlags> _flags;
    private Type _ownerType;
    private InputGestureCollection _inputGestureCollection;
    private byte _commandId;

    /// <summary>Gets the name of the command. </summary>
    /// <returns>The name of the command.</returns>
    public string Name
    {
      get
      {
        return this._name;
      }
    }

    /// <summary>Gets the type that is registered with the command.</summary>
    /// <returns>The type of the command owner.</returns>
    public Type OwnerType
    {
      get
      {
        return this._ownerType;
      }
    }

    internal byte CommandId
    {
      get
      {
        return this._commandId;
      }
    }

    /// <summary>Gets the collection of <see cref="T:System.Windows.Input.InputGesture" /> objects that are associated with this command.</summary>
    /// <returns>The input gestures.</returns>
    public InputGestureCollection InputGestures
    {
      get
      {
        if (this.InputGesturesInternal == null)
          this._inputGestureCollection = new InputGestureCollection();
        return this._inputGestureCollection;
      }
    }

    internal InputGestureCollection InputGesturesInternal
    {
      get
      {
        if (this._inputGestureCollection == null && this.AreInputGesturesDelayLoaded)
        {
          this._inputGestureCollection = this.GetInputGestures();
          this.AreInputGesturesDelayLoaded = false;
        }
        return this._inputGestureCollection;
      }
    }

    internal bool IsBlockedByRM
    {
      [SecurityCritical] get
      {
        return this.ReadPrivateFlag(RoutedCommand.PrivateFlags.IsBlockedByRM);
      }
      [SecurityCritical, UIPermission(SecurityAction.LinkDemand, Unrestricted = true)] set
      {
        this.WritePrivateFlag(RoutedCommand.PrivateFlags.IsBlockedByRM, value);
      }
    }

    internal bool AreInputGesturesDelayLoaded
    {
      get
      {
        return this.ReadPrivateFlag(RoutedCommand.PrivateFlags.AreInputGesturesDelayLoaded);
      }
      [SecurityCritical, SecurityTreatAsSafe] set
      {
        this.WritePrivateFlag(RoutedCommand.PrivateFlags.AreInputGesturesDelayLoaded, value);
      }
    }

    /// <summary>Occurs when changes to the command source are detected by the command manager. These changes often affect whether the command should execute on the current command target.</summary>
    public event EventHandler CanExecuteChanged
    {
      add
      {
        CommandManager.RequerySuggested += value;
      }
      remove
      {
        CommandManager.RequerySuggested -= value;
      }
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Input.RoutedCommand" /> class.</summary>
    public RoutedCommand()
    {
      this._name = string.Empty;
      this._ownerType = (Type) null;
      this._inputGestureCollection = (InputGestureCollection) null;
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Input.RoutedCommand" /> class with the specified name and owner type.</summary>
    /// <param name="name">Declared name for serialization.</param>
    /// <param name="ownerType">The type which is registering the command.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> is null.</exception>
    /// <exception cref="T:System.ArgumentException">the length of <paramref name="name" /> is zero.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="ownerType" /> is null.</exception>
    public RoutedCommand(string name, Type ownerType)
      : this(name, ownerType, (InputGestureCollection) null)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Input.RoutedCommand" /> class with the specified name, owner type, and collection of gestures.</summary>
    /// <param name="name">Declared name for serialization.</param>
    /// <param name="ownerType">The type that is registering the command.</param>
    /// <param name="inputGestures">Default input gestures associated with this command.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> is null.</exception>
    /// <exception cref="T:System.ArgumentException">the length of <paramref name="name" /> is zero- or -<paramref name="ownerType" /> is null.</exception>
    public RoutedCommand(string name, Type ownerType, InputGestureCollection inputGestures)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (name.Length == 0)
        throw new ArgumentException(MS.Internal.PresentationCore.SR.Get("StringEmpty"), "name");
      if (ownerType == (Type) null)
        throw new ArgumentNullException("ownerType");
      this._name = name;
      this._ownerType = ownerType;
      this._inputGestureCollection = inputGestures;
    }

    internal RoutedCommand(string name, Type ownerType, byte commandId)
      : this(name, ownerType, (InputGestureCollection) null)
    {
      this._commandId = commandId;
    }

    void ICommand.Execute(object parameter)
    {
      this.Execute(parameter, RoutedCommand.FilterInputElement(Keyboard.FocusedElement));
    }

    [SecurityCritical]
    bool ICommand.CanExecute(object parameter)
    {
      bool continueRouting;
      return this.CanExecuteImpl(parameter, RoutedCommand.FilterInputElement(Keyboard.FocusedElement), false, out continueRouting);
    }

    /// <summary>Executes the <see cref="T:System.Windows.Input.RoutedCommand" /> on the current command target.</summary>
    /// <param name="parameter">User defined parameter to be passed to the handler.</param>
    /// <param name="target">Element at which to begin looking for command handlers.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="target" /> is not a <see cref="T:System.Windows.UIElement" /> or <see cref="T:System.Windows.ContentElement" />.</exception>
    [SecurityCritical]
    public void Execute(object parameter, IInputElement target)
    {
      if (target != null && !InputElement.IsValid(target))
        throw new InvalidOperationException(MS.Internal.PresentationCore.SR.Get("Invalid_IInputElement", (object) target.GetType()));
      if (target == null)
        target = RoutedCommand.FilterInputElement(Keyboard.FocusedElement);
      this.ExecuteImpl(parameter, target, false);
    }

    /// <summary>Determines whether this <see cref="T:System.Windows.Input.RoutedCommand" /> can execute in its current state.</summary>
    /// <returns>true if the command can execute on the current command target; otherwise, false.</returns>
    /// <param name="parameter">A user defined data type.</param>
    /// <param name="target">The command target.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="target" /> is not a <see cref="T:System.Windows.UIElement" /> or <see cref="T:System.Windows.ContentElement" />.</exception>
    [SecurityCritical]
    public bool CanExecute(object parameter, IInputElement target)
    {
      bool continueRouting;
      return this.CriticalCanExecute(parameter, target, false, out continueRouting);
    }

    [SecurityCritical]
    internal bool CriticalCanExecute(object parameter, IInputElement target, bool trusted, out bool continueRouting)
    {
      if (target != null && !InputElement.IsValid(target))
        throw new InvalidOperationException(MS.Internal.PresentationCore.SR.Get("Invalid_IInputElement", (object) target.GetType()));
      if (target == null)
        target = RoutedCommand.FilterInputElement(Keyboard.FocusedElement);
      return this.CanExecuteImpl(parameter, target, trusted, out continueRouting);
    }

    private InputGestureCollection GetInputGestures()
    {
      if (this.OwnerType == typeof (ApplicationCommands))
        return ApplicationCommands.LoadDefaultGestureFromResource(this._commandId);
      if (this.OwnerType == typeof (NavigationCommands))
        return NavigationCommands.LoadDefaultGestureFromResource(this._commandId);
      if (this.OwnerType == typeof (MediaCommands))
        return MediaCommands.LoadDefaultGestureFromResource(this._commandId);
      if (this.OwnerType == typeof (ComponentCommands))
        return ComponentCommands.LoadDefaultGestureFromResource(this._commandId);
      return new InputGestureCollection();
    }

    private static IInputElement FilterInputElement(IInputElement elem)
    {
      if (elem != null && InputElement.IsValid(elem))
        return elem;
      return (IInputElement) null;
    }

    [SecurityCritical]
    private bool CanExecuteImpl(object parameter, IInputElement target, bool trusted, out bool continueRouting)
    {
      if (target != null && !this.IsBlockedByRM)
      {
        CanExecuteRoutedEventArgs args = new CanExecuteRoutedEventArgs((ICommand) this, parameter);
        args.RoutedEvent = CommandManager.PreviewCanExecuteEvent;
        this.CriticalCanExecuteWrapper(parameter, target, trusted, args);
        if (!args.Handled)
        {
          args.RoutedEvent = CommandManager.CanExecuteEvent;
          this.CriticalCanExecuteWrapper(parameter, target, trusted, args);
        }
        continueRouting = args.ContinueRouting;
        return args.CanExecute;
      }
      continueRouting = false;
      return false;
    }

    [SecurityCritical]
    private void CriticalCanExecuteWrapper(object parameter, IInputElement target, bool trusted, CanExecuteRoutedEventArgs args)
    {
      DependencyObject o = (DependencyObject) target;
      if (InputElement.IsUIElement(o))
        ((UIElement) o).RaiseEvent((RoutedEventArgs) args, trusted);
      else if (InputElement.IsContentElement(o))
      {
        ((ContentElement) o).RaiseEvent((RoutedEventArgs) args, trusted);
      }
      else
      {
        if (!InputElement.IsUIElement3D(o))
          return;
        ((UIElement3D) o).RaiseEvent((RoutedEventArgs) args, trusted);
      }
    }

    [SecurityCritical]
    internal bool ExecuteCore(object parameter, IInputElement target, bool userInitiated)
    {
      if (target == null)
        target = RoutedCommand.FilterInputElement(Keyboard.FocusedElement);
      return this.ExecuteImpl(parameter, target, userInitiated);
    }

    [SecurityCritical]
    private bool ExecuteImpl(object parameter, IInputElement target, bool userInitiated)
    {
      if (target == null || this.IsBlockedByRM)
        return false;
      UIElement uiElement = target as UIElement;
      ContentElement contentElement = (ContentElement) null;
      UIElement3D uiElement3D = (UIElement3D) null;
      ExecutedRoutedEventArgs executedRoutedEventArgs = new ExecutedRoutedEventArgs((ICommand) this, parameter);
      executedRoutedEventArgs.RoutedEvent = CommandManager.PreviewExecutedEvent;
      if (uiElement != null)
      {
        uiElement.RaiseEvent((RoutedEventArgs) executedRoutedEventArgs, userInitiated);
      }
      else
      {
        contentElement = target as ContentElement;
        if (contentElement != null)
        {
          contentElement.RaiseEvent((RoutedEventArgs) executedRoutedEventArgs, userInitiated);
        }
        else
        {
          uiElement3D = target as UIElement3D;
          if (uiElement3D != null)
            uiElement3D.RaiseEvent((RoutedEventArgs) executedRoutedEventArgs, userInitiated);
        }
      }
      if (!executedRoutedEventArgs.Handled)
      {
        executedRoutedEventArgs.RoutedEvent = CommandManager.ExecutedEvent;
        if (uiElement != null)
          uiElement.RaiseEvent((RoutedEventArgs) executedRoutedEventArgs, userInitiated);
        else if (contentElement != null)
          contentElement.RaiseEvent((RoutedEventArgs) executedRoutedEventArgs, userInitiated);
        else if (uiElement3D != null)
          uiElement3D.RaiseEvent((RoutedEventArgs) executedRoutedEventArgs, userInitiated);
      }
      return executedRoutedEventArgs.Handled;
    }

    [SecurityCritical]
    private void WritePrivateFlag(RoutedCommand.PrivateFlags bit, bool value)
    {
      if (value)
        this._flags.Value |= bit;
      else
        this._flags.Value &= ~bit;
    }

    private bool ReadPrivateFlag(RoutedCommand.PrivateFlags bit)
    {
      return (uint) (this._flags.Value & bit) > 0U;
    }

    private enum PrivateFlags : byte
    {
      IsBlockedByRM = 1,
      AreInputGesturesDelayLoaded = 2,
    }
  }
}
