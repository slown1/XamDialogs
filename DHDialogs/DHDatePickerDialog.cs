using System;
using UIKit;
using CoreGraphics;
using Foundation;

namespace DHDialogs
{
	public class DHDatePickerDialog : DHDialogView
	{

		#region Fields

		private UIDatePicker mDatePicker;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the content view.
		/// </summary>
		/// <value>The content view.</value>
		protected override UIView ContentView {
			get 
			{
				return mDatePicker;
			}
		}
			
		/// <summary>
		/// Gets or sets the selected date.
		/// </summary>
		/// <value>The selected date.</value>
		public DateTime SelectedDate {
			get 
			{
				return DateTime.SpecifyKind ((DateTime)mDatePicker.Date, DateTimeKind.Utc).ToLocalTime();
			}
			set 
			{
				mDatePicker.Date = (NSDate)DateTime.SpecifyKind(value, DateTimeKind.Local);
			}
		}
			
		/// <summary>
		/// Called when the selected data has changed
		/// </summary>
		public event EventHandler<DateTime> OnSelectedDateChanged = delegate {		};


		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DHDialogs.DHDatePickerDialog"/> class.
		/// </summary>
		/// <param name="mode">Mode.</param>
		public DHDatePickerDialog (UIDatePickerMode mode) 
			: base(DHDialogType.DatePicker)
		{
			mDatePicker = new UIDatePicker (CGRect.Empty);
			mDatePicker.Mode = mode;
			mDatePicker.TimeZone = NSTimeZone.LocalTimeZone;
			mDatePicker.Calendar = NSCalendar.CurrentCalendar;

			mDatePicker.ValueChanged += OnValueChanged;
		}

		/// <summary>
		/// Raises the value changed event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void OnValueChanged (object sender, EventArgs e)
		{
			if (ConstantUpdates == true)
				OnSelectedDateChanged (this, SelectedDate);
		}


		#endregion

		#region implemented abstract members of DHDialogView

		protected override void HandleCancel ()
		{
			
		}

		protected override bool CanSubmit ()
		{
			return true;
		}

		protected override void HandleSubmit ()
		{
			OnSelectedDateChanged (this, SelectedDate);
		}

		#endregion
	}
}

