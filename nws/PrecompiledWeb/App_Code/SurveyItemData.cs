using System;

namespace oda2
{
  public class SurveyItemData
  {
    //public String prfformatteddate { get; set; }
    //public String prfcdata { get; set; }
    public ErrorData errordata ;
    public Int64 SVId { get; set; }
    public Int32 OUId { get; set; }
    public string timeString { get; set; } 
    public string dateTime { get; set; }
    public string langDesc { get; set; } 
    public Int32 sa1  { get; set; } 
    public Int32 sa2 { get; set; } 
    public Int32 sa3 { get; set; } 
    public Int32 sb1  { get; set; } 
    public Int32 sb2  { get; set; } 
    public Int32 sb3  { get; set; } 
    public Int32 sb4  { get; set; } 
    public Int32 sc1  { get; set; }  
    public Int32 sc2  { get; set; } 
    public Int32 sc3  { get; set; } 
    public Int32 sc4  { get; set; } 
    public Int32 sc5  { get; set; } 
    public Int32 sc6  { get; set; }
    public Int32 sd1  { get; set; } 
    public Int32 sd2  { get; set; } 
    public Int32 sd3  { get; set; } 
    public Int32 se1  { get; set; } 
    public Int32 se2  { get; set; }
    public string se3 { get; set; }
    public Int32 prfWpl { get; set; }
    public Int32 prfId { get; set; }
    public Int32 prfModId { get; set; }


    public SurveyItemData()
    {
      errordata = new ErrorData();
    }

    public void setErrorData(string details)
    {
        errordata.setErrorToTrue(details);

        SVId = -1;
        OUId = -1 ;
        timeString = "" ; 
        dateTime = "";
        langDesc = ""  ; 
        sa1  = 0 ; 
        sa2 =  0; 
        sa3 =  0; 
        sb1  = 0 ; 
        sb2  =  0; 
        sb3  =  0; 
        sb4  =  0; 
        sc1  = 0 ;  
        sc2  = 0 ; 
        sc3  = 0 ; 
        sc4  = 0 ; 
        sc5  = 0 ; 
        sc6  = 0 ;
        sd1  = 0 ; 
        sd2  = 0 ; 
        sd3  = 0 ; 
        se1  = 0 ; 
        se2  = 0 ;
        se3 =  "";
        prfWpl = -1;
        prfId = -1;
        prfModId = -1;
    }

    public void setSurvyItemData(Int64 SVIdIn, 
        Int32 OUIdIn, string timeStringIn, 
        string dateTimeIn, 
        string langDescIn, Int32 sa1In, Int32 sa2In, Int32 sa3In, Int32 sb1In, Int32 sb2In, Int32 sb3In,
        Int32 sb4In, Int32 sc1In, Int32 sc2In, Int32 sc3In, Int32 sc4In, Int32 sc5In, Int32 sc6In, Int32 sd1In, Int32 sd2In, Int32 sd3In, Int32 se1In, Int32 se2In, string se3In,
        Int32 prfWplIn, Int32 prfIdIn, Int32 prfModIdIn
        )
    {
        
        SVId = SVIdIn;
        OUId = OUIdIn;
        timeString = timeStringIn;
        dateTime = dateTimeIn;
        langDesc = langDescIn;
        sa1 = sa1In;
        sa2 = sa2In;
        sa3 = sa3In;
        sb1 = sb1In;
        sb2 = sb2In;
        sb3 = sb3In;
        sb4 = sb4In;
        sc1 = sc1In;
        sc2 = sc2In;
        sc3 = sc3In;
        sc4 = sc4In;
        sc5 = sc5In;
        sc6 = sc6In;
        sd1 = sd1In;
        sd2 = sd2In;
        sd3 = sd3In;
        se1 = se1In;
        se2 = se2In;
        se3 = se3In;
        prfWpl = prfWplIn;
        prfId = prfIdIn;
        prfModId = prfModIdIn;
         
    }
  }
}  