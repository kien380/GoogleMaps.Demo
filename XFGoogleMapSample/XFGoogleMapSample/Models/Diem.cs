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
    }
}
