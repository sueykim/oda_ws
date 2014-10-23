using System;
using System.Web.Script.Serialization;
using System.Web;

namespace oda2
{
//this is an extensionto object to return json format just like toString()
  public static class JSONHelper
  {

    public static string toJSON(this object obj)
    {
      JavaScriptSerializer serializer = new JavaScriptSerializer();
      return serializer.Serialize(obj);
    }

    public static string toJSON(this object obj, int recursionDepth)
    {
      JavaScriptSerializer serializer = new JavaScriptSerializer();
        serializer.RecursionLimit = recursionDepth;
      return serializer.Serialize(obj);
    }
  }
}