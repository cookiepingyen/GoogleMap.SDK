using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.Windows.Forms;
using static GoogleMap.SDK.Contract.Components.AutoComplete.AutoCompleteContract;
using GoogleMap.SDK.UI.Winform.Utility;

namespace GoogleMap.SDK.UI.Winform.Components.AutoComplete
{
    public abstract class BaseAutoCompleteView<TSource, TItem> : TextBox, IAutoCompleteView
    {
        private ListBox _listBox = new ListBox();
        private object _values;
        public event EventHandler<TItem> selectChange;
        private String _formerValue = String.Empty;
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
                _listBox.DataSource = value;
                _listBox.DisplayMember = "Key";
                _listBox.ValueMember = "Value";
                _listBox.Visible = true;
            }
        }

        public BaseAutoCompleteView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            _listBox = new ListBox();

            this.ParentChanged += AutoCompleteTextBox_ParentChanged;
            _listBox.DoubleClick += ListBox_DoubleClick;
            KeyDown += AutoComplete_KeyDown;
            TextChanged += this_inputChange;

        }

        private void AutoComplete_KeyDown(object sender, KeyEventArgs e)
        {
            this_KeyDown(sender, (ConsoleKey)e.KeyValue);
        }

        public void AutoCompleteTextBox_ParentChanged(object sender, EventArgs e)
        {
            Parent.Controls.Add(_listBox);
            _listBox.Left = Left;
            _listBox.Top = Top + Height;
            _listBox.Width = Width;
            _listBox.Visible = false;
            _listBox.BringToFront();
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
            Type type = _listBox.SelectedItem.GetType(); // ComboBoxData 
            Text = type.GetProperty(this._listBox.DisplayMember) // Key
                       .GetValue(_listBox.SelectedItem).ToString();

            _listBox.Visible = false;
            _formerValue = Text;
            GetDetail(_listBox.SelectedValue.ToString());
        }

        public void this_KeyDown(object sender, ConsoleKey e)
        {
            switch (e)
            {
                case ConsoleKey.Tab:
                case ConsoleKey.Enter:
                    {
                        if (_listBox.Visible)
                        {
                            Type type = _listBox.SelectedItem.GetType();

                            Text = type.GetProperty(this._listBox.DisplayMember)
                                       .GetValue(_listBox.SelectedItem).ToString();
                            _listBox.Visible = false;
                            _formerValue = Text;
                            GetDetail(_listBox.SelectedValue.ToString());
                        }
                        break;
                    }
                case ConsoleKey.DownArrow:
                    {
                        if ((_listBox.Visible) && (_listBox.SelectedIndex < _listBox.Items.Count - 1))
                            _listBox.SelectedIndex++;

                        break;
                    }
                case ConsoleKey.UpArrow:
                    {
                        if ((_listBox.Visible) && (_listBox.SelectedIndex > 0))
                            _listBox.SelectedIndex--;

                        break;
                    }
            }
        }



        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Tab:
                    return true;
                default:
                    return base.IsInputKey((Keys)keyData);
            }
        }

    }

}
