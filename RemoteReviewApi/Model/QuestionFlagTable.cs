using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemoteReviewApi.Model
{
    public class QuestionFlagTable
    {
        public int PatientDataKey { get; set; }
        public string SiteId { get; set; }
        public string SiteName { get; set; }
        public int PatientKey { get; set; }
        public string Patient { get; set; }
        public string Event { get; set; }
        public int PatientFormKey { get; set; }
        public string FormName { get; set; }
        public string Question { get; set; }
        public string DataValue { get; set; }
        public int SDVerify { get; set; }
        public int StaffKey { get; set; }
    }

    public class FormFlags
    {
        public int PatientFormKey { get; set; }
        public string FormName { get; set; }
        public int FlagCount { get; set; }
    }


}
