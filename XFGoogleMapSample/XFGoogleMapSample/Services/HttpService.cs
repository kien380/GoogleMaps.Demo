using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace XFGoogleMapSample.Services
{
    public class HttpService
    {
        #region Instance

        private static HttpService _instance;

        public static HttpService Instance => _instance ?? (_instance = new HttpService());

        #endregion

        #region Properties

        private HttpClient _client;

        // Test host
        private const string _host = "http://baonvdemo.azurewebsites.net/";

        #endregion

        public HttpService()
        {
            _client = new HttpClient();
        }

        #region Methods

        public async Task<string> GetAsync(string url)
        {
            try
            {
                var response = await _client.GetAsync(url);
                if (response == null)
                {
                    Debug.Write("Internet is not stable");
                    return null;
                }

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return content;
                }

            }
            catch (Exception e)
            {
                Debug.Write(e);
            }

            return null;
        }



        public async Task<string> PostAsync(string url, string json)
        {
            try
            {
                var contentRequest = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(url, contentRequest);
                if (response == null)
                {
                    Debug.Write("Internet is not stable");
                    return null;
                }

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return content;
                }

            }
            catch (Exception e)
            {
                Debug.Write(e);
            }

            return null;
        }

        #endregion

        #region API URL

        public string ApiTest()
        {
            return $"{_host}Merchant/getlistmerchant";
        }

        public string GetDanhSachDuong()
        {
            return $"{_host}street";
        }

        public string GetToaDoDuong(string msDuong)
        {
            return $"{_host}street/{msDuong}";
        }

        public string GetDanhSachQuan()
        {
            return $"{_host}Merchant/getlistmerchant";
        }

        public string GetDuongGiaoNhau(string msDuong)
        {
            return $"{_host}duonggiaonhau/{msDuong}";
        }

        public string GetDuongNamTrongQuan(string msQuan, string msDuong)
        {
            return $"{_host}street/quan/{msDuong}/{msQuan}";
        }

        public string GetDuongDiQuaQuan(string msDuong)
        {
            return $"{_host}street/district/{msDuong}";
        }

        #endregion
    }
}
