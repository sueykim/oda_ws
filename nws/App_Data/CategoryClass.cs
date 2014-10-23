using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace oda2
{
  public class CategoryClass
  {
     private Hashtable topics;
     private Boolean categoryTurnBol;
     private ArrayList topicsAry;

     public CategoryClass()
     {
       topics = new Hashtable();
       categoryTurnBol = false;
       topicsAry = new ArrayList();
     }

     public void add(TestletData tlt)
     {
       if (!topics.ContainsKey(tlt.topic))
       {
         topics.Add(tlt.topic, new TopicClass());
         topicsAry.Add(tlt.topic);
       }
       ((TopicClass)topics[tlt.topic]).add(tlt);
       

     }

     public TestletData getTestlet()
     {
         TestletData td = new TestletData();

         ICollection tpcs = topics.Keys;
         string[] tpcsstr = new string[tpcs.Count];
         int i = 0;
         foreach (IEnumerable t in tpcs)
         {
             tpcsstr[i++] = (string) t;
         }
         if (!(tpcsstr[0]  == "cul" || tpcsstr[0]  == "soc"))
         {
             tpcsstr.Shuffle();
         }
         

         for (int x = 0; x < tpcsstr.Length; x++)
         {
             TopicClass tc = (TopicClass)topics[tpcsstr[x]];
             if (tc.topicTurn && !tc.cantUseTopic())
             {
                 td = tc.getTestlet();
                 break;
             }   
         }
         
         setCategoryTurn(false);
         updateTopicsTurnIfAllVisited();
         return td;
     }

/* //== original gettestlet() before shuffle the topics in a category
     public TestletData getTestlet()
     {
       TestletData td= new TestletData();

           foreach (TopicClass tc in topics.Values)
           {
               
               if (tc.topicTurn && !tc.cantUseTopic())
               {
                   td = tc.getTestlet();
                   break;
               }
           }

       setCategoryTurn(false);
       updateTopicsTurnIfAllVisited();
       return td;
     }
*/
     public Boolean cantUseCategory()
     {
       Boolean flag = true;
       foreach (TopicClass tc in topics.Values)
       {
         if (!tc.cantUseTopic())
         {
           flag = false;
           break;
         }
       }

       return flag;
     }

     public void clear()
     {
       foreach(TopicClass tc in topics.Values)
       {
         tc.clear();
       }

       topics.Clear();
       setCategoryTurn(false);
     }
     
     public void shuffleAllTopics()
     {
       foreach(TopicClass tc in topics.Values)
       {
         tc.shuffle();
       }
     }

     public Boolean categoryTurn
     {
       get { return categoryTurnBol; }
     }

     public void resetCAtegoryTurn()
     {
       setCategoryTurn(cantUseCategory() ? false : true);
     }

     public void resetAllturns()
     {
       foreach(TopicClass tc in topics.Values)
       {
         tc.resetTopicTurn();
       }
       resetCAtegoryTurn();
     }
     //===================================================
     //=========== PRIVATE FUNCTIONS ================/////


     private void setCategoryTurn(Boolean flag)
     {
       categoryTurnBol = flag;
     }


     private void updateTopicsTurnIfAllVisited()
     {
         Boolean allVisited = true;
         if (!cantUseCategory())
         {
             foreach (TopicClass tc in topics.Values)
             {
                 if (tc.topicTurn)
            
                 {
                     allVisited = false;
                     break;
                 }
             }

             //all got turn (all turns are false) and still i can use category
             if (allVisited)
             {
                 foreach (TopicClass tc in topics.Values)
                 {
                     tc.resetTopicTurn();
                 }
             }
         }
     }




















      /*


     public  CategoryClass(params string[] list) 
     {
       for (int i=0;i<list.Length;i++)
       {
         topics.Add(list[i].ToString(), new TopicClass()); 
         cantuse.Add(list[i].ToString(), true);
         myturn.Add(list[i].ToString(), true);
       }
       cantusecateg = true;
     }

     private string getUsableTopicTurn()
     {
       string index = "";
       foreach (string k in topics.Keys)
       {
         if(((Boolean)myturn[k] == true) && ((Boolean)cantuse[k] == false) )
         {
           index = k;
           break;
         }
       }
      
       return index;
     }

     private void updateCantUse()
     {
       Boolean flag = true;
       foreach (string k in topics.Keys)
       {
         cantuse[k] = ((TopicClass)topics[k]).emptyOrAllUsed;
         if (!(Boolean)cantuse[k])
         {
           flag = false;
         }
       }
       cantusecateg = flag;
     }

     private void resetMyTurn() //take in account cantuse status
     {
       updateCantUse();
       foreach (string k in topics.Keys)
       {
         if((Boolean)cantuse[k])
         {
           myturn[k] = false;
         }
         else
         {
           myturn[k] = true;
         }
       }
     }

     public void add(TestletData tlt)
     {
       ((TopicClass)topics[tlt.topic]).add(tlt);
       cantuse[tlt.topic] = false;
       myturn[tlt.topic] = true;
       cantusecateg = false;
     }

     private void setMyTurn(string categIndex)
     {
       myturn[categIndex] = false;
       Boolean allGotTurn = true;
       foreach(Boolean b in myturn.Values)
       {
         if (b)
         {
           allGotTurn = false;
         }
       }
       if (allGotTurn)
       {
         resetMyTurn();
       }

     }

     public TestletData getAny()
     {
       string categIndex = getUsableTopicTurn();
       int TltIndex = ((TopicClass)topics[categIndex ]).getAny();
       TestletData td = ((TopicClass)topics[ categIndex]).getTestlet(TltIndex);
       updateCantUse();
       setMyTurn(categIndex);
       return td;
     }

     public TestletData getLowMid()
     {
       string categIndex = getUsableTopicTurn();
       int TltIndex = ((TopicClass)topics[categIndex ]).getLowMid();
       TestletData td = ((TopicClass)topics[ categIndex]).getTestlet(TltIndex);
       updateCantUse();
       setMyTurn(categIndex);
       return td;
     }


     public void clear()
     {
       foreach (string k in topics.Keys)
       {
         ((TopicClass)topics[k]).clear();
       }
       topics.Clear();
       cantuse.Clear();
       myturn.Clear();
       cantusecateg =  true;
     }
        private static Boolean randomTopicOrder()
         {
             Boolean reslt = true;
             Random rnd = new Random();
             int anyNo = rnd.Next(1, 100);
             if (anyNo % 2 == 0)
                 reslt = false;
             return reslt;
         } 
       * */
  }
}  