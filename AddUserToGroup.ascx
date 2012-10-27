<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddUserToGroup.ascx.cs" Inherits="Windchime.AddUserToGroup" %>

<div>
    <asp:UpdatePanel ID="AUTG_UpdatePanel1" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="AUTG_btnAddUser" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="AUTG_btnRemoveUser" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            &nbsp;&nbsp;<asp:Label ID="AUTG_lblAddRemError" runat="server" Text="" ForeColor="Red"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table id="AUTG_tblMain">
        <tr>
            <th>Group Name</th>
            <th colspan="3">Users</th>
        </tr>
        <tr>
            <td>
                <asp:ListBox ID="AUTG_lstGroupNames" runat="server" Width="200px" Height="134px"
                    AutoPostBack="true"
                    onselectedindexchanged="AUTG_lstGroupNames_SelectedIndexChanged">
                </asp:ListBox>
            </td>
            <td>
                <asp:UpdatePanel ID="AUTG_UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="AUTG_lstGroupNames" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="AUTG_btnAddUser" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="AUTG_btnRemoveUser" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="AUTG_chkStaffOnly" EventName="CheckedChanged" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:ListBox ID="AUTG_lstGroupMembers" runat="server" Rows="8" Height="134px" 
                            Width="200px" SelectionMode="Multiple">
                        </asp:ListBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="AUTG_UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="AUTG_lstGroupNames" EventName="SelectedIndexChanged" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:Button ID="AUTG_btnAddUser" runat="server" Text="<--Add" Width="80px" 
                        onclick="AUTG_btnAddUser_Click" Enabled="False"/>
                        <br /><br />
                        <asp:Button ID="AUTG_btnRemoveUser" runat="server" Text="Remove-->" 
                            Width="80px" Enabled="False" onclick="AUTG_btnRemoveUser_Click"/>
                        <br /><br />
                        <asp:CheckBox ID="AUTG_chkStaffOnly" runat="server" Text="Staff Only" 
                            Enabled="False" AutoPostBack="True" 
                            oncheckedchanged="AUTG_chkStaffOnly_CheckedChanged" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="AUTG_UpdatePanel4" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="AUTG_lstGroupNames" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="AUTG_btnAddUser" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="AUTG_btnRemoveUser" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="AUTG_chkStaffOnly" EventName="CheckedChanged" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:ListBox ID="AUTG_lstNotMembers" runat="server" Rows="8" Height="134px" 
                            Width="200px" SelectionMode="Multiple">
                        </asp:ListBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</div>

