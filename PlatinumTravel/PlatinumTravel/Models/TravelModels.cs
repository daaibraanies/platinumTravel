using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlatinumTravel.Models
{
    public class Countries
    {
       public  Dictionary<string, int> countries = new Dictionary<string, int>();

        public Countries()
        {
            countries.Add("Болгария",19);
            countries.Add("Вьетнам", 29);
            countries.Add("Греция", 34);
            countries.Add("Доминикана", 38);
        }
    }
}