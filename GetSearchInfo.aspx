<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetSearchInfo.aspx.cs"
    Inherits="WebApplication1.GetSearchInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var h = document.getElementById("NameResultConversation");
        h.style.visibility = "visible";
        function LinkButton1_Click(val) {
            document.getElementById('hdnUserName').innerHTML = val;
            $.post("../Chat.aspx", { hdnUserName: val }, function (data, status) { $("#hdnUserID").html(data) });
        }
    </script>
</head>
<body>
    <div id="hdnUserID">
    </div>
</body>
</html>
