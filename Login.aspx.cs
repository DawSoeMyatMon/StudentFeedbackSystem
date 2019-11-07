using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;
using System.IO;
using WebApplication1.AppCode;
using System.Security.Cryptography;

namespace ChatApp
{
    public partial class Login : System.Web.UI.Page
    {
        #region conn
        DataSet ds;
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ChatApp_ConnectionString"].ConnectionString);
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MMBNoteSheet_ConnectionString"].ConnectionString);
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Title = ConfigurationManager.AppSettings["AppName"];
            }
            clsGlobal.SetSessionValue(clsEnum.SessionVariables.UserName, txtusername.Text);

        }
        #endregion

        #region Login_Click
        protected void Login_Click(object sender, EventArgs e)
        {
            bool isPassword = false;
            clsRegister reg;
            clsUser user;

            string pass = null;
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from SystemUser where SystemUserID= '" + txtusername.Text + "'", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dtResult = new DataTable();
            dtResult.Load(dr);

            if (dtResult.Rows.Count > 0)
            {
                pass = dtResult.Rows[0]["Pwd"].ToString();
                isPassword = true;
            }

            if (isPassword)
            {
                string DecryptPassword = null;
                con.Open();
                SqlCommand cmdnew = new SqlCommand("select * from Users where UserName= '" + txtusername.Text + "'", con);
                SqlDataReader drnew = cmdnew.ExecuteReader();
                DataTable dtResultnew = new DataTable();
                dtResultnew.Load(drnew);
                con.Close();
                conn.Close();
                SqlConnection.ClearAllPools();
                SqlConnection.ClearPool(con);
                SqlConnection.ClearPool(conn);
                if (dtResultnew.Rows.Count > 0)
                {
                    DecryptPassword = clsGlobal.Encrypt(txtpassword.Text.Trim());
                    conn.Close();
                    if (dtResultnew.Rows[0]["Pass"].ToString().Equals(DecryptPassword))
                    {
                        string strRes = "select * from dbo.Register where Name='" + txtusername.Text + "' ";
                        SqlDataAdapter da1 = new SqlDataAdapter(strRes, con);
                        ds = new DataSet();
                        da1.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Session["Admin"] = ds.Tables[0].Rows[0][1].ToString();
                            Session["Image"] = ds.Tables[0].Rows[0][2].ToString();
                            reg = new clsRegister();
                            reg.UserName = txtusername.Text;
                            reg.IsActive = DateTime.Now.ToString();
                            reg.updateIsActive();
                            clsGlobal.SetSessionValue(clsEnum.SessionVariables.Pass, txtpassword.Text);
                            Response.Redirect("Chat.aspx");
                        }

                    }
                    else
                    {
                      
                       Response.Redirect("~/Login.aspx");
                       lblMsg.Text = "Username and Password is invalid.";
                    }
                }
                else
                {
                    try
                    {
                        reg = new clsRegister();
                        user = new clsUser();
                        user.UserName = txtusername.Text.ToString();
                        user.Pass = clsGlobal.Encrypt(txtpassword.Text.ToString());
                        Session["Admin"] = txtusername.Text;
                        Session["Image"] = "~/Style/Images/15.jpg";
                        user.insert();
                        reg.UserName = txtusername.Text.ToString();
                        reg.Image = "~/Style/Images/15.jpg";
                        reg.insert();

                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("~/Login.aspx");
                    }
                }
                Response.Redirect("Chat.aspx");
            }

        }
        #endregion
    }
}