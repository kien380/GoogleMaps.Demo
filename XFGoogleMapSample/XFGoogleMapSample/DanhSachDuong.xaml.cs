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


                /*
                // Test
	            var result = "[{\"X\":\"10.7534\",\"Y\":\"106.6507\"},{\"X\":\"10.7527\",\"Y\":\"106.6535\"},{\"X\":\"10.7530\",\"Y\":\"106.6613\"},{\"X\":\"10.7535\",\"Y\":\"106.6641\"},{\"X\":\"10.7540\",\"Y\":\"106.6666\"},{\"X\":\"10.7546\",\"Y\":\"106.6696\"},{\"X\":\"10.7555\",\"Y\":\"106.6742\"},{\"X\":\"10.7572\",\"Y\":\"106.6781\"},{\"X\":\"10.7595\",\"Y\":\"106.6840\"},{\"X\":\"10.7599\",\"Y\":\"106.6849\"},{\"X\":\"10.7633\",\"Y\":\"106.6869\"},{\"X\":\"10.7667\",\"Y\":\"106.6883\"},{\"X\":\"10.7677\",\"Y\":\"106.6888\"},{\"X\":\"10.7713\",\"Y\":\"106.6933\"},{\"X\":\"10.77139\",\"Y\":\"106.69326\"}]";
                
                */

                if (result != null)
	            {
	                Duong.DeserializeList(result);
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
 