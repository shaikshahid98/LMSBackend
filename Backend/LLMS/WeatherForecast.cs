using System.Text.Json;

namespace LLMS
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
    public class Members
    {
        public int id { get; set; }
        public string uname { get; set; }
        public int uadmid { get; set; }
        public string umail{ get; set; }
        public string udep { get; set; }
        public string upassword { get; set; }
        public int ustatus { get; set; }
        public int urecstatus { get; set; }
        public string ureqj { get; set; }
        public string urecj { get; set; }
    }

    public class Admin
    {
        public int id { get; set; }
        public string name{ get; set; }
        public string email{ get; set; }
        public string password { get; set; }

    }
  
         public class Book
    {
        public int id { get; set; }
        public string btitle { get; set; }
        public string bcatag { get; set; }
        public string bauthor { get; set; }
        public int bcopies { get; set; }
        public string bpub { get; set; }
        public string pubname { get; set; }
        public string bisbn { get; set; }
        public int byear { get; set; }
        public DateTime bdate { get; set; }
        public string bstatus { get; set; }


    }
}
