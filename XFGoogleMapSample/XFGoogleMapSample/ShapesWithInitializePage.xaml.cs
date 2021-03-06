using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using XFGoogleMapSample.Services;

namespace XFGoogleMapSample
{
    public partial class ShapesWithInitializePage : ContentPage
    {
        private Polygon _polygonArea; 

        public ShapesWithInitializePage()
        {
            InitializeComponent();
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(40.78d, -73.96d), Distance.FromMeters(10000)), false);

            #region Default Example
            /*
            
            var polyline = new Polyline();
            polyline.Positions.Add(new Position(40.77d, -73.93d));
            polyline.Positions.Add(new Position(40.81d, -73.91d));
            polyline.Positions.Add(new Position(40.83d, -73.87d));
            polyline.IsClickable = true;
            polyline.StrokeColor = Color.Blue;
            polyline.StrokeWidth = 5f;
            map.Polylines.Add(polyline);

            var polygon = new Polygon();
            polygon.Positions.Add(new Position(40.85d, -73.96d));
            polygon.Positions.Add(new Position(40.87d, -74.00d));
            polygon.Positions.Add(new Position(40.78d, -74.06d));
            polygon.Positions.Add(new Position(40.77d, -74.02d));
            polygon.IsClickable = true;
            polygon.StrokeColor = Color.Green;
            polygon.StrokeWidth = 3f;
            polygon.FillColor = Color.FromRgba(255, 0, 0, 64);
            map.Polygons.Add(polygon);

            var circle = new Circle();
            circle.Center = new Position(40.72d, -73.89d);
            circle.Radius = Distance.FromMeters(3000f);
            circle.StrokeColor = Color.Purple;
            circle.StrokeWidth = 6f;
            circle.FillColor = Color.FromRgba(0, 0, 255, 32);
            map.Circles.Add(circle);

            var pinNewYork = new Pin()
            {
                Type = PinType.Place,
                Label = "Central Park NYC",
                Address = "New York City, NY 10022",
                Position = new Position(40.78d, -73.96d),
                IsDraggable = true
            };
            map.Pins.Add(pinNewYork);
            map.SelectedPin = pinNewYork;

            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(40.78d, -73.96d), Distance.FromMeters(10000)), false);


            */
            #endregion
        }

        private async void ButtonShowArea_OnClicked(object sender, EventArgs e)
        {
            //await TestHttp();

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

        private async Task TestHttp()
        {
            var url = HttpService.Instance.ApiTest();
            var result = await HttpService.Instance.GetAsync(url);
        }
    }
}

