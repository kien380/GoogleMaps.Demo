using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;
using XFGoogleMapSample.Models;
using XFGoogleMapSample.Services;

namespace XFGoogleMapSample
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class XemDuong : ContentPage
	{
	    private Polyline _polyline;
	    private readonly Duong _duong;
	    private List<Diem> _listDiem;

	    public XemDuong(Duong duong)
	    {
	        InitializeComponent();

	        _duong = duong;
	        Title = _duong.StreetName;

	        Task.Run(async () =>
	        {
	            await GetToaDoDuong();

	        });
        }

	    private void ButtonShowArea_OnClicked(object sender, EventArgs e)
	    {

	        
        }


	    private async Task GetToaDoDuong()
	    {
	        try
	        {
	            var url = HttpService.Instance.GetToaDoDuong(_duong.StreetId);
	            var result = await HttpService.Instance.GetAsync(url);

	            if (result != null)
	            {
	                Duong.DeserializeList(result);
	                Device.BeginInvokeOnMainThread(() =>
	                {
	                    LabelDangTai.IsVisible = false;
	                    ButtonXemDuongGiaoNhau.IsVisible = true;
	                    _listDiem = Diem.DeserializeList(result);

                        ShowDuong();
	                });
	            }
	            else
	            {
	                Device.BeginInvokeOnMainThread(() =>
	                {
	                    LabelDangTai.Text = "Quá trình tải bị lỗi, xin thử lại";
	                    ButtonXemDuongGiaoNhau.IsVisible = false;
	                });
	            }
	        }
	        catch (Exception e)
	        {
	            Debug.WriteLine(e);
	            Device.BeginInvokeOnMainThread(() =>
                {
                    LabelDangTai.Text = "Quá trình tải bị lỗi, xin thử lại";
	                ButtonXemDuongGiaoNhau.IsVisible = false;
	            });
	        }
	    }

	    private void ShowDuong()
	    {
	        _polyline = new Polyline();

	        for (int i = 0; i < _listDiem.Count; i++)
	        {
	            _polyline.Positions.Add(new Position(double.Parse(_listDiem[i].X), double.Parse(_listDiem[i].Y)));
	        }

	        _polyline.IsClickable = false;
	        _polyline.StrokeColor = Color.Green;
	        _polyline.StrokeWidth = 3f;

	        int diemGiua = _listDiem.Count / 2;
	        map.Polylines.Add(_polyline);
	        map.MoveToRegion(MapSpan.FromCenterAndRadius(
                new Position(double.Parse(_listDiem[diemGiua].X), double.Parse(_listDiem[diemGiua].Y)),
                Distance.FromMeters(1000)), 
                false);
        }

	    private void ButtonXemDuongGiaoNhau_OnClicked(object sender, EventArgs e)
	    {

	    }
	}
}