using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace oda2
{
  public class TopicClass
  {

    private Hashtable levels;
    private Boolean topicTurnBol;

    public  TopicClass() 
    {
      levels = new Hashtable();
      setTopicTurn(false);
    }
    
    public void add(TestletData tlt)
    {
      if (!levels.ContainsKey(tlt.level))
      {
        levels.Add(tlt.level, new LevelClass());
      }
      ((LevelClass)levels[tlt.level]).add(tlt);

    }

    public TestletData getTestlet()
    {
      string lvl = CommonClass.getCurrentLevel();
      LevelClass l = ((LevelClass)levels[lvl]);
      TestletData td;

      td = l.getTestlet();

      setTopicTurn(false);
      return td;
    }

    public void shuffle()
    {
      foreach (LevelClass l in levels.Values)
      {
        l.shuffle();
      }
    }

    public void clear()
    {
      foreach(LevelClass lc in levels.Values)
      {
        lc.clear();
      }
      levels.Clear();
      setTopicTurn(false);
    }

    public void resetTopicTurn()
    {
      if (!cantUseTopic())
      {
        setTopicTurn(true);
      }
      else
      {
        setTopicTurn(false);
      }
    }

    public Boolean cantUseTopic()
    {
      string lvl = CommonClass.getCurrentLevel();
      if(levels.ContainsKey(lvl))
      {
        return ((LevelClass)levels[lvl]).cantuselevel;
      }
      else
      {
        return true;
      }
      
    }
    
    public Boolean topicTurn
    {
      get { return topicTurnBol; }
      set { setTopicTurn(value);}
    }

    //===================================================
    //=========== PRIVATE FUNCTIONS ================/////
    private void setTopicTurn(Boolean flag)
    {
      topicTurnBol = flag;
    }
  }
}  