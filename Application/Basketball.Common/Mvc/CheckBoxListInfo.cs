/// <summary>
/// This the information that is needed by each checkbox in the
/// CheckBoxList helper.
/// </summary>
 public class CheckBoxListInfo
  {
      public CheckBoxListInfo(string value, string displayText, bool isChecked)
      {
          this.Value = value;
          this.DisplayText = displayText;
          this.IsChecked = isChecked;
      }

      public string Value { get; private set; }
      public string DisplayText { get; private set; }
      public bool IsChecked { get; private set; }
 }