using System;
using System.Data;

namespace oda2
{
  public class ErrorData
  {
    public Boolean hasError         { get; set; }
    public string errordetails      { get; set; }

    public ErrorData()
    {
      hasError = false;
      errordetails = "";
    }

    public void setErrorToTrue(string details)
    {
      hasError = true;
      errordetails = details;
    }


    public Boolean getErrorStatus()
    {
      return hasError;
    }

    public string getErrorDetails()
    {
      return errordetails;
    }
  }

}