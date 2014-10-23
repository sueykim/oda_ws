using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
namespace oda2
{
  public class StoredProcedureGrab
  {
    private SqlConnection conn;
    private SqlCommand cmd;
    private String serverName;
    private String dataBaseName = "";
    private String userName = "";
    private String passWord = "";
    private String storedProcedureName = "";
    private Object[] arguments;


    private void startProcess()
    {
      cmd = new SqlCommand(storedProcedureName, conn);
      cmd.CommandType = CommandType.StoredProcedure;
      //=>todo: try catch any connection error

      conn.Open();
      SqlCommandBuilder.DeriveParameters(cmd);

      int index = 0;
      // Populate the Input Parameters With Values Provided        
      foreach (SqlParameter parameter in cmd.Parameters)
      {
        if (parameter.Direction == ParameterDirection.Input ||
             parameter.Direction == ParameterDirection.
                                         InputOutput)
        {
          Boolean isString = false;
          if (null != arguments[index])
          {
            //trim if string
            Object t = arguments[index];
            isString = t.GetType().Name == "String";
          }

          parameter.Value = null == arguments[index] ? DBNull.Value : (isString ? arguments[index].ToString().Trim() : arguments[index]);
          index++;
        }
      }
    }// startProcess


    public StoredProcedureGrab(String servername, String databaesname, String username, String password, String storedprocedure, Object[] args)
    {
      serverName = servername;
      dataBaseName = databaesname;
      userName = username;
      passWord = password;
      storedProcedureName = storedprocedure;
      arguments = args;

      conn = new SqlConnection("Data Source=" + serverName + ";Database=" + dataBaseName + ";User=" + userName + ";Password=" + passWord + ";Trusted_Connection=False");
    }// StoredProcedureGrab


    public SPReturnData GetReader()
    {
      SPReturnData returndata = new SPReturnData();

      //todo: number and define each error
      try 
      {
        startProcess();
      }
      catch (Exception e)
      {
        returndata.setErrorToTrue(e.ToString());
        return returndata;        
      }

      //=>catching any execution error
      try 
      {
        returndata.setData(cmd.ExecuteReader(CommandBehavior.CloseConnection));
        return returndata;
      }
      catch (Exception e)
      {
        returndata.setErrorToTrue(e.ToString());
        return returndata;
      }

    }// GetReader
  }
}