using System;
using UIKit;

namespace MKInputBoxViewExample {
    
    
    public class ViewController : UIViewController {
        
        #region Methods
        public override void ViewDidLoad() {
            // 
            // {
            //     [super viewDidLoad];
            //     // Do any additional setup after loading the view, typically from a nib.
            // }
        }
        
        public override void DidReceiveMemoryWarning() {
            // 
            // {
            //     [super didReceiveMemoryWarning];
            //     // Dispose of any resources that can be recreated.
            // }
        }
        
        private void Show(Object sender) {
            // 
            // {
            //     MKInputBoxView inputBoxView = [MKInputBoxView boxOfType:LoginAndPasswordInput];
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
            // }
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
