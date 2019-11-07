using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using WebApplication1.AppCode;
using System.Text;

namespace WebApplication1
{
    public partial class GetGroupChatInfo : System.Web.UI.Page
    {
        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form.Get("UserName") != null)
            {
                clsUser user;
                StringBuilder strB = new StringBuilder();
                if (Request.Form.Get("UserName").Trim() != String.Empty)
                {
                    try
                    {
                        strB.Append("<table class='table table-striped table-bordered table-hover'><tr><td> People </td><td></td></tr>");
                        user = new clsUser();
                        user.UserName = Request.Form.Get("UserName").ToString();
                        SqlDataReader dr = user.SelectUserByName();
                        while (dr.Read())
                        {
                            strB.Append(String.Format("<tr><td><img src='" + dr["Image"].ToString().Replace("~/","") + "' style='border:0px;cursor:pointer;' class='btnSelect imggp'/>{0}&nbsp;&nbsp;</td><td><label class='containergp'><input type='checkbox' name='radio' onclick=\"showTextArea('{1}','{2}')\"><span class='checkmark'></span></label></td></tr>", dr["userName"].ToString(),dr["userName"].ToString(),dr["Image"].ToString()));
                        }
                        strB.Append("</table>");
                        Response.Write(strB.ToString());

                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                    }
                }

            }
        }
        #endregion
    }
}