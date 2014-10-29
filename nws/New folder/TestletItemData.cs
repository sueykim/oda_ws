using System;
using System.Data;

namespace oda2
{
  public class TestletItemData
  {
    public ErrorData errordata;
    public string itm_id { get; set; }
		public string itmp_abbr { get; set; }
		public string itm_ptg_id { get; set; }
		public string itm_ntg_id { get; set; }
		public string itm_lstg_id { get; set; }
		public string itm_audio_buttons { get; set; }
		public string itm_main_text { get; set; }
		public string itm_question { get; set; }

    public TestletItemData()
    {
      errordata = new ErrorData();
      itm_id = ""; 
		  itmp_abbr = ""; 
		  itm_ptg_id = ""; 
		  itm_ntg_id = ""; 
		  itm_lstg_id = ""; 
		  itm_audio_buttons = ""; 
		  itm_main_text = ""; 
		  itm_question = ""; 
    }


    public void setErrorData(string details)
    {
      errordata.setErrorToTrue(details);
    }

    public void setTestletItemData(object[] itemarray)
    {

      if (itemarray.Length > 0)
        itm_id = (!object.Equals(itemarray[0], null)) ? (string)itemarray[0] : "";
      if (itemarray.Length > 1)
        itmp_abbr = (!object.Equals(itemarray[1], null)) ? (string)itemarray[1] : "";
      if (itemarray.Length > 2)
        itm_ptg_id = (!object.Equals(itemarray[2], null)) ? (string)itemarray[2] : "";
      if (itemarray.Length > 3)
        itm_ntg_id = (!object.Equals(itemarray[3], null)) ? (string)itemarray[3] : "";
      if (itemarray.Length > 4)
        itm_lstg_id = (!object.Equals(itemarray[4], null)) ? (string)itemarray[4] : "";
      if (itemarray.Length > 5)
        itm_audio_buttons = (!object.Equals(itemarray[5], null)) ? (string)itemarray[5] : "";
      if (itemarray.Length > 6)
        itm_main_text = (!object.Equals(itemarray[6], null)) ? (string)itemarray[6] : "";
      if (itemarray.Length > 7)
        itm_question = (!object.Equals(itemarray[7], null)) ? (string)itemarray[7] : "";
    }
  }
}