using System;

namespace XamDialogs {


	/// <summary>
	/// Type of dialog
	/// </summary>
	public enum XamDialogType {

		PlainTextInput,
		NumberInput,
		PhoneNumberInput,
		EmailInput,
		SecureTextInput,
		LoginAndPasswordInput,
		CustomView,
		DatePicker,
		PickerView,
	}

	/// <summary>
	/// Button mode.
	/// </summary>
	public enum ButtonMode
	{
		OkAndCancel,
		Ok,
		Cancel,
	}
}
