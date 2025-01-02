using Syncfusion.Maui.DataForm;
using Microsoft.Maui.Controls;

namespace BjjTrainer.Views.Components.CustomEditors
{
    public class CustomTextEditor : IDataFormEditor
    {
        private Entry _entry;

        public View CreateEditorView(DataFormItem dataFormItem)
        {
            _entry = new Entry
            {
                BackgroundColor = Colors.WhiteSmoke,
                TextColor = Colors.DarkSlateGray,
                FontSize = 16,
                Margin = new Thickness(10),
                Placeholder = dataFormItem.PlaceholderText
            };

            // Bind Entry Text to Field
            _entry.SetBinding(Entry.TextProperty, dataFormItem.FieldName, BindingMode.TwoWay);

            return _entry;
        }

        public void CommitValue(DataFormItem dataFormItem, View view)
        {

        }

        public void UpdateValue(DataFormItem dataFormItem, View view)
        {

        }

        // Correct the method name to match the interface
        public void UpdateReadyOnly(DataFormItem dataFormItem)
        {
            if (_entry != null)
            {
                _entry.IsEnabled = !dataFormItem.IsReadOnly.GetValueOrDefault(true);
            }
        }
    }
}
