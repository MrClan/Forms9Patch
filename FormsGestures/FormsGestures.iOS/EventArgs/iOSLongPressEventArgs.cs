using System;
using UIKit;

namespace FormsGestures.iOS
{
	public class iOSLongPressEventArgs : LongPressEventArgs
	{
		public iOSLongPressEventArgs(UILongPressGestureRecognizer gr, long duration, CoreGraphics.CGPoint locationAtStart)
		{
			Cancelled = (gr.State == UIGestureRecognizerState.Cancelled || gr.State == UIGestureRecognizerState.Failed);
			ViewPosition = gr.View.BoundsInDipCoord();
			Touches = iOSEventArgsHelper.GetTouches(gr, locationAtStart);
			Duration = duration;
		}
	}
}
