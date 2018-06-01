using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace XFGoogleMapSample.Models
{
    public class Quan
    {
        public string DistrictId { get; set; }
        public string DistrictName { get; set; }



        public Quan()
        {

        }

        public Quan(Quan quan)
        {
            DistrictId = quan.DistrictId;
            DistrictName = quan.DistrictName;
        }


        public static Quan DeserializeObject(string json1, string json2)
        {
            // Get streetId
            var listJson1 = json1.Split('\"');
            string streetId = listJson1[1];

            // Get streetName
            var listJson2 = json2.Split('\"');
            string streetName = listJson2[1];

            return new Quan { DistrictId = streetId, DistrictName = streetName };
        }

        public static List<Quan> DeserializeList(string json)
        {
            try
            {
                var listQuan = new List<Quan>();
                var listJsonQuan = json.Split(':');

                for (int i = 1; i < listJsonQuan.Length - 1; i += 2)
                {
                    var quan = DeserializeObject(listJsonQuan[i], listJsonQuan[i + 1]);
                    listQuan.Add(quan);
                }

                return listQuan;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new List<Quan>();
            }

        }
    }
}
