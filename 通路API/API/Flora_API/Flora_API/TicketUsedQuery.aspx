<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TicketUsedQuery.aspx.cs" Inherits="Flora_API.TicketUsedQuery" %>

<%--<!DOCTYPE html>

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
                var Channel = url_type[0];
                var QR_CODE = url_type[1];
                //var core_code = location.search;
                UsedGen(Channel, QR_CODE);
            });
    </script>
    <script>
        function UsedGen(Channel, QR_CODE) {
            $.ajax({
                type: "POST",
                url: "./TicketUsedQuery.aspx/UsedGen",
                data: '{Channel: "' + Channel + '",QR_CODE: "' + QR_CODE + '"}',
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
</html>--%>

