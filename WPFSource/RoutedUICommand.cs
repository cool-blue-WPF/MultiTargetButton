// Decompiled with JetBrains decompiler
// Type: System.Windows.Input.RoutedUICommand
// Assembly: PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 21C941D8-B1F8-4BF2-940D-B5B9EA76B03B
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_32\PresentationCore\v4.0_4.0.0.0__31bf3856ad364e35\PresentationCore.dll

using System.ComponentModel;

namespace System.Windows.Input
{
  /// <summary>Defines an <see cref="T:System.Windows.Input.ICommand" /> that is routed through the element tree and contains a text property.</summary>
  [TypeConverter("System.Windows.Input.CommandConverter, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null")]
  public class RoutedUICommand : RoutedCommand
  {
    private string _text;

    /// <summary>Gets or sets the text that describes this command.</summary>
    /// <returns>The text that describes the command.  The default is an empty string.</returns>
    public string Text
    {
      get
      {
        if (this._text == null)
          this._text = this.GetText();
        return this._text;
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("value");
        this._text = value;
      }
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Input.RoutedUICommand" /> class.</summary>
    public RoutedUICommand()
    {
      this._text = string.Empty;
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Input.RoutedUICommand" /> class, using the specified descriptive text, declared name, and owner type.</summary>
    /// <param name="text">Descriptive text for the command.</param>
    /// <param name="name">The declared name of the command for serialization.</param>
    /// <param name="ownerType">The type that is registering the command.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="text" /> is null.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> is null.</exception>
    /// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="ownerType" /> is null.</exception>
    public RoutedUICommand(string text, string name, Type ownerType)
      : this(text, name, ownerType, (InputGestureCollection) null)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Input.RoutedUICommand" /> class, using the specified descriptive text, declared name, owner type, and input gestures.</summary>
    /// <param name="text">Descriptive text for the command.</param>
    /// <param name="name">The declared name of the command for serialization.</param>
    /// <param name="ownerType">The type that is registering the command.</param>
    /// <param name="inputGestures">A collection of gestures to associate with the command.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="text" /> is null.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> is null.</exception>
    /// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="ownerType" /> is null.</exception>
    public RoutedUICommand(string text, string name, Type ownerType, InputGestureCollection inputGestures)
      : base(name, ownerType, inputGestures)
    {
      if (text == null)
        throw new ArgumentNullException("text");
      this._text = text;
    }

    internal RoutedUICommand(string name, Type ownerType, byte commandId)
      : base(name, ownerType, commandId)
    {
    }

    private string GetText()
    {
      if (this.OwnerType == typeof (ApplicationCommands))
        return ApplicationCommands.GetUIText(this.CommandId);
      if (this.OwnerType == typeof (NavigationCommands))
        return NavigationCommands.GetUIText(this.CommandId);
      if (this.OwnerType == typeof (MediaCommands))
        return MediaCommands.GetUIText(this.CommandId);
      if (this.OwnerType == typeof (ComponentCommands))
        return ComponentCommands.GetUIText(this.CommandId);
      return (string) null;
    }
  }
}
