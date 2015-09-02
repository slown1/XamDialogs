using System;
using UIKit;
using Foundation;
using CoreGraphics;

namespace MKInputBoxView {
    
    
    public class MKInputBoxView : UIView {
        
        #region Fields
		        
        private String _Title;
        
        private String _Message;
        
        private String _SubmitButtonText;
        
        private String _CancelButtonText;
        
        private MKInputBoxType _BoxType;
        
        private nint _NumberOfDecimals;
        
		private UIBlurEffectStyle _BlurEffectStyle = UIBlurEffectStyle.Light;
        
        private NSMutableArray _Elements;
        
        private UIVisualEffectView _VisualEffectView;
        
        private UITextField _TextInput;
        
        private UITextField _SecureInput;
        
        private UIView _ActualBox;

		private NSObject mKeyboardDidSHowNotification;
		private NSObject mKeyboardDidHideNotification;

        #endregion

		#region Properties


		private String Title {
			get {
				return this._Title;
			}
			set {
				this._Title = value;
			}
		}

		private String Message {
			get {
				return this._Message;
			}
			set {
				this._Message = value;
			}
		}

		private String SubmitButtonText {
			get {
				return this._SubmitButtonText;
			}
			set {
				this._SubmitButtonText = value;
			}
		}

		private String CancelButtonText {
			get {
				return this._CancelButtonText;
			}
			set {
				this._CancelButtonText = value;
			}
		}

		private MKInputBoxType BoxType {
			get {
				return this._BoxType;
			}
			set {
				this._BoxType = value;
			}
		}

		private nint NumberOfDecimals {
			get {
				return this._NumberOfDecimals;
			}
			set {
				this._NumberOfDecimals = value;
			}
		}

		private UIBlurEffectStyle BlurEffectStyle {
			get {
				return this._BlurEffectStyle;
			}
			set {
				this._BlurEffectStyle = value;
			}
		}

		private NSMutableArray Elements {
			get {
				return this._Elements;
			}
			set {
				this._Elements = value;
			}
		}

		private UIVisualEffectView VisualEffectView {
			get {
				return this._VisualEffectView;
			}
			set {
				this._VisualEffectView = value;
			}
		}

		private UITextField TextInput {
			get {
				return this._TextInput;
			}
			set {
				this._TextInput = value;
			}
		}

		private UITextField SecureInput {
			get {
				return this._SecureInput;
			}
			set {
				this._SecureInput = value;
			}
		}

		private UIView ActualBox {
			get {
				return this._ActualBox;
			}
			set {
				this._ActualBox = value;
			}
		}

		/// <summary>
		/// Gets or sets the on cancel.
		/// </summary>
		/// <value>The on cancel.</value>
		public Action OnCancel
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the on submit.
		/// </summary>
		/// <value>The on submit.</value>
		public Func<object[], bool> OnSubmit
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the customise.
		/// </summary>
		/// <value>The customise.</value>
		public Func<UITextField, UITextField> Customise
		{
			get;
			set;
		}

		#endregion
        
        #region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="MKInputBoxView.MKInputBoxView"/> class.
		/// </summary>
		public MKInputBoxView() 
			: this(MKInputBoxType.PlainTextInput) {

        }
        
		/// <summary>
		/// Initializes a new instance of the <see cref="MKInputBoxView.MKInputBoxView"/> class.
		/// </summary>
		/// <param name="boxType">Box type.</param>
		public MKInputBoxView(MKInputBoxType boxType) 
			: base()
         {
             var actualBoxHeight = 155.0f;
             var window        = UIApplication.SharedApplication.Windows[0];
             var allFrame         = window.Frame;
         
			var boxFrame         = new CGRect(0,0,Math.Min(325, window.Frame.Size.Width - 50),actualBoxHeight);
         
			this.Frame = allFrame;

			this.BoxType            = boxType;
			this.BackgroundColor    = UIColor.FromWhiteAlpha(1.0f,0.0f);
			this.AutoresizingMask   = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleHeight;
			this.ActualBox = new UIView(boxFrame);

			this.ActualBox.Center   = new CGPoint(window.Center.X, window.Center.Y);

			this.Center             = new CGPoint(window.Center.X, window.Center.Y);
				
		   this.Add(this.ActualBox);

         }
        
        #endregion
		        
        #region Methods

		/// <summary>
		/// Show this instance.
		/// </summary>
        private void Show() {

             this.Alpha = 0.0f;

			SetupView();

			UIView.Animate(0.3f,()=>
			{
				this.Alpha = 1.0f;
			});
         
            var window = UIApplication.SharedApplication.Windows[0];
			window.Add(this);
			window.BringSubviewToFront(this);

			UIDevice.CurrentDevice.BeginGeneratingDeviceOrientationNotifications();

			         
			NSNotificationCenter.DefaultCenter.AddObserver (new NSString("UIDeviceOrientationDidChangeNotification"), DeviceOrientationDidChange);

			mKeyboardDidSHowNotification = UIKeyboard.Notifications.ObserveDidShow((s,e)=>
			{
				KeyboardDidShow(e.Notification);
			});


			mKeyboardDidHideNotification = UIKeyboard.Notifications.ObserveDidHide((s,e)=>
			{
				KeyboardDidHide(e.Notification);

			});
				
        }
        
		/// <summary>
		/// Hide this instance.
		/// </summary>
        private void Hide() 
		{
			UIView.AnimateNotify(0.3f, ()=>
			{
				this.Alpha = 0.0f;
			}, (finished) => 
			{
				this.RemoveFromSuperview();

				UIDevice.CurrentDevice.EndGeneratingDeviceOrientationNotifications();

				NSNotificationCenter.DefaultCenter.RemoveObserver(new NSString("UIDeviceOrientationDidChangeNotification"));

				mKeyboardDidSHowNotification.Dispose();
				mKeyboardDidHideNotification.Dispose();
			});

        }
        
        private void SetupView() 
		{
            // 
			this.Elements = new NSMutableArray();

            // 
             this.ActualBox.Layer.CornerRadius   = 4.0f;
             this.ActualBox.Layer.MasksToBounds  = true;

            // 
             UIColor titleLabelTextColor     = null;
             UIColor messageLabelTextColor   = null;
             UIColor elementBackgroundColor  = null;
             UIColor buttonBackgroundColor   = null;

            // 
			var style = this.BlurEffectStyle;

            // 
             switch (style) {
                 case UIBlurEffectStyle.Dark:
					{
						titleLabelTextColor     = UIColor.White;
						messageLabelTextColor   = UIColor.White;
						elementBackgroundColor  = UIColor.FromWhiteAlpha(1.0f,0.07f);
						buttonBackgroundColor   = UIColor.FromWhiteAlpha(1.0f,0.07f);
					}
                     break;
                 default:
					{
						titleLabelTextColor     = UIColor.Black;
						messageLabelTextColor   = UIColor.Black;
						elementBackgroundColor  = UIColor.FromWhiteAlpha(1.0f,0.50f);
						buttonBackgroundColor   = UIColor.FromWhiteAlpha(1.0f,0.2f);
					}

                     break;
             }
            

			this.VisualEffectView = new UIVisualEffectView(UIBlurEffect.FromStyle(style));

             var padding  = 10.0f;
             var width    = this.ActualBox.Frame.Size.Width - padding * 2;


		     UILabel titleLabel     = new UILabel( new CGRect(padding, padding, width, 20));

			titleLabel.Font         = UIFont.BoldSystemFontOfSize(17.0f);
             titleLabel.Text         = this.Title;
             titleLabel.TextAlignment= UITextAlignment.Center;
             titleLabel.TextColor    = titleLabelTextColor;

			this.VisualEffectView.ContentView.Add(titleLabel);

		     var messageLabel   = new UILabel( new CGRect(padding, padding + titleLabel.Frame.Size.Height + 5, width, 31.5));
             messageLabel.Lines  = 2;
		     messageLabel.Font       = UIFont.SystemFontOfSize(13.0f);
             messageLabel.Text       = this.Message;
		     messageLabel.TextAlignment  = UITextAlignment.Center;
             messageLabel.TextColor  = messageLabelTextColor;

			 messageLabel.SizeToFit();

			this.VisualEffectView.ContentView.Add(messageLabel);

			switch (this.BoxType) 
			{
				case MKInputBoxType.PlainTextInput:
					{
						this.TextInput = new UITextField(new CGRect(padding, messageLabel.Frame.Location.Y + messageLabel.Frame.Size.Height + padding / 1.5, width, 30));
						this.TextInput.TextAlignment = UITextAlignment.Center;
						if (this.Customise != null) {
							this.TextInput = this.Customise(this.TextInput);
						}
						this.Elements.Add(this.TextInput);

						this.TextInput.AutocorrectionType = UITextAutocorrectionType.No;
					}
					break;
				case MKInputBoxType.NumberInput:
					{
						this.TextInput = new UITextField(new CGRect(padding, messageLabel.Frame.Location.Y + messageLabel.Frame.Size.Height + padding / 1.5, width, 30));

						if (this.Customise != null) {
							this.TextInput = this.Customise(this.TextInput);
						}
						this.Elements.Add(this.TextInput);


						this.TextInput.KeyboardType = UIKeyboardType.NumberPad;

						this.TextInput.EditingChanged += (object sender, EventArgs e) => 
						{
							TextInputDidChange();
						};
							
					}
					break;
				case MKInputBoxType.EmailInput:
					{
						this.TextInput = new UITextField(new CGRect(padding, messageLabel.Frame.Location.Y + messageLabel.Frame.Size.Height + padding / 1.5, width, 30));
						this.TextInput.TextAlignment = UITextAlignment.Center;

						if (this.Customise != null) {
							this.TextInput = this.Customise(this.TextInput);
						}

						this.Elements.Add(this.TextInput);

						this.TextInput.KeyboardType = UIKeyboardType.EmailAddress;
						this.TextInput.AutocorrectionType = UITextAutocorrectionType.No;
					
					}
					break;
				case MKInputBoxType.SecureTextInput:
					{

					}
					break;
				case MKInputBoxType.PhoneNumberInput:
					{

					}
					break;
				case MKInputBoxType.LoginAndPasswordInput:
					{

					}
					break;
				default:
					{
						throw new Exception("You must specify a box type");
					}
			}

            // 
            //     switch (this.boxType) {
            // 
            //         case PlainTextInput:

            // 
            // 
            //         case NumberInput:

            //             break;
            // 
            // 
            //         case EmailInput:

            //             break;
            // 
            // 
            //         case SecureTextInput:
            //             this.TextInput = [[UITextField alloc] initWithFrame:
            //                               new CGRect(padding, messageLabel.Frame.Location.Y + messageLabel.Frame.Size.Height + padding / 1.5, width, 30)];
            //             this.TextInput.TextAlignment = NSTextAlignmentCenter;
            //             if (this.customise) {
            //                 this.TextInput = this.customise(this.TextInput);
            //             }
            //             [this.elements addObject:this.TextInput];
            //             break;
            // 
            // 
            //         case PhoneNumberInput:
            //             this.TextInput = [[UITextField alloc] initWithFrame:
            //                               new CGRect(padding, messageLabel.Frame.Location.Y + messageLabel.Frame.Size.Height + padding / 1.5, width, 30)];
            //             this.TextInput.TextAlignment = NSTextAlignmentCenter;
            //             if (this.customise) {
            //                 this.TextInput = this.customise(this.TextInput);
            //             }
            //             [this.elements addObject:this.TextInput];
            //             this.TextInput.keyboardType = UIKeyboardTypePhonePad;
            //             break;
            // 
            // 
            //         case LoginAndPasswordInput:
            // 
            //             this.TextInput = [[UITextField alloc] initWithFrame:
            //                               new CGRect(padding, messageLabel.Frame.Location.Y + messageLabel.Frame.Size.Height + padding, width, 30)];
            //             this.TextInput.TextAlignment = NSTextAlignmentCenter;
            // 
            //             if (this.customise) {
            //                 this.TextInput = this.customise(this.TextInput);
            //             }
            //             this.TextInput.autocorrectionType = UITextAutocorrectionTypeNo;
            //             [this.elements addObject:this.TextInput];
            // 
            //             this.secureInput = [[UITextField alloc] initWithFrame:
            //                                 new CGRect(padding, this.TextInput.Frame.Location.Y + this.TextInput.Frame.Size.Height + padding, width, 30)];
            //             this.secureInput.TextAlignment = NSTextAlignmentCenter;
            //             this.secureInput.secureTextEntry = true;
            // 
            //             if (this.customise) {
            //                 this.secureInput = this.customise(this.secureInput);
            //             }
            // 
            //             [this.elements addObject:this.secureInput];
            // 
            //             // adjust height!
            //             CGRect extendedFrame = this.actualBox.Frame;
            //             extendedFrame.Size.Height += 45;
            //             this.actualBox.Frame = extendedFrame;
            //             break;
            // 
            //         default:
            //             NSAssert(NO, @"Boom! You should set a proper MKInputStyle! Bailing out...");
            //             break;
            //     }
            // 
            //     for (UITextField element in this.elements) {
            //         element.layer.borderColor   = [UIColor colorWithWhite:0.0f alpha:0.1f].CGColor;
            //         element.layer.borderWidth   = 0.5;
            //         element.BackgroundColor     = elementBackgroundColor;
            //         [this.visualEffectView.contentView addSubview:element];
            //     }
            // 
            //     CGFloat buttonHeight    = 40.0f;
            //     CGFloat buttonWidth     = this.actualBox.Frame.Size.Width / 2;
            // 
            //     UIButton cancelButton  = [[UIButton alloc] initWithFrame:new CGRect(0, this.actualBox.Frame.Size.Height - buttonHeight, buttonWidth, buttonHeight)];
            //     [cancelButton setTitle:this.cancelButtonText != null ? this.cancelButtonText : @"Cancel" forState:UIControlStateNormal];
            //     [cancelButton addTarget:this action:@selector(cancelButtonTapped) forControlEvents:UIControlEventTouchUpInside];
            //     cancelButton.TitleLabel.Font = [UIFont systemFontOfSize:16.0f];
            //     [cancelButton setTitleColor:titleLabelTextColor forState: UIControlStateNormal];
            //     [cancelButton setTitleColor:[UIColor grayColor] forState: UIControlStateHighlighted];
            //     cancelButton.BackgroundColor = buttonBackgroundColor;
            //     cancelButton.layer.borderColor = [UIColor colorWithWhite: 0.0f alpha: 0.1f].CGColor;
            //     cancelButton.layer.borderWidth = 0.5;
            //     [this.visualEffectView.contentView addSubview:cancelButton];
            // 
            //     UIButton submitButton = [[UIButton alloc] initWithFrame:new CGRect(buttonWidth, this.actualBox.Frame.Size.Height - buttonHeight, buttonWidth, buttonHeight)];
            //     [submitButton setTitle:this.submitButtonText != null ? this.submitButtonText : @"OK" forState:UIControlStateNormal];
            //     [submitButton addTarget:this action:@selector(submitButtonTapped) forControlEvents: UIControlEventTouchUpInside];
            //     submitButton.TitleLabel.Font = [UIFont systemFontOfSize:16];
            //     [submitButton setTitleColor:this.blurEffectStyle == UIBlurEffectStyleDark ? [UIColor whiteColor] : [UIColor blackColor] forState: UIControlStateNormal];
            //     [submitButton setTitleColor:[UIColor grayColor] forState:UIControlStateHighlighted];
            //     submitButton.BackgroundColor = buttonBackgroundColor;
            //     submitButton.layer.borderColor = [UIColor colorWithWhite:0.0f alpha: 0.1f].CGColor;
            //     submitButton.layer.borderWidth = 0.5;
            //     [this.visualEffectView.contentView addSubview:submitButton];
            // 
            //     this.visualEffectView.Frame = new CGRect(0, 0, this.actualBox.Frame.Size.Width, this.actualBox.Frame.Size.Height + 45);
            //     [this.actualBox addSubview:this.visualEffectView];

            this.ActualBox.Center = this.Center;
        }
        

        /// <summary>
        /// Determines whether this instance cancel button tapped.
        /// </summary>
        /// <returns><c>true</c> if this instance cancel button tapped; otherwise, <c>false</c>.</returns>
        private void CancelButtonTapped() 
		{
			if (this.OnCancel != null) {
				this.OnCancel();
			}

			this.Hide();
        }
        
		/// <summary>
		/// Submits the button tapped.
		/// </summary>
        private void SubmitButtonTapped() 
		{
             
            if (this.OnSubmit != null) 
			{
               var textValue = this.TextInput.Text;
               var passValue = this.SecureInput.Text;

				if (this.OnSubmit(new object[]{textValue,passValue}))
				{
					Hide();
                }
            } else 
			{
				Hide();
            }

        }
        
		/// <summary>
		/// Texts the input did change.
		/// </summary>
        private void TextInputDidChange()
		{
			var sText = this.TextInput.Text;
			sText = sText.Replace(@".",""); 
			var power = Math.Pow(10.0f, (double)this.NumberOfDecimals);
			var number = Convert.ToDouble(sText) / power;

			this.TextInput.Text = DisplayNDecimal(number, (int)this.NumberOfDecimals);
        }
        
		/// <summary>
		/// Devices the orientation did change.
		/// </summary>
		/// <param name="notification">Notification.</param>
		private void DeviceOrientationDidChange(NSNotification notification) 
		{
			ResetFrame(true);

		}

		/// <summary>
		/// Keyboards the did show.
		/// </summary>
		/// <param name="notification">Notification.</param>
        private void KeyboardDidShow(NSNotification notification) 
		{
			ResetFrame(true);

			UIView.Animate(0.2f,()=>
			{
				CGRect frame = this.ActualBox.Frame;
				frame.Y -= YCorrection();
				this.ActualBox.Frame = frame;

			});
				
        }
        
		/// <summary>
		/// Keyboards the did hide.
		/// </summary>
		/// <param name="notification">Notification.</param>
        private void KeyboardDidHide(NSNotification notification) 
		{
			ResetFrame(true);

        }
        
		/// <summary>
		/// Ys the correction.
		/// </summary>
		/// <returns>The correction.</returns>
        private nfloat YCorrection() 
		{
			var yCorrection = 115.0f;
		

			if (UIKit.UIDeviceOrientationExtensions.IsLandscape (UIDevice.CurrentDevice.Orientation)) 
			{
				if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone) {
					yCorrection = 80.0f;
				}
				else if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad) {
					yCorrection = 100.0f;
				}

				if (this.BoxType == MKInputBoxType.LoginAndPasswordInput) {
					yCorrection += 45.0f;
				}
			}
			else {
				if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad) {
					yCorrection = 0.0f;
				}
			}
			return yCorrection;

        }
        
		/// <summary>
		/// Resets the frame.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
        private void ResetFrame(Boolean animated) 
		{
            
			var window = UIApplication.SharedApplication.Windows[0];

			this.Frame = window.Frame;

			// 
		     if (animated) 
			{
				UIView.Animate(0.3f, ()=>
				{
					this.Center = new CGPoint(window.Center.X, window.Center.Y);
					this.ActualBox.Center = this.Center;

				});

		     }
		     else {
				this.Center = new CGPoint(window.Center.X, window.Center.Y);
				this.ActualBox.Center = this.Center;
		     }

        }

		/// <summary>
		/// Displaies the N decimal.
		/// </summary>
		/// <returns>The N decimal.</returns>
		/// <param name="dbValue">Db value.</param>
		/// <param name="nDecimal">N decimal.</param>
		private string DisplayNDecimal(double dbValue, int nDecimal)
		{
			string decimalPoints = "0";
			if (nDecimal > 0)
			{
				decimalPoints += ".";
				for (int i = 0; i < nDecimal; i++)
					decimalPoints += "0";
			}
			return dbValue.ToString(decimalPoints);
		}
        #endregion
        
        #region Static Methods

		/// <summary>
		/// Boxs the type of the of.
		/// </summary>
		/// <returns>The of type.</returns>
		/// <param name="boxType">Box type.</param>
		public static MKInputBoxView BoxOfType(MKInputBoxType boxType) {
			return new MKInputBoxView(boxType);
        }
        #endregion
    }
}
