<%@ Page Language="C#" AutoEventWireup="true"  CodeBehind="Search.aspx.cs" Inherits="Windchime.Search" MasterPageFile="~/Backend.Master"%>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="Body">

    <asp:TextBox ID="searchbox" runat="server"></asp:TextBox>

    <asp:Button ID="btnSearch" runat="server" Text="Go" 
        onclick="btnSearch_Click" />
    <br />
    <asp:Label ID="resultlabel" runat="server"></asp:Label>

</asp:Content>