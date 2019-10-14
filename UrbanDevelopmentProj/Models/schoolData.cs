using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace locationsFind.Models
{
    public class schoolData
    {
        public int id { get; set; }
        public string SchoolName { get; set; }
        public string GeoLong { get; set; }
        public string GeoLat { get; set; }

    }

    public class shoolDetails
    {
        public string searchObjectType { get; set; }
        public string location { get; set; }
        public string locationLat { get; set; }
        public string locationLng { get; set; }
        public List<searchObjectDetail> searchObjectDetail { get; set; }
        public List<string> suggestion { get; set; }
    }

    public class searchObjectDetail
    { 
        public string name {get; set;}
        public string address {get; set;}
        public string lat {get; set;}
        public string lng { get; set; }
    }
    public class Suggestions
    {
        public string value { get; set; }
        
    }
}