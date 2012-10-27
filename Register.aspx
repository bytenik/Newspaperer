<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Windchime.Registration" MasterPageFile="~/Backend.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="Body">
    <asp:LoginView ID="LoginView1" runat="server">
        <LoggedInTemplate>
            <asp:Label ID="Label1" runat="server" ForeColor="Red" 
                Text="You already have an account!"></asp:Label>
        </LoggedInTemplate>
        <AnonymousTemplate>
            <asp:CreateUserWizard ID="CreateUserWizard1" runat="server">
                <WizardSteps>
                    <asp:CreateUserWizardStep runat="server" />
                    <asp:CompleteWizardStep runat="server" />
                </WizardSteps>
            </asp:CreateUserWizard>
        </AnonymousTemplate>
    </asp:LoginView>
</asp:Content>