<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Groups.aspx.cs" Inherits="Windchime.Groups"  MasterPageFile="~/Backend.Master" %>

<%@ Register src="AddUserToGroup.ascx" tagname="AddUserToGroup" tagprefix="uc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="Body">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAddNewGroup" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            &nbsp;&nbsp;<asp:Label ID="lblNewGroupErr" runat="server" ForeColor="Red"></asp:Label>
            <table>
                <tr>
                    <td valign="middle">
                        <asp:TextBox ID="txtNewGroupName" runat="server" Width="180px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnAddNewGroup" runat="server" Text="Add Group" 
                            onclick="btnAddNewGroup_Click" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <br />
    <br />

    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAddNewGroup" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <uc1:AddUserToGroup ID="AddUserToGroup1" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
