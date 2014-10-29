using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace oda2
{
    public class RecoveryTestSetMaker
    {
        private String _dbName;
        private Hashtable testList = new Hashtable();
        private Int64 _sessionId;
        private string _testletNames;
         

        private StringBuilder sbOUT = new StringBuilder();

        /**/
        string output = "";
        string testletNamesForRecovery = "";

        public RecoveryTestSetMaker(string dbName, Int64 sessionId, string testletNames) //categs:[ "cul|soc", "pol|ecn", ... ]
        {
            _dbName = dbName;
            _sessionId = sessionId;
            _testletNames = testletNames;
            start_test();
            insertTestRecoveryItem();
        }


        //===================================================
        //=========== PRIVATE FUNCTIONS ================/////
        private void getRecoveryTestList()
        {

            char[] delimiterChar0 = { '|' };//, ':', ','
            //string[] delimiterChars = new string[] { "|" };//, ':', ','
            
            string xxx =  (string)_testletNames;
            string[] tllvlNnameAry = xxx.Split(delimiterChar0);
            for (var i = 0; i < tllvlNnameAry.Length-1; i++)
            {
                string curString = tllvlNnameAry[i];
                char[] delimiterChar1 = { ':' };
                string[] curlvlNnames = curString.Split(delimiterChar1);
                //string[] curlvlNnames = curString.Split(delimiterChar1);
                string curlvl = curlvlNnames[0];
                char[] delimiterChar2 = { ',' };
                string[] curTestletNMsAry = (curlvlNnames[1]).Split(delimiterChar2);
                CommonClass.setCurrentLevel(curlvl);
                if (!testList.ContainsKey(curlvl))
                {
                    testList.Add(curlvl, new List<TestletData>());
                }
                List<TestletData> tl = (List<TestletData>)testList[curlvl];
                for (var j =0; j < curTestletNMsAry.Length; j++)
                {
                    string nn = (string) curTestletNMsAry[j];
                    TestletData tmp1 = new TestletData(nn);
                    tl.Add(tmp1);
                }
            }
           
        }

        
        private void start_test()
        {

            getRecoveryTestList();

            //=> debug testing getitems
            IEnumerable<string> kys = testList.Keys.Cast<string>().OrderBy(k => k).Select(k => k);

            

            //StringBuilder sb = new StringBuilder();
            sbOUT.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            sbOUT.AppendLine("<root>");
            sbOUT.AppendLine("<![CDATA[");            
            foreach (string ky in kys)
            {
                sbOUT.AppendFormat("<form id=\"id_L{0}_0\">", ky);
                sbOUT.Append(CommonClass.getIntroScreen(_dbName.Split('_')[2]));
                List<TestletData> tdl = (List<TestletData>)testList[ky];               
                int y = 0;
                for (; y < 3; y++)
                {
                    testletNamesForRecovery += tdl[y].name + ", ";
                    sbOUT.Append(tdl[y].getItems(_dbName));
                }
                sbOUT.AppendLine("</form>");


                sbOUT.AppendFormat("<form id=\"id_L{0}_1\">", ky);
                sbOUT.Append(CommonClass.getIntroScreen(_dbName.Split('_')[2]));
                for (; y < tdl.Count; y++)
                {
                  //  testletNamesForRecovery += tdl[y].name + tlname_end;
                    sbOUT.Append(tdl[y].getItems(_dbName));
                }
                sbOUT.AppendLine("</form>");
            }
            sbOUT.AppendLine(CommonClass.getClosingPage());


            sbOUT.AppendLine("]]>");
            sbOUT.AppendLine("</root>");
            //output += sbOUT.ToString();
        }

       
 
        private List<SessionDataForRecovery> getSessionDataBySid()
        {
            DB db = new DB(_dbName);
            List<SessionDataForRecovery> sdList = new List<SessionDataForRecovery>();
            sdList.AddRange(db.getSessionsDataBySessionId(_sessionId));
            return sdList;
        }

        private void insertTestRecoveryItem()
        {
            List<SessionDataForRecovery> _sessionDataForRecovery = getSessionDataBySid();
            int OUId = _sessionDataForRecovery[0].userid;
            int modId = _sessionDataForRecovery[0].modid;
            int langId = _sessionDataForRecovery[0].langid;
            string startTime = _sessionDataForRecovery[0].starttime;
            string testletNames = testletNamesForRecovery.ToString();
            string lastSubmittedSet = "";
            DB db = new DB("oda_master");
            db.insertTestRecoveryItem(OUId, langId, modId, _sessionId, startTime, testletNames, lastSubmittedSet);

        }
       
      
     

        public string getTest()
        {
            

            List<SessionDataForRecovery> _sessiondataRT = getSessionDataBySid();

            output += "============================= _sessionId = " + _sessionId + "=====" + _sessiondataRT;
            String txtoutput = testletNamesForRecovery.ToString(); // output.ToString();
            String txtfileName = "C:\\Users\\Public\\TestFolder\\mixedtest.txt"; //localhost //C:\\inetpub\\wwwroot\\USERS\\Public\\TestFolder\\mixedtest.txt"; //oda3//\\oda1"D:\\Home\\USERS\\Public\\TestFolder\\mixedtest.txt"; //"C:\\Users\\Public\\TestFolder\\mixedtest.txt"; //localhost //"C:\\inetpub\\wwwroot\\USERS\\Public\\TestFolder\\mixedtestoda2.txt"; //oda2

            if (System.IO.File.Exists(txtfileName))
                System.IO.File.AppendAllText(@txtfileName, txtoutput);
            else
                System.IO.File.WriteAllText(@txtfileName, txtoutput);

            return sbOUT.ToString();//txtoutput; //sbOUT.ToString();
        }


    } // end class
} // end namespace
