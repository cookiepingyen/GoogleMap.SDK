using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Input;
using static GoogleMap.SDK.Contract.Components.AutoComplete.AutoCompleteContract;
using GoogleMap.SDK.UI.WPF.Utility;
using System.Diagnostics;

namespace GoogleMap.SDK.UI.WPF.Components.AutoComplete
{
    public abstract class BaseAutoCompleteView<TSource, TItem> : TextBox, IAutoCompleteView
    {
        private ListBox _listBox = new ListBox();
        private object _values;
        public event EventHandler<TItem> selectChange;
        private String _formerValue = String.Empty;
        private Popup _popup;
        public abstract Task<TItem> ItemDetailResponse(string selectItem);

        public abstract Task<List<TSource>> DataSourceResponseAsync(string text);

        public object DataSource
        {
            get
            {
                return _values;
            }
            set
            {
                _values = value;
                _listBox.ItemsSource = (IEnumerable)value;
                _listBox.DisplayMemberPath = "Key";
                _listBox.SelectedValuePath = "Value";
                _listBox.Visibility = System.Windows.Visibility.Visible;
                _popup.IsOpen = (value != null);
            }
        }

        public BaseAutoCompleteView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            _listBox = new ListBox();
            _popup = new Popup()
            {
                Child = _listBox,
                PlacementTarget = this,
                Placement = PlacementMode.Bottom,
                StaysOpen = false,
                IsOpen = false
            };

            _listBox.MouseDoubleClick += ListBox_DoubleClick;
            PreviewKeyDown += AutoComplete_KeyDown;
            TextChanged += this_inputChange;
        }

        private void AutoComplete_KeyDown(object sender, KeyEventArgs e)
        {
            this_KeyDown(sender, e.Key);
        }

        public void AutoCompleteTextBox_ParentChanged(object sender, EventArgs e)
        {

        }


        public void this_inputChange(object sender, EventArgs e)
        {
            this.DelayCallback(async () =>
            {
                if (Text == _formerValue || Text == "") return;
                _formerValue = Text;
                this.DataSource = await DataSourceResponseAsync(Text);
            }, 1500);

        }

        public async void GetDetail(string selectId)
        {
            TItem placeDetailResModel = await ItemDetailResponse(selectId);
            selectChange?.Invoke(this, placeDetailResModel);
        }


        public void ListBox_DoubleClick(object sender, EventArgs e)
        {
            if (_listBox.SelectedItem == null) return;

            Type type = _listBox.SelectedItem.GetType();
            var displayProperty = type.GetProperty(_listBox.DisplayMemberPath);

            if (displayProperty != null)
            {
                Text = displayProperty.GetValue(_listBox.SelectedItem)?.ToString() ?? "";
            }

            _popup.IsOpen = false; // 修正：用 IsOpen
            _formerValue = Text;
            GetDetail(_listBox.SelectedValue?.ToString() ?? "");
        }

        public void this_KeyDown(object sender, Key wpfKey)
        {
            switch (wpfKey)
            {
                case Key.Tab:
                case Key.Enter:
                    {
                        if (_popup.IsOpen && _listBox.SelectedItem != null) // 修正
                        {
                            Type type = _listBox.SelectedItem.GetType();
                            var displayProperty = type.GetProperty(_listBox.DisplayMemberPath);

                            if (displayProperty != null)
                            {
                                Text = displayProperty.GetValue(_listBox.SelectedItem)?.ToString() ?? "";
                            }

                            _popup.IsOpen = false;
                            _formerValue = Text;
                            GetDetail(_listBox.SelectedValue?.ToString() ?? "");
                        }
                        break;
                    }
                case Key.Down:
                    {
                        if (_popup.IsOpen && _listBox.SelectedIndex < _listBox.Items.Count - 1)
                        {
                            _listBox.SelectedIndex++;
                        }
                        break;
                    }
                case Key.Up:
                    {
                        if (_popup.IsOpen && _listBox.SelectedIndex > 0)
                        {
                            _listBox.SelectedIndex--;
                        }
                        break;
                    }
            }
        }

        private Key ConvertConsoleKeyToKey(ConsoleKey consoleKey)
        {

            switch (consoleKey)
            {
                case ConsoleKey.Enter: return Key.Enter;
                case ConsoleKey.Tab: return Key.Tab;
                case ConsoleKey.DownArrow: return Key.Down;
                case ConsoleKey.UpArrow: return Key.Up;
                case ConsoleKey.LeftArrow: return Key.Left;
                case ConsoleKey.RightArrow: return Key.Right;
                case ConsoleKey.Escape: return Key.Escape;
                case ConsoleKey.Backspace: return Key.Back;
                case ConsoleKey.Delete: return Key.Delete;
                case ConsoleKey.Home: return Key.Home;
                case ConsoleKey.End: return Key.End;
                case ConsoleKey.PageUp: return Key.PageUp;
                case ConsoleKey.PageDown: return Key.PageDown;
                // 可以繼續添加其他需要的映射
                default: return Key.None;
            }
        }
    }
}
