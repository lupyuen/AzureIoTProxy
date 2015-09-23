using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AzureIoTProxy
{
    public partial class GetSensorData : System.Web.UI.Page
    {
        //  This web form is a proxy for http://azureiotservice.cloudapp.net/GetSensorData.aspx?Group=1&fields=Timestamp,Temperature,LightLevel
        //  since azurewebsites.net supports HTTPS but not cloudapp.net.
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Expires = -1;
            var url = new StringBuilder("http://azureiotservice.cloudapp.net/GetSensorData.aspx?");
            var firstPass = true;
            foreach (var key in Request.QueryString.AllKeys)
            {
                if (!firstPass) url.Append("&");
                firstPass = false;
                string value = Request[key];
                url.AppendFormat("{0}={1}", key, value);
            }
            var webClient = new WebClient();
            try
            {
                var result = webClient.DownloadString(url.ToString());
                Response.Write(result);
            }
            catch (Exception ex)
            {
                Response.Write("newRow = null;  \\  " + ex.GetType().ToString() + "\r\n");
            }
            Response.End();
        }
    }
}