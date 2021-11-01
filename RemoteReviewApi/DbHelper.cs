using Microsoft.Extensions.Configuration;
using RemoteReviewApi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RemoteReviewApi
{
    public class DbHelper
    {
        public readonly IConfiguration _configuration;
        public DbHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        internal IEnumerable<QueryTable> GetAllQueries(bool allvaluesrequired= false)
        {
            List<QueryTable> tbl = new List<QueryTable>();

            string ConnectionString = _configuration.GetConnectionString("Connection1");
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand sqlComm = new SqlCommand("select * from QueryTable", conn);

                conn.Open();
                SqlDataReader Locationreader = sqlComm.ExecuteReader();
                while (Locationreader.Read())
                {
                    tbl.Add(new QueryTable
                    {
                        DCFKey=Convert.ToInt32(Locationreader.GetValue(0)),
                        SiteKey = Convert.ToInt32(Locationreader.GetValue(1)),
                        SiteID = (Locationreader.GetValue(2).ToString()),
                        SiteName= Locationreader.GetValue(3).ToString(),
                        PatientKey= Convert.ToInt32(Locationreader.GetValue(4)),
                        Patient = Locationreader.GetValue(5).ToString(),
                        PatientEvent = Locationreader.GetValue(6).ToString(),
                        PatientForm= Locationreader.GetValue(7).ToString(),
                        QueryKey = Convert.ToInt32(Locationreader.GetValue(8)),
                        QueryName = Locationreader.GetValue(9).ToString(),
                        Description= Locationreader.GetValue(10).ToString(),
                        DCfStatus= Convert.ToInt32(Locationreader.GetValue(11)),
                        ActionRequest= Locationreader.GetValue(12).ToString(),
                        Comments= Locationreader.GetValue(13).ToString(),
                        Staffkeys= Convert.ToInt32(Locationreader.GetValue(14))
                    }) ;
                }
                conn.Close();
            }

            if (allvaluesrequired == true)
            {
                return tbl;
            }
            else
            {
                return tbl.Where(x => string.IsNullOrEmpty(x.Comments)).ToList();
            }
        }

        internal IEnumerable<QuestionFlagTable> GetQuestion(int patientFormKey)
        {
            var listcounnt = GetAllFlags();
            if(listcounnt.Count()>0)
            {
                return listcounnt.Where(x => x.PatientFormKey == patientFormKey);
            }
            return null;
        }

        internal IEnumerable<FormFlags> GetFormsFlagCount()
        {
            var listcounnt = GetAllFlags();
            IEnumerable <FormFlags> mapformflag = new List<FormFlags>();
            if (listcounnt.Count() > 0)
            {
                 mapformflag = from r in listcounnt
                                  orderby r.PatientFormKey
                                  group r by r.PatientFormKey into grp
                                  select new FormFlags { PatientFormKey = grp.Key, FormName = grp.First().FormName, FlagCount = grp.Count() };

            }

            return mapformflag;
        }

        internal bool validatelogin(string username, string password)
        {
            var result = false;

            string ConnectionString = _configuration.GetConnectionString("Connection1");
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {

                SqlCommand sqlComm = new SqlCommand("select * from StaffTable where Staff='"+ username + "' and Password = '"+ password + "'", conn);

                conn.Open();
                SqlDataReader Locationreader = sqlComm.ExecuteReader();
                while (Locationreader.Read())
                {
                    var logid = Convert.ToInt32(Locationreader.GetValue(0));
                     if (logid>0)
                    {
                        result = true;
                    }
                }
                conn.Close();
            }

            return result;
        }

        internal int GetAllFlagsCount()
        {
            var listcount = GetAllQueries();
            return listcount.Count();
        }



        internal IEnumerable<QueryTable> GetAllQueriesCount()
        {
            return GetAllQueries(true);         
        }

        internal bool SaveFlags(QuestionFlagTable model)
        {
            string ConnectionString = _configuration.GetConnectionString("Connection1");
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                var querystring = "Update QuestionFlagTable set SDVerify=" + model.SDVerify + " where PatientDataKey=" + model.PatientDataKey + "";
                SqlCommand sqlComm = new SqlCommand(querystring, conn);

                conn.Open();
                try
                {
                    sqlComm.ExecuteNonQuery();
                }
                catch
                {
                    conn.Close();
                    return false;
                }
                conn.Close();
            }
            return true;
        }

        internal IEnumerable<QuestionFlagTable> GetAllFlags(bool isAllRequired = false)
        {
            List<QuestionFlagTable> tbl = new List<QuestionFlagTable>();

            string ConnectionString = _configuration.GetConnectionString("Connection1");
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand sqlComm = new SqlCommand("select * from QuestionFlagTable", conn);

                conn.Open();
                SqlDataReader Locationreader = sqlComm.ExecuteReader();
                while (Locationreader.Read())
                {
                    tbl.Add(new QuestionFlagTable
                    {
                        PatientDataKey = Convert.ToInt32(Locationreader.GetValue(0)),
                        SiteId = Locationreader.GetValue(1).ToString(),
                        SiteName = Locationreader.GetValue(2).ToString(),
                        PatientKey = Convert.ToInt32(Locationreader.GetValue(3)),
                        Patient = Locationreader.GetValue(4).ToString(),
                        Event = Locationreader.GetValue(5).ToString(),
                        PatientFormKey = Convert.ToInt32(Locationreader.GetValue(6)),
                        FormName = Locationreader.GetValue(7).ToString(),
                        Question = Locationreader.GetValue(8).ToString(),
                        DataValue = Locationreader.GetValue(9).ToString(),
                        SDVerify = Convert.ToInt32(Locationreader.GetValue(10)),
                        StaffKey = Convert.ToInt32(Locationreader.GetValue(11))
                    });
                }
                conn.Close();
            }

            if (isAllRequired == true)
            {
                return tbl;
            }
            else
            {
                return tbl.Where(x => x.SDVerify != 1).ToList();
            }
        }

        internal bool SaveQueryComments(QueryTable modelData)
        {
            string ConnectionString = _configuration.GetConnectionString("Connection1");
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                var querystring = "Update QueryTable set Comments='" + modelData.Comments + "' where DCFKey=" + modelData.DCFKey + "";
                SqlCommand sqlComm = new SqlCommand(querystring, conn);

                conn.Open();
                try
                {
                    sqlComm.ExecuteNonQuery();
                }
                catch
                {
                    conn.Close();
                    return false;
                }
                conn.Close();
            }
            return true;
            
        }
    }
}
