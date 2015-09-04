using System;
using UIKit;
using CoreGraphics;
using System.Collections.Generic;

namespace DHDialogs
{
	public class DHSimplePickerDialog : DHDialogView
	{
		private UIPickerView mPicker;

		#region Properties

		/// <summary>
		/// Gets the content view.
		/// </summary>
		/// <value>The content view.</value>
		protected override UIView ContentView {
			get {
				return mPicker;
			}
		}

		/// <summary>
		/// Gets or sets the selected item.
		/// </summary>
		/// <value>The selected item.</value>
		public String SelectedItem 
		{
			get 
			{
				return ((SimplePickerModel)mPicker.Model).SelectedItem;
			}
			set 
			{
				((SimplePickerModel)mPicker.Model).SelectedItem = value;
			}
		}


		/// <summary>
		/// Called when the selected data has changed
		/// </summary>
		public event EventHandler<String> OnSelectedItemChanged = delegate {};

		/// <summary>
		/// Gets or sets the validation function to call when submitting
		/// </summary>
		/// <value>The validate submit.</value>
		public Func<String,bool> ValidateSubmit { get; set; }

		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="DHDialogs.DHSimplePickerDialog"/> class.
		/// </summary>
		/// <param name="items">Items.</param>
		public DHSimplePickerDialog (List<String> items) 
			: base(DHDialogType.PickerView)
		{
			mPicker = new UIPickerView (CGRect.Empty);
			mPicker.Model = new SimplePickerModel (this, items);
		}

		#region Methods

		protected override bool CanSubmit ()
		{
			if (ValidateSubmit != null)
				return ValidateSubmit (SelectedItem);
			
			return true;
		}

		protected override void HandleCancel ()
		{
			
		}

		protected override void HandleSubmit ()
		{
			SelectionDidChange(SelectedItem);
		}

		#endregion

		/// <summary>
		/// Called when the selection did change
		/// </summary>
		/// <param name="item">Item.</param>
		internal void SelectionDidChange(string item)
		{
			OnSelectedItemChanged (this, item);
		}

		private class SimplePickerModel : UIPickerViewModel {

			private DHSimplePickerDialog pvc;
			private List<String> mItems;

			/// <summary>
			/// Gets or sets the selected item.
			/// </summary>
			/// <value>The selected item.</value>
			internal String SelectedItem {
				get ;
				set;
			}


			public SimplePickerModel (DHSimplePickerDialog pvc, List<String> items) {
				this.pvc = pvc;
				mItems = items;
			}

			public override nint GetComponentCount (UIPickerView v)
			{
				return 1;
			}

			public override nint GetRowsInComponent (UIPickerView pickerView, nint component)
			{
				return mItems.Count;
			}

			public override string GetTitle (UIPickerView picker, nint row, nint component)
			{
				return mItems [(int)row];

			}

			public override void Selected (UIPickerView picker, nint row, nint component)
			{
				var item = mItems [(int)row];

				if (item != SelectedItem) 
				{
					SelectedItem = item; 

					if (pvc.ConstantUpdates == true)
						pvc.SelectionDidChange (item);
				}

			}
				
			public override nfloat GetRowHeight (UIPickerView picker, nint component)
			{
				return 40f;
			}
		}
	}
}

