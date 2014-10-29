using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace oda2
{
  public class CommonClass
  {
    private static string[] categoriesArray =  { "cul|soc", "pol|ecn", "geo|env", "mil|sec", "sci|tec" } ;
    private static List<String> levelsList= new List<String>();
    private static Boolean foundHighBool = false;
    private static string currentLevelStr = "";

    public static string getCateg(string topic)
    {
      int index = -1;
      for(int i=0;i<categoriesArray.Length;i++)
      {
        if (categoriesArray[i].IndexOf(topic) > -1)
        {
          index = i;
          break;
        }
      }

      if (index != -1)
      {
        return categoriesArray[index];
      }
      else
      {
        return index.ToString();
      }
    }

    public static Boolean foundHigh()
    {
      return foundHighBool; 
    }

    public static void setFoundHigh(Boolean flag)
    {
      foundHighBool = flag;
    }

    public static string getCurrentLevel()
    {
      return currentLevelStr; 
    }

    public static void setCurrentLevel(string val)
    {
      currentLevelStr = val;
    }

    public static string[] levelsArr
    {
      get { return levelsList.ToArray(); }
    }

    public static string[] categoriesArr
    { 
          get{ return categoriesArray; }
    }

    public static void pushLevel(string lvl)
    {
      if (!levelExists(lvl))
      {
        levelsList.Add(lvl);
      }
    }

   


    public static string getIntroScreen(string mod)
    {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine(" ");
      sb.AppendLine("<div class=\"css_screen\">");
      sb.AppendLine("  <div id=\"id_test_content_intro\"> ");
      if (mod == "rc")
      {
        sb.AppendLine( "    <table id=\"rc_int\">" );
        sb.AppendLine( "      <tr>" );
        sb.AppendLine( "        <td class=\"css_intro_left\"></td>" );
        sb.AppendLine( "        <td class=\"css_intro_center\">" );
        sb.AppendLine( "            <div class=\"prologue\" >" );
        sb.AppendLine( "              <p>You are about to start the diagnostic assessment of your reading skill." );
        sb.AppendLine( "               The assessment will be in the form of one passage and four to six questions on the passage." );
        sb.AppendLine( "               The questions will be presented one at a time.</p>" );
        sb.AppendLine( "               <p>The diagnostic assessment will take anywhere from one to two hours, depending on your speed and performance. Early completion does not in-and-of-itself indicate any level of performance.</p>" );
        sb.AppendLine( "               <p>If you have not already completed the tutorial, it is highly recommended that you do so prior to starting the main assessment. " );
        sb.AppendLine( "               If at any time during the assessment you need help, click either the \"i\" (item help) or the \"Tutorial\" button.</p> " );
        sb.AppendLine( "               <table class=\"css_warning_table\">" );
        sb.AppendLine( "                 <tr>" );
        sb.AppendLine( "                   <td class=\"css_tl\"></td>" );
        sb.AppendLine( "                   <td class=\"css_tc\"></td>" );
        sb.AppendLine( "                   <td  class=\"css_tr\"></td>" );
        sb.AppendLine( "                 </tr>" );
        sb.AppendLine( "                 <tr>" );
        sb.AppendLine( "                   <td class=\"css_cl\"></td>" );
        sb.AppendLine( "                   <td class=\"css_cc\">" );
        sb.AppendLine( "                    <div>" );
        sb.AppendLine( "                    <img src=\"images/wrng.png\" class=\"wrng\" />" );
        sb.AppendLine( "                      <p class=\"alert\">DO NOT use your browser\"s \"Back,\" \"Forward,\" \"Refresh,\" \"Home\" or other navigation buttons during your assessment. " );
        sb.AppendLine( "                       If you do, your session will be terminated and your only option will be to start a new assessment." );
        sb.AppendLine( "                       Once you move to another question, you will not be able to go back.</p>" );
        sb.AppendLine( "                    </div>" );
        sb.AppendLine( "                  </td>" );
        sb.AppendLine( "                  <td class=\"css_cr\"></td>" );
        sb.AppendLine( "                </tr>" );
        sb.AppendLine( "                <tr>" );
        sb.AppendLine( "                  <td class=\"css_bl\"></td>" );
        sb.AppendLine( "                  <td class=\"css_bc\"></td>" );
        sb.AppendLine( "                  <td class=\"css_br\"></td>" );
        sb.AppendLine( "                </tr>" );
        sb.AppendLine( "              </table>" );
        sb.AppendLine( "              <p>When you are ready to begin, click the \"Next\" button.</p>" );
        sb.AppendLine( "          </div>" );
        sb.AppendLine( "        </td>" );
        sb.AppendLine( "        <td class=\"css_intro_right\"></td>" );
        sb.AppendLine( "      </tr>" );
        sb.AppendLine( "    </table>" );        
      }
      else
      {
        sb.AppendLine( "    <table id=\"lc_int\">" );
        sb.AppendLine( "      <tr>" );
        sb.AppendLine( "        <td class=\"css_intro_left\"></td>" );
        sb.AppendLine( "        <td class=\"css_intro_center\">" );
        sb.AppendLine( "          <div class=\"prologue\" >" );
        sb.AppendLine( "            <p>You are about to start the diagnostic assessment of your listening skill. " );
        sb.AppendLine( "            The assessment will be in the form of one main audio passage and four to six questions on the passage. " );
        sb.AppendLine( "            The questions will be presented one at a time.</p>" );
        sb.AppendLine( "            <p>For the first two to three questions of each passage, you can listen to the passage twice, using the \"Repeat\" button. " );
        sb.AppendLine( "            For some levels, a modified version of the authentic recording will be available to you. Listen to the modified  " );
        sb.AppendLine( "            version ONLY if you cannot understand the authentic version. Your answer to the modified version will not be  " );
        sb.AppendLine( "            used to score your performance, but rather to provide you with better feedback on how well you understand deliberately  " );
        sb.AppendLine( "            delivered speech (modified audio passage) versus naturally delivered speech (original, authentic audio passage).</p>" );
        sb.AppendLine( "            <p>The diagnostic assessment will take anywhere from one to two hours, depending on your speed and performance. Early completion does not in-and-of-itself indicate any level of performance.</p>" );
        sb.AppendLine( "            <p>If you have not already completed the tutorial, it is highly recommended that you do so prior to starting the main assessment. " );
        sb.AppendLine( "            If at any time during the assessment you need help, click either the \"i\" (item help) or the \"Tutorial\" button.</p> " );
        sb.AppendLine( "            <table class=\"css_warning_table\">" );
        sb.AppendLine( "              <tr>" );
        sb.AppendLine( "                <td class=\"css_tl\"></td>" );
        sb.AppendLine( "                <td class=\"css_tc\"></td>" );
        sb.AppendLine( "                <td  class=\"css_tr\"></td>" );
        sb.AppendLine( "              </tr>" );
        sb.AppendLine( "              <tr>" );
        sb.AppendLine( "                <td class=\"css_cl\"></td>" );
        sb.AppendLine( "                <td class=\"css_cc\">" );
        sb.AppendLine( "                  <div>" );
        sb.AppendLine( "                    <img src=\"images/wrng.png\" class=\"wrng\" />" );
        sb.AppendLine( "                     <p class=\"alert\">DO NOT use your browser\"s \"Back,\" \"Forward,\" \"Refresh,\" \"Home\" or other navigation buttons during your assessment. " );
        sb.AppendLine( "                      If you do, your session will be terminated and your only option will be to start a new assessment." );
        sb.AppendLine( "                      Once you move to another question, you will not be able to go back.</p>" );
        sb.AppendLine( "                    </div>" );
        sb.AppendLine( "                </td>" );
        sb.AppendLine( "                <td class=\"css_cr\"></td>" );
        sb.AppendLine( "              </tr>" );
        sb.AppendLine( "              <tr>" );
        sb.AppendLine( "                <td class=\"css_bl\"></td>" );
        sb.AppendLine( "                <td class=\"css_bc\"></td>" );
        sb.AppendLine( "                <td class=\"css_br\"></td>" );
        sb.AppendLine( "              </tr>" );
        sb.AppendLine( "            </table>" );
        sb.AppendLine( "          <p>When you are ready to begin, click the \"Next\" button.</p>" );
        sb.AppendLine( "          </div>" );
        sb.AppendLine( "        </td>" );
        sb.AppendLine( "        <td class=\"css_intro_right\"></td>" );
        sb.AppendLine( "      </tr>" );
        sb.AppendLine( "    </table>" );        
      }

      
      sb.AppendLine( "  </div>" );
      sb.AppendLine( "</div>" );

      return sb.ToString();
    }  



    public static string getClosingPage()
    {
      StringBuilder sb = new StringBuilder();

      sb.AppendLine( "<form>");
      sb.AppendLine( "  <div class=\"css_screen\"></div>");
      sb.AppendLine( "  <div class=\"css_screen\">");
      sb.AppendLine( "    <div id=\"id_test_content_intro\"> ");
      sb.AppendLine( "      <table>");
      sb.AppendLine( "        <tr>");
      sb.AppendLine( "          <td class=\"css_intro_left\"></td>");
      sb.AppendLine( "          <td class=\"css_intro_center\">");
      sb.AppendLine( "            <div class=\"epilogue\" >");
      sb.AppendLine( "              <p>The diagnostic assessment process is complete. You can view your <a href=\"#\" class=\"btns\">diagnostic profile</a>");
      sb.AppendLine( "                 when you close this message.</p>");
      sb.AppendLine( "            </div>");
      sb.AppendLine( "          </td>");
      sb.AppendLine( "          <td class=\"css_intro_right\"></td>");
      sb.AppendLine( "	      </tr>");
      sb.AppendLine( "      </table>");
      sb.AppendLine( "    </div>");
      sb.AppendLine( "  </div>");
      sb.AppendLine( "</form>");

      return sb.ToString();
    }



    //===================================================
    //=========== PRIVATE FUNCTIONS ================/////

    private static Boolean levelExists(string lvl)
    {
      return levelsList.Contains(lvl);
    }    
  }
}  