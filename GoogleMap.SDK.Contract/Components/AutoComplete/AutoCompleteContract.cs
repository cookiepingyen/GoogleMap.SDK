using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contract.Components.AutoComplete
{
    public class AutoCompleteContract
    {
        public interface IAutoCompleteView
        {
            object DataSource { get; set; }

            void this_inputChange(object sender, EventArgs e);

            void AutoCompleteTextBox_ParentChanged(object sender, EventArgs e);

            void ListBox_DoubleClick(object sender, EventArgs e);

            void GetDetail(string selectId);
        }

        public interface IAutoCompletePresenter
        {
            Task<List<object>> GetDataSource(string text);

            Task<object> GetItemDetail(string selectedItem, bool with_all_field);
        }
    }
}
