<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebApplication16.WebForm" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>PDF Generator</title>
     <style>
        body {
            min-height: 80vh;
            display: grid;
            place-items: center;
        }
    </style>
</head>
<body>
    <form id="pdfForm" runat="server">
        <input type="text" id="txtName" runat="server" placeholder="Name"/>
        <asp:Button ID="btnGenerate" runat="server" Text="Astrology Document" OnClick="btnGenerate_Click" />
    </form>
</body>
</html>
