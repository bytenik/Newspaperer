<%@ Page Language="C#" MasterPageFile="~/Backend.Master" AutoEventWireup="true" CodeBehind="CollectionBrowserTest.aspx.cs" Inherits="Windchime.CollectionBrowserTest" Title="Untitled Page" %>
<%@ Register src="CollectionBrowser.ascx" tagname="CollectionBrowser" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <uc1:CollectionBrowser ID="CollectionBrowser1" runat="server" />
</asp:Content>
