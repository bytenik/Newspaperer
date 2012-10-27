<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewPublished.aspx.cs" Inherits="Windchime.Publish" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Publish Issue</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Title_lbl" runat="server"></asp:Label>
        <asp:PlaceHolder ID="AssetPrinter_placeholder_cats" runat="server"></asp:PlaceHolder>
        <asp:PlaceHolder ID="AssetPrinter_placeholder1" runat="server"></asp:PlaceHolder>
        <asp:PlaceHolder ID="AssetPrinter_placeholder2" runat="server"></asp:PlaceHolder>
        <asp:PlaceHolder ID="AssetPrinter_placeholder3" runat="server"></asp:PlaceHolder>
        <asp:PlaceHolder ID="AssetPrinter_placeholder4" runat="server"></asp:PlaceHolder>
        <asp:PlaceHolder ID="AssetPrinter_placeholder5" runat="server"></asp:PlaceHolder>
        <asp:PlaceHolder ID="AssetPrinter_placeholder_ads" runat="server"></asp:PlaceHolder>
        <asp:PlaceHolder ID="AssetPrinter_placeholder_else" runat="server"></asp:PlaceHolder>
    </div>
    </form>
</body>
</html>
