<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ChatApp.Login" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!--===============================================================================================-->
    <link rel="icon" type="image/png" href="LoginCss/images/icons/2.ico" />
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="Style/LoginCss/vendor/bootstrap/css/bootstrap.min.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="Style/LoginCss/fonts/font-awesome-4.7.0/css/font-awesome.min.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="Style/LoginCss/fonts/iconic/css/material-design-iconic-font.min.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="Style/LoginCss/vendor/animate/animate.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="Style/LoginCss/vendor/css-hamburgers/hamburgers.min.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="Style/LoginCss/vendor/animsition/css/animsition.min.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="Style/LoginCss/vendor/select2/select2.min.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="Style/LoginCssvendor/daterangepicker/daterangepicker.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="Style/LoginCss/css/util.css">
    <link rel="stylesheet" type="text/css" href="Style/LoginCss/css/main.css">
    <!--===============================================================================================-->
</head>
<body style="height: 300px;">
    <div class="p">
        <center>
            <div class="wrap-login100">
                <form class="login100-form validate-form" id="Form1" runat="server">
                <span class="login100-form-title p-b-10"><font color="#5DBCD9"><b>MMB Chat </b></font>
                </span><span class="login100-form-avatar">
                    <img src="Style/LoginCss/images/MMBLogo_Small.jpg" alt="AVATAR">
                </span>
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                <div class="wrap-input100 validate-input m-t-30 m-b-25" data-validate="Enter username">
                    <asp:TextBox ID="txtusername" class="input100" runat="server"></asp:TextBox>
                    <span class="focus-input100" data-placeholder="Username"></span>
                </div>
                <div class="wrap-input100 validate-input m-b-40" data-validate="Enter password">
                    <asp:TextBox ID="txtpassword" class="input100" runat="server" TextMode="Password"></asp:TextBox>
                    <span class="focus-input100" data-placeholder="Password"></span>
                </div>
                <div class="container-login100-form-btn">
                    <asp:Button ID="btnLogin" runat="server" class="login100-form-btn" Text="Login" OnClick="Login_Click" />
                </div>
                </form>
        </center>
    </div>
    <div id="dropDownSelect1">
    </div>
    <!--===============================================================================================-->
    <script src="LoginCss/vendor/jquery/jquery-3.2.1.min.js"></script>
    <!--===============================================================================================-->
    <script src="LoginCss/vendor/animsition/js/animsition.min.js"></script>
    <!--===============================================================================================-->
    <script src="LoginCss/vendor/bootstrap/js/popper.js"></script>
    <script src="LoginCss/vendor/bootstrap/js/bootstrap.min.js"></script>
    <!--===============================================================================================-->
    <script src="LoginCss/vendor/select2/select2.min.js"></script>
    <!--===============================================================================================-->
    <script src="LoginCss/vendor/daterangepicker/moment.min.js"></script>
    <script src="LoginCss/vendor/daterangepicker/daterangepicker.js"></script>
    <!--===============================================================================================-->
    <script src="LoginCss/vendor/countdowntime/countdowntime.js"></script>
    <!--===============================================================================================-->
    <script src="LoginCss/js/main.js"></script>
</body>
</html>
