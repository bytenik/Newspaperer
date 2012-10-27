<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserPref.aspx.cs" Inherits="Windchime.UserPref" MasterPageFile="~/Backend.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="Body">
    <div style="height: 391px">
        <table align="left" cellpadding="2">
            <tr>
                <td class="style1">
                    Username:</td>
                <td class="style2">
        
        <asp:TextBox ID="boxUsername" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
                <td class="style3">
        
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    Email address:</td>
                <td class="style2">
        <asp:TextBox ID="boxEmail" runat="server"></asp:TextBox>
                </td>
                <td class="style3">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="boxEmail" Display="Dynamic" 
                        ErrorMessage="Email address is required"></asp:RequiredFieldValidator>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="boxEmail" ErrorMessage="Invalid email address" 
                        ValidationExpression="^\w+([-+\.']\w+)*@\w+([-\.]\w+)*\.\w+([-\.]\w+)*$" 
                        Display="Dynamic"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    New password:</td>
                <td class="style2">
        <asp:TextBox ID="boxPassword1" runat="server" TextMode="Password"></asp:TextBox>
                </td>
                <td class="style3">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                        ControlToValidate="boxPassword1" ErrorMessage="Password must be at least 8 characters and contain 1 special character." 
                        ValidationExpression="^.*$" 
                        Display="Dynamic"></asp:RegularExpressionValidator>
                </td>
                <td class="style3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    Confirm new password: </td>
                <td class="style2">
        <asp:TextBox ID="boxPassword2" runat="server" TextMode="Password"></asp:TextBox>
                </td>
                <td class="style3">
                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                        ControlToCompare="boxPassword1" ControlToValidate="boxPassword2" 
                        ErrorMessage="Passwords do not match"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    First name:</td>
                <td class="style2">
        <asp:TextBox ID="boxFirstName" runat="server"></asp:TextBox>
                </td>
                <td class="style3">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="boxFirstName" 
                        ErrorMessage="First name is required"></asp:RequiredFieldValidator>
                    </td>
            </tr>
            <tr>
                <td class="style1">
                    Last name:</td>
                <td class="style2">
        <asp:TextBox ID="boxLastName" runat="server"></asp:TextBox>
                </td>
                <td class="style3">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="boxLastName" 
                        ErrorMessage="Last name is required"></asp:RequiredFieldValidator>
                    </td>
            </tr>
            <tr>
                <td class="style1">
                                        Address 1</td>
                <td class="style2">
        <asp:TextBox ID="boxAddr1" runat="server"></asp:TextBox>
                </td>
                <td class="style3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                                        Address 2</td>
                <td class="style2">
        <asp:TextBox ID="boxAddr2" runat="server"></asp:TextBox>
                </td>
                <td class="style3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    City</td>
                <td class="style2">
        <asp:TextBox ID="boxCity" runat="server"></asp:TextBox>
                </td>
                <td class="style3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    State</td>
                <td class="style2">
                    <asp:DropDownList ID="listState" runat="server">
                        <asp:ListItem Value="--" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="NY">New York</asp:ListItem>
                        <asp:ListItem Value="NJ">New Jersey</asp:ListItem>
                        <asp:ListItem Value="PA">Pennsylvania</asp:ListItem>
                        <asp:ListItem Value="CT">Conneticut</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    Postal code</td>
                <td class="style2">
        <asp:TextBox ID="boxZip" runat="server"></asp:TextBox>
                </td>
                <td class="style3">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                        ControlToValidate="boxZip" ErrorMessage="Invalid postal code" 
                        ValidationExpression="^\d{5}(-\d{4})?$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    </td>
                <td class="style2">
        <asp:Button ID="Submit" runat="server" Text="Save changes" onclick="Submit_Click" />
    
                </td>
                <td class="style3">
                    &nbsp;</td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        
        <br />
        <br />
        &nbsp;<br />
        &nbsp;<br />
        <br />
        <br />   
    </div>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="Head">

    <style type="text/css">
        .style1
        {
            width: 145px;
        }
        .style2
        {
            width: 157px;
        }
        .style3
        {
            width: 302px;
        }
    </style>
</asp:Content>
