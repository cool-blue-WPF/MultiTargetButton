// Decompiled with JetBrains decompiler
// Type: System.Windows.Input.ICommandSource
// Assembly: PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 21C941D8-B1F8-4BF2-940D-B5B9EA76B03B
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_32\PresentationCore\v4.0_4.0.0.0__31bf3856ad364e35\PresentationCore.dll

namespace System.Windows.Input
{
  /// <summary>Defines an object that knows how to invoke a command.</summary>
  public interface ICommandSource
  {
    /// <summary>Gets the command that will be executed when the command source is invoked.</summary>
    /// <returns>The command that will be executed when the command source is invoked.</returns>
    ICommand Command { get; }

    /// <summary>Represents a user defined data value that can be passed to the command when it is executed.</summary>
    /// <returns>The command specific data.</returns>
    object CommandParameter { get; }

    /// <summary>The object that the command is being executed on.</summary>
    /// <returns>The object that the command is being executed on.</returns>
    IInputElement CommandTarget { get; }
  }
}
