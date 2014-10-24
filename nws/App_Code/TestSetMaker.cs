using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace oda2
{
  public class TestSetMaker
  {
    private String _dbName;
    private List<String> tltNames;
    private Hashtable categs = new Hashtable();
    private Hashtable testList = new Hashtable();
    private CategoryClass currCateg; //current category class
    private Hashtable freq = new Hashtable();
    private Int64 _sessionId;
    //for 2nd database
    private Boolean mixed = false;
    private String _dbName2;
    private List<String> tltNames2;
    private Hashtable categs2 = new Hashtable();
    private Hashtable testList2 = new Hashtable();

    //for combined list
    private int checkC = 0;
    private Hashtable testList3 = new Hashtable();
    private String tlt2Nameabb = "kor"; //for nkorean 

    private StringBuilder sbOUT = new StringBuilder();
   
    /**/string output = "";
    string testletNamesForRecovery = "";

    public TestSetMaker(string dbName, Int64 sessionId) //categs:[ "cul|soc", "pol|ecn", ... ]
    {
      _dbName = dbName;
      _sessionId = sessionId;
      if (_dbName.ToString().IndexOf("nkorean") > -1)
      {
        mixed = true;
        string hname =  _dbName.ToString().IndexOf("oda2") > -1 ? "oda2" : (_dbName.ToString().IndexOf("oda3") >-1) ? "oda3" :"oda";
        string dskill = _dbName.ToString().IndexOf("rc") > -1 ? "rc" : "lc";
        _dbName2 = hname + "_korean_" + dskill;
        /**/ //output += "mixed = " + mixed + " --- _dbName2 = " + _dbName2;
      }
      /*
       * used to validate the shuffle and pick routine
       * get percent of testlets among group 
       *
       * 
       */
      //tltNames = getTestletsFromDB();
      //use this to 
      //getPercentAmongGroup();
       
      
      /*
      for (int i=0;i<0;i++)//how many times to generate the test
      {
        start_test();
     
        addtpfreq(HtestList, freq);
        testList.Clear();
      }
      */
      //normal start 
      start_test();
      insertTestRecoveryItem();
    }


    //===================================================
    //=========== PRIVATE FUNCTIONS ================/////
    private void start_test()
    {
      foreach(CategoryClass cs in categs.Values)
        {
          cs.clear();
        }
      if (mixed)
      {
        foreach (CategoryClass cs in categs2.Values)
            {
              cs.clear();
            }       
      }
      tltNames = getTestletsFromDB(_dbName);
      if (mixed)
      {
           List<string> tltnameall2 = tltNames.FindAll(
                        delegate(String name)
                        {
                            return name.IndexOf(tlt2Nameabb) > -1;
                        });
          tltNames2 = tltnameall2;
          List<string> tltnameall = tltNames.FindAll(
                        delegate(String name)
                        {
                            return name.IndexOf(tlt2Nameabb) < 0;
                        });
          tltNames = tltnameall;
         // tltNames2 = getTestletsFromDB(_dbName2);
         
      }
     
      distributeTestlets(tltNames, categs);
      if (mixed)
          distributeTestlets(tltNames2, categs2);
      shuffleAllLevels();
      getTestList();

      //=> debug testing getitems
      IEnumerable<string> kys = testList.Keys.Cast<string>().OrderBy(k => k).Select(k => k);     

      if (mixed)
      {
          IEnumerable<string> kys2 = testList2.Keys.Cast<string>().OrderBy(k => k).Select(k => k);
          
          buildMixedTestSet();
          //output += "IEnumerable mixed ---";
          
      }
 
      //StringBuilder sb = new StringBuilder();
      sbOUT.AppendLine( "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" );
      sbOUT.AppendLine( "<root>" );
      sbOUT.AppendLine( "<![CDATA[" );
      if (mixed)
      {
          foreach (string ky in kys)
          {
              sbOUT.AppendFormat("<form id=\"id_L{0}_0\">", ky);
              sbOUT.Append(CommonClass.getIntroScreen(_dbName.Split('_')[2]));
              List<TestletData> tdl = (List<TestletData>)testList3[ky];
              testletNamesForRecovery += ky + ":";
              int y = 0;
              for (; y < 3; y++)
              {
                  testletNamesForRecovery += tdl[y].name + ",";
                  if (tdl[y].name.IndexOf(tlt2Nameabb) > -1)
                      sbOUT.Append(tdl[y].getItems(_dbName)); 
                  else
                      sbOUT.Append(tdl[y].getItems(_dbName));
              }
              sbOUT.AppendLine("</form>");


              sbOUT.AppendFormat("<form id=\"id_L{0}_1\">", ky);
              sbOUT.Append(CommonClass.getIntroScreen(_dbName.Split('_')[2]));
              for (; y < tdl.Count; y++)
              {
                  string tlname_end = y == tdl.Count - 1 ? "|" : ",";
                  testletNamesForRecovery += tdl[y].name + tlname_end;
                  if (tdl[y].name.IndexOf(tlt2Nameabb) > -1)
                      sbOUT.Append(tdl[y].getItems(_dbName));
                  else
                      sbOUT.Append(tdl[y].getItems(_dbName));
              }
              sbOUT.AppendLine("</form>");
          };
      }
      else
      {
          foreach (string ky in kys)
          {
              sbOUT.AppendFormat("<form id=\"id_L{0}_0\">", ky);
              sbOUT.Append(CommonClass.getIntroScreen(_dbName.Split('_')[2]));
              List<TestletData> tdl = (List<TestletData>)testList[ky];
              testletNamesForRecovery += ky + ":";
              int y = 0;
              for (; y < 3; y++)
              {
                  testletNamesForRecovery += tdl[y].name + ",";
                  sbOUT.Append(tdl[y].getItems(_dbName));
              }
              sbOUT.AppendLine("</form>");


              sbOUT.AppendFormat("<form id=\"id_L{0}_1\">", ky);
              sbOUT.Append(CommonClass.getIntroScreen(_dbName.Split('_')[2]));
              for (; y < tdl.Count; y++)
              {
                  string tlname_end = y == tdl.Count - 1 ? "|" : ",";
                  testletNamesForRecovery += tdl[y].name + tlname_end;
                  sbOUT.Append(tdl[y].getItems(_dbName));
              }
              sbOUT.AppendLine("</form>");
          }
      }
      sbOUT.AppendLine(CommonClass.getClosingPage());

      
      sbOUT.AppendLine( "]]>" );
      sbOUT.AppendLine( "</root>" );
      //output += sbOUT.ToString();
    }

    private void add(TestletData tlt, Hashtable ctgs)
    {
      if (!ctgs.ContainsKey(tlt.category))
      {
        ctgs.Add(tlt.category, new CategoryClass());
      }
      ((CategoryClass)ctgs[tlt.category]).add(tlt);
    }
    
    private List<String> getTestletsFromDB(string dbN)
    {
      DB db = new DB(dbN);
      return db.getTestletsNames();
    }

    private void distributeTestlets(List<string> tltNMs, Hashtable ctgs)
    {
        foreach (string name in tltNMs)
      {
        TestletData temp = new TestletData(name);
        CommonClass.pushLevel(temp.level);
        add(temp, ctgs);
      }
    }

    private void shuffleAllLevels()
    {
      foreach(CategoryClass cc in categs.Values)
      {
        cc.shuffleAllTopics();
      }
      if (mixed)
      {
          foreach (CategoryClass cc in categs2.Values)
          {
              cc.shuffleAllTopics();
          }
      }
    }

    private void getTestList()
    {
      string[] lvlsArr =  CommonClass.levelsArr;
      for(var i=0;i<lvlsArr.Length;i++)
      {
        CommonClass.setCurrentLevel(lvlsArr[i]) ;
        if ( !testList.ContainsKey(lvlsArr[i]) )
        {
          testList.Add(lvlsArr[i], new List<TestletData>());
        }
        doLevel();
      }
      if (mixed)
      {
          string[] lvlsArr2 = CommonClass.levelsArr;
          for (var i = 0; i < lvlsArr2.Length; i++)
          {
              CommonClass.setCurrentLevel(lvlsArr2[i]);
              if (!testList2.ContainsKey(lvlsArr2[i]))
              {
                  testList2.Add(lvlsArr2[i], new List<TestletData>());
                  testList3.Add(lvlsArr2[i], new List<TestletData>());
              }
              doLevel2();
          }         
      }
    }

    //keeping only the categs used, might not be all of them,
    //keeping the order of categories array in common (very imortant to get at least one culture)
    private string[] getOnlyUsedCategories(Hashtable ctgs)
    {
      List<string> R = new List<string>();
      for (int i = 0; i < CommonClass.categoriesArr.Length; i++)
      {
        if (ctgs.ContainsKey(CommonClass.categoriesArr[i]))
        {
          R.Add(CommonClass.categoriesArr[i]);
        }
      }

      return R.ToArray();
    }

    private void resetAllTurns(Hashtable ctgs)
    {
      foreach(CategoryClass cs in ctgs.Values)
      {
        cs.resetAllturns();
      }
    }

    private void doLevel()
    {
      //string[] categs = CommonClass.categoriesArr;
      string[] orderedCategs = getOnlyUsedCategories(categs); //only categs that are in the current testlsets 
      List<TestletData> tl = (List<TestletData>)testList[CommonClass.getCurrentLevel()]; //pointer to store testlest for curr level
      int i = 0;
      
      resetAllTurns(categs);
      for(; (tl.Count < 6) ; )
      {
        currCateg = ((CategoryClass)categs[orderedCategs[i]]); //current category
        CommonClass.setFoundHigh(false);//reset found high
        if  ( !currCateg.cantUseCategory() && currCateg.categoryTurn )
        {
          TestletData tmp = currCateg.getTestlet();
          if (!String.IsNullOrEmpty(tmp.name))
          {
            tl.Add(tmp);
          }
          updateCategoriesTurnIfAllVisited(categs);
        }
        i = updateIndex(i, orderedCategs.Length);
      }

      //clean up selection
      //shuffle all 6
      /*
      output = output + "--- ---"  + CommonClass.getCurrentLevel();
      output = output + "--- BEG BEFORE RE ORDER ---";
      foreach(TestletData tlt in tl)
      {
        output = output + tlt.name + "---";
      }
      output = output + "   --- END BEFORE RE ORDER  ---";
      //Console.WriteLine(output);
      */
      tl.Shuffle();


      //check if we got a full 6 testlets
      foreach(TestletData t in tl)
      {
        if (String.IsNullOrEmpty(t.name))
        {
          throw new System.ArgumentException("Testlet Name is Null");
        }
      }

      // final ordering - taking care of multiple tlt per topic
      reOrderTLTs(getCounterForAllTopics(testList));

      
      //output = output + "--- BEG after RE ORDER L1---";
      foreach(TestletData tlt in tl)
      {
        output = output + tlt.name + "---";
      }
      //output = output + "   --- END after RE ORDER  ---";
      //Console.WriteLine(output);
      
    }

    private void doLevel2()
    {
        //string[] categs = CommonClass.categoriesArr;
        string[] orderedCategs = getOnlyUsedCategories(categs2); //only categs that are in the current testlsets 
        List<TestletData> tl = (List<TestletData>)testList2[CommonClass.getCurrentLevel()]; //pointer to store testlest for curr level
        int i = 0;

        resetAllTurns(categs2);
        for (; (tl.Count < 6); )
        {
            currCateg = ((CategoryClass)categs2[orderedCategs[i]]); //current category
            CommonClass.setFoundHigh(false);//reset found high
            if (!currCateg.cantUseCategory() && currCateg.categoryTurn)
            {
                TestletData tmp = currCateg.getTestlet();
                if (!String.IsNullOrEmpty(tmp.name))
                {
                    tl.Add(tmp);
                }
                updateCategoriesTurnIfAllVisited(categs2);
            }
            i = updateIndex(i, orderedCategs.Length);
        }

        //clean up selection
        //shuffle all 6
        /*
        output = output + "--- ---" + CommonClass.getCurrentLevel();
        output = output + "--- BEG BEFORE RE ORDER ---";
        foreach (TestletData tlt in tl)
        {
            output = output + tlt.name + "---";
        }
        //output = output + "   --- END BEFORE RE ORDER  ---";
        //Console.WriteLine(output);
        */
        tl.Shuffle();


        //check if we got a full 6 testlets
        foreach (TestletData t in tl)
        {
            if (String.IsNullOrEmpty(t.name))
            {
                throw new System.ArgumentException("Testlet Name is Null");
            }
        }

        // final ordering - taking care of multiple tlt per topic
        reOrderTLTs2(getCounterForAllTopics(testList2));

        /*
        output = output + "--- BEG after RE ORDER ---";
        foreach (TestletData tlt in tl)
        {
            output = output + tlt.name + "---";
        }
        //output = output + "   --- END after RE ORDER  ---";
        //Console.WriteLine(output);
        */
    }

    private void buildMixedTestSet()
    {
        IEnumerable<string> kys3 = testList2.Keys.Cast<string>().OrderBy(k => k).Select(k => k);
        foreach (string ky in kys3)
        {
            List<TestletData> tdl2 = (List<TestletData>)testList2[ky];
            List<TestletData> tdl = (List<TestletData>)testList[ky];
            //testList3.Add(ky, new List<TestletData>());
            List<TestletData> tl = (List<TestletData>)testList3[ky];            

            //to find if Range "H" exists in testList2
            var h = tdl2.Find(
            delegate(TestletData tlt)
            {
                return tlt.levelRange == "H";
            });

            //to remove "H" from testList if "H" exists in testList2 && if "H" exists in testList
            if (h != null)
            {
                foreach (TestletData tlt in tdl)
                {
                    if (tlt.levelRange == "H")
                    {
                        TestletData tmp = tlt;
                        if (!String.IsNullOrEmpty(tmp.name))
                        {
                            tdl.Remove(tmp);
                        }
                    }
                }
            }
            //to mix 2 nkr and 4 kor
            checkC = 0;
            tdl.Shuffle();
            tdl2.Shuffle();
            while (tdl2.Count > 0)
            {
                if (checkC < 2)
                {
                    var tmp2 = tdl2.First<TestletData>();
                    var currTopic = tmp2.topic;

                    List<TestletData> all = tdl.FindAll(
                        delegate(TestletData tlt)
                        {
                            return tlt.topic == currTopic;
                        });
                    if (all.Count > 0)
                    {
                        TestletData tmp = all.First<TestletData>();
                        if (!String.IsNullOrEmpty(tmp.name))
                        {
                            tl.Add(tmp);
                            tdl.Remove(tmp);
                            tdl2.Remove(tmp2);
                            checkC++;
                            if (checkC == 2 && tdl2.Count > 0)
                            {
                                while (tdl2.Count > 0)
                                {
                                    TestletData tmp3 = tdl2.First<TestletData>();
                                    tl.Add(tmp3);
                                    tdl2.Remove(tmp3);
                                }
                            }
                        }
                    }
                    else
                    {
                        tl.Add(tmp2);
                        tdl2.Remove(tmp2);
                    }
                }
            }
            if (tdl2.Count == 0 && checkC < 2)
            {
                while (checkC < 2)
                {
                    List<TestletData> all = tl.FindAll(
                        delegate(TestletData tlt)
                        {
                            return tlt.name.IndexOf(tlt2Nameabb) > -1;
                        });
                    if (all.Count > 0)
                    {
                        TestletData tmp = all.First<TestletData>();
                        TestletData tmp2 = tdl.First<TestletData>();
                        if (!String.IsNullOrEmpty(tmp.name))
                        {
                            tl.Remove(tmp);
                            tl.Add(tmp2);
                            tdl.Remove(tmp2);
                            checkC++;
                        }
                    }
                }
            }

            //check if we got a full 6 testlets
            foreach (TestletData t in tl)
            {
                if (String.IsNullOrEmpty(t.name))
                {
                    throw new System.ArgumentException("Testlet Name is Null");
                }
            }


            reOrderTLTs3(getCounterForAllTopics3(testList3, ky), ky);
            output = output + " ---" + ky + "--- ";
            // final ordering - taking care of multiple tlt per topic           
            /**/
            output = output + "--- BEG AFTER RE ORDER ---";
            foreach (TestletData tlt in tl)
            {
                output = output + tlt.name + "---";
            }
            output = output + "   "; //--- END AFTER RE ORDER  ---"; 
             /**/
        }        
    }

    private void reOrderTLTs(List<DictionaryEntry> desc)
    {
      List<TestletData> tl = (List<TestletData>)testList[CommonClass.getCurrentLevel()]; //pointer to store testlest for curr level
      List<TestletData> g1 = new List<TestletData>(); //group 1
      List<TestletData> g2 = new List<TestletData>();
      Boolean g1turn = true;

      for (int i=0;i<desc.Count;i++)
      {
        //get all tlt for this categ
        List<TestletData> all = tl.FindAll(
          delegate(TestletData tlt)
          {
            return tlt.topic == (string)desc[i].Key;
          } );


        //loop throug store in the two groups, topics with tlts > 1 will go first since 
        //desc is desc ordered 
        /*
         * alternative solutions: recreate the tl in desc order then store in the order (1 3 5) (2 4 6)
         * this is better if lenghth is changed from 6
         */
        foreach(TestletData tlt in all)
        {
          if (g1turn)
          {
            g1.Add(tlt);
            g1turn = false;
          }
          else
          {
            g2.Add(tlt);
            g1turn = true;
          }
        }
      }


      //now find the high and put the set with the high as the second one
      tl.Clear();
      g1.Shuffle();
      g2.Shuffle();

      var h = g1.Find(
        delegate(TestletData tlt)
        {
          return tlt.levelRange == "H";
        });

      if (h == null)
      {
        tl.AddRange(g1);
        tl.AddRange(g2);
      }
      else
      {
        tl.AddRange(g2);
        tl.AddRange(g1);
      }
    }// reOrderTLTs

    private void reOrderTLTs2(List<DictionaryEntry> desc)
    {
        List<TestletData> tl = (List<TestletData>)testList2[CommonClass.getCurrentLevel()]; //pointer to store testlest for curr level
        List<TestletData> g1 = new List<TestletData>(); //group 1
        List<TestletData> g2 = new List<TestletData>();
        Boolean g1turn = true;

        for (int i = 0; i < desc.Count; i++)
        {
            //get all tlt for this categ
            List<TestletData> all = tl.FindAll(
              delegate(TestletData tlt)
              {
                  return tlt.topic == (string)desc[i].Key;
              });


            //loop throug store in the two groups, topics with tlts > 1 will go first since 
            //desc is desc ordered 
            /*
             * alternative solutions: recreate the tl in desc order then store in the order (1 3 5) (2 4 6)
             * this is better if lenghth is changed from 6
             */
            foreach (TestletData tlt in all)
            {
                if (g1turn)
                {
                    g1.Add(tlt);
                    g1turn = false;
                }
                else
                {
                    g2.Add(tlt);
                    g1turn = true;
                }
            }
        }


        //now find the high and put the set with the high as the second one
        tl.Clear();
        g1.Shuffle();
        g2.Shuffle();

        var h = g1.Find(
          delegate(TestletData tlt)
          {
              return tlt.levelRange == "H";
          });

        if (h == null)
        {
            tl.AddRange(g1);
            tl.AddRange(g2);
        }
        else
        {
            tl.AddRange(g2);
            tl.AddRange(g1);
        }
    }// reOrderTLTs2

    private void reOrderTLTs3(List<DictionaryEntry> desc, String KY)
    {
        List<TestletData> tl = (List<TestletData>)testList3[KY]; //pointer to store testlest for curr level
        List<TestletData> g1 = new List<TestletData>(); //group 1
        List<TestletData> g2 = new List<TestletData>();       

        Boolean g1turn = true;

        for (int i = 0; i < desc.Count; i++)
        {
            //get all tlt for this categ
            List<TestletData> all = tl.FindAll(
              delegate(TestletData tlt)
              {
                  return tlt.topic == (string)desc[i].Key;
              });


            //loop throug store in the two groups, topics with tlts > 1 will go first since 
            //desc is desc ordered 
            /*
             * alternative solutions: recreate the tl in desc order then store in the order (1 3 5) (2 4 6)
             * this is better if lenghth is changed from 6
             */
            foreach (TestletData tlt in all)
            {
                if (g1turn)
                {
                    g1.Add(tlt);
                    g1turn = false;
                }
                else
                {
                    g2.Add(tlt);
                    g1turn = true;
                }
            }
        }

        //to find if the nkr distributed one for each set
        List<TestletData> g1all = g1.FindAll(
                delegate(TestletData tlt)
                {
                    return !(tlt.name.IndexOf(tlt2Nameabb) > -1);
                });

       // output += "g1all.Count: " + g1all.Count + " --- ";
        List<TestletData> g2all = g2.FindAll(
            delegate(TestletData tlt)
            {
                return !(tlt.name.IndexOf(tlt2Nameabb) > -1);
            });
       //output += "g2all.Count: " + g2all.Count + " --- ";
        
        if (g1all.Count != 1)
        {
            TestletData tmp3 = null;
            TestletData tmp4 = null;
           if (g1all.Count == 2)
            {
                tmp3 = g1all.Last<TestletData>();
                tmp4 = g2.Last<TestletData>();
                addRemoveTestLets(g1, tmp3, g2, tmp4);
            }
           else //Sif (g1all.Count == 0) //g1 has 0 nkr
           {
               tmp4 = g2all.Last<TestletData>();
               tmp3 = g1.Last<TestletData>();
               addRemoveTestLets(g1, tmp3, g2, tmp4);
           }
        }
        //now find the high and put the set with the high as the second one
        tl.Clear();
        g1.Shuffle();
        g2.Shuffle();

        var h = g1.Find(
            delegate(TestletData tlt)
            {
                return tlt.levelRange == "H";
            });

        if (h == null)
        {
            tl.AddRange(g1);
            tl.AddRange(g2);
        }
        else
        {
            tl.AddRange(g2);
            tl.AddRange(g1);
        }
        /*

        output = output + "--- BEG AFTER RE ORDER --- " ;
        foreach (TestletData tlt in tl)
        {
            output = output + tlt.name + "---";
        }
        output = output + "   --- END AFTER RE ORDER  ---";
        */
    }// reOrderTLTs3

    private void addRemoveTestLets(List<TestletData> G1, TestletData TMP, List<TestletData> G2, TestletData TMP2)
    {
        if (!String.IsNullOrEmpty(TMP.name) && !String.IsNullOrEmpty(TMP2.name))
        {
            G1.Remove(TMP);
            G2.Remove(TMP2);
            G1.Add(TMP2);
            G2.Add(TMP);
        }
    }
    private List<DictionaryEntry> getCounterForAllTopics(Hashtable tstLst)
    {
      List<TestletData> tl = (List<TestletData>)tstLst[CommonClass.getCurrentLevel()]; //pointer to store testlest for curr level
      Hashtable counter =  new Hashtable();
      //List<string> removals = new List<string>();

      //get count for all topics
      foreach (TestletData tlt in tl)
      {
        if (!counter.ContainsKey(tlt.topic))
        {
          counter.Add(tlt.topic, 0);
        }
        counter[tlt.topic] = (int)counter[tlt.topic] + 1;
      }

      //order desc then retrun
      return counter.Cast<DictionaryEntry>().OrderByDescending(entry => entry.Value).ToList();
      /*
      output = output + "--- BEG ---";
      for(int i=0;i<l.Count;i++)
      {
        output =  output + l[i].Key + " = " + l[i].Value + " --- ";
      }

      output = output + "   --- END ---";
      return l;
      
      //remove counts that are 1, keep > 1
      foreach (string s in counter.Keys)
      {
        if ((int)counter[s] == 1)
        {
          removals.Add(s);
        }
      }
      foreach( string s in removals)
      {
        counter.Remove(s);
      }
      */
            
      /*
      //printing it out to screen - debugging
      output = output + "BEG ---";
      foreach (DictionaryEntry d in counter)
      {
        output = output + d.Key + " = ";
        output = output + d.Value + " --- ";
      }
      output = output + "END ---";
       

      //printing it out to screen - debugging
      output = output + "--- AFTER:";
      foreach (DictionaryEntry d in counter)
      {
        output = output + d.Key + " = ";
        output = output + d.Value + " --- ";
        
      }
      */
    }

    private List<DictionaryEntry> getCounterForAllTopics3(Hashtable tstLst, String KY)
    {
        List<TestletData> tl = (List<TestletData>)tstLst[KY]; //pointer to store testlest for curr level
        Hashtable counter = new Hashtable();
        //List<string> removals = new List<string>();

        //get count for all topics
        foreach (TestletData tlt in tl)
        {
            if (!counter.ContainsKey(tlt.topic))
            {
                counter.Add(tlt.topic, 0);
            }
            counter[tlt.topic] = (int)counter[tlt.topic] + 1;
        }

        //order desc then retrun
        return counter.Cast<DictionaryEntry>().OrderByDescending(entry => entry.Value).ToList();

    }

    private void updateCategoriesTurnIfAllVisited(Hashtable ctgs)
    {
      Boolean allVisited =  true;
      foreach(CategoryClass cs in ctgs.Values)
      {
        if (cs.categoryTurn)
        {
          allVisited = false;
        }
      }

      if (allVisited)
      {
        foreach(CategoryClass cs in ctgs.Values)
        {
          cs.resetCAtegoryTurn();
        }
      }
    }
    
    private int updateIndex(int i, int max)
    {
      return i == (max-1) ? 0 : (i+1);
    }

    private Boolean IsOdd(int value)
    {
	    return value % 2 != 0;
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
    /*
    //get percent of testlets among group
    private void getPercentAmongGroup()
    {
      SortedDictionary<string, List<string>> lvlsTpc =  new SortedDictionary<string, List<string>>();

      foreach (string name in tltNames)
      {
        TestletData temp = new TestletData(name);
        string ky = ""+temp.level+"_"+temp.topic;
        if (!lvlsTpc.ContainsKey(ky))
        {
          lvlsTpc.Add(ky, new List<string>());
        }
        ((List<string>)lvlsTpc[ky]).Add(temp.name);
      }
      
      

      foreach (KeyValuePair<string, List<string>> pair in lvlsTpc)
      {
        int len = ((List<string>)pair.Value).Count;
        output = output + "\n" + pair.Key + " : " + decimal.Round((decimal)(1.00 / len), 2) ;
        //foreach (string s in (List<string>)pair.Value)
        //{
        //  output = output + s.Split('_')[2] + '_' + s.Split('_')[3] + " : " + decimal.Round((decimal)(1.00 / len), 2) + " \n ";
        //}
      }
    }
    */



    //==>> DEBUGGING FUNCTION <<=========
    
    public void addone(string key, Hashtable frq)
    {
      frq[key] = (int)frq[key] + 1;
    }   

    private void addtpfreq(Hashtable tstLst, Hashtable frq)
    {
        foreach (List<TestletData> td in tstLst.Values)
        {
            foreach (TestletData t in td)
            {
                if (!frq.ContainsKey(t.name))
                {
                    frq.Add(t.name, 1);
                }
                else
                {
                    addone(t.name, frq);
                }
            }
        }
    }
   
    


    public string getTest()
    {
      /*
      foreach (List<TestletData> tds in testList.Values)
      {

        //List<TestletData> tds = (List<TestletData>)testList["3N"];
        foreach(TestletData td in tds)
        {
          output = output + td.name + " - ";
        }
        output = output + "-----";
      }
      

      
      //List<TestletData> tds = (List<TestletData>)testList["3N"];
      output = tds.FindIndex((n) => n.levelRange.IndexOf('H') > -1).ToString();
      return output;
       */
        //return output.ToString();
        
        List<SessionDataForRecovery> _sessiondataRT = getSessionDataBySid(); 

        output += "============================= _sessionId = " + _sessionId + "=====" + _sessiondataRT;
        String txtoutput = testletNamesForRecovery.ToString(); // output.ToString();
        String txtfileName =  "C:\\Users\\Public\\TestFolder\\mixedtest.txt"; //localhost //C:\\inetpub\\wwwroot\\USERS\\Public\\TestFolder\\mixedtest.txt"; //oda3//\\oda1"D:\\Home\\USERS\\Public\\TestFolder\\mixedtest.txt"; //"C:\\Users\\Public\\TestFolder\\mixedtest.txt"; //localhost //"C:\\inetpub\\wwwroot\\USERS\\Public\\TestFolder\\mixedtestoda2.txt"; //oda2
   
        if (System.IO.File.Exists(txtfileName))
          System.IO.File.AppendAllText(@txtfileName, txtoutput);
        else
            System.IO.File.WriteAllText(@txtfileName, txtoutput);

        return sbOUT.ToString();//txtoutput; //sbOUT.ToString();
    }


  } // end class
} // end namespace