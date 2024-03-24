using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otonom_Hazine_Avcısı
{
    public class Location
    {

        public HashSet<(int,int)> locations = new HashSet<(int,int)> ();
        public List<int> locationsx {  get; set; }
        public List<int> locationsy {get; set; }
        public Location()
        {

        }
        public Location(List<int> locationsx,List<int> locationsy)
        {
            this.locationsx = locationsx;
            this.locationsy = locationsy;
        }

        public Location(HashSet<(int, int)> locations)
        {
            this.locations = locations;
        }
    }
}
