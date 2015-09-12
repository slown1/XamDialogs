using System;

namespace DHDialogs {


	/// <summary>
	/// Type of dialog
	/// </summary>
	public enum DHDialogType {

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
