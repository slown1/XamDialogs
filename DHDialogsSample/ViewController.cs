using System;

using UIKit;

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

		partial void OnShowDialog (UIButton sender)
		{
			var dialog = new MKInputBoxView.MKInputBoxView(MKInputBoxView.MKInputBoxType.LoginAndPasswordInput)
			{
				Title = "Who are you?",
				Message = "Please enter your username and password to get access to the system.",
				BlurEffectStyle = UIBlurEffectStyle.ExtraLight,
				CancelButtonText = "Not yet",

			};

			//     [inputBoxView setTitle:@"Who are you?"];
			//     [inputBoxView setMessage:@"Please enter your username and password to get access to the system."];
			//     [inputBoxView setBlurEffectStyle:UIBlurEffectStyleExtraLight];
			// 
			//     [inputBoxView setCancelButtonText:@"Not yet"];
			// 
			//     inputBoxView.customise = ^(UITextField textField) {
			//         textField.placeholder = @"Your eMail address";
			//         if (textField.secureTextEntry) {
			//             textField.placeholder = @"Your password";
			//         }
			//         textField.TextColor = [UIColor blackColor];
			//         textField.layer.cornerRadius = 4.0f;
			//         return textField;
			//     };
			// 
			//     inputBoxView.onSubmit = ^(NSString value1, NSString value2) {
			//         NSLog(@"user: %@", value1);
			//         NSLog(@"pass: %@", value2);
			//     };
			// 
			//     inputBoxView.onCancel = ^{
			//         NSLog(@"Cancel!");
			//     };
			// 
			//     [inputBoxView show];

			dialog.Show();
		}
	}
}

