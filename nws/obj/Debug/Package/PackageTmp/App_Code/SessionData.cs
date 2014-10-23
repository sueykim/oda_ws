using System;

namespace oda2
{
  public class SessionData
  {

    public ErrorData errordata;
    public Int64     sessionid   { get; set; }
    public int       userid      { get; set; }
    public string    modality    { get; set; }
    public string    language    { get; set; }
    public string    starttime   { get; set; }
    public string    endtime     { get; set; }


    public SessionData()
    {
      errordata = new ErrorData();
    }

    public void setErrorData(string details)
    {
      errordata.setErrorToTrue(details);
      sessionid   = 0;
      userid      = 0;
      modality    = "";
      language    = "";
      starttime   = "";
      endtime     = "";
    }

    public void setSessionData(Int64 sessionId, int userId, string modalityName, string languageName, string startTime, string endTime)
    {
      sessionid = sessionId;
      userid    = userId;
      modality  = modalityName;
      language  = languageName;
      starttime = startTime;
      endtime   = endTime;
    }

  }

}