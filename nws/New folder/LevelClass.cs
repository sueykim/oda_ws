using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace oda2
{
  public class LevelClass
  {
    
    private List<TestletData> testlets;
    private Boolean cantUseLevelBool;
       
    public LevelClass()
    {
      testlets = new List<TestletData>();
      cantUseLevelBool = true;
    }

    public void add(TestletData tlt)
    {
      testlets.Add(tlt);
      updateCantUse();
    }

    public TestletData getTestlet()
    {
      int index = CommonClass.foundHigh() ? getLowMidIndex() : getAnyIndex();
      testlets[index].setIsUsed(true);
      updateCantUse();
      updateFoundHigh(testlets[index].levelRange);
      return testlets[index];
    }

    public void shuffle()
    {
      //shufffle testlet list, put high at the botton
      List<TestletData> high = testlets.FindAll(
        delegate(TestletData tlt)
        {
          return (tlt.levelRange == "H");
        }
      );
      testlets.RemoveAll(
        delegate(TestletData tlt)
        {
          return (tlt.levelRange == "H");
        }
      );
      testlets.Shuffle();
      high.Shuffle();
      testlets.AddRange(high);
    }

    public Boolean cantuselevel
    {
      get 
      {
        updateCantUse();
        return cantUseLevelBool; 
      }
    }

    public void clear()
    {
      testlets.Clear();
      updateCantUse();
    }

    //===================================================
    //=========== PRIVATE FUNCTIONS ================/////
    private int getAnyIndex()
    {
      int result = testlets.FindIndex(
        delegate(TestletData tlt)
        {
          return tlt.isUsed == false;
        }
      );

      return result;
    }

    private int getLowMidIndex()
    {
      int result = testlets.FindIndex(
        delegate(TestletData tlt)
        {
          return ((tlt.isUsed == false) && (tlt.levelRange != "H"));
        }
      );

      return result;
    }

    private void updateFoundHigh(string levelrange)
    {
      if (levelrange == "H")
      {
        CommonClass.setFoundHigh(true);
      }
    }

    private void updateCantUse()
    {
      Boolean flag = true;
      TestletData td;

      if (CommonClass.foundHigh())
      {
        td = testlets.Find
        (
          delegate(TestletData t)
          {
            return ((t.levelRange != "H") &&
                    (!t.isUsed));
          }
        );
      }
      else
      {
        td = testlets.Find
        (
          delegate(TestletData t)
          {
            return (!t.isUsed);
          }
        );
      }  

      if (td != null)
      {
        flag = false;
      }

      /*
      if (CommonClass.foundHigh())
      {
        for (int i = 0; i < testlets.Count; i++)
        {
          if ((testlets[i].levelRange != "H") && (!testlets[i].isUsed))
          {
            flag = false;
            break;
          }
        }
      }
      else
      {
        for (int i = 0; i < testlets.Count; i++)
        {
          if (!testlets[i].isUsed)
          {
            flag = false;
            break;
          }
        }
      }
      */

      cantUseLevelBool = flag;
    }

  }
}  