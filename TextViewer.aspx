<%@ Page Title="Asset Editor" Language="C#" MasterPageFile="~/Backend.Master" AutoEventWireup="true" CodeBehind="TextViewer.aspx.cs" Inherits="Windchime.Asset1" %>

<asp:Content ContentPlaceHolderID="Body" runat="server">
    <h5>Asset Name:</h5>
    <asp:TextBox ID="AssetName_box" runat="server" CssClass="block h1-size" />
    <ajaxToolkit:TextBoxWatermarkExtender WatermarkCssClass="watermarked" runat="server" WatermarkText="Asset name"
        TargetControlID="AssetName_box" />
    
    <h5>Content:</h5>
    <asp:TextBox ID="Content_box" runat="server" CssClass="block" />
    <ajaxToolkit:TextBoxWatermarkExtender runat="server" WatermarkCssClass="watermarked" WatermarkText="Asset content" />
</asp:Content>