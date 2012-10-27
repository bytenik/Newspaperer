<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Installer.aspx.cs" Inherits="Windchime.Installer" MasterPageFile="~/Backend.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="Body">


    <asp:Button ID="ClearDBButton" runat="server" onclick="ClearDBButton_Click" 
        Text="Clear DB" />
    <asp:Button ID="LoadTestDataButton" runat="server" Enabled="False" 
        onclick="LoadTestDataButton_Click" Text="Load Test Data" />


</asp:Content>