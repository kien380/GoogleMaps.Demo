using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace XFGoogleMapSample.Models
{
    public class Duong
    {
        public string StreetId { get; set; }
        public string StreetName { get; set; }

        public Duong()
        {
            
        }

        public Duong(Duong duong)
        {
            StreetId = duong.StreetId;
            StreetName = duong.StreetName;
        }

        public static Duong DeserializeObject(string json1, string json2)
        {
            // Get streetId
            var listJson1 = json1.Split('\"');
            string streetId = listJson1[1];

            // Get streetName
            var listJson2 = json2.Split('\"');
            string streetName = listJson2[1];

            return new Duong{StreetId = streetId, StreetName = streetName};
        }

        public static List<Duong> DeserializeList(string json)
        {
            try
            {
                var listDuong = new List<Duong>();
                var listJsonDuong = json.Split(':');

                for (int i = 1; i < listJsonDuong.Length - 1; i+=2)
                {
                    var duong = DeserializeObject(listJsonDuong[i], listJsonDuong[i+1]);
                    listDuong.Add(duong);
                }

                return listDuong;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new List<Duong>();
            }

        }
    }
}
