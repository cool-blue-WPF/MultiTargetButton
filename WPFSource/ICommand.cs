﻿// Decompiled with JetBrains decompiler
// Type: System.Windows.Input.ICommand
// Assembly: System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 9D40932B-6A38-40D3-886E-FCBAAFE5FD7C
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.dll

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Markup;

namespace System.Windows.Input
{
  /// <summary>Defines a command.</summary>
  [TypeForwardedFrom("PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
  [TypeConverter("System.Windows.Input.CommandConverter, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null")]
  [ValueSerializer("System.Windows.Input.CommandValueSerializer, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null")]
  [__DynamicallyInvokable]
  public interface ICommand
  {
    /// <summary>Occurs when changes occur that affect whether or not the command should execute.</summary>
    [__DynamicallyInvokable]
    event EventHandler CanExecuteChanged;

    /// <summary>Defines the method that determines whether the command can execute in its current state.</summary>
    /// <returns>true if this command can be executed; otherwise, false.</returns>
    /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
    [__DynamicallyInvokable]
    bool CanExecute(object parameter);

    /// <summary>Defines the method to be called when the command is invoked.</summary>
    /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
    [__DynamicallyInvokable]
    void Execute(object parameter);
  }
}