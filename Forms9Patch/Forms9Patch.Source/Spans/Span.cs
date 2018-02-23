﻿using System.ComponentModel;
using P42.Utils;
using System;

namespace Forms9Patch
{
	/// <summary>
	/// Forms9Patch FormattedString Span
	/// </summary>
	abstract class Span : INotifyPropertyChanged, ICopiable<Span>
	{
		internal string Key;

		int _start=-1;
		/// <summary>
		/// Gets or sets the span's start.
		/// </summary>
		/// <value>The start.</value>
		public int Start {
			get { return _start; }
			set { 
				if (_start == value)
					return;
				_start = value;
				OnPropertyChanged ("Start");
			}
		}

		// use int.MaxValue to indicate that the span is unterminated (goes to the end of the string)
		int _end=-1;
		/// <summary>
		/// Gets or sets the span's end.
		/// </summary>
		/// <value>The end.</value>
		public int End {
			get { return _end; }
			set {
				if (_end == value)
					return;
				_end = value;
				OnPropertyChanged ("End");
			}
		}

		/// <summary>
		/// Gets or sets the length.
		/// </summary>
		/// <value>The length.  int.MaxValue to indicate that the span is unterminated (goes to the end of the string)</value>
		public int Length {
			get { 
				if (_end == int.MaxValue) 
					return int.MaxValue;
			
				return _end - _start + 1; }
			set {
				if ((_end - _start + 1) == value)
					return;
				if (value == int.MaxValue) 
					_end = int.MaxValue;
				else				
					_end = _start + value - 1;
				OnPropertyChanged ("End");
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Forms9Patch.Span"/> class.
		/// </summary>
		/// <param name="start">Start.</param>
		/// <param name="end">End.</param>
		protected Span(int start, int end) {
			_start = start;
			_end = end;
		}

		#region INotifyPropertyChanged implementation

		/// <summary>
		/// Occurs when property changed.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		internal void OnPropertyChanged (string propertyName = null) {
			PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (propertyName));
		}

		#endregion


		#region
		public void PropertiesFrom(Span source)
		{
			Key = source.Key;
			Start = source.Start;
			End = source.End;
		}

		public virtual Span Copy()
		{
			throw new NotImplementedException();
		}
		#endregion
	}

}
