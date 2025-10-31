using GoogleMap.SDK.Contract.GoogleMapAPI.Models;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Place.PlaceDetail;
using GoogleMap.SDK.Core.Component.AutoComplete;
using GoogleMap.SDK.UI.Winform.Components.AutoComplete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GoogleMap.SDK.Contract.Components.AutoComplete.AutoCompleteContract;

namespace GoogleMap.SDK.UI.Winform.Test
{
    public partial class Form1 : Form
    {
        IAutoCompleteView autoCompleteView;

        public Form1(IAutoCompleteView autoCompleteView)
        {
            InitializeComponent();
            this.autoCompleteView = autoCompleteView;
            PlaceAutoCompleteView placeAutoCompleteView = (PlaceAutoCompleteView)this.autoCompleteView;

            placeAutoCompleteView.selectChange += PlaceAutoCompleteView_selectChange;
            panel1.Controls.Add(placeAutoCompleteView);
        }

        private void PlaceAutoCompleteView_selectChange(object sender, PlaceDetailResModel e)
        {
            Console.WriteLine($"name: {e.result.name}, place_id: {e.result.place_id}");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
