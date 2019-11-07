using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using WebApplication1.AppCode;

namespace WebApplication1
{
    public partial class GetSearchInfo : System.Web.UI.Page
    {
        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.Form.Get("Name") != null)
                {
                    clsUser user;
                    StringBuilder strB = new StringBuilder();
                    if (Request.Form.Get("Name").Trim() != String.Empty)
                    {
                        try
                        {
                            strB.Append("<table><tr><td>People</td></tr><tr><td>&nbsp;</td></tr>");
                            user = new clsUser();
                            string nonUser = clsGlobal.GetSessionValue(clsEnum.SessionVariables.UserName);
                            user.UserName = Request.Form.Get("Name").ToString();
                            SqlDataReader da = user.SelectUser(nonUser);
                            while (da.Read())
                            {
                                strB.Append(String.Format("<tr><td><img src='" + da["Image"].ToString().Replace("~/", "") + "' style='border:0px;' class='imggp'/></td><td style='cursor:pointer;padding-left:10px;text-decoration:none;'><a id='#' href='' onclick=\"LinkButton1_Click('{1}')\" runat='server'>{0}</a>&nbsp;&nbsp;</td></tr><tr><td style='height:1px;'></td><td style='width:100%;height:1px;'><hr style='border-top: 1px solid #ccc5c5;'/></td></tr>", da["Name"].ToString(), da["Name"].ToString()));
                            }
                            strB.Append("</table>");
                            Response.Write(strB.ToString());
                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.ToString());
                        }
                    }
                }
            }
        }
        #endregion

    }
}