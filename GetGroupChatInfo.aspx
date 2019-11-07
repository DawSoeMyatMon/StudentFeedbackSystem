<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetGroupChatInfo.aspx.cs"
    Inherits="WebApplication1.GetGroupChatInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <link href="Style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var h = document.getElementById("NameResult");
        h.style.visibility = "visible";
        function showTextArea(userName, Image) {
            var x = document.getElementById("txtareagp");
            x.style.display = "block";
            var userName1 = userName.toString();
            var img = Image.toString();
            var imgs = img.replace("~/", "");

            var div = document.createElement('DIV');
           
            div.style.float = "left";
            div.style.width = "90px";
            div.style.height = "80px";
            div.style.display = "inline-block";
            div.style.margin_left = "10px";
           
            div.style.borderRadius = "50%";

            var addcounter = userName1.replace(/\s+/g, '');
            div.id = "childdiv" + addcounter;
            if (document.getElementById("txtUser" + addcounter) == null) {
                div.innerHTML = GetDynamicWorkingExperience("", addcounter, userName1, imgs);
                document.getElementById("childgp").appendChild(div);
            }
            else {
                var removediv = document.getElementById("childdiv" + addcounter);
                removediv.parentNode.removeChild(removediv);
            }
        }
        function GetDynamicWorkingExperience(value, counter, userName, Img) {
            return '<image src="Style/images/close.png" alt="Close" style="width: 10px; height: 10px;" onclick="removeUser(\'' + counter + '\');"></image><image src="' + Img + '" id="img' + counter + '" name="img"></image><label id="txtUser' + counter + '" name="txtUser" style="color:#31708f;">' + userName + '</label><input type="hidden" name="txtUserName" value="' + counter + '" />';
        }

        function removeUser(counter) {
            var ele = document.getElementById("childdiv" + counter);
            ele.parentNode.removeChild(ele);
        }
    </script>
</head>
<body>
</body>
</html>
