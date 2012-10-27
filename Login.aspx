<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Windchime.Login" MasterPageFile="~/Backend.Master"%>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="Body">

    <asp:Login ID="Login1" runat="server" DestinationPageUrl="~\Logout.aspx" 
        DisplayRememberMe="False">
</asp:Login>

</asp:Content>