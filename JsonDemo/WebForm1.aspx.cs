using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;

namespace JsonDemo
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string json = "[{\"firstName\":\"KHABT\",\"lastName\":\"KHA\",\"email\":\"abc @mail\",\"age\":24},{\"firstName\":\"TUANDT\",\"lastName\":\"TUAN\",\"email\":\"abc @mail\",\"age\":24}]";

            JavaScriptSerializer jsser = new JavaScriptSerializer();

            var listEmp = jsser.Deserialize<List<Employee>>(json);

            var strJson = jsser.Serialize(listEmp);
            Response.Write(strJson);
        }
    }

    public class Employee
    {
        public string firstName { set; get; }
        public string lastName { set; get; }
        public string email { set; get; }
        public int age { set; get; }
    }
}