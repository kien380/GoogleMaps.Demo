using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFGoogleMapSample.Models;
using XFGoogleMapSample.Services;

namespace XFGoogleMapSample
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DanhSachDuong : ContentPage
	{
		public DanhSachDuong ()
		{
			InitializeComponent ();

		    Task.Run(async () =>
		    {
                await GetDanhSachDuong();
            });
            
		}

	    public DanhSachDuong(List<Duong> listDuong)
	    {
	        InitializeComponent();

	        LabelDangTai.IsVisible = false;
	        ListViewDuong.ItemsSource = listDuong;
        }

        private async void ListViewDuong_OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
	        var item = (Duong) e.Item;
	        await Navigation.PushAsync(new XemDuong(item));
	    }

	    private async Task GetDanhSachDuong()
	    {
	        try
	        {
                var url = HttpService.Instance.GetDanhSachDuong();
	            var result = await HttpService.Instance.GetAsync(url);

                if (result != null)
	            {
	                Device.BeginInvokeOnMainThread(() =>
	                {
	                    LabelDangTai.IsVisible = false;
	                    ListViewDuong.ItemsSource = Duong.DeserializeList(result);
                    });
	            }
	            else
	            {
	                Device.BeginInvokeOnMainThread(() => { LabelDangTai.Text = "Quá trình tải bị lỗi, xin thử lại"; });
                }
	        }
	        catch (Exception e)
	        {
	            Debug.WriteLine(e);
	            Device.BeginInvokeOnMainThread(() => { LabelDangTai.Text = "Quá trình tải bị lỗi, xin thử lại"; });
	        }
	    }
	}
}
 