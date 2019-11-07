<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="ChatApp.Chat" %>

<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="icon" type="image/png" href="Style/LoginCss/images/icons/2.ico" />
    <link href="../Style/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Style/css/messsages.css" rel="stylesheet" />
    <link href="../Style/fonts/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../Style.css" rel="stylesheet" type="text/css" />
    <script src="../Style/js/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Style/js/jquery-ui.min1.js" type="text/javascript"></script>
    <script src="../Style/js/jquery.min.js" type="text/javascript"></script>
    <style type="text/css">
        .RightAlign
        {
            text-align: right;
        }
    </style>
    <script type="text/javascript">
    setTimeout(function(){
    location.reload();},30000);

    function funMsgAlert(count)
    {
   document.getElementById('<%= hdnmsgAlert.ClientID%>').value = count;
    (function () 
        {
        if ("Notification" in window) 
        {
          var permission = Notification.permission;
           if (permission === "denied" || permission === "granted") 
           {
                    return;
           }
           Notification.requestPermission();     
        }})();

        (function () 
            {
            if ("Notification" in window) 
            {
                var permission = Notification.permission;
                Notification
                .requestPermission()
                .then(function (ans) 
                {      
                if(ans == "granted")
                {  
                if(count>0)
                {
                   var notification = new Notification("MMB Chat",{icon:'../Style/images/info-popup.png',body: "(" + count + ") Message(s)"},);
                   }
                }
                });
             }})();
               }

        $(document).ready(function () {
            $('#messagewindow').animate({
                scrollTop: $('#messagewindow')[0].scrollHeight
            }, 0);
        });

        function button_click(txt) {
            if (window.event.keyCode == 13) {
                $("#btnSent").click();
            }
        }
        function cancel() {
            document.getElementById("ConfirmationPassword").required = false;
            document.getElementById("CurrentPassword").required = false;
            document.getElementById("NewPassword").required = false;
        }

        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    $('#blah')
                        .attr('src', e.target.result);
                };

                reader.readAsDataURL(input.files[0]);
                var img = document.getElementById("blah");
                img.style.visibility = "visible";
            }
        }

        function showForm() {
            $('#mask').hide();
            $('.window').hide();
        }

        function showFormOne() {
            $('#maskone').hide();
            $('.windowone').hide();
        }

        function showFormTwo() {
            $('#masktwo').hide();
            $('.windowtwo').hide();
        }

        function showFormThree() {
            $('#maskthree').hide();
            $('.windowthree').hide();
        }
        function showFormFour() {
            $('#maskfour').hide();
            $('.windowfour').hide();
        }
        function showFormFive() {
            $('#maskfive').hide();
            $('.windowfive').hide();
        }
        function showFormSix() {
            $('#masksix').hide();
            $('.windowsix').hide();
        }
        function showFormSeven() {
            $('#maskseven').hide();
            $('.windowseven').hide();
        }
        function showFormEight() {
            $('#maskeight').hide();
            $('.windoweight').hide();
        }
        function gpNameKeyUp() {
            $.post("../GetGroupChatInfo.aspx", { UserName: $("#UserName").val() }, function (data, status) { $("#NameResult").html(data) });
        }

        function gpNameKeyUpConversation() {
            $.post("../GetSearchInfo.aspx", { Name: $("#Name").val() }, function (data, status) { $("#NameResultConversation").html(data) });
        }

        function correctcurrent(currentpass) {
            var Password = document.getElementById('<%=hdnPass.ClientID %>');
            var PassworVal = Password.value;
            var currentPassword = $("#CurrentPassword").val();
            if (currentPassword != PassworVal) {
                alert("CurrentPassword is invalid.");
            }
            else {
                document.getElementById("NewPassword").disabled = false;
            }
        }

        function confirmationpassword(confirmpassword) {
            var NewPassword = $("#NewPassword").val();
            var ConfirmPassword = confirmpassword;
            if (NewPassword != ConfirmPassword) {
                alert("NewPassword and ConfirmationPassword must be equal.");
                $("#btnChange").attr('disabled', true);
            }
            else {
                $("#hdnPassword").val(ConfirmPassword);
                $("#btnChange").attr('disabled', false);
            }
        }

        $(document).ready(function () {

            /*-----------------------------------------------------*/
            $("#EmployeeName").keypress(function (event) {

                alert(event.keyCode);
                if (event.keyCode == 13) {
                    event.preventDefault();
                }
            });
            $("#EmployeeName").keyup(function () {

                alert($("#EmployeeName").val());
                $.post("../MasterForms/GetEmployeeInfoForAJAX.aspx",
                {
                    EmployeeName: $("#EmployeeName").val()
                },
                function (data, status) {
                    $("#NameResult").html(data);

                });
            });

            $('.window .close .btn-default').click(function (e) {
                $('#mask').hide();
                $('.window').hide();
            });
        });

        function ShowPeopleList(a) {
            var ans = a.getAttribute("rel");
            document.getElementById('<%=hdnAddUserName.ClientID%>').innerHTML = ans;
            document.getElementById('<%=hdnAddUserName.ClientID %>').value = ans;
            var id = $(a).attr('href');
            var maskHeight = $(document).height();
            var maskWidth = $(window).width();
            //Set heigth and width to mask to fill up the whole screen
            $('#mask').css({ 'width': maskWidth, 'height': maskHeight });
            $('#mask').fadeIn(300);
            $('#mask').fadeTo("slow", 0.8);

            //Get the window height and width
            var winH = $(window).height();
            var winW = $(window).width();

            $(id).css('top', winH / 2 - $(id).height() / 2);
            $(id).css('left', winW / 2 - $(id).width() / 2);

            //transition effect
            $(id).fadeIn(500);
        }

        function ShowConversation(a) {

            var id = $(a).attr('href');
            var maskHeight = $(document).height();
            var maskWidth = $(window).width();
            //Set heigth and width to mask to fill up the whole screen
            $('#maskthree').css({ 'width': maskWidth, 'height': maskHeight });
            $('#maskthree').fadeIn(300);
            $('#maskthree').fadeTo("slow", 0.8);

            //Get the window height and width
            var winH = $(window).height();
            var winW = $(window).width();

            $(id).css('top', winH / 2 - $(id).height() / 2);
            $(id).css('left', winW / 2 - $(id).width() / 2);

            //transition effect
            $(id).fadeIn(500);
        }

        function ChangePassword(a) {
            var id = $(a).attr('href');
            var wHeight = $(document).height();
            var wWidth = $(window).width();
            $('#maskone').css({ 'width': wWidth, 'height': wHeight });
            $('#maskone').fadeIn(300);
            $('#maskone').fadeTo("slow", 0.8);
            $(id).fadeIn(800);
        }

        function Bio(a) {

            var ans = document.getElementById('<%=lblBio.ClientID%>').innerText;
            document.getElementById('<%=txtEditBio.ClientID%>').value = ans;
            var id = $(a).attr('href');
            var wHeight = $(document).height();
            var wWidth = $(window).width();
            $('#maskeight').css({ 'width': wWidth, 'height': wHeight });
            $('#maskeight').fadeIn(300);
            $('#maskeight').fadeTo("slow", 0.8);
            $(id).fadeIn(800);
        }

        function onmouseover() {
            var OldBio = $("#hdnEditBio").val();
            var NewBio = $("#txtEditBio").val();
            if (OldBio != NewBio) {
                $("#lbtnEditBio").attr('disabled', false);
            }
            else {
                $("#lbtnEditBio").attr('disabled', true);
            }
        }

        function Setting(a) {
            var id = $(a).attr('href');
            var wHeight = $(document).height();
            var wWidth = $(window).width();
            $('#masktwo').css({ 'width': wWidth, 'height': wHeight });
            $('#masktwo').fadeIn(300);
            $('#masktwo').fadeTo("slow", 0.8);
            $(id).fadeIn(800);
        }

        function Manage(a) {
            var ans = a.getAttribute("rel");
            document.getElementById('<%=hdngpName.ClientID%>').innerHTML = ans;
            document.getElementById('<%=hdngpName.ClientID %>').value = ans;
            var id = $(a).attr('href');
            var height = $(document).height();
            var width = $(window).width();
            $('#maskfour').css({ 'width': width, 'height': height });
            $('#maskfour').fadeIn(300);
            $('#maskfour').fadeTo("slow", 0.8);
            $(id).fadeIn(800);
        }

        function Delete(a) {
            var ans = a.getAttribute("rel");
            document.getElementById('<%=hdnDeleteName.ClientID%>').innerHTML = ans;
            document.getElementById('<%=hdnDeleteName.ClientID%>').value = ans;
            var id = $(a).attr('href');
            var height = $(document).height();
            var width = $(window).width();
            $('#maskfive').css({ 'width': width, 'height': height });
            $('#maskfive').fadeIn(300);
            $('#maskfive').fadeTo("slow", 0.8);
            $(id).fadeIn(800);
        }
        function Leave(a) {
            var ans = a.getAttribute("rel");
            document.getElementById('<%=hdnLeaveName.ClientID%>').innerHTML = ans;
            document.getElementById('<%=hdnLeaveName.ClientID%>').value = ans;
            var id = $(a).attr('href');
            var height = $(document).height();
            var width = $(window).width();
            $('#masksix').css({ 'width': width, 'height': height });
            $('#masksix').fadeIn(300);
            $('#masksix').fadeTo("slow", 0.8);
            $(id).fadeIn(800);
        }
        function DeleteConversation(a) {
            var ans = a.getAttribute("rel");
            document.getElementById('<%=hdnDeleteConversation.ClientID%>').innerHTML = ans;
            document.getElementById('<%=hdnDeleteConversation.ClientID%>').value = ans;
            var id = $(a).attr('href');
            var height = $(document).height();
            var width = $(window).width();
            $('#maskseven').css({ 'width': width, 'height': height });
            $('#maskseven').fadeIn(300);
            $('#maskseven').fadeTo("slow", 0.8);
            $(id).fadeIn(800);
        }
        function AddEmployeeIDName(EmployeeID, EmployeeName, p1, p2, p3, p4, p5, p6, p7, p8) {
            $("#<%=txtEmployeeID.ClientID %>").val(EmployeeID);
            $("#<%=txtEmployeeName.ClientID %>").val(EmployeeName);
            $("#mask").hide();
            $(".window").hide();
        }
        function confirmation() {
            if (confirm('Are you sure you want to delete ?')) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="Form1" runat="server">
    <asp:HiddenField ID="hdnmsgAlert" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid">
        <div class="container ng-scope">
            <div class="block-header">
                <h2>
                </h2>
            </div>
            <div class="card m-b-0" id="messages-main" style="box-shadow: 0 0 40px 1px #c9cccd;">
                <div class="ms-menu" style="overflow: scroll; overflow-x: hidden;" id="ms-scrollbar">
                    <div class="ms-block">
                        <div class="lv-avatar pull-right">
                            <asp:LinkButton ID="btnBack" runat="server" OnClick="btnBack_Onclick" Text="Back"
                                Style="font-size: 12px; font-weight: bold; padding-left: 13px;"></asp:LinkButton>
                        </div>
                        <div class="ms-user">
                            <asp:Image ID="Image1" runat="server" Style="width: 50px; height: 50px; border-radius: 50px;" />
                            <h5 class="q-title" style="text-align: center; padding-left: 10px; margin-left: 10px;">
                                <asp:Label ID="lblSender" runat="server" Text="Label"></asp:Label>
                                <br />
                                <br />
                                <asp:Label ID="lblAllMessageCount" runat="server" Visible="true" ForeColor="Blue"></asp:Label>
                                <asp:Image ID="AllmsgImage" runat="server" Visible="false" ImageUrl="~/Style/images/sms..png"
                                    Width="20px" Height="20px" />
                            </h5>
                            <asp:Label ID="lblBio" runat="server" ForeColor="GrayText" Font-Size="Small"></asp:Label>
                            <br />
                            <div class="dropdown">
                                <button class="dropbtn">
                                    <img src="Style/images/download.png" /></button>
                                <div class="dropdown-content">
                                    <a id="A5" href="#Bio" runat="server" onclick="Bio(this);" class="colorblue">Edit Bio</a>
                                    <a id="A6" href="~/Calendar/Calendar.aspx" runat="server" onclick="Calendar(this);"
                                        class="colorblue">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/img/calendar3.png" Style="padding-top: 2px;
                                            padding-left: 1px; width: 70px; height: 40px;" /></a> <a id="A1" href="#Setting"
                                                runat="server" onclick="Setting(this);" class="colorblue">Upload Photo</a>
                                    <a id="A2" href="#ChangePassword" runat="server" onclick="ChangePassword(this);"
                                        class="colorblue">Change Password</a> <a id="A3" href="Login.aspx" runat="server"
                                            class="colorred">Logout</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div>
                        <asp:Label ID="lblMsg" runat="server" Style="text-align: center;"></asp:Label></div>
                    <div class="ms-block">
                        <asp:TextBox ID="txtEmployeeID" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtEmployeeName" runat="server" Visible="false"></asp:TextBox>
                        <asp:Label ID="hdnUserName" runat="server" Visible="true"></asp:Label>
                        <a id="A4" href="#EmployeeList" onclick="ShowPeopleList(this);" runat="server" class="btn btn-primary btn-block ms-new">
                            Create Group </a>
                    </div>
                    <hr />
                    <div style="padding: 1px 10px 15px 10px;">
                        <asp:TextBox ID="txtSearchPeople" runat="server" CssClass="form-control" ToolTip="Search People"
                            OnTextChanged="txtSearchPeople_OnTextChanged" OnPreRender="txtSearchPeople_OnPreRender"
                            AutoPostBack="true"></asp:TextBox></div>
                    <div class="listview lv-user m-t-20">
                        <asp:DataList ID="ddlUserList" runat="server" OnItemDataBound="ddlUserList_OnItemDataBound">
                            <ItemTemplate>
                                <div class="lv-item media">
                                    <div class="lv-avatar pull-left">
                                        <asp:Image ID="Image2" runat="server" ImageUrl='<%# Bind("Image","{0}") %>' />
                                    </div>
                                    <div class="media-body">
                                        <div class="lv-title">
                                        </div>
                                        <asp:LinkButton ID="lbtnUserList" ForeColor="Black" runat="server" Text='<%# Eval("Name") %>'
                                            OnClick="lbtnUserList_Click" OnClientClick="srcoll()"></asp:LinkButton>
                                        <asp:Image ID="msgImage" runat="server" Visible="false" ImageUrl="~/Style/images/sms..png"
                                            Width="20px" Height="20px" />
                                        <asp:Label ID="msgCount" runat="server" Visible="false" ForeColor="Red" Style="font-size: 15px;"></asp:Label><br />
                                        <asp:Label ID="lbl" runat="server"></asp:Label>
                                        <asp:Label ID="span" runat="server"> </asp:Label>
                                        <asp:Label ID="lblIsActive" ForeColor="Black" Visible="false" runat="server" Text='<%# Bind("IsActive") %>'></asp:Label>
                                    </div>
                                    <div class="dropdown">
                                        <button class="dropbtn" id="btn" name="btn" runat="server" visible="false">
                                            <img src="Style/images/info-popup.png" style="width: 30px; height: 30px;" /></button>
                                        <div class="dropdown-content">
                                            <a id="managegp" href="#Manage" runat="server" onclick="Manage(this)" class="colorblue"
                                                rel='<%# Eval("Name") %>'>Upload Photo </a><a id="deletegp" href="#Delete" runat="server"
                                                    onclick="Delete(this)" class="colorblue" rel='<%# Eval("Name") %>'>Delete Group</a>
                                            <a id="addusergp" href="#EmployeeList" runat="server" onclick="ShowPeopleList(this)"
                                                class="colorblue" rel='<%# Eval("Name") %>'>Add User</a> <a id="leavegp" href="#Leave"
                                                    runat="server" onclick="Leave(this)" class="colorblue" rel='<%# Eval("Name") %>'>
                                                    Leave Group</a> <a id="deleteconversation" href="#DeleteConversation" runat="server"
                                                        onclick="DeleteConversation(this)" class="colorblue" rel='<%# Eval("Name") %>'>Delete
                                                        Conversation</a>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </div>
                <div class="ms-body">
                    <div class="listview lv-message">
                        <div class="lv-header-alt clearfix">
                            <div id="lstBody" runat="server" visible="true">
                                <div class="lv-avatar pull-left">
                                </div>
                            </div>
                            <div class="lvh-label hidden-xs" id="lstBodyV" runat="server" visible="false">
                                <div class="lv-avatar pull-left">
                                    <asp:Image ID="Image3" runat="server" />
                                </div>
                                <span class="c-black pull-left">
                                    <asp:Label ID="lblReceiver" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="spanall" runat="server"></asp:Label>
                                    <asp:Label ID="lblIsActiveall" ForeColor="Black" runat="server" Text=""></asp:Label>
                                </span>
                                <div class="dropdown">
                                    <asp:Button CssClass="dropbtn" ID="btn" runat="server" Text="..." Visible="false" />
                                    <div class="dropdown-content">
                                        <a id="managegp" href="#Manage" runat="server" onclick="Manage(this)" class="colorblue"
                                            rel='<%# Eval("Name") %>'>Upload Photo</a><a href="#Delete" runat="server" id="deletegp"
                                                onclick="Delete(this)" class="colorblue" rel='<%# Eval("Name") %>'>Delete Group</a>
                                        <a id="addusergp" href="#EmployeeList" runat="server" onclick="ShowPeopleList(this)"
                                            class="colorblue" rel="All">Add User</a> <a id="leavegp" href="#Leave" runat="server"
                                                onclick="Leave(this)" class="colorblue" rel='<%# Eval("Name") %>'>Leave Group</a>
                                        <a id="deleteconversation" href="#DeleteConversation" runat="server" onclick="DeleteConversation(this)"
                                            class="colorblue" rel='<%# Eval("Name") %>'>Delete Conversation</a>
                                    </div>
                                </div>
                            </div>
                            <asp:Label ID="lblBioEdit" ForeColor="GrayText" Font-Size="Small" runat="server"></asp:Label>
                        </div>
                        <div id="chatwindow">
                            <div id="messagewindow">
                                <div class="lv-body" id="ms-scrollbar" style="overflow: scroll; overflow-x: hidden;">
                                    <div class="lv-body" id="msBody" runat="server" visible="true" style="margin-top: 50px;">
                                        <div style="text-align: center; font-family: Times New Roman; font-size: medium;">
                                            <h1>
                                                <span style="font-weight: bold">Welcome ,
                                                    <asp:Label runat="server" ID="lblUser"></asp:Label></span>
                                            </h1>
                                            <asp:Image ID="imgUser" runat="server" Style="border: 1px dashed; border-radius: 50px;
                                                width: 50px; height: 50px;" />
                                        </div>
                                        <div>
                                            <a href="#ShowConversation" onclick="ShowConversation(this);" runat="server" class="btnone btn btn-primary btn-block ms-new"
                                                id="ah">Start a Conversation </a>
                                        </div>
                                        <div>
                                            <asp:Label ID="lblOne" runat="server" Text="Search for someone to start chatting with or"
                                                CssClass="lblOne"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="lblTwo" runat="server" Text="go to Contacts to see who is available."
                                                CssClass="lblTwo"></asp:Label>
                                        </div>
                                        <div style="font-weight: bold; text-align: center; margin-top: 25px;">
                                            You are signed in as
                                            <asp:Label ID="lblThree" runat="server" CssClass="lblThree"></asp:Label>
                                        </div>
                                    </div>
                                    <asp:UpdatePanel ID="updatePanel" runat="server">
                                        <ContentTemplate>
                                            <div>
                                                <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="6000">
                                                </asp:Timer>
                                                <asp:DataList ID="ddlChat" runat="server" OnItemDataBound="ddlChat_OnItemDataBound"
                                                    Width="100%">
                                                    <ItemTemplate>
                                                        <div style="width: 100%;">
                                                            <div>
                                                                <div class="lv-item media box">
                                                                    <div>
                                                                        <asp:Image ID="Image4" CssClass="circular--square" Style="width: 30px; height: 30px;"
                                                                            runat="server" ImageUrl='<%# Bind("Image") %>' />
                                                                    </div>
                                                                    <asp:Panel ID="pnlMediaBody" runat="server" CssClass="media-body">
                                                                        <div>
                                                                            <asp:Label runat="server" ID="lblReciever" Text='<%# Bind("Sender") %>'></asp:Label>
                                                                            <div class="ms-item">
                                                                                <asp:Label ID="Message" ForeColor="Black" runat="server" Text='<%# String.Format("{0}", Eval("Message")) %>'></asp:Label>
                                                                                <asp:LinkButton ID="btnDownloadFile" Text='<%# Eval("FileName") %>' CommandArgument='<%# Eval("FileName") %>'
                                                                                    Visible="false" OnClick="btnDownloadFile_OnClick" runat="server" />
                                                                                <asp:Image ID="imgDownload" runat="server" ImageUrl="" Visible="false" Style="width: 70px;
                                                                                    height: 70px;" />
                                                                                <asp:LinkButton ID="btnImgDownload" runat="server" Visible="false" CommandArgument='<%# Eval("FileName") %>'
                                                                                    Text='<%# Eval("FileName") %>' OnClick="btnDownloadFile_OnClick"></asp:LinkButton>
                                                                            </div>
                                                                            <br />
                                                                            <asp:Label ID="Date" runat="server" ForeColor="Black" Font-Bold="false" Text='<%# Bind("Date") %>'></asp:Label><br />
                                                                            <asp:Label ID="seenID" runat="server" Text='<%# Bind("ID") %>'> Visible="false" Style="text-align: center;
                                                                    font-weight: bold; color: Gray; font-size: 12px;"></asp:Label>
                                                                            <asp:Image ID="imgSeen" CssClass="circular--square" Style="width: 20px; height: 20px;
                                                                                border-radius: 50px;" Visible="false" runat="server" ImageUrl='<%# Bind("Image") %>' />
                                                                            <asp:Panel ID="pnl" runat="server">
                                                                            </asp:Panel>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </div>
                                                            </div>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:TextBox ID="hdnFocus" runat="server" Style="width: 0%; height: 0%; border: none;"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="clearfix">
                        </div>
                        <div class="lv-footer ms-reply" id="lstFooter" runat="server" visible="true">
                            <div class="textarea">
                            </div>
                        </div>
                        <div class="lv-footer ms-reply" id="lstFooterV" runat="server" visible="false">
                            <asp:TextBox CssClass="textarea" ID="txtMessage" placeholder="Write messages..."
                                TextMode="MultiLine" runat="server"></asp:TextBox>
                            <asp:FileUpload ID="fileUploadName" runat="server" Style="background-color: #f5f5f5;" />
                            <button id="btnSent" runat="server" class="button" onserverclick="btnsent_onServerClick">
                                <img id="Img1" src="Style/images/sendimg1.jpg" runat="server" alt="img" style="width: 30px;
                                    padding-bottom: 20px;" />
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="EmployeeList" class="window" style="padding-top: 20px;">
        <div class="form-group">
            <div class="col-sm-10">
                <img id="imgClose" src="Style/images/close.png" alt="Close" onclick="showForm();"
                    class="col-sm-2" />
                <label class="control-label col-sm-6" style="font-size: 25px; margin-left: 90px;">
                    Add to Group
                </label>
            </div>
            <div class="col-sm-2">
                <asp:LinkButton ID="btngpDone" Text="Done" class="btn btn-primary" runat="server"
                    OnClick="btngpdone_onclick"></asp:LinkButton>
            </div>
        </div>
        <div class="form-group" style="padding-top: 50px; padding-left: 10px; padding-right: 10px;">
            <div class="col-sm-13">
                <input type="text" id="UserName" name="UserName" class="form-control" placeholder="Search People"
                    onkeyup="gpNameKeyUp();" />
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-3">
                <asp:HiddenField ID="hdnAddUserName" runat="server" />
                <asp:FileUpload ID="fileUploadGP" runat="server" />
                <asp:Image ID="ImageGP" runat="server" Style="width: 300px; height: 100px;" Visible="false" />
                <div id="txtareagp" style="display: none; overflow-x: scroll;" class="divgp">
                    <div id="childgp" class="divchild" runat="server">
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="form-group">
            <div id="NameResult" style="width: 500px; height: 330px; overflow: scroll; margin-top: 90px;
                padding-left: 45px; visibility: hidden;">
            </div>
        </div>
    </div>
    <div id="mask">
    </div>
    <div id="ChangePassword" class="windowone">
        <div class="form-group" style="margin-bottom: 60px; margin-left: 10px;">
            <div class="col-sm-7">
                <img id="imgCloseOne" src="Style/images/close.png" alt="Close" onclick="showFormOne();"
                    class="col-sm-2 top" /></div>
            <div class="col-sm-4">
            </div>
        </div>
        <div class="form-group">
            <asp:HiddenField ID="hdnPass" ClientIDMode="Static" runat="server" />
        </div>
        <div class="form-group">
            <div class="col-sm-4">
                Current Password :
            </div>
            <div class="col-sm-7">
                <input type="password" id="CurrentPassword" name="CurrentPassword" class="form-control"
                    onchange="correctcurrent(this.value);" required />
            </div>
        </div>
        <br />
        <br />
        <br />
        <div class="form-group">
            <div class="col-sm-4">
                New Password :
            </div>
            <div class="col-sm-7">
                <input type="password" id="NewPassword" name="NewPassword" class="form-control" disabled
                    required />
            </div>
        </div>
        <br />
        <br />
        <div class="form-group">
            <div class="col-sm-4">
                Confirmation Password :
            </div>
            <div class="col-sm-7">
                <input type="password" id="ConfirmationPassword" name="ConfirmationPassword" class="form-control"
                    onchange="confirmationpassword(this.value);" required />
            </div>
        </div>
        <br />
        <br />
        <div class="form-group">
            <div class="col-sm-3">
            </div>
            <div class="col-sm-3">
                <asp:HiddenField ID="hdnPassword" ClientIDMode="Static" runat="server" />
                <asp:Button ID="btnChange" Text="Confirm" class="btn btn-primary" runat="server"
                    OnClick="btnChange_onclick" />
            </div>
            <div class="col-sm-3">
                <asp:Button ID="btnCancel" Text="Cancel" class="btn btn-default" runat="server" OnClientClick="cancel();"
                    OnClick="btnCancel_onclick" />
            </div>
        </div>
    </div>
    <div id="maskone">
    </div>
    <div id="Setting" class="windowtwo bgcolor">
        <div class="form-group" style="margin-bottom: 90px;">
            <div class="col-sm-7">
                <img id="imgCloseTwo" src="Style/images/close.png" alt="Close" onclick="showFormTwo();"
                    class="col-sm-2 top" /></div>
            <div class="col-sm-4">
                &nbsp;
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-4">
                Upload Photo :
            </div>
            <div class="col-sm-7">
                <asp:FileUpload ID="fileUpload" runat="server" />
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-1">
            </div>
            <div class="col-sm-10">
                <asp:Image ID="imgUpload" runat="server" Style="width: 300px; height: 100px;" Visible="false" />
            </div>
        </div>
        <br />
        <div class="form-group" style="margin-left: 130px; margin-top: 40px;">
            <div class="col-sm-3">
                <asp:Button ID="btnUpload" Text="Commit" class="btn btn-primary" runat="server" OnClientClick="cancel();"
                    OnClick="btnUpload_OnClick" />
            </div>
            <div class="col-sm-4">
                <asp:Button ID="CancelOne" Text="Cancel" runat="server" OnClientClick="cancel();"
                    OnClick="btnCancel_onclick" CssClass="btn btn-default" />
            </div>
        </div>
    </div>
    <div id="masktwo">
    </div>
    <div id="ShowConversation" class="windowthree" style="padding-top: 20px;">
        <div class="form-group">
            <div class="col-sm-10">
                <img id="img2" src="Style/images/close.png" alt="Close" onclick="showFormThree();"
                    class="col-sm-2" />
                <label class="control-label col-sm-6" style="font-size: 25px; margin-left: 90px;">
                    New Chat
                </label>
            </div>
            <div class="col-sm-2">
            </div>
        </div>
        <div class="form-group" style="padding-top: 50px;">
            <div class="col-sm-13">
                <input type="text" id="Name" name="Name" class="form-control" placeholder="Search"
                    style="height: 60px; background-color: #f3f1f1; font-size: 20px; font-weight: bold;
                    font-family: Times New Roman;" onkeyup="gpNameKeyUpConversation();" />
            </div>
        </div>
        <br />
        <div class="form-group">
            <div id="NameResultConversation" style="width: 547px; height: 400px; overflow: scroll;
                padding-left: 15px; visibility: hidden;">
            </div>
        </div>
    </div>
    <div id="maskthree">
    </div>
    <div id="Manage" class="windowfour bgcolor" style="margin-left: 430px; margin-top: 200px;
        height: 230px;">
        <div class="form-group" style="margin-bottom: 90px;">
            <div class="col-sm-7">
                <img id="img3" src="Style/images/close.png" alt="Close" onclick="showFormFour();"
                    class="col-sm-2 top" /></div>
            <div class="col-sm-4">
                &nbsp;
            </div>
        </div>
        <div class="form-group">
            <asp:HiddenField ID="hdngpName" runat="server" />
        </div>
        <div class="form-group">
            <div class="col-sm-4">
                Upload Photo :
            </div>
            <div class="col-sm-7">
                <asp:FileUpload ID="fileUploadGpPhoto" runat="server" />
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-1">
            </div>
            <div class="col-sm-10">
                <asp:Image ID="imgGpPhoto" runat="server" Style="width: 300px; height: 100px;" Visible="false" />
            </div>
        </div>
        <br />
        <div class="form-group" style="margin-left: 130px; margin-top: 40px;">
            <div class="col-sm-3">
                <asp:Button ID="btnCancelGp" Text="Cancel" runat="server" OnClientClick="cancel();"
                    OnClick="btnCancel_onclick" CssClass="btn btn-default" />
            </div>
            <div class="col-sm-4">
                <asp:Button ID="btnUploadGp" Text="Commit" class="btn btn-primary" runat="server"
                    OnClientClick="cancel();" OnClick="btnUploadGp_OnClick" />
            </div>
        </div>
    </div>
    <div id="maskfour">
    </div>
    <div id="Delete" class="windowfive bgcolor" style="margin-left: 330px; margin-top: 210px;
        height: 160px; width: 500px;">
        <div class="form-group" style="margin-bottom: 50px;">
            <div class="col-sm-7">
                <img id="img4" src="Style/images/close.png" alt="Close" onclick="showFormFive();"
                    class="col-sm-2 top" /></div>
            <div class="col-sm-4">
                &nbsp;
            </div>
        </div>
        <div class="form-group">
            <asp:HiddenField ID="hdnDeleteName" runat="server" />
        </div>
        <div class="form-group">
            <div class="col-sm-1">
            </div>
            <div class="col-sm-10">
                <asp:Label ID="lblDeleteMsg" Text="Are you sure you want to delete this group?" runat="server"
                    Style="font-size: 15px;"></asp:Label>
            </div>
        </div>
        <br />
        <div class="form-group" style="margin-left: 150px; margin-top: 40px;">
            <div class="col-sm-3">
                <asp:LinkButton ID="btnExit" Text="Cancel" CssClass="btn btn-default" runat="server"
                    OnClick="btnCancel_onclick"></asp:LinkButton>
            </div>
            <div class="col-sm-4">
                <asp:LinkButton ID="btnConfirm" Text="Confirm" class="btn-primary" runat="server"
                    OnClick="btnGpDelete_OnClick"><img src="Style/images/delete.png" alt="delete" style="width:50px;height:35px;border-radius:3px;" /></asp:LinkButton>
            </div>
        </div>
    </div>
    <div id="maskfive">
    </div>
    <div id="Leave" class="windowsix bgcolor" style="margin-left: 330px; margin-top: 210px;
        height: 160px; width: 500px;">
        <div class="form-group" style="margin-bottom: 50px;">
            <div class="col-sm-7">
                <img id="img5" src="Style/images/close.png" alt="Close" onclick="showFormSix();"
                    class="col-sm-2 top" /></div>
            <div class="col-sm-4">
                &nbsp;
            </div>
        </div>
        <div class="form-group">
            <asp:HiddenField ID="hdnLeaveName" runat="server" />
        </div>
        <div class="form-group">
            <div class="col-sm-1">
            </div>
            <div class="col-sm-10">
                <asp:Label ID="lblLeaveName" Text="Are you sure you want to leave this group?" runat="server"
                    Style="font-size: 15px;"></asp:Label>
            </div>
        </div>
        <br />
        <div class="form-group" style="margin-left: 150px; margin-top: 40px;">
            <div class="col-sm-3">
                <asp:LinkButton ID="lbtnExit" Text="Cancel" CssClass="btn btn-default" runat="server"
                    OnClick="btnCancel_onclick"></asp:LinkButton>
            </div>
            <div class="col-sm-4">
                <asp:LinkButton ID="lbtnConfirm" Text="Confirm" class="btn btn-primary" runat="server"
                    OnClick="btnGpLeave_OnClick"></asp:LinkButton>
            </div>
        </div>
    </div>
    <div id="masksix">
    </div>
    <div id="DeleteConversation" class="windowseven bgcolor" style="margin-left: 430px;
        margin-top: 210px; height: 160px; width: 500px;">
        <div class="form-group" style="margin-bottom: 50px;">
            <div class="col-sm-7">
                <img id="img6" src="Style/images/close.png" alt="Close" onclick="showFormSeven();"
                    class="col-sm-2 top" /></div>
            <div class="col-sm-4">
                &nbsp;
            </div>
        </div>
        <div class="form-group">
            <asp:HiddenField ID="hdnDeleteConversation" runat="server" />
        </div>
        <div class="form-group">
            <div class="col-sm-1">
            </div>
            <div class="col-sm-10">
                <asp:Label ID="lblDeleteConversation" Text="Are you sure you want to delete this conversation? It will only be deleted for you and no one else."
                    runat="server" Style="font-size: 15px;"></asp:Label>
            </div>
        </div>
        <br />
        <div class="form-group" style="margin-left: 150px; margin-top: 40px;">
            <div class="col-sm-3">
                <asp:LinkButton ID="lbtnCancelOne" Text="Cancel" class="btn btn-default" runat="server"
                    OnClick="btnCancel_onclick"></asp:LinkButton>
            </div>
            <div class="col-sm-4">
                <asp:LinkButton ID="lbtnDeleteOne" Text="Delete" CssClass="" runat="server" OnClick="lbtnDeleteConversation_onclick"><img src="Style/images/delete.png" alt="delete" style="width:50px;height:35px;border-radius:3px;" /></asp:LinkButton>
            </div>
        </div>
    </div>
    <div id="maskseven">
    </div>
    <div id="Bio" class="windoweight bgcolor" style="margin-left: 430px; margin-top: 210px;
        height: 160px; width: 500px;">
        <div class="form-group" style="margin-bottom: 50px;">
            <div class="col-sm-7">
                <img id="img7" src="Style/images/close.png" alt="Close" onclick="showFormEight();"
                    class="col-sm-2 top" /></div>
            <div class="col-sm-4">
                &nbsp;
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-2">
                Edit Bio
            </div>
            <div class="col-sm-10">
                <asp:HiddenField ID="hdnEditBio" runat="server" />
                <asp:TextBox ID="txtEditBio" runat="server" TextMode="MultiLine" Width="100%" OnTextChanged="txtEditBio_OnTextChanged" />
            </div>
        </div>
        <br />
        <div class="form-group" style="margin-left: 150px; margin-top: 40px;">
            <div class="col-sm-3">
                <asp:LinkButton ID="lbtnEditBio" Text="Commit" CssClass="btn btn-success" runat="server"
                    OnClick="lbtnEditBio_onclick"></asp:LinkButton>
            </div>
            <div class="col-sm-4">
                <asp:LinkButton ID="lbtCancelEight" Text="Cancel" CssClass="btn btn-default" runat="server"
                    OnClick="btnCancel_onclick"></asp:LinkButton>
            </div>
        </div>
    </div>
    <div id="maskeight">
    </div>
    </form>
</body>
</html>
