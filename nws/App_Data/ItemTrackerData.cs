using System;

namespace oda2
{
  public class ItemTrackerData
  {

    public ErrorData errordata;
    public Int64  itid            { get; set; }
    public string ansrfldid       { get; set; }   //IT_AF_id
    public int    tstgrpid        { get; set; }   //IT_TG_id - test group
    public string matches         { get; set; }   //IT_matches
    public string useranswer      { get; set; }   //IT_user_answer
    public string jdgngkeys       { get; set; }
    public double  userscore       { get; set; }   //IT_user_score
    public double possiblepoints { get; set; }   //IT_possible_points
    public double pointsgained { get; set; }   //IT_points_gained
    public byte   passorfail      { get; set; }   //IT_pass_or_fail	
    public string imtpid    { get; set; }
      

    public ItemTrackerData()
    {
      errordata = new ErrorData();
    }

    public void setErrorData(string details)
    {
      errordata.setErrorToTrue(details);
      itid             = 0;
      ansrfldid        = "";   //IT_AF_id
      tstgrpid         = 0;   //IT_TG_id - test group
      matches          = "";   //IT_matches
      useranswer       = "";   //IT_user_answer
      jdgngkeys        = "";
      userscore        = 0;   //IT_user_score
      possiblepoints   = 0;   //IT_possible_points
      pointsgained     = 0;   //IT_points_gained
      passorfail       = 0;   //IT_pass_or_fail	
      imtpid           = "";
    }

    public void setItemTrackerData(Int64 itId, string ansrFldId, int tstGrpId, string Matches, string userAnswer, string jdgngKeys,
    double userScore, double possiblePoints, double pointsGained, byte passOrFail, string imtpId)
    {
      itid            = itId;
      ansrfldid       = ansrFldId;
      tstgrpid = tstGrpId;
      //matches = System.Web.HttpUtility.UrlPathEncode(Matches);
      matches = Matches;
      useranswer      = userAnswer;
      jdgngkeys = jdgngKeys;
      userscore       = userScore;
      possiblepoints  = possiblePoints;
      pointsgained    = pointsGained;
      passorfail      = passOrFail;
      imtpid = imtpId;
    }
  }

}