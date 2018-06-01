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
	    private List<Duong> _listDuongGiaoNhau;

	    public XemDuong(Duong duong)
	    {
	        InitializeComponent();

	        _duong = duong;

	        Title = _duong.StreetName;
	        ButtonXemDuongGiaoNhau.Text = "Các đường giao với " + duong.StreetName;

	        Task.Run(async () =>
	        {
	            await GetDuongGiaoNhau();
	            await GetToaDoDuong();
	        });
        }


	    private async Task GetToaDoDuong()
	    {
	        try
	        {
	            var url = HttpService.Instance.GetToaDoDuong(_duong.StreetId);
	            var result = await HttpService.Instance.GetAsync(url);

	            if (result != null)
	            {
	                Device.BeginInvokeOnMainThread(() =>
	                {
	                    LabelDangTai.IsVisible = false;
	                    ButtonXemDuongGiaoNhau.IsVisible = true;
	                    _listDiem = Diem.DeserializeList(result);

                        VeDuong();
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

	    private void VeDuong()
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

	    private async Task GetDuongGiaoNhau()
	    {
	        try
	        {
	            var url = HttpService.Instance.GetDuongGiaoNhau(_duong.StreetId);
	            var result = await HttpService.Instance.GetAsync(url);

	            if (result != null)
	            {
	                _listDuongGiaoNhau = Duong.DeserializeList(result);
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
	        catch (Exception ex)
	        {
	            Debug.WriteLine(ex);
	            Device.BeginInvokeOnMainThread(() =>
	            {
	                LabelDangTai.Text = "Quá trình tải bị lỗi, xin thử lại";
	                ButtonXemDuongGiaoNhau.IsVisible = false;
	            });
	        }
        }

	    private async void ButtonXemDuongGiaoNhau_OnClicked(object sender, EventArgs e)
	    {

	        LabelDangTai.Text = "Đang tải . . .";
	        LabelDangTai.IsVisible = true;
	        ButtonXemDuongGiaoNhau.IsVisible = false;


	        await Navigation.PushAsync(new DanhSachDuong(_listDuongGiaoNhau));
	        LabelDangTai.IsVisible = false;
	        ButtonXemDuongGiaoNhau.IsVisible = true;
        }

	    private void EntryDuongGiaoNhau_OnTextChanged(object sender, TextChangedEventArgs e)
	    {
	        if (string.IsNullOrWhiteSpace(e.NewTextValue))
	        {
	            LabelDuongGiaoNhau.Text = "";
	            return;
            }

	        string text = e.NewTextValue.RemoveUnicodeCharacter().ToLower().Trim();
	        foreach (var duongGiaoNhau in _listDuongGiaoNhau)
	        {
	            if (text == duongGiaoNhau.StreetName.RemoveUnicodeCharacter().ToLower())
	            {
	                LabelDuongGiaoNhau.Text = $"Đường {_duong.StreetName} có giao với đường {e.NewTextValue}";
                    return;
	            }
	        }


	        LabelDuongGiaoNhau.Text = $"Đường {_duong.StreetName} không giao với đường {e.NewTextValue}";
        }

	    private void EntryQuanDiQua_OnTextChanged(object sender, TextChangedEventArgs e)
	    {

	    }
	}
}