<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EazyCard_api.aspx.cs" Inherits="Flora_API.EazyCard_api" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
</head>
    <script>
        $(document).ready(function () {
            var url_code = window.location.href.split('?');
            var url_type = url_code[1].split('&');
            var Card_Type = url_type[0];
            var core_code = url_type[1];
            //var core_code = location.search;
            UDDGen(Card_Type,core_code);
         });
    </script>
    <script>
        function UDDGen(Card_Type,core_code) {
            $.ajax({
                type: "POST",
                url: "./EazyCard_api.aspx/UDDGen",
                data: '{Card_Type: "' + Card_Type + '",core_code: "' + core_code + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    document.write(response.d);
                    //var array = $.parseJSON(response.d);
                    //var json = array[0];
                }
            });
        }       
    </script>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
