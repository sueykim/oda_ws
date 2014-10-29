using System;

namespace oda2
{
  public class ProfileData
  {
    //public String prfformatteddate { get; set; }
    //public String prfcdata { get; set; }
    public ErrorData errordata ;
    public int prfid { get; set; }
    public int prfouid { get; set; }
    public string prflng { get; set; }
    public string prfmod { get; set; }
    public string prfwpl { get; set; }
    public string prfcpl { get; set; }
    public string prfdate { get; set; }
    public string prftimestring { get; set; }
    public string prflink { get; set; }
    public string prfcdata { get; set; }
    public string prfdbnm { get; set; }


    public ProfileData()
    {
      errordata = new ErrorData();
    }

    public void setErrorData(string details)
    {
      errordata.setErrorToTrue(details);
      prfid = 0;
      prfouid = 0;
      prflng = "";
      prfmod = "";
      prfwpl = "";
      prfcpl = "";
      prfdate = "";
      prftimestring = "";
      prflink = "";
      prfcdata = "";
      prfdbnm = "";
    }

    public void setProfileData(int prfId, int prfOUId, string prfLng, string prfMod, string prfWPL, string prfCPL, 
    string prfDate, string prfTimeString, string prfLink, string prfCData, string prfDbNm)
    {
      prfid = prfId;
      prfouid = prfOUId;
      prflng = prfLng;
      prfmod = prfMod;
      prfwpl = prfWPL;
      prfcpl = prfCPL;
      prfdate = prfDate;
      prftimestring = prfTimeString;
      prflink = prfLink;
      prfcdata = prfCData;
      prfdbnm = prfDbNm;
    }
  }
}  