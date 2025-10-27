using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Place.PlaceDetail;
using GoogleMap.SDK.UI.WPF.Components.AutoComplete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static GoogleMap.SDK.Contract.Components.AutoComplete.AutoCompleteContract;

namespace GoogleMap.SDK.UI.WPF.Test
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        IAutoCompleteView autoCompleteView;

        public MainWindow(IAutoCompleteView autoCompleteView)
        {
            InitializeComponent();
            this.autoCompleteView = autoCompleteView;
            PlaceAutoCompleteView placeAutoCompleteView = (PlaceAutoCompleteView)this.autoCompleteView;

            placeAutoCompleteView.selectChange += PlaceAutoCompleteView_selectChange;
            StackPanel1.Children.Add(placeAutoCompleteView);
        }

        private void PlaceAutoCompleteView_selectChange(object sender, PlaceDetailResModel e)
        {
            Console.WriteLine($"name: {e.result.name}, place_id: {e.result.place_id}");
        }
    }
}
