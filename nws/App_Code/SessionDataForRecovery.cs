using System;

namespace oda2
{
  public class SessionDataForRecovery
  {

    public ErrorData errordata;
    public Int64     sessionid   { get; set; }
    public int       userid      { get; set; }
    public int       modid       { get; set; }
    public int       langid      { get; set; }
    public string    starttime   { get; set; }
    public string    endtime     { get; set; }
    public string    resumedtime { get; set; }

    public SessionDataForRecovery()
    {
      errordata = new ErrorData();
    }

    public void setErrorDataForRecovery(string details)
    {
      errordata.setErrorToTrue(details);
      sessionid   = 0;
      userid      = 0;
      modid       = -999;
      langid      = -999;
      starttime   = "";
      endtime     = "";

    }

    public void setSessionDataForRecovery(Int64 sessionId, int userId, int modId, int langId, string startTime, string endTime)
    {
      sessionid = sessionId;
      userid    = userId;
      modid     = modId;
      langid    = langId;
      starttime = startTime;
      endtime   = endTime;

    }

  }

}