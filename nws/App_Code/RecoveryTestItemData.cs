using System;

namespace oda2
{
  public class RecoveryTestItemData
  {
    public ErrorData errordata ;
    public Int64 TRId { get; set; }
    public Int32 TROUId { get; set; }
    public Int32 TRLNGId { get; set; }
    public Int32 TRMODId { get; set; }
    public Int64 TRSId { get; set; }
    public string TRstartTimeString { get; set; }
    public string TRtestletNames { get; set; }
    public string TRlastSubmittedSet { get; set; } 
    

    public RecoveryTestItemData()
    {
      errordata = new ErrorData();
    }

    public void setErrorData(string details)
    {
        errordata.setErrorToTrue(details);

        TRId = -1;
        TROUId = -1 ;
        TRLNGId = -999;
        TRMODId = -999;
        TRSId = -1;
        TRstartTimeString = "";
        TRtestletNames = "";
        TRlastSubmittedSet = ""; 
    }

    public void setRecoveryTestItemData(Int64 TRIdIn, Int32 TROUIdIn, Int32 TRLNGIdIn, Int32 TRMODIdIn, Int64 TRSIdIn, 
        string TRstartTimeStringIn, string TRtestletNamesIn, string TRlastSubmittedSetIn)
    {
        
        TRId = TRIdIn;
        TROUId = TROUIdIn;
        TRLNGId = TRLNGIdIn;
        TRMODId = TRMODIdIn;
        TRSId = TRSIdIn;
        TRstartTimeString = TRstartTimeStringIn;
        TRtestletNames = TRtestletNamesIn;
        TRlastSubmittedSet = TRlastSubmittedSetIn;
         
    }
  }
}  