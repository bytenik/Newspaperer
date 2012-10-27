<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnassignedAssignment.aspx.cs" Inherits="Windchime.UnassignedAssignment" MasterPageFile="~/Backend.Master" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="Body">
    <center>
        <asp:TextBox ID="email_txt" ReadOnly="true" runat="server" Height="313px" Width="600px"></asp:TextBox>
        <br />
        <asp:Button ID="email_btn" runat="server" Text="Send Email To All Staff" OnClick="sendUnassignedMail()" />
    </center>
</asp:Content>