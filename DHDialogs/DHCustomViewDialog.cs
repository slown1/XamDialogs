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


		protected override UIKit.UIView ContentView 
		{
			get 
			{
				var aView = new UIDatePicker(CGRect.Empty);

				//aView.BackgroundColor = UIColor.Red;

				return aView;
			}
		}


		public DHCustomViewDialog () 
			: base(DHDialogType.CustomView)
		{
			
		}
	}
}

