using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace XFGoogleMapSample.Models
{
    public class Diem
    {
        public string X { get; set; }
        public string Y { get; set; }


        public Diem()
        {

        }

        public Diem(Diem duong)
        {
            X = duong.X;
            Y = duong.Y;
        }

        public static Diem DeserializeObject(string json1, string json2)
        {
            // Get X
            var listJson1 = json1.Split('\"');
            string x = listJson1[1];

            // Get Y
            var listJson2 = json2.Split('\"');
            string y = listJson2[1];

            return new Diem { X = x, Y = y };
        }

        public static List<Diem> DeserializeList(string json)
        {
            try
            {
                var listDiem = new List<Diem>();
                var listJsonDiem = json.Split(':');

                for (int i = 1; i < listJsonDiem.Length - 1; i += 2)
                {
                    var diem = DeserializeObject(listJsonDiem[i], listJsonDiem[i + 1]);
                    listDiem.Add(diem);
                }

                return listDiem;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new List<Diem>();
            }

        }

        public static double DistanceInKilometer(double lat1,
            double lon1,
            double lat2,
            double lon2)
        {
            var R = 6371d; // Radius of the earth in km
            var dLat = Deg2Rad(lat2 - lat1);  // deg2rad below
            var dLon = Deg2Rad(lon2 - lon1);
            var a =
                Math.Sin(dLat / 2d) * Math.Sin(dLat / 2d) +
                Math.Cos(Deg2Rad(lat1)) * Math.Cos(Deg2Rad(lat2)) *
                Math.Sin(dLon / 2d) * Math.Sin(dLon / 2d);
            var c = 2d * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1d - a));
            var d = R * c; // Distance in km
            return d;
        }

        private static double Deg2Rad(double deg)
        {
            return deg * (Math.PI / 180d);
        }
    }
}
