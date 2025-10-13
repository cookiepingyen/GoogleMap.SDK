using GoogleMap.SDK.Contract.Components.AutoComplete;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Place.PlaceAutocomplete;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Place.PlaceDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GoogleMap.SDK.Contract.Components.AutoComplete.AutoCompleteContract;

namespace GoogleMap.SDK.UI.Winform.Components.AutoComplete
{
    public class PlaceAutoCompleteView : BaseAutoCompleteView<ComboBoxData, PlaceDetailResModel>
    {
        IAutoCompletePresenter autoCompletePresenter;
        public PlaceAutoCompleteView(IAutoCompletePresenter autoCompletePresenter)
        {
            this.autoCompletePresenter = autoCompletePresenter;
        }

        public override async Task<List<ComboBoxData>> DataSourceResponseAsync(string text)
        {
            List<object> list = await autoCompletePresenter.GetDataSource(text);
            return list.Select(x => (ComboBoxData)x).ToList();
        }

        public override async Task<PlaceDetailResModel> ItemDetailResponse(string selectItem)
        {
            object itemDetail = await autoCompletePresenter.GetItemDetail(selectItem);
            return (PlaceDetailResModel)itemDetail;
        }
    }

}
