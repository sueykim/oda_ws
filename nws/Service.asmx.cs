using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Diagnostics;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

namespace oda2
{

    [WebService(Namespace = "http://oda.lingnet.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]

    public class Service : System.Web.Services.WebService
    {
        private string[] langs = new string[17] { "arabic", "chinese", "korean", "farsi", "russian", "nkorean", "spanish", "tagalog", "pashto", "french", "iraqi", "urdu", "portuguese", "levantine", "dari", "balochi", "somali" };
        private string[] langsNames = new string[17] { "Arabic", "Chinese", "Korean", "Farsi", "Russian", "North-Korean", "Spanish", "Tagalog", "Pashto", "French", "Iraqi", "Urdu", "Portuguese", "Levantine", "Dari", "Balochi", "Somali" };


        string hostname = "oda2";// HttpContext.Current.Request.Url.Host; oda2, oda, oda3


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string StartNewSession(string dbName, Int64 ouId, string modality, string language, DateTime startTime)
        {
            DB db = new DB(dbName);
            GenericData gd;
            gd = db.startNewSession(ouId, modality, language, startTime);

            return gd.toJSON();
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetSessionsDataByDate(int thisyear, int thismonth, int thisday)
        {
            List<SessionData> sdList = new List<SessionData>();
            DB db;// = new DB(dbName);


            foreach (var lng in langs)
            {
                db = new DB(hostname + "_" + lng + "_rc");
                sdList.AddRange(db.getSessionsDataByDate(thisyear, thismonth, thisday));
                db = new DB(hostname + "_" + lng + "_lc");
                sdList.AddRange(db.getSessionsDataByDate(thisyear, thismonth, thisday));
            }


            //sdList = db.getSessionsDataByDate(thisyear, thismonth, thisday); <this is a fake change.>

            return sdList.toJSON();
        }
        /*
              [WebMethod]
              [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
              public string GetSessionsDataBySessionId(String dbname, Int64 sessionid)
              {
                  List<SessionData> sdList = new List<SessionData>();
                  DB db = new DB(dbname);

                  sdList.AddRange(db.getSessionsDataBySessionId(sessionid));  
          

                  //sdList = db.getSessionsDataByDate(thisyear, thismonth, thisday);

                  return sdList.toJSON();
              }
        */
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string InsertTestGroup(string dbName, Int64 sessionId, String timeString, String updnScore, String dproScore,
        String rawResponse, String modDesc, String lngDesc, Int64 userId, DateTime dateIn)
        {
            DB db = new DB(dbName);
            return db.insertTestGroup(sessionId, timeString, updnScore, dproScore, rawResponse, modDesc, lngDesc, userId, dateIn).toJSON();
        }



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetTestGroupsDataBySession(string dbName, int sessionId)
        {
            DB db = new DB(dbName);
            List<TestGroupData> tgdList;
            tgdList = db.getTestGroupsDataBySession(sessionId);

            return tgdList.toJSON();
        }



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string InsertItemTracker(string dbName, string ansrfldid, int tstgrpid, string matches, string useranswer,
        double userscore, double possiblepoints, double pointsgained, int passorfail, string judgingkeys)
        {
            DB db = new DB(dbName);
            return db.insertItemsTracker(ansrfldid, tstgrpid, matches, useranswer, userscore, possiblepoints, pointsgained,
            passorfail, judgingkeys).toJSON();
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetItemTrackerDataByTestGroup(string dbName, int testgroupid)
        {
            DB db = new DB(dbName);
            List<ItemTrackerData> itdList;
            itdList = db.getItemTrackerDataByTestGroup(testgroupid);

            return itdList.toJSON();
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string InsertProfileData(string dbName, Int64 OUid, string lngDesc, string modDesc, int wrkPLid, int clngPLid,
        DateTime prfDate, string timeString, string prfLink, string CData, Int64 sessionID)
        {
            DB db = new DB(dbName);
            GenericData gd = new GenericData();
            return db.insertProfileData(OUid, lngDesc, modDesc, wrkPLid, clngPLid, prfDate, timeString, prfLink, CData, sessionID).toJSON();
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetProfileDataById(string dbName, int profileId)
        {
            DB db = new DB(dbName);
            return db.getProfileDataById(profileId).toJSON();

        }
/*
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
              //gets all profiles from ALL DATABASES
              public string GetProfilesByUserId(int userId)
              {
                List<ProfileData> globalList = new List<ProfileData>();
                DB db;

                foreach (var lng in langs)
                {
                  db = new DB(hostname+"_"+lng+"_rc");
                  globalList.AddRange(db.getProfilesByUserId(userId));
                  db = new DB(hostname + "_" + lng + "_lc");
                  globalList.AddRange(db.getProfilesByUserId(userId));
                }

                return globalList.toJSON();
              }
        */
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string InsertODAError(string dbName, string errDesc, string errURL, string errData, string langDesc, string modDesc)
        {
            DB db = new DB(dbName);
            return db.insertODAError(errDesc, errURL, errData, langDesc, modDesc).toJSON();
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetTest(string dbName, Int64 sessionId)
        {
            TestSetMaker tsm = new TestSetMaker(dbName, sessionId);
            //return  String.Join(",", tsm.testData().ToArray());
            return tsm.getTest();
            //DB db = new DB(dbName);
            //return db.insertODAError(errDesc, errURL, errData, langDesc, modDesc).toJSON();
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetReveryTest(string dbName, Int64 sessionId, string testletNames)
        {
            RecoveryTestSetMaker rtsm = new RecoveryTestSetMaker(dbName, sessionId, testletNames);
            //return  String.Join(",", tsm.testData().ToArray());
            return rtsm.getTest();
            //DB db = new DB(dbName);
            //return db.insertODAError(errDesc, errURL, errData, langDesc, modDesc).toJSON();
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string InsertSurveyItem(Int32 OUId, string timeString, DateTime dateTime, string languageDatabaseName, Int32 sa1, Int32 sa2,
            Int32 sa3, Int32 sb1, Int32 sb2, Int32 sb3, Int32 sb4, Int32 sc1, Int32 sc2, Int32 sc3, Int32 sc4, Int32 sc5, Int32 sc6,
            Int32 sd1, Int32 sd2, Int32 sd3, Int32 se1, Int32 se2, string se3)
        {
            DB db = new DB("oda_master");
            return db.insertSurveyItem(OUId, timeString, dateTime, languageDatabaseName, sa1, sa2, sa3, sb1, sb2, sb3, sb4, sc1,
                sc2, sc3, sc4, sc5, sc6, sd1, sd2, sd3, se1, se2, se3).toJSON();
        }

        //insertSurveyItemUserWpl
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string insertSurveyItemUserWpl(Int32 OUId, string timeString, DateTime dateTime, string languageDatabaseName, Int32 sa1, Int32 sa2,
            Int32 sa3, Int32 sb1, Int32 sb2, Int32 sb3, Int32 sb4, Int32 sc1, Int32 sc2, Int32 sc3, Int32 sc4, Int32 sc5, Int32 sc6,
            Int32 sd1, Int32 sd2, Int32 sd3, Int32 se1, Int32 se2, string se3)
        {
            DB db = new DB("oda_master");
            return db.insertSurveyItemUserWpl(OUId, timeString, dateTime, languageDatabaseName, sa1, sa2, sa3, sb1, sb2, sb3, sb4, sc1,
                sc2, sc3, sc4, sc5, sc6, sd1, sd2, sd3, se1, se2, se3).toJSON();
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //gets all profiles from ALL DATABASES
        public string GetAllSurveyItems(string osvDate1, string osvDate2)
        {
            DB db = new DB("oda_master");
            return db.getAllSurveyItems(osvDate1, osvDate2).toJSON();
        }

        //insertTestRecoveryItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string InsertTestRecoveryItem(Int32 OUId, Int32 langId, Int32 modId, Int64 sessionId, string startTime, string testletNames, string lastSubmittedSet)
        {
            DB db = new DB("oda_master");
            return db.insertTestRecoveryItem(OUId, langId, modId, sessionId, startTime, testletNames, lastSubmittedSet).toJSON();
        }


    }
}