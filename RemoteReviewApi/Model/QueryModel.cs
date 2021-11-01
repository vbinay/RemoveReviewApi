using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemoteReviewApi.Model
{
    public class QueryTable
    {
        public int DCFKey { get; set; }
        public int SiteKey { get; set; }
        public string SiteID { get; set; }
        public string SiteName { get; set; }
        public int PatientKey { get; set; }
        public string Patient { get; set; }
        public string PatientEvent { get; set; }
        public string PatientForm { get; set; }
        public int QueryKey { get; set; }
        public string QueryName { get; set; }
        public string Description { get; set; }
        public int DCfStatus { get; set; }
        public string ActionRequest { get; set; }
        public string Comments { get; set; }
        public int Staffkeys { get; set; }
    }
}
