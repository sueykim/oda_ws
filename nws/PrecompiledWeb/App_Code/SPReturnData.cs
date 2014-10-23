using System;
using System.Data;
using System.Data.SqlClient;

namespace oda2
{
  public class SPReturnData
  {
    public DataTable data           { get; set; }
    //public Boolean hasError         { get; set; }
    //public string errordetails      { get; set; }
    public ErrorData errdata;

    public SPReturnData()
    {
       errdata = new ErrorData();
    }

    public void setErrorToTrue(string details)
    {
      errdata.setErrorToTrue(details);
    }

    public void setData(SqlDataReader reader)
    {
      data = new DataTable();
      data.Load(reader);
    }

    public string getTheOnlyValue()
    {
      return data.Rows[0][0].ToString();
    }

    public DataTable getDataTable()
    {
      return data;
    }

    public Boolean getErrorStatus()
    {
      return errdata.getErrorStatus();
    }

    public string getErrorDetails()
    {
      return errdata.getErrorDetails();
    }
  }

}