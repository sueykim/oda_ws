using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using System.Diagnostics;

namespace oda2
{
  public class DB
  {
    //private SqlDataReader reader;
    //private QuestionData questiondata;
    //private QuizData quizdata;
    private String serverName;
    private String dbName;
    private String userName;
    private String passWord;
    private SPReturnData spData;

    

    // =============================  public function section  ===============================================
    public DB(string dbname)
    {
      //set up vars for  DB
        serverName = "localhost";//sql02a, localhost, sql01a (oda2 &oda3)
      dbName = dbname;
      userName = "wwwoda";//odauser, wwwoda
      passWord = "oda123";//@WSX1qaz, oda123
    }


    public GenericData startNewSession(Int64 ouid, string modality, string language, DateTime starttime)
    {
      GenericData gd = new GenericData();
      object[] arg = new Object[4] { ouid, modality, language, starttime};
      StoredProcedureGrab storedprocedure = new StoredProcedureGrab(serverName, dbName, userName, passWord, "sp_start_session", arg);
      spData = storedprocedure.GetReader();

      if (!spData.getErrorStatus())
      {
        gd.setGenericData(spData.getTheOnlyValue());
      }
      else
      {
        gd.setGenericData(spData.getErrorDetails());
      }  
      //=>
      //reader.Read();
      //gd.setGenericData(reader["S_id"].ToString());
      //reader.Close();

      return gd;
    }



    public List<SessionData> getSessionsDataByDate(int thisYear, int thisMonth, int thisDay)
    {
      List<SessionData> sdList = new List<SessionData>();

      object[] arg = new Object[3] { thisYear, thisMonth, thisDay };
      StoredProcedureGrab storedprocedure = new StoredProcedureGrab(serverName, dbName, userName, passWord, "sp_get_sessions_data_by_date", arg);
      spData = storedprocedure.GetReader();

      if (!spData.getErrorStatus())
      {
        foreach (DataRow row in spData.getDataTable().Rows)
        {
          var tempsd = new SessionData();
          tempsd.setSessionData((Int64)row[0], (int)row[1], (string)row[2], (string)row[3], String.Format("{0:M/d/yyyy HH:mm:ss}", row[4]), String.Format("{0:M/d/yyyy HH:mm:ss}", row[5]));
          sdList.Add(tempsd);
        }
      }
      else
      {
        var tempsd = new SessionData();
        tempsd.setErrorData(spData.getErrorDetails());
        sdList.Add(tempsd);
      }

      
      return sdList;
    }

    //added for get the session info by sessionId (20141017 by SK)
    public List<SessionDataForRecovery> getSessionsDataBySessionId(Int64 sessionid)
    {
        List<SessionDataForRecovery> sdList = new List<SessionDataForRecovery>();

        object[] arg = new Object[1] { sessionid };
        StoredProcedureGrab storedprocedure = new StoredProcedureGrab(serverName, dbName, userName, passWord, "sp_get_sessions_data_by_S_id", arg);
        spData = storedprocedure.GetReader();

        if (!spData.getErrorStatus())
        {
            foreach (DataRow row in spData.getDataTable().Rows)
            {
                var tempsd = new SessionDataForRecovery();
                tempsd.setSessionDataForRecovery((Int64)row[0], (int)row[1], (int)row[2], (int)row[3], String.Format("{0:M/d/yyyy HH:mm:ss}", row[4]), String.Format("{0:M/d/yyyy HH:mm:ss}", row[5]));
                sdList.Add(tempsd);
            }
        }
        else
        {
            var tempsd = new SessionDataForRecovery();
            tempsd.setErrorDataForRecovery(spData.getErrorDetails());
            sdList.Add(tempsd);
        }


        return sdList;
    }


    public GenericData insertTestGroup(Int64 sessionid, String timestring, String updnscore, String dproscore, String rawresponse,
    String moddesc, String lngdesc, Int64 userid, DateTime datein)
    {
      GenericData gd = new GenericData();
      Object[] arg = new Object[9] { sessionid, timestring, updnscore, dproscore, rawresponse, moddesc, lngdesc, userid, datein };
      StoredProcedureGrab storedprocedure = new StoredProcedureGrab(serverName, dbName, userName, passWord, "sp_insert_test_group", arg);
      spData = storedprocedure.GetReader();

      if (!spData.getErrorStatus())
      {
        gd.setGenericData(spData.getTheOnlyValue());
      }
      else
      {
        gd.setGenericData(spData.getErrorDetails());
      }  

      //=>
      //reader.Read();
      //gd.setGenericData(reader["TG_id"].ToString());
      //reader.Close();

      return gd;
    }


    public List<TestGroupData> getTestGroupsDataBySession(int sessionId)
    {
      List<TestGroupData> tgdList = new List<TestGroupData>();

      object[] arg = new Object[1] { sessionId };
      StoredProcedureGrab storedprocedure = new StoredProcedureGrab(serverName, dbName, userName, passWord, "sp_get_test_groups_data_by_session", arg);
      spData = storedprocedure.GetReader();

      if (!spData.getErrorStatus())
      {
        //gd.setGenericData(spData.getTheOnlyValue());
        foreach (DataRow row in spData.getDataTable().Rows)
        {
          var temptgd = new TestGroupData();
          temptgd.setTestGroupData((int)row[0], (Int64)row[1], (string)row[2], (string)row[3], (string)row[4], (string)row[5],
          (string)row[6], (string)row[7], (int)row[8], String.Format("{0:M/d/yyyy HH:mm:ss}", row[9]), (int)row[10]);
          tgdList.Add(temptgd);
        }
      }
      else
      {
        var temptgd = new TestGroupData();
        temptgd.setErrorData(spData.getErrorDetails());
        tgdList.Add(temptgd);
      }


      return tgdList;
    }



    public GenericData insertItemsTracker(string ansrfldid, int tstgrpid, string matches, string useranswer, double userscore,
    double possiblepoints, double pointsgained, int passorfail, string judgingkeys)
    {
      GenericData gd = new GenericData();
      Object[] arg = new Object[9] { ansrfldid, tstgrpid, matches, useranswer, userscore, possiblepoints, pointsgained,
      passorfail, judgingkeys };
      StoredProcedureGrab storedprocedure = new StoredProcedureGrab(serverName, dbName, userName, passWord, "sp_insert_items_tracker", arg);
      spData = storedprocedure.GetReader();

      if (!spData.getErrorStatus())
      {
        gd.setGenericData(spData.getTheOnlyValue());
      }
      else
      {
        gd.setGenericData(spData.getErrorDetails());
      }  
      //=>
      //reader.Read();
      ///////////gd.setGenericData(reader["genstring"].ToString());
      //reader.Close();

      return gd;
    }



    public List<ItemTrackerData> getItemTrackerDataByTestGroup(int testGroupId)
    {
      List<ItemTrackerData> tgdList = new List<ItemTrackerData>();

      object[] arg = new Object[1] { testGroupId };
      StoredProcedureGrab storedprocedure = new StoredProcedureGrab(serverName, dbName, userName, passWord, "sp_get_item_tracker_data_by_test_group", arg);
      spData = storedprocedure.GetReader();


      if (!spData.getErrorStatus())
      {
        //gd.setGenericData(spData.getTheOnlyValue());
        foreach (DataRow row in spData.getDataTable().Rows)
        {
          var tempitd = new ItemTrackerData();
          string imtp;
          if (row[10] != null)
          {
            imtp = row[10].ToString();
          }
          else
          {
            imtp = String.Empty;
          }

          tempitd.setItemTrackerData((Int64)row[0], (string)row[1], (int)row[2], (string)row[3], (string)row[4], (string)row[5],
          (double)row[6], (double)row[7], (double)row[8], (byte)row[9], imtp);
          tgdList.Add(tempitd);
        }
      }
      else
      {
        var tempitd = new ItemTrackerData();
        tempitd.setErrorData(spData.getErrorDetails());
        tgdList.Add(tempitd);
      }

      return tgdList;
    }



    

    public GenericData insertProfileData(Int64 ouid, string lngdesc, string moddesc, int wrkplid, int clngplid, DateTime prfdate,
    string timestring, string prflink, string cdata, Int64 sessionid)
    {
      GenericData gd = new GenericData();
      Object[] arg = new Object[10] { ouid, lngdesc, moddesc, wrkplid, clngplid, prfdate, timestring, prflink, cdata, sessionid };
      StoredProcedureGrab storedprocedure = new StoredProcedureGrab(serverName, dbName, userName, passWord, "sp_insert_profiles", arg);
      spData = storedprocedure.GetReader();


      if (!spData.getErrorStatus())
      {
        gd.setGenericData(spData.getTheOnlyValue());
      }
      else
      {
        gd.setGenericData(spData.getErrorDetails());
      }  
      //=>
      //reader.Read();
      //gd.setGenericData(reader["PRF_id"].ToString());
      //reader.Close();

      return gd;
    }


    
    public List<ProfileData> getProfileDataById(int profileid)
    {
      List<ProfileData> prfdList = new List<ProfileData>();
      Object[] arg = new Object[1] { profileid };
      StoredProcedureGrab storedprocedure = new StoredProcedureGrab(serverName, dbName, userName, passWord, "sp_get_profile_data_by_id", arg);
      spData = storedprocedure.GetReader();


      if (!spData.getErrorStatus())
      {
        var tempPrfData = new ProfileData();
        var row = spData.getDataTable().Rows[0];
        tempPrfData.setProfileData( (int)row[0], (int)row[1], (string) row[2], (string) row[3], (string) row[4],
        (string) row[5], String.Format("{0:M/d/yyyy HH:mm:ss}", row[6]), (string) row[7], (string) row[8], (string) row[9], "" );
        prfdList.Add(tempPrfData);

      }
      else
      {
        var tempPrfData = new ProfileData();
        tempPrfData.setErrorData(spData.getErrorDetails());
        prfdList.Add(tempPrfData);
      }

      return prfdList;
    }




    public List<ProfileData> getProfilesByUserId(int userid)
    {
      List<ProfileData> prfdList = new List<ProfileData>();
      Object[] arg = new Object[1] { userid };
      StoredProcedureGrab storedprocedure = new StoredProcedureGrab(serverName, dbName, userName, passWord, "sp_get_profiles_by_user_id", arg);
      spData = storedprocedure.GetReader();

      if (!spData.getErrorStatus())
      {
        foreach (DataRow row in spData.getDataTable().Rows)
        {
          var tempPrfData = new ProfileData();

          tempPrfData.setProfileData((int)row[0],                                       //int prfid
                                      0,                                                 //int prfouid
                                      (string)row[1],                                   //string prflng
                                      (string)row[2],                                   //string prfmod
                                      "",                                                //string prfwpl
                                      "",                                                //string prfcpl
                                      String.Format("{0:M/d/yyyy HH:mm:ss}", row[3]),    //string prfdate 
                                      "",                                                //string prftimestring
                                      "",                                                //string prflink 
                                      "",                                               //string prfdbnm
                                      (string)row[4]);

          prfdList.Add(tempPrfData);
        }
      }
      else
      {
        var tempPrfData = new ProfileData();
        tempPrfData.setErrorData(spData.getErrorDetails());
        prfdList.Add(tempPrfData);
      }


      return prfdList;
    }


    

    public GenericData insertODAError( string errdesc, string errurl, string errdata,  string langdesc, string moddesc)
    {
      GenericData gd = new GenericData();
      Object[] arg = new Object[5] { errdesc, errurl, errdata,  langdesc, moddesc};
      StoredProcedureGrab storedprocedure = new StoredProcedureGrab(serverName, dbName, userName, passWord, "sp_insert_oda_error", arg);
      spData = storedprocedure.GetReader();

      if (!spData.getErrorStatus())
      {
        gd.setGenericData(spData.getTheOnlyValue());
      }
      else
      {
        gd.setGenericData(spData.getErrorDetails());
      }  

      return gd;
    }



    public List<String> getTestletsNames()
    {
      List<String> tltList = new List<String>();

      object[] arg = new Object[] {  };
      StoredProcedureGrab storedprocedure = new StoredProcedureGrab(serverName, dbName, userName, passWord, "sp_ws_getTestletsNames", arg);
      spData = storedprocedure.GetReader();

      if (!spData.getErrorStatus())
      {
        foreach (DataRow row in spData.getDataTable().Rows)
        {
          tltList.Add((string)row[0]);
        }
      }
      else
      {
        tltList.Add("error");
      }


      return tltList;
    }


    public List<TestletItemData> getTestletItemData(string testletname)
    {
      List<TestletItemData> tltItemList = new List<TestletItemData>();

      object[] arg = new Object[1] { testletname };
      StoredProcedureGrab storedprocedure = new StoredProcedureGrab(serverName, dbName, userName, passWord, "sp_ws_get_testlet_items", arg);
      spData = storedprocedure.GetReader();

      if (!spData.getErrorStatus())
      {
        foreach (DataRow row in spData.getDataTable().Rows)
        {
          //int len = row.ItemArray.Length;
          object[] data = row.ItemArray;
          var tmp = new TestletItemData();
          //tempud.setUserData((Int64)row[0], (string)row[1], (string)row[2], (int)(row[3] ?? 0), (string)(row[4] ?? ""));//String.Format("{0:M/d/yyyy HH:mm:ss}", row[4]));
          tmp.setTestletItemData(data);//String.Format("{0:M/d/yyyy HH:mm:ss}", row[4]));
          tltItemList.Add(tmp);
        }
      }
      else
      {
        var tmp = new TestletItemData();
        tmp.setErrorData(spData.getErrorDetails());
        tltItemList.Add(tmp);
      }

      return tltItemList;      
    }



    /*

    public QuizData getQuizData(int tpc_id)
    {
      quizdata = new QuizData();
      Object[] arg = new Object[1] { tpc_id };

      StoredProcedureGrab storeprocedure = new StoredProcedureGrab(serverName, dbName, userName, passWord, "sp2_get_quiz_data", arg);
      reader = storeprocedure.GetReader();

      //get first record, set the topic once 
      reader.Read();
      setTopic();
      setQuestionData();

      while (reader.Read())
      {
        setQuestionData();
      }
      reader.Close();
      //storeprocedure.closeConnection();
      return quizdata;
    }

    public List<TopicData> getTopicData(int testament) //0:old, 1:new, 2:all(pass null to SP)
    {
      TopicData topicdata;
      List<TopicData> topix = new List<TopicData>();

      Object[] arg;
      if (testament < 2)
      {
        arg = new Object[1] { testament };
      }
      else
      {
        arg = new Object[1] { null };
      }

      StoredProcedureGrab storeprocedure = new StoredProcedureGrab(serverName, dbName, userName, passWord, "sp3_get_topics", arg);
      reader = storeprocedure.GetReader();

      while (reader.Read())
      {
        topicdata = new TopicData();
        topicdata.setTopicData((int)reader["tpc_id"], (int)reader["tpc_bk_id"], reader["tpc_str_ar"].ToString());
        topix.Add(topicdata);
      }
      reader.Close();
      //storeprocedure.closeConnection();
      return topix;
    }

    public LogInData verifyLogin(String siteUserLogin, String siteUserPassword)
    {
      LogInData logindata = new LogInData();
      Object[] arg;

      if (siteUserLogin.Length > 0 && siteUserPassword.Length > 0)
      {
        arg = new Object[2] { siteUserLogin.ToString(), siteUserPassword.ToString() };
        StoredProcedureGrab storeprocedure = new StoredProcedureGrab(serverName, dbName, userName, passWord, "sp4_verify_login", arg);
        reader = storeprocedure.GetReader();

        while (reader.Read())
        {
          logindata = new LogInData();
          if ((int)reader["status_code"] == 1)
          {

            logindata.setLogInData((int)reader["status_code"], (Int64)reader["lgn_id"], reader["lgn_first_name"].ToString(), reader["lgn_last_name"].ToString(), reader["lgn_email"].ToString(), (bool)reader["lgn_is_active"]);
          }
          else if ((int)reader["status_code"] == 2)
          {
            logindata.setLogInData((int)reader["status_code"], 0, "", "", "", false);
          }

        }
      }

      reader.Close();
      return logindata;
    }

    
     //  takes:    all fields to fill a record in progress table
     //  retuns:   progress record id, using generc data class for the xml fields
     //  function: calls the sp2_insert_progress stored procedure, to insert a progress record, then returns the id for 
     //            that record
     
    public GenericData insertProgress(Object[] input)
    {
      GenericData genericdata;

      Object[] arg;
      arg = new Object[input.Length];
      for (int x = 0; x < input.Length; x++)
      {
        arg[x] = input[x];
      }

      StoredProcedureGrab storeprocedure = new StoredProcedureGrab(serverName, dbName, userName, passWord, "sp2_insert_progress", arg);
      reader = storeprocedure.GetReader();

      reader.Read();
      genericdata = new GenericData();
      genericdata.setGenericData(reader["prgs_id"].ToString());
      reader.Close();

      return genericdata;
    }



    
     //  takes:    user id and testament id 0:old, 1:new
     //  retuns:   a string of topics that are passed from progress table for that user
     //  function: calls sp2_get_passed_topics to get 0||1||5  STRING is trimmed on sql level and no extra || at the end
     //
    public GenericData getPassedTopics(int userId, int testamentId)
    {
      GenericData genericdata;

      Object[] arg;
      arg = new Object[2] { userId, testamentId };

      StoredProcedureGrab storeprocedure = new StoredProcedureGrab(serverName, dbName, userName, passWord, "sp2_get_passed_topics", arg);
      reader = storeprocedure.GetReader();

      reader.Read();
      genericdata = new GenericData();
      genericdata.setGenericData(reader["passed_topic_str"].ToString());
      reader.Close();

      return genericdata;
    }

  */

    public GenericData insertSurveyItem(Int32 OUId, string timeString, DateTime dateTime, string languageDatabaseName, Int32 sa1, Int32 sa2,
          Int32 sa3, Int32 sb1, Int32 sb2, Int32 sb3, Int32 sb4, Int32 sc1, Int32 sc2, Int32 sc3, Int32 sc4, Int32 sc5, Int32 sc6,
          Int32 sd1, Int32 sd2, Int32 sd3, Int32 se1, Int32 se2, string se3)
    {
      GenericData gd = new GenericData();
      Object[] arg = new Object[23] { OUId,  timeString,  dateTime,  languageDatabaseName,  sa1 ,  sa2, sa3,  sb1 ,  sb2 ,  sb3 ,  sb4 ,  sc1 ,
              sc2 ,  sc3 ,  sc4 ,  sc5 ,  sc6 , sd1 ,  sd2 ,  sd3 ,  se1 ,  se2 ,  se3 };
      StoredProcedureGrab storedprocedure = new StoredProcedureGrab(serverName, dbName, userName, passWord, "sp_ws_insert_survey_item", arg);
      spData = storedprocedure.GetReader();

      if (!spData.getErrorStatus())
      {
        gd.setGenericData(spData.getTheOnlyValue());
      }
      else
      {
        gd.setGenericData(spData.getErrorDetails());
      }  

      return gd;
    }

    //sp_ws_insert_survey_item adding user_wpl
    public GenericData insertSurveyItemUserWpl(Int32 OUId, string timeString, DateTime dateTime, string languageDatabaseName, Int32 sa1, Int32 sa2,
          Int32 sa3, Int32 sb1, Int32 sb2, Int32 sb3, Int32 sb4, Int32 sc1, Int32 sc2, Int32 sc3, Int32 sc4, Int32 sc5, Int32 sc6,
          Int32 sd1, Int32 sd2, Int32 sd3, Int32 se1, Int32 se2, string se3)
    {
        GenericData gd = new GenericData();
        Object[] arg = new Object[23] { OUId,  timeString,  dateTime,  languageDatabaseName,  sa1 ,  sa2, sa3,  sb1 ,  sb2 ,  sb3 ,  sb4 ,  sc1 ,
              sc2 ,  sc3 ,  sc4 ,  sc5 ,  sc6 , sd1 ,  sd2 ,  sd3 ,  se1 ,  se2 ,  se3 };
        StoredProcedureGrab storedprocedure = new StoredProcedureGrab(serverName, dbName, userName, passWord, "sp_ws_insert_survey_item_user_wpl", arg);
        spData = storedprocedure.GetReader();

        if (!spData.getErrorStatus())
        {
            gd.setGenericData(spData.getTheOnlyValue());
        }
        else
        {
            gd.setGenericData(spData.getErrorDetails());
        }

        return gd;
    }
    //sp_ws_insert_survey_item adding user_wpl


    public List<SurveyItemData> getAllSurveyItems(string osvDate1, string osvDate2)
    {
        List<SurveyItemData> svItemList = new List<SurveyItemData>();
        //DateTime osvDate1T = new DateTime(int.Parse(osvDate1.Split('-')[0]), int.Parse(osvDate1.Split('-')[1]), int.Parse(osvDate1.Split('-')[2]));
        //DateTime osvDate2T = new DateTime(int.Parse(osvDate2.Split('-')[0]), int.Parse(osvDate2.Split('-')[1]), int.Parse(osvDate2.Split('-')[2]));
        //DateTime osvDate1T = new DateTime(2013, 1, 1);
        //DateTime osvDate2T = new DateTime(2016,1,1);
        osvDate1 = String.IsNullOrEmpty(osvDate1) ? "2006-01-01" : osvDate1;
        osvDate2 = String.IsNullOrEmpty(osvDate2) ? (DateTime.Now.ToString("yyyy-M-d")) + " 23:59:59" : osvDate2; //to include today (DateTime.Now.ToString("yyyy-M-d"))
        object[] arg = new Object[2] { osvDate1, osvDate2 };
        StoredProcedureGrab storedprocedure = new StoredProcedureGrab(serverName, dbName, userName, passWord, "sp_ws_get_all_survey_items", arg);
      spData = storedprocedure.GetReader();

      if (!spData.getErrorStatus())
      {
        foreach (DataRow row in spData.getDataTable().Rows)
        {

            var tempsvItem = new SurveyItemData();
            tempsvItem.setSurvyItemData(
                (Int64)row[0],
                (int)row[1],
                (string)row[2],
                String.Format("{0:M/d/yyyy HH:mm:ss}", row[3]),
                (string)row[4],
                (int)row[5],
                (int)row[6], (int)row[7], (int)row[8], (int)row[9],
                (int)row[10], (int)row[11], (int)row[12], (int)row[13], (int)row[14], (int)row[15], (int)row[16], (int)row[17], (int)row[18], (int)row[19], (int)row[20], (int)row[21], (int)row[22], (string)row[23], (int)row[24], (int)row[25], (int)row[26]);
                
                
            svItemList.Add(tempsvItem);
        }
      }
      else
      {
          var tempsvItem = new SurveyItemData();
          tempsvItem.setErrorData(spData.getErrorDetails());
          svItemList.Add(tempsvItem);
      }


      return svItemList;
    }

    //sp_ws_insert_test_recovery_item
    public GenericData insertTestRecoveryItem(Int32 OUId, Int32 langId, Int32 modId, Int64 sessionId, String startTime, String testletNames, String lastSubmittedSet)
    {
        GenericData gd = new GenericData();
        Object[] arg = new Object[7] { OUId, langId, modId, sessionId, startTime, testletNames, lastSubmittedSet };
        StoredProcedureGrab storedprocedure = new StoredProcedureGrab(serverName, dbName, userName, passWord, "sp_ws_insert_test_recovery_item", arg);
        spData = storedprocedure.GetReader();

        if (!spData.getErrorStatus())
        {
            gd.setGenericData(spData.getTheOnlyValue());
        }
        else
        {
            gd.setGenericData(spData.getErrorDetails());
        }

        return gd;
    }
      //sp_ws_insert_test_recovery_item



    
  }
}