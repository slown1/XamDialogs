using System;
using UIKit;
using DHDialogs;
using Foundation;
using System.Collections.Generic;

namespace DHDialogsSample
{
	public partial class ViewController : UIViewController
	{
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			this.View.BackgroundColor = UIColor.Gray;

		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		async partial void OnShowDialog (UIButton sender)
		{
//			var dialog = new DHDatePickerDialog(UIDatePickerMode.DateAndTime)
//			{
//				Title = "Who are you?",
//				Message = "Please enter your username and password to get access to the system.",
//				BlurEffectStyle = UIBlurEffectStyle.ExtraLight,
//				CancelButtonText = "Not yet",
//				ConstantUpdates = false,
//			};
//				
//			dialog.SelectedDate = new DateTime(1978,6,30,7,30,00,00);
//
//			dialog.ButtonMode = ButtonMode.Ok;
//
//			dialog.ValidateSubmit = (DateTime data)=>
//			{
//				return true;
//			};
//
//			dialog.OnSelectedDateChanged += (object s, DateTime e) => 
//			{
//				Console.WriteLine(e);
//			};
//
//			dialog.Show();

			var dialog = new DHSimplePickerDialog(new List<String>(){"Dave","Rob","Jamie"})
			{
				Title = "Who are you?",
				Message = "Please enter your username and password to get access to the system.",
				BlurEffectStyle = UIBlurEffectStyle.ExtraLight,
				CancelButtonText = "Not yet",
				ConstantUpdates = false,
			};

			dialog.OnSelectedItemChanged += (object s, string e) => 
			{
				Console.WriteLine(e);
			};

			dialog.Show();

//			var result = await DHDatePickerDialog.ShowDialog(UIDatePickerMode.DateAndTime,"Date of Birth","Select your Date of Birth", new DateTime(1978,6,30,7,30,00,00) );
//
//			Console.WriteLine(result);

		}
	}
}

