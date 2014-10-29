using System;
using System.Collections.Generic;
using System.Text;

namespace oda2
{
  public class TestletData
  {
    private string nameStr;
    private string topicStr;
    private string categoryStr;
    private Boolean isUsedBol;
    private string levelStr;
    private string levelRangeStr;


    public TestletData() // empty one
    {
      nameStr = "";
      topicStr = "";
      categoryStr = "";
      isUsedBol = false;
      levelStr = "";
      levelRangeStr = "";
    }

    public TestletData(string testletName) // TLT_arb_cul011_M2P
    {
      if (!String.IsNullOrEmpty(testletName))
      {
        var _tn = testletName.Split('_');
        nameStr = testletName;
        topicStr = _tn[2].Substring(0, 3);
        categoryStr = CommonClass.getCateg(topicStr);
        isUsedBol = false;
        levelStr = _tn[3].Substring(1, 2);
        levelRangeStr = _tn[3].Substring(0, 1);
      }
      else
      {
        throw new System.ArgumentException("Testlet Name is Null");
      }

    }

    public string name
    {
      get { return nameStr; }
    }

    public string topic
    {
      get { return topicStr; }
    }

    public string category
    {
      get { return categoryStr; }
    }

    public Boolean isUsed
    {
      get { return isUsedBol; }
    }

    public string level
    {
     get { return levelStr; }
    }

    public string levelRange
    {
      get { return levelRangeStr; }
    }

    public void setIsUsed(Boolean val)
    {
      isUsedBol =  val;
    }

    public string getItems(string dbName)
    {
      StringBuilder sb = new StringBuilder(); 
      DB db = new DB(dbName);
      string cleanname = nameStr.Split('_')[1] + "_" + nameStr.Split('_')[2];
      List<TestletItemData> tidl =  db.getTestletItemData(cleanname);

      for(int i=0;i<tidl.Count;i++)
      {
        /*
         
              wl( spf( '  <div class="css_screen" qn="~" itemType="~" function="~" subfunction="~" LSFeature="~">', b ) )
              ws( a.childNodes[0].childNodes[0].nodeValue )
              ws( a.childNodes[1].childNodes[0].nodeValue )
              wl( '  </div>' )
         */
        string[] args = {tidl[i].itm_id, tidl[i].itmp_abbr};
        sb.AppendFormat("  <div class=\"css_screen\" qn=\"qn_{0}\" itemType=\"{1}\" function=\"null\" subfunction=\"null\" LSFeature=\"null\">", args);
        sb.AppendLine(tidl[i].itm_main_text);
        sb.AppendLine(tidl[i].itm_audio_buttons);
        sb.AppendLine(tidl[i].itm_question);
        sb.AppendLine("  </div>");
          
      }

      //string xx = sb.ToString();
      return sb.ToString();
    }

  }
}  