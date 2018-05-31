using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;
using XFGoogleMapSample.Models;

namespace XFGoogleMapSample
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class XemDuong : ContentPage
	{
	    private Polygon _polygonArea;
        public XemDuong ()
		{
			InitializeComponent ();
		    map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(40.78d, -73.96d), Distance.FromMeters(10000)), false);
        }

	    public XemDuong(Duong duong)
	    {
	        InitializeComponent();
	        map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(40.78d, -73.96d), Distance.FromMeters(10000)), false);
        }

	    private void ButtonShowArea_OnClicked(object sender, EventArgs e)
	    {

	        if (_polygonArea == null)
	        {
	            // Show _polygonArea
	            _polygonArea = new Polygon();
	            _polygonArea.Positions.Add(new Position(40.85d, -73.96d));
	            _polygonArea.Positions.Add(new Position(40.87d, -74.00d));
	            _polygonArea.Positions.Add(new Position(40.78d, -74.06d));
	            _polygonArea.Positions.Add(new Position(40.77d, -74.02d));
	            _polygonArea.Positions.Add(new Position(40.80d, -73.90d));
	            _polygonArea.IsClickable = true;
	            _polygonArea.StrokeColor = Color.Green;
	            _polygonArea.StrokeWidth = 3f;
	            _polygonArea.FillColor = Color.FromRgba(255, 0, 0, 64);
	            map.Polygons.Add(_polygonArea);
	        }
	        else
	        {
	            // Remove _polygonArea
	            map.Polygons.Remove(_polygonArea);
	            _polygonArea = null;
	        }
        }
	}
}