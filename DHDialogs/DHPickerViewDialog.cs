using System;

namespace DHDialogs
{
	public class DHPickerViewDialog : DHDialogView
	{
		
		#region implemented abstract members of DHDialogView

		protected override UIKit.UIView ContentView {
			get {
				throw new NotImplementedException ();
			}
		}

		#endregion

		public DHPickerViewDialog () 
			: base(DHDialogType.PickerView)
		{
			
		}

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
	}
}

