// Decompiled with JetBrains decompiler
// Type: System.Windows.Input.CommandBinding
// Assembly: PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 21C941D8-B1F8-4BF2-940D-B5B9EA76B03B
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_32\PresentationCore\v4.0_4.0.0.0__31bf3856ad364e35\PresentationCore.dll

namespace System.Windows.Input
{
  /// <summary>Binds a <see cref="T:System.Windows.Input.RoutedCommand" /> to the event handlers that implement the command. </summary>
  public class CommandBinding
  {
    private ICommand _command;

    /// <summary>Gets or sets the <see cref="T:System.Windows.Input.ICommand" /> associated with this <see cref="T:System.Windows.Input.CommandBinding" />. </summary>
    /// <returns>The command associated with this binding.</returns>
    [Localizability(LocalizationCategory.NeverLocalize)]
    public ICommand Command
    {
      get
      {
        return this._command;
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("value");
        this._command = value;
      }
    }

    /// <summary>Occurs when the command associated with this <see cref="T:System.Windows.Input.CommandBinding" /> executes.</summary>
    public event ExecutedRoutedEventHandler PreviewExecuted;

    /// <summary>Occurs when the command associated with this <see cref="T:System.Windows.Input.CommandBinding" /> executes.</summary>
    public event ExecutedRoutedEventHandler Executed;

    /// <summary>Occurs when the command associated with this <see cref="T:System.Windows.Input.CommandBinding" /> initiates a check to determine whether the command can be executed on the current command target.</summary>
    public event CanExecuteRoutedEventHandler PreviewCanExecute;

    /// <summary>Occurs when the command associated with this <see cref="T:System.Windows.Input.CommandBinding" /> initiates a check to determine whether the command can be executed on the command target.</summary>
    public event CanExecuteRoutedEventHandler CanExecute;

    /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Input.CommandBinding" /> class.</summary>
    public CommandBinding()
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Input.CommandBinding" /> class by using the specified <see cref="T:System.Windows.Input.ICommand" />.</summary>
    /// <param name="command">The command to base the new <see cref="T:System.Windows.Input.RoutedCommand" /> on.</param>
    public CommandBinding(ICommand command)
      : this(command, (ExecutedRoutedEventHandler) null, (CanExecuteRoutedEventHandler) null)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Input.CommandBinding" /> class by using the specified <see cref="T:System.Windows.Input.ICommand" /> and the specified <see cref="E:System.Windows.Input.CommandBinding.Executed" /> event handler.</summary>
    /// <param name="command">The command to base the new <see cref="T:System.Windows.Input.RoutedCommand" /> on.</param>
    /// <param name="executed">The handler for the <see cref="E:System.Windows.Input.CommandBinding.Executed" /> event on the new <see cref="T:System.Windows.Input.RoutedCommand" />.</param>
    public CommandBinding(ICommand command, ExecutedRoutedEventHandler executed)
      : this(command, executed, (CanExecuteRoutedEventHandler) null)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Input.CommandBinding" /> class by using the specified <see cref="T:System.Windows.Input.ICommand" /> and the specified <see cref="E:System.Windows.Input.CommandBinding.Executed" /> and <see cref="E:System.Windows.Input.CommandBinding.CanExecute" /> event handlers.</summary>
    /// <param name="command">The command to base the new <see cref="T:System.Windows.Input.RoutedCommand" /> on.</param>
    /// <param name="executed">The handler for the <see cref="E:System.Windows.Input.CommandBinding.Executed" /> event on the new <see cref="T:System.Windows.Input.RoutedCommand" />.</param>
    /// <param name="canExecute">The handler for the <see cref="E:System.Windows.Input.CommandBinding.CanExecute" /> event on the new <see cref="T:System.Windows.Input.RoutedCommand" />.</param>
    public CommandBinding(ICommand command, ExecutedRoutedEventHandler executed, CanExecuteRoutedEventHandler canExecute)
    {
      if (command == null)
        throw new ArgumentNullException("command");
      this._command = command;
      if (executed != null)
        this.Executed += executed;
      if (canExecute == null)
        return;
      this.CanExecute += canExecute;
    }

    internal void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
      if (e.Handled)
        return;
      if (e.RoutedEvent == CommandManager.CanExecuteEvent)
      {
        // ISSUE: reference to a compiler-generated field
        if (this.CanExecute != null)
        {
          // ISSUE: reference to a compiler-generated field
          this.CanExecute(sender, e);
          if (!e.CanExecute)
            return;
          e.Handled = true;
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          if (e.CanExecute || this.Executed == null)
            return;
          e.CanExecute = true;
          e.Handled = true;
        }
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        if (this.PreviewCanExecute == null)
          return;
        // ISSUE: reference to a compiler-generated field
        this.PreviewCanExecute(sender, e);
        if (!e.CanExecute)
          return;
        e.Handled = true;
      }
    }

    private bool CheckCanExecute(object sender, ExecutedRoutedEventArgs e)
    {
      CanExecuteRoutedEventArgs e1 = new CanExecuteRoutedEventArgs(e.Command, e.Parameter);
      e1.RoutedEvent = CommandManager.CanExecuteEvent;
      e1.Source = e.OriginalSource;
      e1.OverrideSource(e.Source);
      this.OnCanExecute(sender, e1);
      return e1.CanExecute;
    }

    internal void OnExecuted(object sender, ExecutedRoutedEventArgs e)
    {
      if (e.Handled)
        return;
      if (e.RoutedEvent == CommandManager.ExecutedEvent)
      {
        // ISSUE: reference to a compiler-generated field
        if (this.Executed == null || !this.CheckCanExecute(sender, e))
          return;
        // ISSUE: reference to a compiler-generated field
        this.Executed(sender, e);
        e.Handled = true;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        if (this.PreviewExecuted == null || !this.CheckCanExecute(sender, e))
          return;
        // ISSUE: reference to a compiler-generated field
        this.PreviewExecuted(sender, e);
        e.Handled = true;
      }
    }
  }
}
