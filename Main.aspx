<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Windchime.Main" MasterPageFile="~/Backend.Master"%>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="Body">
<asp:HyperLink runat="server" NavigateUrl="~/Assignments.aspx" Text="View Assignments" /><br />
<asp:HyperLink runat="server" NavigateUrl="~/Register.aspx" Text="Register" /><br />
<asp:HyperLink runat="server" NavigateUrl="~/UserPref.aspx" Text="User Preferences" /><br />
<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Groups.aspx" Text="Groups" /><br />
<asp:HyperLink runat="server" NavigateUrl="~/Publish.aspx" Text="Publish Issue" /><br />
<asp:HyperLink runat="server" NavigateUrl="~/EditCollections.aspx" Text="Edit Collections" /><br />
</asp:Content>