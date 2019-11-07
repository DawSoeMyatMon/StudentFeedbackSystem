using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Text;
using System.Net;
using WebApplication1.AppCode;


namespace ChatApp
{
    public partial class Chat : System.Web.UI.Page
    {
        int msgAllCount = 0;
        HtmlControl theDiv = null;

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            this.txtMessage.Attributes.Add("onkeypress", "button_click(this)");
            this.registerPostBackControl();
            if (!IsPostBack)
            {
                if (Session["Admin"] != null)
                {
                    String Image = Session["Image"].ToString();
                    String Admin = Session["Admin"].ToString();
                    get_User(Image, Admin);
                    Load_Frends();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
                this.Title = ConfigurationManager.AppSettings["AppName"];
                lblUser.Text = clsGlobal.GetSessionValue(clsEnum.SessionVariables.UserName);
                lblThree.Text = clsGlobal.GetSessionValue(clsEnum.SessionVariables.UserName);
                imgUser.ImageUrl = Session["Image"].ToString();
                //Response.AppendHeader("Refresh", "6000");
            }
            hdnPass.Value = clsGlobal.GetSessionValue(clsEnum.SessionVariables.Pass);
            lblBio.Text = clsGlobal.GetSessionValue(clsEnum.SessionVariables.Bio);
            hdnEditBio.Value = clsGlobal.GetSessionValue(clsEnum.SessionVariables.Bio);
            String s = hdnPassword.Value;
            if (Request.Form.Get("hdnUserName") != null)
            {
                String ans = Request.Form.Get("hdnUserName").ToString();
                clsGlobal.SetSessionValue(clsEnum.SessionVariables.HiddenName, ans);
                callfun();
            }
            if (clsGlobal.GetSessionValue(clsEnum.SessionVariables.HiddenName) != "")
            {
                if (clsGlobal.GetSessionValue(clsEnum.SessionVariables.HiddenName) != lblSender.Text && Request.Form.Get("hdnUserName") == null)
                {
                    callfun();
                }
            }
           
            ClientScript.RegisterStartupScript(GetType(), "JavaScript", "javascript:funMsgAlert(" + msgAllCount + ");", true);
        }
        #endregion

        #region registerPostBackControl
        private void registerPostBackControl()
        {
            foreach (DataListItem item in ddlChat.Items)
            {
                LinkButton lbtn = (LinkButton)item.FindControl("btnDownloadFile") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterPostBackControl(lbtn);
            }

        }

        #endregion

        #region callfun
        public void callfun()
        {
            lblMsg.Text = "&nbsp;";
            String ans = null;
            if (clsGlobal.GetSessionValue(clsEnum.SessionVariables.HiddenName) != lblSender.Text)
            {
                ans = clsGlobal.GetSessionValue(clsEnum.SessionVariables.HiddenName);
            }
            lstBody.Visible = false;
            lstBodyV.Visible = true;
            lstFooterV.Visible = true;
            lstFooter.Visible = false;
            msBody.Visible = false;

            String res = null;
            lblReceiver.Text = ans;

            clsRegister reg;
            try
            {
                reg = new clsRegister();
                reg.UserName = lblReceiver.Text;
                if (lblReceiver.Text.Contains(','))
                {
                    reg.UserName = null;
                    String[] strBd = null;
                    String[] resname = null;
                    int cou = 0;
                    resname = lblReceiver.Text.Split(',');
                    foreach (string resone in resname)
                    {
                        if (resone != clsGlobal.GetSessionValue(clsEnum.SessionVariables.HiddenName))
                        {
                            cou += 1;
                        }
                    }
                    String[] resf = new String[cou];
                    int i = 0;
                    foreach (string resone in resname)
                    {
                        if (resone != clsGlobal.GetSessionValue(clsEnum.SessionVariables.HiddenName))
                        {
                            reg.UserName = resone;
                            strBd = reg.SelectIsActiveAll();
                            resf[i] = strBd[0].ToString();
                            i++;
                        }
                    }
                    Array.Sort(resf, StringComparer.InvariantCulture);
                    res = resf[cou - 1];
                }
                else
                {
                    res = reg.SelectIsActive();
                }
            }
            catch (Exception ex)
            {

                Response.Write(ex.ToString());
            }
            lblIsActiveall.Text = res;
            DateTime Active = Convert.ToDateTime(lblIsActiveall.Text);
            lblIsActiveall.Style.Add("padding-left", "20px");
            if (Active.Date == DateTime.Now.Date && Active.Hour == DateTime.Now.Hour && (DateTime.Now.Minute - Active.Minute < 2))
            {
                lblIsActiveall.Text = "Active Now";
                spanall.Style.Add(HtmlTextWriterStyle.MarginLeft, "8px");
                spanall.Style.Add(HtmlTextWriterStyle.Position, "absolute");
                spanall.Style.Add(HtmlTextWriterStyle.MarginTop, "8px");
                spanall.Style.Add(HtmlTextWriterStyle.Width, "8px");
                spanall.Style.Add(HtmlTextWriterStyle.Height, "8px");
                spanall.Style.Add(HtmlTextWriterStyle.BackgroundColor, " #1de025");
                spanall.Style.Add("border-radius", "50%");
            }
            else
            {
                string value, hour, minute = null;
                string yy, mm, dd = null;
                int year = DateTime.Now.Year - Active.Year;
                int month = DateTime.Now.Month - Active.Month;
                int day = DateTime.Now.Day - Active.Day;
                hour = Convert.ToString(DateTime.Now.Hour - Active.Hour);
                minute = Convert.ToString(DateTime.Now.Minute - Active.Minute);
                if (year > 0)
                {
                    value = year + " year";
                    lblIsActiveall.Text = "Last " + value + " ago";
                }
                else if (month > 0)
                {
                    value = month + " month";
                    lblIsActiveall.Text = "Last " + value + " ago";
                }
                else if (day > 0)
                {
                    value = day + " day";
                    lblIsActiveall.Text = "Last " + value + " ago";
                }
                else
                {
                    if (Convert.ToInt32(hour) > 1 || (Convert.ToInt32(hour) == 1 && DateTime.Now.Minute > Active.Minute))
                    {
                        value = hour + " hour";
                        lblIsActiveall.Text = "Last " + value + " ago";
                    }
                    else if (Convert.ToInt32(hour) == 1 && DateTime.Now.Minute < Active.Minute)
                    {
                        int min = (60 - Active.Minute) + DateTime.Now.Minute;
                        value = min + " minute";
                        lblIsActiveall.Text = "Last " + value + " ago";
                    }
                    else if (Convert.ToInt32(minute) > 0)
                    {
                        value = minute + " minute";
                        lblIsActiveall.Text = "Last " + value + " ago";
                    }
                    spanall.Style.Add(HtmlTextWriterStyle.MarginLeft, "3px");
                    spanall.Style.Add(HtmlTextWriterStyle.Position, "absolute");
                    spanall.Style.Add(HtmlTextWriterStyle.MarginTop, "3px");
                    spanall.Style.Add(HtmlTextWriterStyle.Width, "16px");
                    spanall.Style.Add(HtmlTextWriterStyle.Height, "16px");
                    spanall.Style.Add("border-radius", "50%");
                    spanall.Style.Add(HtmlTextWriterStyle.BackgroundImage, "url(/Style/images/clock1_kC1_icond.ico)");
                }
            }
            clsRegister regs;
            string url = null;
            try
            {
                regs = new clsRegister();
                regs.UserName = ans;
                url = regs.SelectImage();
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
            Image3.ImageUrl = url;
            LoadChatbox();

        }
        #endregion

        #region LoadChatBox
        public void LoadChatbox()
        {
            DateTime date = DateTime.Now;
            string date3 = date.ToString("dd-MM-yyyy");
            clsChatBox clsCB;
            clsCB = new clsChatBox();

            #region dtOne
            DataTable dtOne = new DataTable();
            try
            {
                clsCB.Sender = lblSender.Text;
                clsCB.Reciever = lblReceiver.Text;
                dtOne = clsCB.selectChatBox();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Login.aspx");
            }
            #endregion

            #region dtTwo
            DataTable dtTwo = new DataTable();
            try
            {
                clsCB.Sender = lblReceiver.Text;
                clsCB.Reciever = lblSender.Text;
                dtTwo = clsCB.selectChatBox();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Login.aspx");
            }
            #endregion

            if (lblReceiver.Text != "")
            {
                if (lblReceiver.Text.Contains(','))
                {
                    DataTable dtThree = new DataTable();
                    try
                    {
                        clsCB.Reciever = lblReceiver.Text;
                        dtThree = clsCB.selectChatBoxByReciever();
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.ToString());
                    }
                    DataView dtView = dtThree.DefaultView;
                    dtView.Sort = "Date ASC";
                    ddlChat.DataSource = dtView;
                }
                else
                {
                    dtTwo.Merge(dtOne);
                    DataView dtView = dtTwo.DefaultView;
                    if (dtView.Count > 0)
                    {
                        dtView.Sort = "Date ASC";
                        ddlChat.DataSource = dtView;
                    }
                }
            }

            ddlChat.DataBind();

            if (ddlChat.Items.Count > 0)
            {
                msBody.Visible = false;
            }
            #region seen
            if (clsGlobal.GetSessionValue(clsEnum.SessionVariables.UserName) != "")
            {
                if (lblReceiver.Text.Contains(','))
                {
                    clsCB.Sender = lblSender.Text;
                    clsCB.Reciever = lblReceiver.Text;
                    clsCB.ReadUser = lblSender.Text;
                    clsCB.updateReadUser();
                }
                else
                {
                    try
                    {
                        clsCB.Sender = lblReceiver.Text;
                        clsCB.Reciever = clsGlobal.GetSessionValue(clsEnum.SessionVariables.UserName);
                        clsCB.ReadDate = System.DateTime.Now;
                        clsCB.updateRead();
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("~/Login.aspx");
                    }
                }
            }
            #endregion

            #region read
            String ReadID = null;
            if (clsGlobal.GetSessionValue(clsEnum.SessionVariables.UserName) != "")
            {
                try
                {
                    clsCB = new clsChatBox();
                    clsCB.Sender = clsGlobal.GetSessionValue(clsEnum.SessionVariables.UserName);
                    clsCB.Reciever = lblReceiver.Text;
                    clsCB.ReadDate = System.DateTime.Now;
                    ReadID = clsCB.ReadID();
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Login.aspx");
                }
            }

            #endregion
            txtMessage.Focus();

        }
        #endregion

        #region get_User
        public void get_User(String Image, String Admin)
        {
            Image1.ImageUrl = Image;
            lblSender.Text = Admin;
            clsUser clsUser;
            try
            {
                clsUser = new clsUser();
                clsUser.UserName = lblSender.Text;
                string res = clsUser.GetBio();
                lblBio.Text = res;
                clsGlobal.SetSessionValue(clsEnum.SessionVariables.Bio, res);
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        #endregion

        #region btnsent_onServerClick
        protected void btnsent_onServerClick(object sender, EventArgs e)
        {

            if ((txtMessage.Text != "") || (fileUploadName.FileName != ""))
            {
                if (fileUploadName.FileName != null)
                {
                    string fileName = Path.GetFileName(fileUploadName.PostedFile.FileName);
                    if (fileName != "")
                    {
                        fileUploadName.SaveAs(Server.MapPath("~/Files/" + fileName));
                    }
                    string contentType = fileUploadName.PostedFile.ContentType;
                    using (Stream fs = fileUploadName.PostedFile.InputStream)
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            byte[] bytes = br.ReadBytes((Int32)fs.Length);
                            DateTime date = DateTime.Now;
                            string date3 = date.ToString("dd-MM-yyyy");
                            TimeSpan timespan = date.TimeOfDay;
                            clsChatBox clsCB;

                            try
                            {
                                clsCB = new clsChatBox();
                                clsCB.Sender = lblSender.Text;
                                clsCB.Reciever = lblReceiver.Text;
                                clsCB.Message = txtMessage.Text;
                                clsCB.Date = date;
                                clsCB.Time = timespan;
                                clsCB.Image = Image1.ImageUrl.ToString();
                                clsCB.FileName = fileName;
                                clsCB.FileContentType = contentType;
                                clsCB.FileData = bytes;

                                int res = clsCB.insert();
                                if (res >= 1)
                                {
                                    txtMessage.Text = "";
                                    LoadChatbox();
                                }
                            }
                            catch (Exception ex)
                            {
                                Response.Redirect("~/Login.aspx");
                            }

                        }
                    }

                }
            }

        }
        #endregion

        #region Load_Frends
        public void Load_Frends()
        {
            clsRegister clsReg;
            clsChatBox clsCb;
            DataTable dtOne;
            DataTable dtTwo;

            try
            {
                dtOne = new DataTable();
                dtTwo = new DataTable();
                clsReg = new clsRegister();
                if (txtSearchPeople.Text != "")
                {
                    clsReg.UserName = txtSearchPeople.Text;
                    dtTwo = clsReg.selectByName(lblSender.Text);
                }
                else
                {
                    clsCb = new clsChatBox();
                    clsCb.Sender = lblSender.Text;
                    dtTwo = clsCb.SelectByNName();
                    //clsReg.UserName = lblSender.Text;
                    //dtOne = clsReg.select();
                    //dtTwo = clsReg.SelectByNName();
                    //dtTwo.Merge(dtOne);
                }
                ddlUserList.DataSource = dtTwo;
                ddlUserList.DataBind();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Login.aspx");
            }

        }
        #endregion

        #region lbtnUserList_Click
        protected void lbtnUserList_Click(object sender, EventArgs e)
        {
            String ans = null;
            lstBody.Visible = false;
            lstBodyV.Visible = true;
            lstFooterV.Visible = true;
            lstFooter.Visible = false;
            msBody.Visible = false;
            String res = null;
            LinkButton lBtn = sender as LinkButton;
            lblReceiver.Text = lBtn.Text;
            if (lblReceiver.Text.Contains(','))
            {
                btn.Visible = true;
                btn.Style.Add(HtmlTextWriterStyle.PaddingLeft, "50px");
            }
            else
            {
                lstBodyV.Attributes.Remove("class");
                btn.Visible = false;
            }
            #region seen
            clsChatBox clsCB;
            clsCB = new clsChatBox();
            if (lblReceiver.Text.Contains(','))
            {
                clsCB.Sender = lblSender.Text;
                clsCB.Reciever = lblReceiver.Text;
                clsCB.ReadUser = lblSender.Text;
                clsCB.updateReadUser();
            }
            else
            {
                try
                {
                    clsCB.Sender = lblReceiver.Text;
                    clsCB.Reciever = clsGlobal.GetSessionValue(clsEnum.SessionVariables.UserName);
                    clsCB.ReadDate = System.DateTime.Now;
                    clsCB.updateRead();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
            }
            #endregion

            clsRegister reg;
            try
            {
                reg = new clsRegister();
                reg.UserName = lblReceiver.Text;
                if (lblReceiver.Text.Contains(','))
                {
                    reg.UserName = null;
                    String[] strBd = null;
                    String[] resname = null;
                    int cou = 0;
                    resname = lblReceiver.Text.Split(',');
                    foreach (string resone in resname)
                    {
                        if (resone != clsGlobal.GetSessionValue(clsEnum.SessionVariables.UserName))
                        {
                            cou += 1;
                        }
                    }
                    String[] resf = new String[cou];
                    int i = 0;
                    foreach (string resone in resname)
                    {
                        if (resone != clsGlobal.GetSessionValue(clsEnum.SessionVariables.UserName))
                        {
                            reg.UserName = resone;
                            strBd = reg.SelectIsActiveAll();
                            resf[i] = strBd[0].ToString();
                            i++;
                        }
                    }
                    Array.Sort(resf, StringComparer.InvariantCulture);
                    res = resf[cou - 1];
                    lblBioEdit.Text = null;
                }
                else
                {
                    res = reg.SelectIsActive();
                    clsUser clsUser;
                    try
                    {
                        clsUser = new clsUser();
                        clsUser.UserName = lblReceiver.Text;
                        lblBioEdit.Text = clsUser.GetBio();
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.ToString());
                    }

                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
            lblIsActiveall.Text = res;
            DateTime Active = Convert.ToDateTime(lblIsActiveall.Text);
            lblIsActiveall.Style.Add("padding-left", "20px");
            if (Active.Date == DateTime.Now.Date && Active.Hour == DateTime.Now.Hour && (DateTime.Now.Minute - Active.Minute < 2))
            {
                lblIsActiveall.Text = "Active Now";
                spanall.Style.Add(HtmlTextWriterStyle.MarginLeft, "8px");
                spanall.Style.Add(HtmlTextWriterStyle.Position, "absolute");
                spanall.Style.Add(HtmlTextWriterStyle.MarginTop, "8px");
                spanall.Style.Add(HtmlTextWriterStyle.Width, "8px");
                spanall.Style.Add(HtmlTextWriterStyle.Height, "8px");
                spanall.Style.Add(HtmlTextWriterStyle.BackgroundColor, " #1de025");
                spanall.Style.Add("border-radius", "50%");
            }
            else
            {
                string value, hour, minute = null;
                string yy, mm, dd = null;
                int year = DateTime.Now.Year - Active.Year;
                int month = DateTime.Now.Month - Active.Month;
                int day = DateTime.Now.Day - Active.Day;
                hour = Convert.ToString(DateTime.Now.Hour - Active.Hour);
                minute = Convert.ToString(DateTime.Now.Minute - Active.Minute);
                if (year > 0)
                {
                    value = year + " year";
                    lblIsActiveall.Text = "Last " + value + " ago";
                }
                else if (month > 0)
                {
                    value = month + " month";
                    lblIsActiveall.Text = "Last " + value + " ago";
                }
                else if (day > 0)
                {
                    value = day + " day";
                    lblIsActiveall.Text = "Last " + value + " ago";
                }
                else
                {
                    if (Convert.ToInt32(hour) > 1 || (Convert.ToInt32(hour) == 1 && DateTime.Now.Minute > Active.Minute))
                    {
                        value = hour + " hour";
                        lblIsActiveall.Text = "Last " + value + " ago";
                    }
                    else if (Convert.ToInt32(hour) == 1 && DateTime.Now.Minute < Active.Minute)
                    {
                        int min = (60 - Active.Minute) + DateTime.Now.Minute;
                        value = min + " minute";
                        lblIsActiveall.Text = "Last " + value + " ago";
                    }
                    else if (Convert.ToInt32(minute) > 0)
                    {
                        value = minute + " minute";
                        lblIsActiveall.Text = "Last " + value + " ago";
                    }
                    spanall.Style.Add(HtmlTextWriterStyle.MarginLeft, "3px");
                    spanall.Style.Add(HtmlTextWriterStyle.Position, "absolute");
                    spanall.Style.Add(HtmlTextWriterStyle.MarginTop, "3px");
                    spanall.Style.Add(HtmlTextWriterStyle.Width, "16px");
                    spanall.Style.Add(HtmlTextWriterStyle.Height, "16px");
                    spanall.Style.Add("border-radius", "50%");
                    spanall.Style.Add(HtmlTextWriterStyle.BackgroundImage, "url(/Style/images/clock1_kC1_icond.ico)");
                }
            }
            DataListItem item = (DataListItem)lBtn.NamingContainer;
            Image NameLabel = (Image)item.FindControl("Image2");
            string url = NameLabel.ImageUrl.ToString();
            Image3.ImageUrl = url;
            LoadChatbox();
        }
        #endregion

        #region ddlChat_OnItemDataBoundItem
        public void ddlChat_OnItemDataBound(object sender, DataListItemEventArgs e)
        {
            theDiv = e.Item.FindControl("container") as HtmlControl;
            Label lblowner = (Label)e.Item.FindControl("lblReciever") as Label;
            Image imgowner = (Image)e.Item.FindControl("Image4") as Image;
            Label lblMsgowner = (Label)e.Item.FindControl("Message") as Label;
            Label lblDate = (Label)e.Item.FindControl("Date") as Label;
            Label lblSeen = (Label)e.Item.FindControl("SeenID") as Label;
            LinkButton btnDownload = (LinkButton)e.Item.FindControl("btnDownloadFile") as LinkButton;
            LinkButton btnImgDownload = (LinkButton)e.Item.FindControl("btnImgDownload") as LinkButton;
            Image imgDownload = (Image)e.Item.FindControl("imgDownload") as Image;
            Image imgSeen = (Image)e.Item.FindControl("imgSeen") as Image;
            if (lblowner.Text == lblSender.Text)
            {
                Panel pnlMediaBody = e.Item.FindControl("pnlMediaBody") as Panel;
                pnlMediaBody.Style.Add("text-align", "right");
                imgowner.Visible = false;
            }
            if (btnDownload.Text.Contains(".pdf") || btnDownload.Text.Contains(".xlsx") || btnDownload.Text.Contains(".docx") || btnDownload.Text.Contains(".ppt") || btnDownload.Text.Contains(".rar") || btnDownload.Text.Contains(".mp4") || btnDownload.Text.Contains(".mp3"))
            {
                btnDownload.Visible = true;
            }
            else if (btnDownload.Text.Contains(".jpg") || btnDownload.Text.Contains(".jpeg") || btnDownload.Text.Contains(".png"))
            {
                imgDownload.Visible = true;
                btnImgDownload.Visible = true;
            }
            else
            {
                btnDownload.Visible = false;
                imgDownload.Visible = false;
                btnImgDownload.Visible = false;
            }
            String ReadID = null;
            String ReadUser = null;
            if (clsGlobal.GetSessionValue(clsEnum.SessionVariables.UserName) != "")
            {
                clsChatBox clsCB;
                try
                {
                    clsCB = new clsChatBox();
                    clsCB.Sender = clsGlobal.GetSessionValue(clsEnum.SessionVariables.UserName);
                    clsCB.Reciever = lblReceiver.Text;
                    clsCB.ReadDate = System.DateTime.Now;
                    ReadID = clsCB.ReadID();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
                try
                {
                    clsCB = new clsChatBox();
                    clsCB.Sender = clsGlobal.GetSessionValue(clsEnum.SessionVariables.UserName);
                    clsCB.Reciever = lblReceiver.Text;
                    ReadUser = clsCB.ReadUsers();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }

                if (lblSeen.Text.ToString() == ReadUser)
                {
                    // to add img
                    clsCB = new clsChatBox();
                    String names = null;
                    String img = null;
                    names = clsCB.SelectImageByReadUser(ReadUser);
                    String[] namevalues = names.Split(',');
                    clsRegister clsReg;
                    Panel pnl = (Panel)e.Item.FindControl("pnl") as Panel;
                    foreach (string value in namevalues)
                    {
                        clsReg = new clsRegister();
                        clsReg.UserName = value;
                        img = clsReg.SelectImage();
                        System.Web.UI.WebControls.Image imgs = new System.Web.UI.WebControls.Image();
                        imgs.Style.Add("width", "30px");
                        imgs.Style.Add("height", "30px");
                        imgs.Style.Add("border-radius", "50px");
                        imgs.ImageUrl = img;
                        pnl.Controls.Add(imgs);

                    }
                    lblSeen.Visible = false;
                    lblSeen.Text = "seen";
                }
                else if (lblSeen.Text.ToString() == ReadID)
                {
                    lblSeen.Visible = false;
                    lblSeen.Text = "seen";
                    imgSeen.ImageUrl = Image3.ImageUrl;
                    imgSeen.Visible = true;

                }
                else
                {
                    lblSeen.Visible = false;
                    imgSeen.Visible = false;
                }

            }
            imgDownload.ImageUrl = "~/Files/" + btnDownload.Text;
        }
        #endregion

        #region ddlUserList_OnItemDataBound
        public void ddlUserList_OnItemDataBound(object sender, DataListItemEventArgs e)
        {
            Label lblIsActive = (Label)e.Item.FindControl("lblIsActive") as Label;
            var lblIsActive1 = (Label)e.Item.FindControl("lbl") as Label;
            var span = (Label)e.Item.FindControl("span") as Label;
            var btn = (System.Web.UI.HtmlControls.HtmlButton)e.Item.FindControl("btn") as System.Web.UI.HtmlControls.HtmlButton;
            LinkButton lblbtn = (LinkButton)e.Item.FindControl("lbtnUserList") as LinkButton;
            Image msgImage = (Image)e.Item.FindControl("msgImage") as Image;
            Label msgCount = (Label)e.Item.FindControl("msgCount") as Label;

            String res = null;
            clsRegister reg;
            clsChatBox clsCB;
            try
            {
                reg = new clsRegister();
                reg.UserName = lblbtn.Text;
                if (lblbtn.Text.Contains(','))
                {
                    btn.Visible = true;
                    btn.Style.Add(HtmlTextWriterStyle.PaddingLeft, "50px");
                    reg.UserName = null;
                    String[] strBd = null;
                    String[] resname = null;
                    int cou = 0;
                    resname = lblbtn.Text.Split(',');
                    foreach (string resone in resname)
                    {
                        if (resone != clsGlobal.GetSessionValue(clsEnum.SessionVariables.UserName))
                        {
                            cou += 1;
                        }
                    }
                    String[] resf = new String[cou];
                    int i = 0;
                    foreach (string resone in resname)
                    {
                        if (resone != clsGlobal.GetSessionValue(clsEnum.SessionVariables.UserName))
                        {
                            reg.UserName = resone;
                            strBd = reg.SelectIsActiveAll();
                            resf[i] = strBd[0].ToString();
                            i++;
                        }
                    }
                    Array.Sort(resf, StringComparer.InvariantCulture);
                    res = resf[cou - 1];
                }
                else
                {
                    btn.Visible = false;
                    btn.EnableViewState = false;
                    res = reg.SelectIsActive();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
            lblIsActiveall.Text = res;
            if (lblIsActiveall.Text != "")
            {
                DateTime Active = Convert.ToDateTime(lblIsActiveall.Text);
                lblIsActiveall.Style.Add("padding-left", "20px");

                if (Active.Date == DateTime.Now.Date && Active.Hour == DateTime.Now.Hour && (DateTime.Now.Minute - Active.Minute < 2))
                {
                    lblIsActive1.Text = "Active Now ";
                    span.Style.Add(HtmlTextWriterStyle.MarginLeft, "8px");
                    span.Style.Add(HtmlTextWriterStyle.Position, "absolute");
                    span.Style.Add(HtmlTextWriterStyle.MarginTop, "8px");
                    span.Style.Add(HtmlTextWriterStyle.Width, "8px");
                    span.Style.Add(HtmlTextWriterStyle.Height, "8px");
                    span.Style.Add(HtmlTextWriterStyle.BackgroundColor, " #1de025");
                    span.Style.Add("border-radius", "50%");
                }
                else
                {
                    string value, hour, minute = null;
                    string yy, mm, dd = null;
                    int year = DateTime.Now.Year - Active.Year;
                    int month = DateTime.Now.Month - Active.Month;
                    int day = DateTime.Now.Day - Active.Day;
                    hour = Convert.ToString(DateTime.Now.Hour - Active.Hour);
                    minute = Convert.ToString(DateTime.Now.Minute - Active.Minute);
                    if (year > 0)
                    {
                        value = year + " year";
                        lblIsActive1.Text = "Last " + value + " ago";
                    }
                    else if (month > 0)
                    {
                        value = month + " month";
                        lblIsActive1.Text = "Last " + value + " ago";
                    }
                    else if (day > 0)
                    {
                        value = day + " day";
                        lblIsActive1.Text = "Last " + value + " ago";
                    }

                    else
                    {
                        if (Convert.ToInt32(hour) > 1 || (Convert.ToInt32(hour) == 1 && DateTime.Now.Minute > Active.Minute))
                        {
                            value = hour + " hour";
                            lblIsActive1.Text = "Last " + value + " ago";
                        }
                        else if (Convert.ToInt32(hour) == 1 && DateTime.Now.Minute < Active.Minute)
                        {
                            int min = (60 - Active.Minute) + DateTime.Now.Minute;
                            value = min + " minute";
                            lblIsActive1.Text = "Last " + value + " ago";
                        }
                        else if (Convert.ToInt32(minute) > 0)
                        {
                            value = minute + " minute";
                            lblIsActive1.Text = "Last " + value + " ago";
                        }
                        span.Style.Add(HtmlTextWriterStyle.MarginLeft, "3px");
                        span.Style.Add(HtmlTextWriterStyle.Position, "absolute");
                        span.Style.Add(HtmlTextWriterStyle.MarginTop, "3px");
                        span.Style.Add(HtmlTextWriterStyle.Width, "16px");
                        span.Style.Add(HtmlTextWriterStyle.Height, "16px");
                        span.Style.Add("border-radius", "50%");
                        span.Style.Add(HtmlTextWriterStyle.BackgroundImage, "url(/Style/images/clock1_kC1_icond.ico)");
                    }

                }
            }
            if (clsGlobal.GetSessionValue(clsEnum.SessionVariables.UserName) != "")
            {
                #region seenMessageCount for one
                if (!lblbtn.Text.Contains(','))
                {
                    try
                    {
                        int count = 0;
                        clsCB = new clsChatBox();
                        clsCB.Sender = lblbtn.Text;
                        clsCB.Reciever = clsGlobal.GetSessionValue(clsEnum.SessionVariables.UserName);
                        count = clsCB.ReadMsgCount();
                        if (count > 0)
                        {
                            msgCount.Visible = true;
                            msgImage.Visible = true;
                            msgCount.Text = count.ToString();
                            msgAllCount += Convert.ToInt32(msgCount.Text);
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.ToString());
                    }

                }
                #endregion

                #region seenMessageCount for group
                else
                {
                    try
                    {
                        int count = 0;
                        DataTable dtCount;
                        dtCount = new DataTable();
                        clsCB = new clsChatBox();
                        clsCB.Sender = clsGlobal.GetSessionValue(clsEnum.SessionVariables.UserName);
                        clsCB.Reciever = lblbtn.Text;
                        dtCount = clsCB.ReadMsgCountforGroup();
                        count = Convert.ToInt32(Convert.ToDouble(dtCount.Rows[0]["RowCount"].ToString()) + Convert.ToDouble(dtCount.Rows[0]["NullRowCount"].ToString()));
                        if (count > 0)
                        {
                            msgCount.Visible = true;
                            msgImage.Visible = true;
                            msgCount.Text = count.ToString();
                            msgAllCount += Convert.ToInt32(msgCount.Text);
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.ToString());
                    }
                }
                #endregion

                if (msgAllCount > 0)
                {
                    AllmsgImage.Visible = true;
                    lblAllMessageCount.Text = "(" + "<span style='color:Red;'>" + msgAllCount.ToString() + "</span>" + ")" + "Message(s)";
                }
                else
                {
                    lblAllMessageCount.Text = "(" + "<span style='color:Red;'>" + "0" + "</span>" + ")" + "Message(s)";
                }

            }
        }
        #endregion

        #region Timer1_Tick
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            LoadChatbox();
            Load_Frends();
            clsRegister reg;
            try
            {
                reg = new clsRegister();
                reg.UserName = clsGlobal.GetSessionValue(clsEnum.SessionVariables.UserName);
                reg.IsActive = DateTime.Now.ToString();
                reg.updateIsActive();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (msgAllCount > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "JavaScript", "javascript:callNF()", true);
            }
            
        }
        #endregion

        #region btngpdone_onclick
        protected void btngpdone_onclick(object sender, EventArgs e)
        {

            int result = 0;
            String imgs = String.Empty;
            String[] userName = Request["txtUserName"].ToString().Split(',');
            clsRegister user = new clsRegister();
            foreach (String value in userName)
            {
                user.UserName = value;
                imgs += user.SelectImage();
            }
            if (hdnAddUserName.Value != "" && hdnAddUserName.Value != "All")
            {
                user.UserName = Request["txtUserName"].ToString();
                result = user.updateAddUser(hdnAddUserName.Value);

            }
            else if (hdnAddUserName.Value == "All")
            {
                user.UserName = Request["txtUserName"].ToString();
                result = user.updateAddUser(lblReceiver.Text);
            }
            else
            {
                user.UserName = Request["txtUserName"].ToString() + "," + lblSender.Text;
                if (fileUploadGP.FileName != null)
                {
                    String FilePath = Server.MapPath("~/Style/Images/");
                    if (!Directory.Exists(FilePath))
                    {
                        Directory.CreateDirectory(FilePath);
                    }
                    fileUploadGP.SaveAs(FilePath + Path.GetFileName(fileUploadGP.FileName));
                    ImageGP.Visible = true;
                    ImageGP.ImageUrl = "~/Style/Images/" + Path.GetFileName(fileUploadGP.FileName);
                    user.Image = ImageGP.ImageUrl;
                    result = user.insert();

                }
            }

            if (result > 0)
            {
                Load_Frends();
                LoadChatbox();
                Response.Redirect("~/Chat.aspx");
            }

            //lblMsg.Text = "Please, choose group photo.";
            //lblMsg.Style.Add(HtmlTextWriterStyle.Color, "Red");
            //lblMsg.Style.Add(HtmlTextWriterStyle.PaddingLeft, "20px");

        }
        #endregion

        #region btnChange_onclick
        protected void btnChange_onclick(object sender, EventArgs e)
        {
            clsUser user;
            int Res = 0;
            try
            {
                user = new clsUser();
                user.UserName = clsGlobal.GetSessionValue(clsEnum.SessionVariables.UserName);
                user.Pass = clsGlobal.Encrypt(hdnPassword.Value);
                Res = user.UpdatePassword();
                if (Res > 0)
                {
                    lblMsg.Text = "Password was successfully changed.";
                    lblMsg.ForeColor = System.Drawing.Color.Blue;
                    lblMsg.Font.Size = 10;
                }
                else
                {
                    lblMsg.Text = "Password was not changed.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Font.Size = 10;

                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }
        #endregion

        #region btnCancel_onclick
        protected void btnCancel_onclick(object sender, EventArgs e)
        {
            Response.Redirect("~/Chat.aspx");
        }
        #endregion

        #region btnUpload_OnClick
        protected void btnUpload_OnClick(object sender, EventArgs e)
        {
            clsRegister reg = new clsRegister();
            reg.UserName = clsGlobal.GetSessionValue(clsEnum.SessionVariables.UserName);
            if (fileUpload.FileName != null)
            {
                String filePath = Server.MapPath("~/Style/Images/");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                fileUpload.SaveAs(filePath + Path.GetFileName(fileUpload.FileName));
                imgUpload.Visible = true;
                imgUpload.ImageUrl = "~/Style/Images/" + Path.GetFileName(fileUpload.FileName);
                reg.Image = imgUpload.ImageUrl;
                int res = reg.uploadImage();
            }
            else
            {
                Response.Write("Please upload the image file");
            }

        }
        #endregion

        #region btnBack_Onclick
        protected void btnBack_Onclick(object sender, EventArgs e)
        {
            clsGlobal.RemoveSessionValue(clsEnum.SessionVariables.HiddenName);
            Response.Redirect("~/Chat.aspx");
        }

        #endregion

        #region btnUploadGp_OnClick
        protected void btnUploadGp_OnClick(object sender, EventArgs e)
        {
            string lbl = null;
            clsRegister reg = new clsRegister();
            if (this.hdngpName.Value != "")
            {
                reg.UserName = this.hdngpName.Value;
            }
            else
            {
                reg.UserName = lblReceiver.Text;
            }
            if (fileUploadGpPhoto.FileName != null)
            {
                String filePath = Server.MapPath("~/Style/Images/");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                fileUploadGpPhoto.SaveAs(filePath + Path.GetFileName(fileUploadGpPhoto.FileName));
                imgGpPhoto.Visible = true;
                imgGpPhoto.ImageUrl = "~/Style/Images/" + Path.GetFileName(fileUploadGpPhoto.FileName);
                reg.Image = imgGpPhoto.ImageUrl;
                int res = reg.uploadImage();
                if (res > 0)
                {
                    Load_Frends();
                    LoadChatbox();
                    Response.Redirect("~/Chat.aspx");
                }
            }
            else
            {
                Response.Write("Please upload the image file");
            }

        }
        #endregion

        #region btnDownloadFile_OnClick
        protected void btnDownloadFile_OnClick(object sender, EventArgs e)
        {
            LinkButton lbtn = (LinkButton)sender;
            String FilePath = Server.MapPath("~/Files/");
            String fName = lbtn.CommandArgument;
            byte[] bytes;
            FileInfo finfo = new FileInfo(FilePath + fName);
            string fileName = null, contentType = null;
            clsChatBox clsCB;
            DataTable dtFD;
            try
            {
                clsCB = new clsChatBox();
                dtFD = new DataTable();
                clsCB.FileName = fName;
                dtFD = clsCB.selectFileData();
                bytes = System.Text.Encoding.ASCII.GetBytes(dtFD.Rows[0]["FileData"].ToString());
                contentType = dtFD.Rows[0]["FileContentType"].ToString();
                fileName = dtFD.Rows[0]["FileName"].ToString();
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.AppendHeader("Content-Disposition", ("attachment; filename=" + fileName));
            Response.ContentType = contentType;
            Response.TransmitFile(Server.MapPath("~/Files/" + fName));
        }
        #endregion

        #region btnGpDelete_OnClick
        protected void btnGpDelete_OnClick(object sender, EventArgs e)
        {
            clsRegister clsReg;
            int res = 0;
            try
            {
                clsReg = new clsRegister();
                if (hdnDeleteName.Value != "")
                {

                    clsReg.UserName = hdnDeleteName.Value;
                }
                else
                {
                    clsReg.UserName = lblReceiver.Text;
                }
                res = clsReg.DeleteGp();
                if (res > 0)
                {
                    Load_Frends();
                    Load_Frends();
                    Response.Redirect("~/Chat.aspx");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }


        }
        #endregion

        #region btnGpLeave_OnClick
        protected void btnGpLeave_OnClick(object sender, EventArgs e)
        {
            clsRegister clsReg;
            int res = 0;
            try
            {
                clsReg = new clsRegister();
                if (hdnLeaveName.Value != "")
                {
                    clsReg.UserName = hdnLeaveName.Value;
                }
                else
                {
                    clsReg.UserName = lblReceiver.Text;
                }
                res = clsReg.updateRemoveUser(lblSender.Text);
                if (res > 0)
                {
                    Load_Frends();
                    LoadChatbox();
                    Response.Redirect("~/Chat.aspx");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }
        #endregion

        #region lbtnDeleteConversation_onclick
        protected void lbtnDeleteConversation_onclick(object sender, EventArgs e)
        {
            clsChatBox clsCB;
            int res = 0;
            try
            {
                clsCB = new clsChatBox();
                if (hdnDeleteConversation.Value != "")
                {
                    clsCB.Reciever = hdnDeleteConversation.Value;
                }
                else
                {
                    clsCB.Reciever = lblReceiver.Text;
                }
                clsCB.Sender = lblSender.Text;
                res = clsCB.deleteConversation();
                if (res > 0)
                {
                    Load_Frends();
                    LoadChatbox();
                    Response.Redirect("~/Chat.aspx");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }
        #endregion

        #region txtSearchPeople_OnPreRender
        protected void txtSearchPeople_OnPreRender(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.Attributes.Add("placeholder", txt.ToolTip);
        }
        #endregion

        #region txtSearchPeople_OnTextChanged
        protected void txtSearchPeople_OnTextChanged(object sender, EventArgs e)
        {
            // ScriptManager.RegisterClientScriptBlock(this, GetType(), "", "onkeypress();", true);
            clsRegister clsReg;
            clsChatBox clsCb;
            DataTable dtOne;
            DataTable dtTwo;

            try
            {
                dtOne = new DataTable();
                dtTwo = new DataTable();
                clsReg = new clsRegister();
                if (txtSearchPeople.Text != "")
                {
                    clsReg.UserName = txtSearchPeople.Text;
                    dtTwo = clsReg.selectByName(lblSender.Text);
                }
                else
                {
                    clsCb = new clsChatBox();
                    clsCb.Sender = lblSender.Text;
                    dtTwo = clsCb.SelectByNName();
                }
                ddlUserList.DataSource = dtTwo;
                ddlUserList.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }
        #endregion

        #region lbtnEditBio_onclick
        protected void lbtnEditBio_onclick(object sender, EventArgs e)
        {
            clsUser clsUser;
            try
            {
                clsUser = new clsUser();
                clsUser.UserName = clsUser.GetSessionValue(clsEnum.SessionVariables.UserName);
                clsUser.Bio = txtEditBio.Text;
                int res = clsUser.EditBio();
                Response.Redirect("~/Chat.aspx");

            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }
        #endregion

        #region txtEditBio_OnTextChanged
        protected void txtEditBio_OnTextChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "", "onmouseover();", true);
        }
        #endregion
    }
}