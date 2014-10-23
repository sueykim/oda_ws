using System;

namespace oda2
{
  public class TestGroupData
  {

    public ErrorData errordata;
    public int    tgid        { get; set; }
    public Int64  sessionid   { get; set; }
    public string timestring  { get; set; }
    public string updnscore   { get; set; }
    public string dproscore   { get; set; }
    public string rawresponse { get; set; }
    public string moddesc     { get; set; }
    public string lngdesc     { get; set; }
    public int    userid      { get; set; }
    public string datein      { get; set; }
    public int    profileid   { get; set; }

    public TestGroupData()
    {
      errordata = new ErrorData();
    }

    public void setErrorData(string details)
    {
      errordata.setErrorToTrue(details);
      tgid         = 0;
      sessionid    = 0;
      timestring   = "";
      updnscore    = "";
      dproscore    = "";
      rawresponse  = "";
      moddesc      = "";
      lngdesc      = "";
      userid        = 0;
      datein       = "";
      profileid    = 0;
    }

    public void setTestGroupData(int tgId, Int64 sessionId, string timeString, string updnScore, string dproScore, string rawResponse,
    string modDesc, string lngDesc, int userId, string dateIn, int profileId)
    {
      tgid          = tgId;
      sessionid     = sessionId;
      timestring    = timeString;
      updnscore     = updnScore;
      dproscore     = dproScore;
      rawresponse   = rawResponse;
      moddesc       = modDesc;
      lngdesc       = lngDesc;
      userid        = userId;
      datein        = dateIn;
      profileid     = profileId;
    }
  }
}