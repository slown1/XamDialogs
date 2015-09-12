using System;
using UIKit;
using CoreGraphics;

namespace DHDialogs
{
	/// <summary>
	/// DHDialog with customView
	/// </summary>
	public class DHCustomViewDialog : DHDialogView
	{


		#region implemented abstract members of DHDialogView

		protected override bool CanSubmit ()
		{
			throw new NotImplementedException ();
		}

		protected override void HandleCancel ()
		{
			throw new NotImplementedException ();
		}

		protected override void HandleSubmit ()
		{
			throw new NotImplementedException ();
		}

		#endregion

		protected override UIKit.UIView ContentView 
		{
			get 
			{
				var aView = new UIView (new CGRect (0, 0, 320, 240));

				aView.BackgroundColor = UIColor.Red;

				return aView;
			}
		}


		public DHCustomViewDialog () 
			: base(DHDialogType.CustomView)
		{
			
		}
	}
}

