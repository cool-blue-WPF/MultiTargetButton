using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MS.Internal.Controls;
using MS.Internal.Data;
using MS.Utility;

namespace ContentToggleButton.Enumerators
{
	internal abstract class ContentButtonModelTreeEnumerator : IEnumerator
	{
		internal ContentButtonModelTreeEnumerator (object content)
		{
			_content = content;
		}
 
		#region IEnumerator
 
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}
 
		bool IEnumerator.MoveNext()
		{
			return this.MoveNext();
		}
 
		void IEnumerator.Reset()
		{
			this.Reset();
		}
 
		#endregion
 
		#region Protected
 
		protected object Content
		{
			get
			{
				return _content;
			}
		}
 
		protected int Index
		{
			get
			{
				return _index;
			}
 
			set
			{
				_index = value;
			}
		}
 
		protected virtual object Current
		{
			get
			{
				// Don't VerifyUnchanged(); According to MSDN:
				//     If the collection is modified between MoveNext and Current,
				//     Current will return the element that it is set to, even if
				//     the enumerator is already invalidated.
 
				if (_index == 0)
				{
					return _content;
				}
 
				throw new InvalidOperationException("EnumeratorInvalidOperation");
			}
		}
 
		protected virtual bool MoveNext()
		{
			if (_index < 1)
			{
				// Singular content, can move next to 0 and that's it.
				_index++;
 
				if (_index == 0)
				{
					// don't call VerifyUnchanged if we're returning false anyway.
					// This permits users to change the Content after enumerating
					// the content (e.g. in the invalidation callback of an inherited
					// property).  See 
 
					VerifyUnchanged();
					return true;
				}
			}
 
			return false;
		}
 
		protected virtual void Reset()
		{
			VerifyUnchanged();
			_index = -1;
		}
 
		protected abstract bool IsUnchanged
		{
			get;
		}
 
		protected void VerifyUnchanged()
		{
			// If the content has changed, then throw an exception
			if (!IsUnchanged)
			{
				throw new InvalidOperationException("EnumeratorVersionChanged");
			}
		}
 
		#endregion
 
		#region Data
 
		private int _index = -1;
		private object _content;
 
		#endregion
	}
}
//---------------------------------------------------------------------------
//
// <copyright file="ModelTreeEnumerator" company="Microsoft">
//    Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
// 
//---------------------------------------------------------------------------
 
